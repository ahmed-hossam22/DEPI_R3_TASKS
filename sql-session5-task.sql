USE [StoreDB]
---#1-------------------------------------------
select product_name , list_price , 
CASE
When list_price < 300 then 'Economy'
When list_price between 299 and 1000 then 'Standard'
When list_price between 999 and 2500 then 'Premium'
When list_price >= 2500 then 'Luxury'
End as price_category
from [production].[products]
---#2-----------------------------------------
select  o.order_id,o.order_date,
  CASE 
  WHEN o.order_status = 1 THEN 'Order Received'
  WHEN o.order_status = 2 THEN 'In Preparation'
  WHEN o.order_status = 3 THEN 'Order Cancelled'
  WHEN o.order_status = 4 THEN 'Order Delivered'
  End as status_des,
    CASE
   WHEN o.order_status = 1 AND DATEDIFF(day, o.order_date, GETDATE()) > 5 THEN 'URGENT'
   WHEN o.order_status = 2 AND DATEDIFF(day, o.order_date, GETDATE()) > 3 THEN 'HIGH'
        ELSE 'NORMAL'
    END AS priority_lev
from [sales].[orders] o
---#3-----------------------------------------
select first_name , last_name , COUNT(order_id) AS num_of_orders ,
CASE 
when COUNT(order_id)=  0 then 'New Staff'
when COUNT(order_id) between 1 and 10 then 'Junior Staff'
when COUNT(order_id) between 11 and 25 then 'Senior Staff'
when COUNT(order_id) >= 26 then 'Expert Staff' 
End as Staff_lev
from [sales].[staffs] s JOIN [sales].[orders] o  
ON s.staff_id = o.staff_id 
Group by first_name , last_name 
---#4-----------------------------------------
select customer_id, first_name,last_name,email,
    ISNULL(phone, 'Phone Not Available') AS contact_phone,
    COALESCE(phone, email, 'No Contact Method') AS preferred_contact,
    street,city,state,zip_code
from  [sales].[customers]
---#5-----------------------------------------
select p.product_name,p.list_price,
    ISNULL(s.quantity, 0) AS stock_quantity,
    CASE 
  WHEN ISNULL(s.quantity, 0) = 0 THEN 'Out of Stock'
  WHEN ISNULL(s.quantity, 0) BETWEEN 1 AND 10 THEN 'Low Stock'
  WHEN ISNULL(s.quantity, 0) BETWEEN 11 AND 50 THEN 'In Stock'
  ELSE 'High Stock'
    END AS stock_status,
    CASE 
   WHEN ISNULL(s.quantity, 0) = 0 THEN 0
   ELSE p.list_price / NULLIF(s.quantity, 0)
    END AS price_per_unit
FROM [production].[products] p JOIN [production].[stocks] s 
ON p.product_id = s.product_id AND s.store_id = 1 
----#6----------------------------------------
select  first_name,last_name,
    COALESCE(street, '') AS street,
    COALESCE(city, '') AS city,
    COALESCE(state, '') AS state,
    COALESCE(zip_code, '') AS zip_code,
    CASE 
 WHEN street IS NULL AND city IS NULL AND state IS NULL AND zip_code IS NULL 
 THEN 'Address Not Available'
        ELSE
 CASE WHEN street IS NOT NULL THEN street + ', ' ELSE '' END +
 CASE WHEN city IS NOT NULL THEN city + ', ' ELSE '' END +
 CASE WHEN state IS NOT NULL THEN state + ' ' ELSE '' END +
 CASE WHEN zip_code IS NOT NULL THEN zip_code ELSE '' END
    END AS formatted_address
from  [sales].[customers]
---#7----------------------------------------
SELECT 
    c.customer_id,
    c.first_name + ' ' + c.last_name AS customer_name,
    SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_spent
FROM [sales].[customers] c JOIN [sales].[orders] o ON c.customer_id = o.customer_id
JOIN  [sales].[order_items] oi ON o.order_id = oi.order_id
GROUP BY c.customer_id, c.first_name, c.last_name
HAVING 
   SUM(oi.quantity * oi.list_price * (1 - oi.discount)) > 1500
ORDER BY 
    total_spent DESC;
---#8-----------------------------------------
WITH CategoryRevenue AS (
    SELECT 
        c.category_id,
        c.category_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_revenue
    FROM 
        [production].[categories] c
    JOIN 
        [production].[products] p ON c.category_id = p.category_id
    JOIN 
        [sales].[order_items] oi ON p.product_id = oi.product_id
    GROUP BY 
        c.category_id, c.category_name
),
CategoryAvgOrder AS (
    SELECT 
        c.category_id,
        AVG(oi.quantity * oi.list_price * (1 - oi.discount)) AS avg_order_value
    FROM 
        [production].[categories] c
    JOIN 
        [production].[products] p ON c.category_id = p.category_id
    JOIN 
        [sales].[order_items] oi ON p.product_id = oi.product_id
    GROUP BY 
        c.category_id
)
SELECT 
    cr.category_id,
    cr.category_name,
    cr.total_revenue,
    ca.avg_order_value,
    CASE 
        WHEN cr.total_revenue > 50000 THEN 'Excellent'
        WHEN cr.total_revenue > 20000 THEN 'Good'
        ELSE 'Needs Improvement'
    END AS performance
FROM 
    CategoryRevenue cr
JOIN 
    CategoryAvgOrder ca ON cr.category_id = ca.category_id
ORDER BY 
    cr.total_revenue DESC
---#9-----------------------------------------------
WITH MonthlySales AS (
    SELECT 
        YEAR(order_date) AS year,
        MONTH(order_date) AS month,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS monthly_total
    FROM 
        [sales].[orders] o
    JOIN 
        [sales].[order_items] oi ON o.order_id = oi.order_id
    GROUP BY 
        YEAR(order_date), MONTH(order_date)
),
SalesComparison AS (
    SELECT 
        year,
        month,
        monthly_total,
        LAG(monthly_total, 1) OVER (ORDER BY year, month) AS prev_month_total
    FROM 
        MonthlySales
)
SELECT 
    year,
    month,
    monthly_total,
    prev_month_total,
    CASE 
        WHEN prev_month_total IS NULL THEN NULL
        ELSE ROUND(((monthly_total - prev_month_total) / prev_month_total * 100), 2)
    END AS growth_percent
FROM 
    SalesComparison
ORDER BY 
    year, month
---#10------------------------------------------
WITH RankedProducts AS (
    SELECT 
        p.product_id,
        p.product_name,
        c.category_name,
        p.list_price,
        ROW_NUMBER() OVER (PARTITION BY p.category_id ORDER BY p.list_price DESC) AS row_num,
        RANK() OVER (PARTITION BY p.category_id ORDER BY p.list_price DESC) AS price_rank,
        DENSE_RANK() OVER (PARTITION BY p.category_id ORDER BY p.list_price DESC) AS dense_price_rank
    FROM  [production].[products] p JOIN [production].[categories] c 
    ON p.category_id = c.category_id
)
SELECT 
    product_id,
    product_name,
    category_name,
    list_price,
    price_rank,
    dense_price_rank
FROM RankedProducts
WHERE  row_num <= 3
ORDER BY  category_name, price_rank;
----#11--------------------------------------------
WITH CustomerSpending AS (
    SELECT 
        c.customer_id,
        c.first_name + ' ' + c.last_name AS customer_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_spent,
        RANK() OVER (ORDER BY SUM(oi.quantity * oi.list_price * (1 - oi.discount)) DESC) AS spending_rank,
        NTILE(5) OVER (ORDER BY SUM(oi.quantity * oi.list_price * (1 - oi.discount)) DESC) AS spending_group
    FROM  [sales].[customers] c JOIN  [sales].[orders] o ON c.customer_id = o.customer_id
    JOIN [sales].[order_items] oi ON o.order_id = oi.order_id
    GROUP BY   c.customer_id, c.first_name, c.last_name
)
SELECT 
    customer_id,
    customer_name,
    total_spent,
    spending_rank,
    CASE 
        WHEN spending_group = 1 THEN 'VIP'
        WHEN spending_group = 2 THEN 'Gold'
        WHEN spending_group = 3 THEN 'Silver'
        WHEN spending_group = 4 THEN 'Bronze'
        ELSE 'Standard'
    END AS customer_tier
FROM 
    CustomerSpending
ORDER BY 
    spending_rank
---#12-----------------------------------------------
WITH StorePerformance AS (
    SELECT 
        s.store_id,
        s.store_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_revenue,
        COUNT(DISTINCT o.order_id) AS order_count,
        RANK() OVER (ORDER BY SUM(oi.quantity * oi.list_price * (1 - oi.discount)) DESC) AS revenue_rank,
        RANK() OVER (ORDER BY COUNT(DISTINCT o.order_id) DESC) AS order_rank,
        PERCENT_RANK() OVER (ORDER BY SUM(oi.quantity * oi.list_price * (1 - oi.discount))) AS revenue_percentile
    FROM 
        [sales].[stores] s
    JOIN 
        [sales].[orders] o ON s.store_id = o.store_id
    JOIN 
        [sales].[order_items] oi ON o.order_id = oi.order_id
    GROUP BY 
        s.store_id, s.store_name
)
SELECT 
    store_id,
    store_name,
    total_revenue,
    order_count,
    revenue_rank,
    order_rank,
    ROUND(revenue_percentile * 100, 2) AS revenue_percentile,
    CASE 
        WHEN revenue_percentile >= 0.8 THEN 'Top Performance'
        WHEN revenue_percentile >= 0.6 THEN 'Above Average'
        WHEN revenue_percentile >= 0.4 THEN 'Average'
        WHEN revenue_percentile >= 0.2 THEN 'Below Average'
        ELSE 'Needs Improvement'
    END AS performance_level
FROM 
    StorePerformance
ORDER BY 
    revenue_rank
---#13--------------------------------------------
SELECT *
FROM (
    SELECT 
        c.category_name,
        b.brand_name,
        p.product_id
    FROM [production].[products] p
    JOIN [production].[categories] c ON p.category_id = c.category_id
    JOIN [production].[brands] b ON p.brand_id = b.brand_id
) AS SourceTable
PIVOT (
    COUNT(product_id)
    FOR brand_name IN ([Electra], [Haro], [Trek], [Surly])
) AS PivotTable
ORDER BY category_name;
---#14-----------------------------------------------
SELECT *
FROM (
    SELECT 
        s.store_name,
        FORMAT(o.order_date, 'MMM') AS month,
        oi.quantity * oi.list_price * (1 - oi.discount) AS revenue
    FROM [sales].[orders] o
    JOIN [sales].[order_items] oi ON o.order_id = oi.order_id
    JOIN [sales].[stores] s ON o.store_id = s.store_id
) AS SourceTable
PIVOT (
    SUM(revenue)
    FOR month IN ([Jan], [Feb], [Mar], [Apr], [May], [Jun], 
                 [Jul], [Aug], [Sep], [Oct], [Nov], [Dec])
) AS PivotTable
ORDER BY store_name;
---#15----------------------------------------------
SELECT *
FROM (
    SELECT 
        s.store_name,
        CASE o.order_status
            WHEN 1 THEN 'Pending'
            WHEN 2 THEN 'Processing'
            WHEN 3 THEN 'Completed'
            WHEN 4 THEN 'Rejected'
        END AS status,
        o.order_id
    FROM [sales].[orders] o
    JOIN [sales].[stores] s ON o.store_id = s.store_id
) AS SourceTable
PIVOT (
    COUNT(order_id)
    FOR status IN ([Pending], [Processing], [Completed], [Rejected])
) AS PivotTable;
---#16---------------------------------------------------------
SELECT 
    brand_name,
    [2016], [2017], [2018],
    ROUND(([2017]-[2016])/NULLIF([2016],0)*100, 2) AS growth_2016_2017,
    ROUND(([2018]-[2017])/NULLIF([2017],0)*100, 2) AS growth_2017_2018
FROM (
    SELECT 
        b.brand_name,
        YEAR(o.order_date) AS year,
        oi.quantity * oi.list_price * (1 - oi.discount) AS revenue
    FROM [sales].[order_items] oi
    JOIN [sales].[orders] o ON oi.order_id = o.order_id
    JOIN [production].[products] p ON oi.product_id = p.product_id
    JOIN [production].[brands] b ON p.brand_id = b.brand_id
    WHERE YEAR(o.order_date) BETWEEN 2016 AND 2018
) AS SourceTable
PIVOT (
    SUM(revenue)
    FOR year IN ([2016], [2017], [2018])
) AS PivotTable;

---#17-----------------------------------------------------
SELECT p.product_id, product_name, 'In Stock' AS status
FROM [production].[products] p
JOIN [production].[stocks] s ON p.product_id = s.product_id
WHERE s.quantity > 0

UNION

SELECT p.product_id, product_name, 'Out of Stock' AS status
FROM [production].[products] p
JOIN [production].[stocks] s ON p.product_id = s.product_id
WHERE s.quantity = 0 OR s.quantity IS NULL

UNION

SELECT product_id, product_name, 'Discontinued' AS status
FROM [production].[products] p
WHERE NOT EXISTS (
    SELECT 1 FROM [production].[stocks] s 
    WHERE s.product_id = p.product_id
)
---#18---
SELECT customer_id, first_name, last_name
FROM [sales].[customers] c
WHERE EXISTS (
    SELECT 1 FROM [sales].[orders] o 
    WHERE o.customer_id = c.customer_id AND YEAR(o.order_date) = 2017
)

INTERSECT

SELECT customer_id, first_name, last_name
FROM [sales].[customers] c
WHERE EXISTS (
    SELECT 1 FROM [sales].[orders] o 
    WHERE o.customer_id = c.customer_id AND YEAR(o.order_date) = 2018
)
---#19---
SELECT product_id, product_name, 'In All Stores' AS availability
FROM [production].[products] p
WHERE EXISTS (SELECT 1 FROM [production].[stocks] WHERE product_id = p.product_id AND store_id = 1)
AND EXISTS (SELECT 1 FROM [production].[stocks] WHERE product_id = p.product_id AND store_id = 2)
AND EXISTS (SELECT 1 FROM [production].[stocks] WHERE product_id = p.product_id AND store_id = 3)

UNION

SELECT product_id, product_name, 'Only in Store 1' AS availability
FROM [production].[products] p
WHERE EXISTS (SELECT 1 FROM [production].[stocks] WHERE product_id = p.product_id AND store_id = 1)
AND NOT EXISTS (SELECT 1 FROM [production].[stocks] WHERE product_id = p.product_id AND store_id = 2);

---#20---
SELECT customer_id, first_name, last_name, 'Lost' AS status
FROM [sales].[customers] c
WHERE EXISTS (
    SELECT 1 FROM [sales].[orders] o 
    WHERE o.customer_id = c.customer_id AND YEAR(o.order_date) = 2016
)
AND NOT EXISTS (
    SELECT 1 FROM [sales].[orders] o 
    WHERE o.customer_id = c.customer_id AND YEAR(o.order_date) = 2017
)

UNION ALL

SELECT customer_id, first_name, last_name, 'New' AS status
FROM [sales].[customers] c
WHERE EXISTS (
    SELECT 1 FROM [sales].[orders] o 
    WHERE o.customer_id = c.customer_id AND YEAR(o.order_date) = 2017
)
AND NOT EXISTS (
    SELECT 1 FROM [sales].[orders] o 
    WHERE o.customer_id = c.customer_id AND YEAR(o.order_date) = 2016
)

UNION ALL

SELECT customer_id, first_name, last_name, 'Retained' AS status
FROM [sales].[customers] c
WHERE EXISTS (
    SELECT 1 FROM [sales].[orders] o 
    WHERE o.customer_id = c.customer_id AND YEAR(o.order_date) = 2016
)
AND EXISTS (
    SELECT 1 FROM [sales].[orders] o 
    WHERE o.customer_id = c.customer_id AND YEAR(o.order_date) = 2017
)