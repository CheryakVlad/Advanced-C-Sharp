DECLARE 
	@ORDERS_COUNT INT = 150

SELECT EmployeeID  
FROM Northwind.dbo.Employees AS EMPLOYEES
WHERE (SELECT COUNT(OrderID) 
        FROM Northwind.dbo.Orders AS ORDERS 
        WHERE ORDERS.EmployeeID = EMPLOYEES.EmployeeID) > @ORDERS_COUNT;