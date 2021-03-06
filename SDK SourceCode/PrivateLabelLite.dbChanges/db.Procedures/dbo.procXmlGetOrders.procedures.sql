USE [PrivateLabelLite]
GO
/****** Object:  StoredProcedure [dbo].[procXmlGetOrders]    Script Date: 11/30/17 12:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************          
PROCEDURE NAME:   procXmlGetOrders
AUTHOR:			
DATE:			  	
DESCRIPTION:          
------------          
 
          
REVISION 		 DATE     		EMAIL								DESCRIPTION          
-------- 		 ----       		-----     							-----------          
1.0			30-Nov-2017		neeraj.katiyar@blue-thread.com			Create
1.1			29-Dec-2017		neeraj.katiyar@blut-thread.com			do not get cancelled order
1.2			18-jan-2018		neeraj.katiyar@blut-thread.com			select result(order by CompanyName)
****************************************************************************/  

Alter procedure [dbo].[procXmlGetOrders]
(
	@IsAdmin bit,
	@UserEmailId nvarchar(100),
	@OrderNumber nvarchar(50),
	@UserName nvarchar(100),
	@SkuName nvarchar(100),
	@rowCount int,
	@Pageno int,
	@CompanyName nvarchar(100)
)
as

begin
  

Declare @fromRow as int,@TotalRecords as int, @CompanyId as int                                   
set @fromRow=@rowCount*@Pageno   

Select @CompanyId = (Select Top 1 Companyid from Enduser where Email = @UserEmailId),
       @UserName = Isnull(@UserName,''), @UserEmailId = ISNULL(@UserEmailId,''),
	  @SkuName = ISNULL(@SkuName,''), @OrderNumber = ISNULL(@OrderNumber,''), @CompanyName = ISNULL(@CompanyName,'')

Set @TotalRecords = ( 
					   Select	 Count(OrderNumber) 
					   From	 (
								Select  Oh.OrderNumber,
									   (
										  Select	P.SkuName '@SkuName' 
										  From	OrderLine Ol join Product P on Ol.Sku = P.Sku
										  Where	Ol.OrderNumber = Oh.OrderNumber and 
												Ol.SkuName like '%' + @SkuName + '%'
										  for	xml path('OrderLine'), root('OrderLines'),Type 
									   ) OrderLines 
								From	   OrderHeader Oh Join CompanyOrder Co on Oh.OrderNumber = Co.SalesOrderId
								Where  (Oh.Status <> 'Cancelled') and ( (@IsAdmin = 1) or (Co.CompanyId = @CompanyId )) and
									  (@OrderNumber = '' or Oh.OrderNumber like '%' + @OrderNumber + '%') and
									  (Oh.EndUserName like '%' + @UserName + '%') and
									  ((@IsAdmin = 0 or @CompanyName = '') or 
									   (Co.CompanyId in (Select CompanyId 
													 from Company 
													 where name like '%' + @CompanyName + '%'
													 )
									   )
									  )					 
							 ) Result
					   Where	 OrderLines is not null
				  )



    Select * From (
				Select ROW_NUMBER() over (order by C.Name) Row, Oh.OrderNumber,Oh.OrderDate,Oh.OrderType,Oh.Status,Oh.Domain,Oh.EndUserName,
					  Oh.EndUserEmail,Oh.TotalSalesPrice,Oh.CurrencySymbol,Oh.CurrencyCode,@TotalRecords TotalRecords, C.CompanyId, C.Name CompanyName,
				    (
					   Select	 Ol.LineId '@LineId', Ol.Sku '@Sku',P.SkuName '@SkuName' ,Ol.Quantity '@Quantity' ,Ol.UnitPrice '@UnitPrice',
							 Ol.ManufacturerPartNumber '@ManufacturerPartNumber',Ol.LineStatus '@LineStatus',Ol.CurrencySymbol '@CurrencySymbol',
							 Ol.CurrencyCode '@CurrencyCode'
					   From	 OrderLine Ol join Product P on Ol.Sku = P.Sku
					   Where	 Ol.OrderNumber = Oh.OrderNumber and 
							 Ol.SkuName like '%' + @SkuName + '%'
					   for	 xml path('OrderLine'), root('OrderLines'),Type 
				    ) OrderLines 
				From  OrderHeader Oh Join CompanyOrder Co on Oh.OrderNumber = Co.SalesOrderId
					 Join Company C on Co.CompanyId = C.CompanyId
				Where  (Oh.Status <> 'Cancelled') and ( (@IsAdmin = 1) or (Co.CompanyId = @CompanyId )) and
									  (@OrderNumber = '' or Oh.OrderNumber like '%' + @OrderNumber + '%') and
									  (Oh.EndUserName like '%' + @UserName + '%') and
									  ((@IsAdmin = 0 or @CompanyName = '') or 
									   (Co.CompanyId in (Select CompanyId 
													 from Company 
													 where name like '%' + @CompanyName + '%'
													 )
									   )
									  )					 
			 ) Result
    Where	  (Row Between @fromRow -@rowCount + 1 And @fromRow ) and
		  OrderLines is not null
end