SELECT CustomerID, 
		Country
FROM  Northwind.dbo.Customers
WHERE SUBSTRING(Country, 1, 1) BETWEEN 'B' AND 'G'
ORDER BY Country;