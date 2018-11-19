DECLARE 
	@YEAR INT = 1998

SELECT CustomerID,
	   EmployeeID,
	   COUNT(OrderID)   AS 'Amount'
FROM Northwind.dbo.Orders
WHERE YEAR(OrderDate) = @YEAR
GROUP BY CustomerID, EmployeeID;