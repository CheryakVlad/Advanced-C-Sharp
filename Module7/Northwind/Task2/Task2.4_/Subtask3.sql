SELECT CustomerId
FROM Northwind.dbo.Customers AS CUSTOMERS
WHERE NOT EXISTS (SELECT OrderId 
                    FROM Northwind.dbo.Orders AS ORDERS 
                    WHERE ORDERS.CustomerID = CUSTOMERS.CustomerID);