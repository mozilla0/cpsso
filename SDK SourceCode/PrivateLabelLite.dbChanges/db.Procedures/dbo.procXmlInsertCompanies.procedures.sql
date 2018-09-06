USE [PrivateLabelLite]
GO
/****** Object:  StoredProcedure [dbo].[procXmlInsertCompanies]    Script Date: 08/Jan/18 4:54:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************          
PROCEDURE NAME:   procXmlInsertCompanies
AUTHOR:			
DATE:			  	
DESCRIPTION:          
------------          
 
          
REVISION 		 DATE     		EMAIL								DESCRIPTION          
-------- 		 ----       		-----     							-----------          
1.0			08-Jan-18		neeraj.katiyar@blue-thread.com			
****************************************************************************/  



Create procedure [dbo].[procXmlInsertCompanies]
(
	@Companies xml	,
	@CreatedBy nvarchar(100)
)
as

begin
  

    Insert Into Company(Name,Created,CreatedBy)
    Select c.value('.','nvarchar(100)'),GETDATE(),@CreatedBy      
    From 
		 @Companies.nodes('Companies/Company') T(c) left join 
		 Company Com On  c.value('.','nvarchar(100)') = Com.Name
    Where Com.Name is null





end