Use PrivateLabelLite
Go
 
 Alter Table OrderHeader
 Add PONumber nvarchar(50)
  
 Go
  ALTER TABLE OrderLine
  ALTER COLUMN Quantity nvarchar(50);