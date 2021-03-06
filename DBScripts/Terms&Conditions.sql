GO
IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES
				 WHERE TABLE_SCHEMA = 'dbo'  
                 and TABLE_NAME = 'sitecontent'))
		BEGIN
				
				 Declare @CountRows as int
				 select @CountRows =  count(*) from sitecontent
				 if(@CountRows=0)
				    begin
						SET IDENTITY_INSERT [dbo].[SiteContent] ON 
						INSERT [dbo].[SiteContent] ([Id], [Key], [Value]) VALUES (1, N'TermsAndConditions', N'
						Customer Acceptable Use Policy for Web App
						
						Customer agrees not to copy, modify, port, adapt, translate, frame or mirror the Web App here in use, or any related software, or to reverse engineer, decompile, disassemble or otherwise attempt to discover the source code of such software. Customer shall not sublicense, assign or transfer Web App here in use or any rights in Web App here in use, or authorize or permit any portion of Web App here in use to be accessed by another individual or entity other than employees and individual contractors (e.g., temporary employees) of Customer that have been authorized by Customer to access Web App here in use.
						
						Customer is not permitted to (a) use the Web App here in use on behalf of third parties; (b) rent, lease, lend or grant other rights in the Web App here in use or (c) use any component, library, database or other technology included with the Web App other than solely in connection with Customer’s use of the Web App here in use.
						
						Customer may download and make copies of the Web App documentation for Customer’s internal use, but no more than the amount reasonably necessary. Customer must retain on all such copies all copyright and other proprietary notices that appear on or in such documentation.
						
						Customer shall use commercially reasonable efforts to prevent unauthorized access to or use of Web App here in use through its systems or accounts, and shall promptly notify Web App Admin of any such unauthorized access or use of which it is aware. Customer shall not use the Web App here in use in any unlawful manner or to facilitate any unlawful acts.
						
						The Web App, all related software (“Software”), and any copies that Customer is authorized to make are the intellectual property. The structure, organization and code of the Software are the valuable trade secrets and confidential information. The Software is protected by copyright, including without limitation by United States Copyright Law, international treaty provisions and applicable laws in the country in which it is being used. Except as expressly stated herein, access to the Web App does not grant to Customer any intellectual property rights in the Software
						
						Customer grants to Web App Admin licensors a worldwide, non-exclusive, perpetual, irrevocable, fully paid, royalty-free, sublicensable license to use and incorporate into the Web App any suggestions, enhancement requests, recommendations or other feedback regarding features of functions of the Web App provided by Customer relating to the Web App.
						
						BY CLICKING “ACCEPT” BELOW, YOU ACKNOWLEDGE THAT YOU HAVE READ THIS AGREEMENT, THAT YOU UNDERSTAND IT, AND THAT YOU AGREE TO BE BOUND BY ITS TERMS AND CONDITIONS.
						')
						SET IDENTITY_INSERT [dbo].[SiteContent] OFF
				    end
				 else	
					begin
					SET IDENTITY_INSERT [dbo].[SiteContent] ON 
					update SiteContent set value  = '
						Customer Acceptable Use Policy for Web App
						
						Customer agrees not to copy, modify, port, adapt, translate, frame or mirror the Web App here in use, or any related software, or to reverse engineer, decompile, disassemble or otherwise attempt to discover the source code of such software. Customer shall not sublicense, assign or transfer Web App here in use or any rights in Web App here in use, or authorize or permit any portion of Web App here in use to be accessed by another individual or entity other than employees and individual contractors (e.g., temporary employees) of Customer that have been authorized by Customer to access Web App here in use.
						
						Customer is not permitted to (a) use the Web App here in use on behalf of third parties; (b) rent, lease, lend or grant other rights in the Web App here in use or (c) use any component, library, database or other technology included with the Web App other than solely in connection with Customer’s use of the Web App here in use.
						
						Customer may download and make copies of the Web App documentation for Customer’s internal use, but no more than the amount reasonably necessary. Customer must retain on all such copies all copyright and other proprietary notices that appear on or in such documentation.
						
						Customer shall use commercially reasonable efforts to prevent unauthorized access to or use of Web App here in use through its systems or accounts, and shall promptly notify Web App Admin of any such unauthorized access or use of which it is aware. Customer shall not use the Web App here in use in any unlawful manner or to facilitate any unlawful acts.
						
						The Web App, all related software (“Software”), and any copies that Customer is authorized to make are the intellectual property. The structure, organization and code of the Software are the valuable trade secrets and confidential information. The Software is protected by copyright, including without limitation by United States Copyright Law, international treaty provisions and applicable laws in the country in which it is being used. Except as expressly stated herein, access to the Web App does not grant to Customer any intellectual property rights in the Software
						
						Customer grants to Web App Admin licensors a worldwide, non-exclusive, perpetual, irrevocable, fully paid, royalty-free, sublicensable license to use and incorporate into the Web App any suggestions, enhancement requests, recommendations or other feedback regarding features of functions of the Web App provided by Customer relating to the Web App.
						
						BY CLICKING “ACCEPT” BELOW, YOU ACKNOWLEDGE THAT YOU HAVE READ THIS AGREEMENT, THAT YOU UNDERSTAND IT, AND THAT YOU AGREE TO BE BOUND BY ITS TERMS AND CONDITIONS.
						'
					where id = '1'	
					SET IDENTITY_INSERT [dbo].[SiteContent] OFF
					end

		END


