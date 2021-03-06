USE [PrivateLabelLite]
GO
/****** Object:  StoredProcedure [dbo].[procXmlInsertProducts]    Script Date: 12/11/17 4:54:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************          
PROCEDURE NAME:   procXmlInsertProducts
AUTHOR:			
DATE:			  	
DESCRIPTION:          
------------          
 
          
REVISION 		 DATE     		EMAIL								DESCRIPTION          
-------- 		 ----       		-----     							-----------          
1.0			11-Dec-2017		neeraj.katiyar@blue-thread.com			
****************************************************************************/  



Create procedure [dbo].[procXmlUpsertProducts]
(
	@Products xml	
)
as

begin
  

Insert Into Product(Sku,SkuName,ManufacturerPartNumber,Article,VendorMapId,ProductType,QtyMin,QtyMax,LastUpdated)
Select c.value('@Sku','nvarchar(100)'),c.value('@SkuName','nvarchar(200)'),c.value('@ManufacturerPartNumber','nvarchar(100)'),
       c.value('@Article','nvarchar(100)'), c.value('@VendorMapId','nvarchar(100)'),c.value('@ProductType','nvarchar(100)'),
	  c.value('@QtyMin','int'),c.value('@QtyMax','int'),c.value('@LastUpdated','datetime')
From 
       @Products.nodes('Products/Product') T(c) left join Product P On  c.value('@Sku','nvarchar(100)') = P.SKU
	  Where P.Sku is null


Update P
Set	  P.SkuName = c.value('@SkuName','nvarchar(200)'),P.ManufacturerPartNumber =c.value('@ManufacturerPartNumber','nvarchar(100)'),
       P.Article = c.value('@Article','nvarchar(100)'),P.VendorMapId = c.value('@VendorMapId','nvarchar(100)'),P.ProductType = c.value('@ProductType','nvarchar(100)'),
	  P.QtyMin = c.value('@QtyMin','int'),P.QtyMax = c.value('@QtyMax','int'), P.LastUpdated = c.value('@LastUpdated','datetime')
From 
       @Products.nodes('Products/Product') T(c) left join Product P On  c.value('@Sku','nvarchar(100)') = P.SKU
	   
      
 Select 1 IsValid


end