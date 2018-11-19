SELECT OrderID, 
CASE 
	WHEN ShippedDate IS NULL
	THEN 'Not Shipped'
END AS 'ShippedDate'
FROM Northwind.dbo.Orders
WHERE ShippedDate IS NULL