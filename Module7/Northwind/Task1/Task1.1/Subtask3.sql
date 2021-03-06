﻿DECLARE 
	@DATE DATETIME = Convert(DATETIME, '1998-05-06'),
	@DEFAULT_DATETIME_FORMAT INT = 0

SELECT OrderID	AS 'Orger Number', 
CASE 
	WHEN ShippedDate IS NULL
	THEN 'Not Shipped'
	ELSE CONVERT(VARCHAR(30), ShippedDate, @DEFAULT_DATETIME_FORMAT)
END AS 'Shipped Date'
FROM Northwind.dbo.Orders
WHERE ShippedDate IS NULL OR ShippedDate > @DATE