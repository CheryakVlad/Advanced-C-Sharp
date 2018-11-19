SELECT ContactName, 
	   Country
FROM Northwind.dbo.Customers
WHERE Country IN ('USA','CANADA')
ORDER BY ContactName, Address;