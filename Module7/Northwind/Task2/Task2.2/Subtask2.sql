SELECT  (SELECT CONCAT(Employees.LastName, ' ', FirstName)
		 FROM Northwind.dbo.Employees
		 WHERE Employees.EmployeeID = ORDERS.EmployeeID) AS 'SELLER',
		COUNT(Orders.OrderID) AS 'AMOUNT'
FROM Northwind.dbo.Orders ORDERS
GROUP BY EmployeeID
ORDER BY 'AMOUNT' DESC;