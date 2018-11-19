﻿DECLARE 
	@REGION_WESTERN VARCHAR(20) = 'Western'

SELECT DISTINCT CONCAT(EMPLOYEES.FirstName, ' ', EMPLOYEES.LastName) AS 'Employees',
	   REGION.RegionDescription										 AS 'Region'
FROM Northwind.dbo.Employees AS EMPLOYEES
INNER JOIN Northwind.dbo.EmployeeTerritories AS EMPLOYEE_TERRITORIES ON EMPLOYEES.EmployeeID = EMPLOYEE_TERRITORIES.EmployeeID
INNER JOIN Northwind.dbo.Territories AS TERRITORIES ON EMPLOYEE_TERRITORIES.TerritoryID = TERRITORIES.TerritoryID
INNER JOIN Northwind.dbo.Region AS REGION ON TERRITORIES.RegionID = REGION.RegionID
WHERE REGION.RegionDescription = @REGION_WESTERN;