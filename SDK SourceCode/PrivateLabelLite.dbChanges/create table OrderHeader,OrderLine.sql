Use PrivateLabelLite

Create table OrderHeader
(
  OrderNumber nvarchar(50) not null primary key,
  OrderDate datetime,
  OrderType nvarchar(50),
  EndUserName nvarchar(100),
  EndUserEmail nvarchar(100),
  TotalSalesPrice numeric(18,2),
  Status nvarchar(50),
  Domain nvarchar(100),
  CurrencySymbol nvarchar(10),
  CurrencyCode nvarchar(10),
  LastUpdated datetime,
)


Create table OrderLine
(
  LineId numeric(18,0) identity(1,1),
  OrderNumber nvarchar(50),
  Sku nvarchar(50),
  SkuName nvarchar(200),
  ManufacturerPartNumber nvarchar(50),
  UnitPrice numeric(18,0),
  Quantity int,
  LineStatus nvarchar(50),
  CurrencySymbol nvarchar(10),
  CurrencyCode nvarchar(10)
  primary key (OrderNumber,Sku)
)


Create table Product
(
  Id numeric(18,0) identity(1,1),
  Sku nvarchar(50),
  SkuName nvarchar(200),
  ManufacturerPartNumber nvarchar(50),
  Article nvarchar(50),
  VendorMapId nvarchar(100),
  ProductType nvarchar(50),
  QtyMin int,
  QtyMax int,
  LastUpdated datetime,
  primary key (Sku)
)
