SELECT CompanyName
FROM Northwind.dbo.Suppliers
WHERE SupplierID IN (SELECT SupplierID
                                    FROM Northwind.dbo.Products 
                                    WHERE UnitsInStock = 0)