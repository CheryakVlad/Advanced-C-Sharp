SELECT C.CustomerID, 
	   C.ContactName,
	   EMPLOYEERS.EmployeeID,
	   CONCAT(EMPLOYEERS.FirstName, '', EMPLOYEERS.LastName) AS EmployeeName,
	   C.City
FROM Northwind.dbo.Customers AS C
CROSS APPLY
	(
	SELECT * 
	FROM Northwind.dbo.Employees AS E
	WHERE C.City = E.City
	) AS EMPLOYEERS