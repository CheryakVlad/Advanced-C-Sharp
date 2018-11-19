SELECT ContactName
FROM  Northwind.dbo.Customers
WHERE SUBSTRING(Country, 1, 1) >= 'B' AND SUBSTRING(Country, 1, 1) <= 'G'
ORDER BY Country;