SELECT L_CUSTOMERS.CustomerID    AS 'CUSTOMER ID',
       CUSTOMERS.CustomerID      AS 'NEIGHBOR ID',
       L_CUSTOMERS.City          AS 'CITY'
FROM Northwind.dbo.Customers AS L_CUSTOMERS
OUTER APPLY 
   ( 
   SELECT * 
   FROM Northwind.dbo.Customers R_CUSTOMERS
   WHERE L_CUSTOMERS.City = R_CUSTOMERS.City AND 
		 L_CUSTOMERS.CustomerID <> R_CUSTOMERS.CustomerID 
   ) CUSTOMERS 
   ORDER BY L_CUSTOMERS.CustomerID