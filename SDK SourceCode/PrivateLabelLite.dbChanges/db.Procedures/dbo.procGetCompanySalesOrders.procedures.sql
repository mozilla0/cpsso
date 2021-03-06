USE [PrivateLabelLite]
GO
/****** Object:  StoredProcedure [dbo].[procGetCompanySalesOrders]    Script Date: 12/04/17 3:03:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************          
PROCEDURE NAME:   procGetCompanySalesOrders
AUTHOR:			
DATE:			  	
DESCRIPTION:          
------------          
 
          
REVISION 		 DATE     		EMAIL								DESCRIPTION          
-------- 		 ----       		-----     							-----------          
1.0			04-Sep-2017		neeraj.katiyar@blue-thread.com			Get User's
1.1			18-Jan-2018		neeraj.katiyar@blue-thread.com			sort by companyName
***************************************************************************

procGetCompanySalesOrders 6,'sandeep','',10,1
*/  

ALTER procedure [dbo].[procGetCompanySalesOrders]
(
	@CompanyId numeric(18,0),
	@EndUser nvarchar(100),
	@SalesOrderNo nvarchar(100),
	@rowCount int,
	@Pageno int
)
as

begin
declare @fromRow as int,@TotalRecords as int
                                   
set @fromRow=@rowCount*@Pageno 

Declare @temp table(
    companyId int
    )

Insert into @temp
Select CompanyId
From EndUser
Where Email like '%'+@EndUser + '%' or name like '%'+@EndUser + '%'

If (IsNull(@EndUser,'')> '')
  Begin
    
    Set @CompanyId = -1
  End

select	@TotalRecords = COUNT(C.CompanyId)
From Company C , CompanyOrder S	
Where C.CompanyId = S.CompanyId and
      (IsNUll(@CompanyId,0) = 0 or (C.CompanyId = @CompanyId or C.CompanyId in (select CompanyId from @temp))) and
	 (IsNUll(@SalesOrderNo,'') = '' or S.SalesOrderId like '%' + @SalesOrderNo +'%') 

select * from(
			 select  ROW_NUMBER() over (order by C.Name) Row, C.Name CompanyName, S.SalesOrderId, C.CompanyId, RecordId, S.Created , S.CreatedBy,@TotalRecords TotalRecords
			 From     CompanyOrder S	Left Join Company C
			 on   S.CompanyId = C.CompanyId
			 Where C.CompanyId = S.CompanyId and
				  (IsNUll(@CompanyId,0) = 0 or (C.CompanyId = @CompanyId or C.CompanyId in (select CompanyId from @temp))) and
				  (IsNUll(@SalesOrderNo,'') = '' or S.SalesOrderId like '%' + @SalesOrderNo +'%') 
	        ) r
Where Row between @fromRow-@rowCount+1 AND  @fromRow


end
