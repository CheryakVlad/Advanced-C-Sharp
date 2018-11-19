SELECT YEAR(OrderDate) AS 'YEAR',
		COUNT(OrderID) AS 'TOTAL'
FROM Northwind.dbo.Orders
GROUP BY YEAR(OrderDate);