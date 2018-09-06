/****************************************************************************          
PROCEDURE NAME:   procGetUsers
AUTHOR:			
DATE:			  	
DESCRIPTION:          
------------          
 
          
REVISION 		 DATE     		EMAIL								DESCRIPTION          
-------- 		 ----       		-----     							-----------          
1.0			04-Sep-2017		neeraj.katiyar@blue-thread.com			Get User's
1.1			18-Jan-2018		neeraj.katiyar@blue-thread.com			sort by companyName
****************************************************************************/  

Alter procedure [dbo].[procGetUsers]
(
	@Id numeric(18,0),
	@UserName nvarchar(100),
	@CompanyName nvarchar(100),
	@rowCount int,
	@pageNo int,
	@orderBy nvarchar(50),
	@CompanyId numeric(18,0)
)
as

begin
declare @fromRow as int,@TotalRecords as int
                                   
set @fromRow=@rowCount*@Pageno 


select	@TotalRecords =  COUNT(U.EnduserId) 	
From	     EndUser U join Company C
on	      U.CompanyId = C.CompanyId 
Where	(U.EnduserId = @Id OR @Id = 0)and
		(@CompanyId = 0 or C.CompanyId = @CompanyId) and
		(U.Name = ''  or U.Name like IsnULL(@UserName,'') + '%') and
		(Isnull(@CompanyName,'') = '' or C.Name = @CompanyName)




select * from(
			 select   ROW_NUMBER() over (order by C.Name ) as Row, U.EnduserId,U.Name EndUserName, C.Name CompanyName, U.Email, @TotalRecords TotalRecords
			 From     EndUser U join Company C
			 on	     U.CompanyId = C.CompanyId 
			 Where    (U.EnduserId = @Id OR @Id = 0)and
				     (@CompanyId = 0 or C.CompanyId = @CompanyId) and
					(U.Name = ''  or U.Name like IsnULL(@UserName,'') + '%') and
					(Isnull(@CompanyName,'') = '' or C.Name = @CompanyName)
	        ) r
Where Row between @fromRow-@rowCount+1 AND  @fromRow

end

