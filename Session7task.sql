USE StoreDB

-- Customer activity log
CREATE TABLE sales.customer_log (
    log_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT,
    action VARCHAR(50),
    log_date DATETIME DEFAULT GETDATE()
);

-- Price history tracking
CREATE TABLE production.price_history (
    history_id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT,
    old_price DECIMAL(10,2),
    new_price DECIMAL(10,2),
    change_date DATETIME DEFAULT GETDATE(),
    changed_by VARCHAR(100)
);

-- Order audit trail
CREATE TABLE sales.order_audit (
    audit_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT,
    customer_id INT,
    store_id INT,
    staff_id INT,
    order_date DATE,
    audit_timestamp DATETIME DEFAULT GETDATE()
);

---#1-----
CREATE NONCLUSTERED INDEX IX_customers_email
ON [sales].[customers] (email)
---#2---
CREATE NONCLUSTERED INDEX IX_products_category_brand
ON [production].[products] (category_id, brand_id)
---#3---
CREATE NONCLUSTERED INDEX IX_orders_order_date_includes
ON [sales].[orders] (order_date)
INCLUDE (customer_id, store_id, order_status)
---#4---
CREATE TRIGGER tr_customer_welcome
ON [sales].[customers]
AFTER INSERT
AS
BEGIN
    INSERT INTO customer_log (customer_id, action, details)
    SELECT 
        i.customer_id, 
        'New Customer', 
        CONCAT('Welcome email sent to ', i.email)
    FROM inserted i
END
---$5---
CREATE TRIGGER tr_product_price_change
ON [production].[products]
AFTER UPDATE
AS
BEGIN
    IF UPDATE(list_price)
    BEGIN
        INSERT INTO price_history (product_id, old_price, new_price)
        SELECT 
            i.product_id, 
            d.list_price, 
            i.list_price
        FROM inserted i
        JOIN deleted d ON i.product_id = d.product_id
        WHERE i.list_price <> d.list_price;
    END
END
---#6--
CREATE TRIGGER tr_prevent_category_delete
ON [production].[categories]
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1 
        FROM deleted d JOIN [production].[products] p 
        ON d.category_id = p.category_id
    )
    BEGIN
        RAISERROR('Cannot delete category with associated products', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        DELETE FROM [production].[categories]
        WHERE category_id IN (SELECT category_id FROM deleted);
    END
END
---#7---
CREATE TRIGGER tr_update_inventory
ON [sales].[order_items]
AFTER INSERT
AS
BEGIN
    UPDATE s
    SET s.quantity = s.quantity - i.quantity
    FROM [production].[stocks] s
    JOIN inserted i ON s.product_id = i.product_id
    JOIN [sales].[orders] o ON i.order_id = o.order_id
    WHERE s.store_id = o.store_id;
END
---#8---
CREATE TRIGGER tr_order_audit
ON [sales].[orders]
AFTER INSERT
AS
BEGIN
    INSERT INTO order_audit (order_id, customer_id, order_date, order_status)
    SELECT 
        i.order_id, 
        i.customer_id, 
        i.order_date, 
        i.order_status
    FROM inserted i
END
-------------------END--------------------------------