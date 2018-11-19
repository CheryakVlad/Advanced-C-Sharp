SELECT SUM(Quantity * UnitPrice * (1 - Discount)) AS 'Totals'
FROM Northwind.dbo.[Order Details];