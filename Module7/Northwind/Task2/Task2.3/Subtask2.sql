SELECT CUSTOMERS.CustomerID, 
	   CUSTOMERS.ContactName,
	   COUNT(ORDERS.OrderID)
FROM Northwind.dbo.Customers AS CUSTOMERS
LEFT JOIN Northwind.dbo.Orders AS ORDERS ON CUSTOMERS.CustomerID = ORDERS.CustomerID
GROUP BY CUSTOMERS.CustomerID, CUSTOMERS.ContactName;