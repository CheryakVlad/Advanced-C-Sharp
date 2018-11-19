SELECT ContactName, 
	   Country
FROM Northwind.dbo.Customers
WHERE Country NOT IN ('USA','CANADA')
ORDER BY ContactName;