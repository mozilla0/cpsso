USE [PrivateLabelLite]
GO
/****** Object:  StoredProcedure [dbo].[procXmlDeleteCompanyOrderMapping]    Script Date: 1/23/2018 6:42:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************          
PROCEDURE NAME:   procXmlDeleteCompanyOrderMapping
AUTHOR:			
DATE:			  	
DESCRIPTION:          
------------          
 
          
REVISION 		 DATE     		EMAIL								DESCRIPTION          
-------- 		 ----       	-----     							-----------          
1.0				22-Jan-18		karan.pasrija@blue-thread.com		Created
****************************************************************************/  

Create proc [dbo].[procXmlDeleteCompanyOrderMapping]
 @recordIds xml
 As
 BEGIN

 Declare @SalesOrderTable table(SalesOrderId nvarchar(50)) /*table to store salesorderid */
 insert into @SalesOrderTable(SalesOrderId) 
 select salesorderid
	From	CompanyOrder
	Where	RecordId in ( 
							Select c.value('.','numeric(18,0)')    
							From 
								 @recordIds.nodes('RecordIds/RecordId') T(c)
						  )
 
   delete from companyorder where SalesOrderId in(select SalesOrderId from @SalesOrderTable) 
   delete from orderline where OrderNumber  in(select SalesOrderId from @SalesOrderTable) 
   delete from OrderHeader where OrderNumber in(select SalesOrderId from @SalesOrderTable) 
  END