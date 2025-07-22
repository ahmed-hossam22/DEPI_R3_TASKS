USE StoreDB
--#1---
DECLARE @CustomerID INT = 1
DECLARE @TotalSpent DECIMAL(10,2)

SELECT @TotalSpent = SUM(oi.quantity * oi.list_price * (1 - oi.discount))
FROM [sales].[orders] o JOIN [sales].[order_items] oi 
ON o.order_id = oi.order_id
WHERE o.customer_id = @CustomerID;

SELECT 
    @CustomerID AS CustomerID,
    @TotalSpent AS TotalSpent,
    CASE 
        WHEN @TotalSpent > 5000 THEN 'VIP Customer'
        ELSE 'Regular Customer'
    END AS CustomerStatus;
--#2--
DECLARE @PriceThreshold DECIMAL(10,2) = 1500
DECLARE @ProductCount INT

SELECT @ProductCount = COUNT(*) FROM [production].[products] 
WHERE list_price > @PriceThreshold;

SELECT 
    CONCAT('There are ', @ProductCount,' products priced above $', @PriceThreshold) AS ReportMessage
--#3--
DECLARE @StaffID INT = 2
DECLARE @Year INT = 2017
DECLARE @TotalSales DECIMAL(10,2)

SELECT @TotalSales = SUM(oi.quantity * oi.list_price * (1 - oi.discount))
FROM [sales].[orders] o JOIN [sales].[order_items] oi ON o.order_id = oi.order_id
WHERE o.staff_id = @StaffID AND YEAR(o.order_date) = @Year;

SELECT 
    @StaffID AS StaffID,
    @Year AS Year,
    @TotalSales AS TotalSales,
    CONCAT('Staff #', @StaffID, ' achieved $', 
           FORMAT(@TotalSales, 'N2'), ' in sales during ', @Year) AS PerformanceSummary     
 --#4--
SELECT 
    @@SERVERNAME AS ServerName,
    @@VERSION AS SQLServerVersion,
    @@ROWCOUNT AS RowsAffected
 --#5--
DECLARE @ProductID INT = 1;
DECLARE @StoreID INT = 1;
DECLARE @Quantity INT;

SELECT @Quantity = quantity 
FROM [production].[stocks] 
WHERE product_id = @ProductID AND store_id = @StoreID

IF @Quantity > 20
    SELECT 'Well stocked' AS InventoryStatus
ELSE IF @Quantity BETWEEN 10 AND 20
    SELECT 'Moderate stock' AS InventoryStatus
ELSE IF @Quantity < 10
    SELECT 'Low stock - reorder needed' AS InventoryStatus
ELSE
    SELECT 'No stock data available' AS InventoryStatus
 --#6--
DECLARE @BatchSize INT = 3
DECLARE @RowsAffected INT = 1

WHILE @RowsAffected > 0
BEGIN
    UPDATE TOP (@BatchSize) [production].[stocks]
    SET quantity = quantity + 10
    WHERE quantity < 5
    
    SET @RowsAffected = @@ROWCOUNT
    
    IF @RowsAffected > 0
        PRINT CONCAT('Updated ', @RowsAffected, ' low-stock items')
END
PRINT 'All low-stock items updated'
--#7--
SELECT 
    product_id,
    product_name,
    list_price,
    CASE 
        WHEN list_price < 300 THEN 'Budget'
        WHEN list_price BETWEEN 300 AND 800 THEN 'Mid-Range'
        WHEN list_price BETWEEN 801 AND 2000 THEN 'Premium'
        ELSE 'Luxury'
    END AS PriceCategory
FROM [production].[products]
ORDER BY list_price DESC
--#8--
DECLARE @CustomerID INT = 5;
IF EXISTS (SELECT 1 FROM [sales].[customers] WHERE customer_id = @CustomerID)
    SELECT COUNT(*) AS OrderCount 
    FROM [sales].[orders] 
    WHERE customer_id = @CustomerID;
ELSE
    SELECT 'Customer not found' AS Message
 --#9--
 CREATE FUNCTION CalculateShipping(@OrderTotal DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN CASE 
        WHEN @OrderTotal > 100 THEN 0
        WHEN @OrderTotal BETWEEN 50 AND 99 THEN 5.99
        ELSE 12.99
    END
END
--#10--
CREATE FUNCTION GetProductsByPriceRange(@MinPrice DECIMAL(10,2), @MaxPrice DECIMAL(10,2))
RETURNS TABLE
AS
RETURN
    SELECT 
        p.product_id,
        p.product_name,
        p.list_price,
        b.brand_name,
        c.category_name
    FROM [production].[products] p JOIN [production].[brands] b 
    ON p.brand_id = b.brand_id JOIN [production].[categories] c ON p.category_id = c.category_id
    WHERE p.list_price BETWEEN @MinPrice AND @MaxPrice
--#11--
CREATE FUNCTION GetCustomerYearlySummary(@CustomerID INT)
RETURNS @Summary TABLE (
    Year INT,
    OrderCount INT,
    TotalSpent DECIMAL(10,2),
    AvgOrderValue DECIMAL(10,2)
)
AS
BEGIN
    INSERT INTO @Summary
    SELECT 
        YEAR(order_date) AS Year,
        COUNT(DISTINCT o.order_id) AS OrderCount,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS TotalSpent,
        AVG(oi.quantity * oi.list_price * (1 - oi.discount)) AS AvgOrderValue
    FROM [sales].[orders] o JOIN [sales].[order_items] oi
    ON o.order_id = oi.order_id
    WHERE o.customer_id = @CustomerID
    GROUP BY YEAR(order_date)
    
    RETURN
END
--#12--

CREATE FUNCTION CalculateBulkDiscount(@Quantity INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    RETURN CASE 
        WHEN @Quantity BETWEEN 1 AND 2 THEN 0
        WHEN @Quantity BETWEEN 3 AND 5 THEN 5
        WHEN @Quantity BETWEEN 6 AND 9 THEN 10
        ELSE 15
    END
END
--#13--
CREATE PROCEDURE sp_GetCustomerOrderHistory
    @CustomerID INT,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL
AS
BEGIN
    SELECT 
        o.order_id,
        o.order_date,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS OrderTotal
    FROM [sales].[orders] o JOIN [sales].[order_items] oi 
    ON o.order_id = oi.order_id
    WHERE o.customer_id = @CustomerID
    AND (@StartDate IS NULL OR o.order_date >= @StartDate)
    AND (@EndDate IS NULL OR o.order_date <= @EndDate)
    GROUP BY o.order_id, o.order_date
    ORDER BY o.order_date DESC
END
--#14--
CREATE PROCEDURE sp_RestockProduct
    @StoreID INT,
    @ProductID INT,
    @RestockQty INT,
    @OldQty INT OUTPUT,
    @NewQty INT OUTPUT,
    @Success BIT OUTPUT
AS
BEGIN
    SELECT @OldQty = quantity 
    FROM [production].[stocks] 
    WHERE store_id = @StoreID AND product_id = @ProductID;
    
    IF @OldQty IS NULL
    BEGIN
        SET @Success = 0;
        RETURN;
    END
    
    UPDATE [production].[stocks]
    SET quantity = quantity + @RestockQty
    WHERE store_id = @StoreID AND product_id = @ProductID
    
    SET @NewQty = @OldQty + @RestockQty
    SET @Success = 1
END
--#15--
CREATE PROCEDURE sp_ProcessNewOrder
    @CustomerID INT,
    @ProductID INT,
    @Quantity INT,
    @StoreID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        IF NOT EXISTS (SELECT 1 FROM [sales].[customers] WHERE customer_id = @CustomerID)
            THROW 50001, 'Customer not found', 1;
            
        DECLARE @StockQty INT;
        SELECT @StockQty = quantity 
        FROM [production].[stocks] 
        WHERE product_id = @ProductID AND store_id = @StoreID;
        
        IF @StockQty < @Quantity
            THROW 50002, 'Insufficient stock', 1
            
        DECLARE @OrderID INT
        INSERT INTO [sales].[orders] (customer_id, order_date, store_id, staff_id)
        VALUES (@CustomerID, GETDATE(), @StoreID, 1)
        
        SET @OrderID = SCOPE_IDENTITY()
        
        DECLARE @Price DECIMAL(10,2)
        SELECT @Price = list_price FROM [production].[products] WHERE product_id = @ProductID
        
        INSERT INTO [sales].[order_items] (order_id, product_id, quantity, list_price, discount)
        VALUES (@OrderID, @ProductID, @Quantity, @Price, 0)
        
        UPDATE [production].[stocks]
        SET quantity = quantity - @Quantity
        WHERE product_id = @ProductID AND store_id = @StoreID
        
        COMMIT TRANSACTION
        
        SELECT 'Order processed successfully' AS Result, @OrderID AS OrderID
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
            
        SELECT 'Error: ' + ERROR_MESSAGE() AS Result
    END CATCH
END
--#16---

CREATE PROCEDURE sp_SearchProducts
    @SearchTerm VARCHAR(255) = NULL,
    @CategoryID INT = NULL,
    @MinPrice DECIMAL(10,2) = NULL,
    @MaxPrice DECIMAL(10,2) = NULL,
    @SortColumn VARCHAR(50) = 'product_name'
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);
    
    SET @SQL = N'
    SELECT 
        p.product_id,
        p.product_name,
        p.list_price,
        b.brand_name,
        c.category_name
    FROM [production].[products] p
    JOIN [production].[brands] b ON p.brand_id = b.brand_id
    JOIN [production].[categories] c ON p.category_id = c.category_id
    WHERE 1=1';
    
    IF @SearchTerm IS NOT NULL
        SET @SQL = @SQL + N' AND p.product_name LIKE ''%' + @SearchTerm + '%'''
    
    IF @CategoryID IS NOT NULL
        SET @SQL = @SQL + N' AND p.category_id = ' + CAST(@CategoryID AS VARCHAR)
    
    IF @MinPrice IS NOT NULL
        SET @SQL = @SQL + N' AND p.list_price >= ' + CAST(@MinPrice AS VARCHAR)
    
    IF @MaxPrice IS NOT NULL
        SET @SQL = @SQL + N' AND p.list_price <= ' + CAST(@MaxPrice AS VARCHAR)
    
    SET @SQL = @SQL + N' ORDER BY ' + @SortColumn
    
    EXEC sp_executesql @SQL
END
--#17--
DECLARE @StartDate DATE = '2023-01-01'
DECLARE @EndDate DATE = '2023-03-31'
DECLARE @BaseRate DECIMAL(5,2) = 0.05

SELECT 
    s.staff_id,
    s.first_name + ' ' + s.last_name AS staff_name,
    SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales,
    CASE 
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) > 50000 THEN @BaseRate + 0.03
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) > 20000 THEN @BaseRate + 0.02 
        ELSE @BaseRate 
    END AS bonus_rate,
    SUM(oi.quantity * oi.list_price * (1 - oi.discount)) * 
    CASE 
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) > 50000 THEN @BaseRate + 0.03
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) > 20000 THEN @BaseRate + 0.02
        ELSE @BaseRate
    END AS bonus_amount
FROM [sales].[orders] o JOIN [sales].[order_items] oi 
ON o.order_id = oi.order_id JOIN [sales].[staffs] s
ON o.staff_id = s.staff_id
WHERE o.order_date BETWEEN @StartDate AND @EndDate
GROUP BY s.staff_id, s.first_name, s.last_name
ORDER BY bonus_amount DESC
---#18---
UPDATE s
SET s.quantity = 
    CASE 
        WHEN c.category_name = 'Premium' AND s.quantity < 10 THEN s.quantity + 20
        WHEN c.category_name = 'Standard' AND s.quantity < 5 THEN s.quantity + 15
        ELSE s.quantity + 10
    END
FROM [production].[stocks] s
JOIN [production].[products] p ON s.product_id = p.product_id
JOIN [production].[categories] c ON p.category_id = c.category_id
WHERE s.quantity < 10
---#19---
SELECT 
    c.customer_id,
    c.first_name + ' ' + c.last_name AS customer_name,
    COALESCE(SUM(oi.quantity * oi.list_price * (1 - oi.discount)), 0) AS total_spent,
    CASE 
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) IS NULL THEN 'No Orders'
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) > 5000 THEN 'Platinum'
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) > 2000 THEN 'Gold'
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) > 500 THEN 'Silver'
        ELSE 'Standard'
    END AS loyalty_tier
FROM [sales].[customers] c
LEFT JOIN [sales].[orders] o ON c.customer_id = o.customer_id
LEFT JOIN [sales].[order_items] oi ON o.order_id = oi.order_id
GROUP BY c.customer_id, c.first_name, c.last_name
ORDER BY total_spent DESC
---#20---
CREATE PROCEDURE sp_DiscontinueProduct
    @ProductID INT,
    @ReplacementProductID INT = NULL
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        IF EXISTS (
            SELECT 1 
            FROM [sales].[order_items] oi
            JOIN [sales].[orders] o ON oi.order_id = o.order_id
            WHERE oi.product_id = @ProductID AND o.order_status IN (1, 2) 
        )
        BEGIN
            IF @ReplacementProductID IS NOT NULL
            BEGIN
        
                UPDATE oi
                SET oi.product_id = @ReplacementProductID,
                    oi.list_price = p.list_price
                FROM [sales].[order_items] oi
                JOIN [sales].[orders] o ON oi.order_id = o.order_id
                JOIN [production].[products] p ON @ReplacementProductID = p.product_id
                WHERE oi.product_id = @ProductID AND o.order_status IN (1, 2)
                
                PRINT 'Pending orders updated with replacement product'
            END
            ELSE
            BEGIN
                THROW 50003, 'Product has pending orders with no replacement specified', 1
            END
        END
        
        DELETE FROM [production].[stocks] WHERE product_id = @ProductID
        COMMIT TRANSACTION;
        
        SELECT 'Product discontinued successfully' AS Result
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
            
        SELECT 'Error: ' + ERROR_MESSAGE() AS Result
    END CATCH
END


-----------------------END----------------------------------
