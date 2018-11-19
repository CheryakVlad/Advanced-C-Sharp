DECLARE 
	@SHIP_VIA INT = 2,
	@DATE DATETIME = Convert(DATETIME, '1998-05-06')

SELECT OrderID, ShippedDate, ShipVia 
FROM Northwind.dbo.Orders
WHERE ShipVia >= @SHIP_VIA AND OrderDate >= @DATE