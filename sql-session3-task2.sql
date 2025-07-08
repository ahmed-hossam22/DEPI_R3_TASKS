USE StoreDB
----------#1-->List all products with list price greater than 1000-----------
Select product_id , product_name , list_price 
from [production].[products]
where list_price>1000
ORDER BY list_price DESC
----------#2-->Get customers from "CA" or "NY" states----------------------
Select * 
from [sales].[customers]
where state='CA' or state = 'NY'
----------#3-->Retrieve all orders placed in 2023----------- 
Select * 
from [sales].[orders]
where YEAR(order_date) =2023
----------#4-->Show customers whose emails end with @gmail.com-----------
select customer_id, first_name, last_name, email 
from [sales].[customers]
where email like '%@gmail.com'
----------#5-->Show all inactive staff-----------------
select * 
from [sales].[staffs]
where active=0
----------#6-->List top 5 most expensive products-------------
select top 5 *
from [production].[products]
ORDER BY list_price DESC
----------#7-->Show latest 10 orders sorted by date-----------
select top 10  * 
from [sales].[orders]
ORDER BY order_date DESC 
----------#8-->Retrieve the first 3 customers alphabetically by last name-----------
select top 3 customer_id, first_name, last_name
from [sales].[customers]
order by last_name ASC
----------#9-->Find customers who did not provide a phone number--------------------
select * 
from [sales].[staffs]
where manager_id IS NOT NULL 
----------#10-->Show all staff who have a manager assigned-------------------------
SELECT*
FROM [sales].[staffs] 
WHERE manager_id IS NOT NULL
----------#11-->Count number of products in each category---------------------------
select category_name , COUNT(*) AS 'NUM'
from [production].[products] A JOIN [production].[categories] B
ON A.category_id = B.category_id
GROUP BY category_name
----------#12-->Count number of customers in each state-----------
SELECT state, COUNT(*) AS Counterr
FROM [sales].[customers]
GROUP BY state
----------#13-->Get average list price of products per brand-----------
SELECT brand_name, AVG(A.list_price) AS 'AVG'
FROM [production].[products] A JOIN [production].[brands] B
On A.brand_id = B.brand_id
GROUP BY brand_name
----------#14-->Show number of orders per staff----------
SELECT first_name, last_name ,phone , COUNT(*) AS Order_Count
FROM [sales].[staffs] A JOIN [sales].[orders] B
ON A.staff_id = B.staff_id
GROUP BY first_name, last_name
----------#15-->ind customers who made more than 2 orders---------
SELECT A.customer_id , first_name , last_name , COUNT(*) AS num
FROM [sales].[customers] A JOIN [sales].[orders] B
ON A.customer_id = B.customer_id 
GROUP BY A.customer_id , first_name , last_name
HAVING COUNT(*) > 2;
----------#16----------
select * 
from [production].[products]
where list_price BETWEEN  500 and 1500
----------#17----------
select * 
from [sales].[customers]
where city like 'S%'
----------#18----------
SELECT *
FROM [sales].[orders]
WHERE order_status IN (2, 4)
----------#19----------
select * 
from [production].[categories]
where category_id in (1,2,3)
----------#20----------
SELECT * 
FROM [sales].[staffs]
WHERE store_id = 1 OR phone IS NULL;
----------Done----------