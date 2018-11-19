
SELECT SUPPLIERS.CompanyName
FROM Northwind.dbo.Suppliers AS SUPPLIERS
WHERE SupplierID IN (SELECT SupplierID
                                    FROM Northwind.dbo.Products 
                                    WHERE UnitsInStock = 0);