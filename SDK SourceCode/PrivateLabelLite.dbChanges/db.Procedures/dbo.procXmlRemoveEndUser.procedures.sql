USE [PrivateLabelLite]
GO
/****** Object:  StoredProcedure [dbo].[procXmlRemoveEndUser]    Script Date: 1/19/2018 5:14:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************          
PROCEDURE NAME:   procXmlRemoveEndUser
AUTHOR:			
DATE:			  	
DESCRIPTION:          
------------          
 
          
REVISION 		 DATE     		EMAIL								DESCRIPTION          
-------- 		 ----       	-----     							-----------          
1.0				19-Jan-18		karan.pasrija@blue-thread.com		Created
****************************************************************************/  

Create proc procXmlRemoveEndUser 
 @customerIds xml
 as
 Begin

	Delete
	From	Enduser 
	Where	EnduserId in ( 
							Select c.value('.','numeric(18,0)')    
							From 
								 @customerIds.nodes('CustomerIds/CustomerId') T(c)
						  )
 End