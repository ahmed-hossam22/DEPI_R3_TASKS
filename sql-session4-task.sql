USE [StoreDB]
---------------------------------#Start---------------------------------------------------

---#1-->Count the total number of products in the database-------------------------------
select COUNT(*) AS Total_number
from [production].[products]

---#2-->Find the average, minimum, and maximum price of all products---------------------
select
    AVG(list_price) as average_price, 
    MAX(list_price) as maximum_price,
    MIN(list_price) as minimum_price
FROM production.products

---#3-->Count how many products are in each category---------------------------------------
select B.category_name , p.product_name, Count(*) AS num_of_products
from production.products p join production.categories B
on p.category_id = B.category_id
Group by B.category_name , p.product_name

---#4-->Find the total number of orders for each store--------------------------------
SELECT o.order_id, s.store_name, COUNT(o.order_id) AS total_orders
FROM [sales].[orders] o JOIN [sales].[stores] s 
ON o.store_id = s.store_id
GROUP BY o.order_id , s.store_name 

---#5--> Show customer first names in UPPERCASE and last names in lowercase for the first 10 customers---------------
Select TOP 10 customer_id,
       UPPER(first_name) AS first_name_upper,
       LOWER(last_name) AS last_name_lower
FROM [sales].[customers]
ORDER BY customer_id

---#6-->Get the length of each product name. Show product name and its length for the first 10 products---------------
select top 10 product_name ,
LEN(product_name) AS Length_product_name
from [production].[products]
order by product_id

---#7-->Format customer phone numbers to show only the area code (first 3 digits) for customers 1-15-------------------
select top 15 [first_name] ,
left (phone , 3 ) as area_code
from [sales].[customers]
WHERE phone IS NOT NULL
ORDER BY customer_id

---#8-->Show the current date and extract the year and month from order dates for orders 1-10--------------------------
SELECT TOP 10 order_id, 
       GETDATE() AS current_date_,
       YEAR(order_date) AS order_year, 
       MONTH(order_date) AS order_month
FROM [sales].[orders]
ORDER BY order_id

---#9-->Join products with their categories. Show product name and category name for first 10 products------------------
select top 10 product_name , category_name
from [production].[products] p join [production].[categories] c 
on p.category_id = c.category_id 
order by product_id

---#10-->Join customers with their orders. Show customer name and order date for first 10 orders------------------Error--
select top 10 first_name, last_name , order_date
from [sales].[customers] s join [sales].[orders] o 
on s.customer_id = o.customer_id
order by o.order_id

---#11-->Show all products with their brand names, even if some products don't have brands. Include product name, brand name (show 'No Brand' if null)------------------
SELECT p.product_name, 
       COALESCE(b.brand_name, 'No Brand') AS brand_name,
       p.list_price
FROM [production].[products] p  JOIN [production].[brands] b
ON p.brand_id = b.brand_id

---#12-->Find products that cost more than the average product price. Show product name and price------------------
select product_name , list_price
from [production].[products] 
where list_price > (
select avg(list_price) from [production].[products]
)
---#13-->Find customers who have placed at least one order. Use a subquery with IN. Show customer_id and customer_name------------------##
select customer_id , first_name , last_name 
from [sales].[customers]
where customer_id in (
 select customer_id FROM sales.orders
)
---#14-->For each customer, show their name and total number of orders using a subquery in the SELECT clause------------------Error
SELECT customer_id, first_name, last_name,
  (select COUNT(order_id) from sales.orders)AS 'total_num'
FROM [sales].[customers] 

---#15->Create a simple view called easy_product_list that shows product name, category name, and price. Then write a query to select all products from this view where price > 100------------------
GO
CREATE VIEW easy_product_list AS 
SELECT p.product_name, c.category_name, p.list_price
FROM [production].[products] p JOIN [production].[categories] c 
ON p.category_id = c.category_id;
GO

SELECT * 
FROM easy_product_list 
WHERE list_price > 100;

---#16-->Create a view called customer_info that shows customer ID, full name (first + last), email, and city and state combined. Then use this view to find all customers from California (CA)------------------
GO

CREATE VIEW customer_info_ AS 
select customer_id , first_name + ' ' + last_name as full_name , email , city , state
from [sales].[customers]
GO 

select * 
from customer_info_ 
where state = 'CA'

---#17-->Find all products that cost between $50 and $200. Show product name and price, ordered by price from lowest to highest------------------
select product_name , list_price 
from [production].[products]
where list_price between 50 and 200 
order by list_price ASC

---#18-->Count how many customers live in each state. Show state and customer count, ordered by count from highest to lowest---------------------
select state , COUNT(*) as customer_count
from [sales].[customers]
where state IS NOT NULL
group by state 
order by customer_count DESC 

---#19-->Find the most expensive product in each category. Show category name, product name, and price------------------
SELECT c.category_name, p.product_name, p.list_price
FROM production.products p
JOIN production.categories c 
    ON p.category_id = c.category_id
WHERE p.list_price = (
    SELECT MAX(S.list_price)
    FROM production.products S
    WHERE S.category_id = p.category_id
)
---#20-->Show all stores and their cities, including the total number of orders from each store. Show store name, city, and order count------------------
select store_name , city , COUNT(order_id) as order_count
from [sales].[stores] s  join [sales].[orders] o 
on s.store_id = o.store_id
group by store_name , city
---------------------------------#s---------------------------------------------------



