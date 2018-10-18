if exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'subscriptionsummarydetail')
	begin
			IF NOT EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'MarkUpPercentage' AND Object_ID = Object_ID(N'dbo.subscriptionsummarydetail'))
			alter table subscriptionsummarydetail add MarkUpPercentage [float] NULL
			
			IF NOT EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'SalesPrice' AND Object_ID = Object_ID(N'dbo.subscriptionsummarydetail'))
			
			alter table subscriptionsummarydetail add [SalesPrice] [float] NULL
			
			IF NOT EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'SeatLimit' AND Object_ID = Object_ID(N'dbo.subscriptionsummarydetail'))
			
			alter table subscriptionsummarydetail add SeatLimit [float] NULL
			
			IF NOT EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'TaxStatus' AND Object_ID = Object_ID(N'dbo.subscriptionsummarydetail'))
			alter table subscriptionsummarydetail add TaxStatus [nvarchar](50) NULL
			
			IF NOT EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'SeatLimitStartTime' AND Object_ID = Object_ID(N'dbo.subscriptionsummarydetail'))
			alter table subscriptionsummarydetail add SeatLimitStartTime [datetime] NULL
			
			IF NOT EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'SeatLimitEndTime' AND Object_ID = Object_ID(N'dbo.subscriptionsummarydetail'))
			alter table subscriptionsummarydetail add SeatLimitEndTime [datetime] NULL
			
			IF NOT EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'SeatCounter' AND Object_ID = Object_ID(N'dbo.subscriptionsummarydetail'))
			alter table subscriptionsummarydetail add SeatCounter [int] NULL
			
			IF NOT EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'TemporarySeatLimit' AND Object_ID = Object_ID(N'dbo.subscriptionsummarydetail'))
			alter table subscriptionsummarydetail add TemporarySeatLimit [float] NULL
			
			IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS  WHERE TABLE_NAME = 'SubscriptionSummaryDetail' and CONSTRAINT_NAME = 'PK_SubscriptionSummaryDetail')
			ALTER TABLE [dbo].[SubscriptionSummaryDetail] DROP CONSTRAINT [PK_SubscriptionSummaryDetail] WITH ( ONLINE = OFF )
			
			IF NOT EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'tblSubsId' AND Object_ID = Object_ID(N'dbo.subscriptionsummarydetail'))
			ALTER TABLE [dbo].[SubscriptionSummaryDetail] ADD [tblSubsId]  int NOT NULL identity(1,1) 

			/****** Object:  Index [PK_SubscriptionSummaryDetail]    Script Date: 10/18/2018 3:53:22 PM ******/
			IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS  WHERE TABLE_NAME = 'SubscriptionSummaryDetail' and CONSTRAINT_NAME = 'PK_SubscriptionSummaryDetail')
			ALTER TABLE [dbo].[SubscriptionSummaryDetail] ADD  CONSTRAINT [PK_SubscriptionSummaryDetail] PRIMARY KEY CLUSTERED 
			(
				[tblSubsId] ASC
			)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
	end

else
	begin
		SET ANSI_NULLS ON
		
		SET QUOTED_IDENTIFIER ON
		
		CREATE TABLE [dbo].[SubscriptionSummaryDetail](
			[OrderNumber] [nvarchar](50) NOT NULL,
			[VendorId] [nvarchar](50) NULL,
			[VendorName] [nvarchar](50) NULL,
			[SKU] [nvarchar](50) NULL,
			[SkuName] [nvarchar](50) NULL,
			[Quantity] [nvarchar](50) NULL,
			[Article] [nvarchar](50) NULL,
			[PaymentMethod] [nvarchar](50) NULL,
			[EndCustomerName] [nvarchar](50) NULL,
			[EndCustomerEmail] [nvarchar](50) NULL,
			[Company] [nvarchar](50) NULL,
			[OrderSource] [nvarchar](50) NULL,
			[UnitPrice] [nvarchar](50) NULL,
			[CurrencySymbol] [nvarchar](50) NULL,
			[CurrencyCode] [nvarchar](50) NULL,
			[CreatedDate] [datetime] NULL,
			[UpdatedDate] [datetime] NULL,
			[LineStatus] [nvarchar](50) NULL,
			[Domain] [nvarchar](50) NULL,
			[MsSubscriptionId] [nvarchar](50) NULL,
			[MicrosoftId] [nvarchar](50) NULL,
			[SubscriptionHistoryJson] [nvarchar](max) NULL,
			[SubscriptionId] [uniqueidentifier] NOT NULL,
			[MappingStatus] [nvarchar](50) NULL,
			[MarkUpPercentage] [float] NULL,
			[SalesPrice] [float] NULL,
			[SeatLimit] [float] NULL,
			[TaxStatus] [nvarchar](50) NULL,
			[SeatLimitStartTime] [datetime] NULL,
			[SeatLimitEndTime] [datetime] NULL,
			[SeatCounter] [int] NULL,
			[TemporarySeatLimit] [float] NULL,
			[tblSubsId] [int] IDENTITY(1,1) NOT NULL,
			CONSTRAINT [PK_SubscriptionSummaryDetail] PRIMARY KEY CLUSTERED 
			(
				[tblSubsId] ASC
			)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	end

if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'__MigrationHistory')
	begin
			/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 6/12/2018 11:32:59 AM ******/
			SET ANSI_NULLS ON
			
			SET QUOTED_IDENTIFIER ON
			
			SET ANSI_PADDING ON
			
			CREATE TABLE [dbo].[__MigrationHistory](
				[MigrationId] [nvarchar](150) NOT NULL,
				[ContextKey] [nvarchar](300) NOT NULL,
				[Model] [varbinary](max) NOT NULL,
				[ProductVersion] [nvarchar](32) NOT NULL,
			CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
			(
				[MigrationId] ASC,
				[ContextKey] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
			
			
			SET ANSI_PADDING OFF
	end
	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'AspNetRoles')
	begin
			/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 6/12/2018 11:32:59 AM ******/
			SET ANSI_NULLS ON
			
			SET QUOTED_IDENTIFIER ON
			
			CREATE TABLE [dbo].[AspNetRoles](
				[Id] [nvarchar](128) NOT NULL,
				[Name] [nvarchar](256) NOT NULL,
			CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			) ON [PRIMARY]
	end
			
	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'AspNetUserClaims')
	begin
			/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 6/12/2018 11:32:59 AM ******/
			SET ANSI_NULLS ON
			
			SET QUOTED_IDENTIFIER ON
			
			CREATE TABLE [dbo].[AspNetUserClaims](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[UserId] [nvarchar](128) NOT NULL,
				[ClaimType] [nvarchar](max) NULL,
				[ClaimValue] [nvarchar](max) NULL,
			CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	end	

	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'AspNetUserLogins')
	begin
			/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 6/12/2018 11:32:59 AM ******/
			SET ANSI_NULLS ON
			
			SET QUOTED_IDENTIFIER ON
			
			CREATE TABLE [dbo].[AspNetUserLogins](
				[LoginProvider] [nvarchar](128) NOT NULL,
				[ProviderKey] [nvarchar](128) NOT NULL,
				[UserId] [nvarchar](128) NOT NULL,
			CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
			(
				[LoginProvider] ASC,
				[ProviderKey] ASC,
				[UserId] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			) ON [PRIMARY]
	end	
	
	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'AspNetUserRoles')
	begin
	/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 6/12/2018 11:32:59 AM ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[AspNetUserRoles](
		[UserId] [nvarchar](128) NOT NULL,
		[RoleId] [nvarchar](128) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
	(
		[UserId] ASC,
		[RoleId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	end
		
	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'AspNetUsers')
	begin
		/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 6/12/2018 11:32:59 AM ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[AspNetUsers](
		[Id] [nvarchar](128) NOT NULL,
		[Email] [nvarchar](256) NULL,
		[EmailConfirmed] [bit] NOT NULL,
		[PasswordHash] [nvarchar](max) NULL,
		[SecurityStamp] [nvarchar](max) NULL,
		[PhoneNumber] [nvarchar](max) NULL,
		[PhoneNumberConfirmed] [bit] NOT NULL,
		[TwoFactorEnabled] [bit] NOT NULL,
		[LockoutEndDateUtc] [datetime] NULL,
		[LockoutEnabled] [bit] NOT NULL,
		[AccessFailedCount] [int] NOT NULL,
		[UserName] [nvarchar](256) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	
	end

if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'Company')
	begin
		/****** Object:  Table [dbo].[Company]    Script Date: 6/12/2018 11:32:59 AM ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[Company](
		[CompanyId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](255) NOT NULL,
		[Created] [datetime] NOT NULL,
		[CreatedBy] [nvarchar](250) NOT NULL,
	CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
	(
		[CompanyId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	end	

if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'CompanyOrder')	
	begin
	/****** Object:  Table [dbo].[CompanyOrder]    Script Date: 6/12/2018 11:32:59 AM ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[CompanyOrder](
		[RecordId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
		[CompanyId] [numeric](18, 0) NOT NULL,
		[SalesOrderId] [nvarchar](50) NOT NULL,
		[Created] [datetime] NULL,
		[CreatedBy] [nvarchar](100) NULL,
	CONSTRAINT [PK_CompanyOrder] PRIMARY KEY CLUSTERED 
	(
		[RecordId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	end
	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'Configs')	
	begin
	/****** Object:  Table [dbo].[Configs]    Script Date: 6/12/2018 11:32:59 AM ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[Configs](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Key] [nvarchar](max) NULL,
		[Value] [nvarchar](max) NULL,
		[Type] [nvarchar](max) NULL,
	CONSTRAINT [PK_dbo.Configs] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	end	
	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'Enduser')		
	begin
	/****** Object:  Table [dbo].[Enduser]    Script Date: 6/12/2018 11:32:59 AM ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[Enduser](
		[EnduserId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
		[SAPEnduserId] [uniqueidentifier] NULL,
		[CompanyId] [numeric](18, 0) NULL,
		[Created] [datetime] NULL,
		[CreatedBy] [nvarchar](255) NULL,
		[Email] [nvarchar](255) NOT NULL,
		[Name] [nvarchar](255) NOT NULL,
	CONSTRAINT [PK_Enduser] PRIMARY KEY CLUSTERED 
	(
		[EnduserId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	end
	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'Logs')		
	begin
	/****** Object:  Table [dbo].[Logs]    Script Date: 6/12/2018 11:32:59 AM ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[Logs](
		[Message] [nvarchar](max) NULL,
		[ErrorCode] [nvarchar](max) NULL,
		[Result] [nvarchar](max) NULL,
		[Key] [int] NULL,
		[IsSuccess] [bit] NULL,
		[IsValid] [bit] NULL,
		[DateTime] [datetime] NULL,
		[Browser] [nvarchar](50) NULL,
		[CurrentExecutionFilePath] [nvarchar](max) NULL,
		[RequestType] [nvarchar](50) NULL,
		[UserHostAddress] [nvarchar](max) NULL,
		[UserHostName] [nvarchar](max) NULL
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	
	end	
	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'OrderHeader')	
	begin
	/****** Object:  Table [dbo].[OrderHeader]    Script Date: 6/12/2018 11:32:59 AM ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[OrderHeader](
		[OrderNumber] [nvarchar](50) NOT NULL,
		[OrderDate] [datetime] NULL,
		[OrderType] [nvarchar](50) NULL,
		[EndUserName] [nvarchar](100) NULL,
		[EndUserEmail] [nvarchar](100) NULL,
		[TotalSalesPrice] [numeric](18, 2) NULL,
		[Status] [nvarchar](50) NULL,
		[Domain] [nvarchar](100) NULL,
		[CurrencySymbol] [nvarchar](10) NULL,
		[CurrencyCode] [nvarchar](10) NULL,
		[LastUpdated] [datetime] NULL,
		[OrderJson] [nvarchar](max) NULL,
		[PONumber] [nvarchar](50) NULL,
	PRIMARY KEY CLUSTERED 
	(
		[OrderNumber] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	
	end	
	
	
	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'OrderLine')	
	begin
	/****** Object:  Table [dbo].[OrderLine]    Script Date: 6/12/2018 11:32:59 AM ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[OrderLine](
		[LineId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
		[OrderNumber] [nvarchar](50) NOT NULL,
		[Sku] [nvarchar](50) NOT NULL,
		[SkuName] [nvarchar](200) NULL,
		[ManufacturerPartNumber] [nvarchar](50) NULL,
		[UnitPrice] [numeric](18, 0) NULL,
		[Quantity] [nvarchar](50) NULL,
		[LineStatus] [nvarchar](50) NULL,
		[CurrencySymbol] [nvarchar](10) NULL,
		[CurrencyCode] [nvarchar](10) NULL,
	CONSTRAINT [PK_OrderLine] PRIMARY KEY CLUSTERED 
	(
		[OrderNumber] ASC,
		[Sku] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	end
	
	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'Product')	
	begin
	/****** Object:  Table [dbo].[Product]    Script Date: 6/12/2018 11:32:59 AM ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[Product](
		[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
		[Sku] [nvarchar](50) NOT NULL,
		[SkuName] [nvarchar](200) NULL,
		[ManufacturerPartNumber] [nvarchar](50) NULL,
		[Article] [nvarchar](50) NULL,
		[VendorMapId] [nvarchar](100) NULL,
		[ProductType] [nvarchar](50) NULL,
		[QtyMin] [int] NULL,
		[QtyMax] [int] NULL,
		[LastUpdated] [datetime] NULL,
	CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC,
		[Sku] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	end	
	
	
if not exists(select * from information_schema.tables where table_schema = N'dbo' and table_name = N'SiteContent')	
	begin
	/****** Object:  Table [dbo].[SiteContent]    Script Date: 6/12/2018 11:32:59 AM ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[SiteContent](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Key] [nvarchar](100) NULL,
		[Value] [nvarchar](max) NULL
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	end
	
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'RoleNameIndex' AND object_id = OBJECT_ID('AspNetRoles'))
    BEGIN
        -- Index with this name, on this table does NOT exist
   
	SET ANSI_PADDING ON
	/****** Object:  Index [RoleNameIndex]    Script Date: 6/12/2018 11:32:59 AM ******/
	CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
	(
		[Name] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 END		

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_UserId' AND object_id = OBJECT_ID('AspNetUserClaims'))
    BEGIN
	SET ANSI_PADDING ON
	/****** Object:  Index [IX_UserId]    Script Date: 6/12/2018 11:32:59 AM ******/
	CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
	(
		[UserId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	End

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_UserId' AND object_id = OBJECT_ID('AspNetUserLogins'))
    BEGIN
	SET ANSI_PADDING ON
	
	
	/****** Object:  Index [IX_UserId]    Script Date: 6/12/2018 11:32:59 AM ******/
	CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
	(
		[UserId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	End

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_RoleId' AND object_id = OBJECT_ID('AspNetUserRoles'))
    BEGIN
	SET ANSI_PADDING ON
	/****** Object:  Index [IX_RoleId]    Script Date: 6/12/2018 11:32:59 AM ******/
	CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
	(
		[RoleId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	End

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_UserId' AND object_id = OBJECT_ID('AspNetUserRoles'))
    BEGIN
	SET ANSI_PADDING ON
	/****** Object:  Index [IX_UserId]    Script Date: 6/12/2018 11:32:59 AM ******/
	CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
	(
		[UserId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	End

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'UserNameIndex' AND object_id = OBJECT_ID('AspNetUsers'))
    BEGIN
	SET ANSI_PADDING ON
	/****** Object:  Index [UserNameIndex]    Script Date: 6/12/2018 11:32:59 AM ******/
	CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
	(
		[UserName] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	End


	

IF  EXISTS (SELECT *  FROM sys.foreign_keys WHERE name = (N'FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId') AND parent_object_id = OBJECT_ID(N'dbo.AspNetUserClaims'))
BEGIN
		
		    ALTER TABLE dbo.AspNetUserClaims DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
END

			ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
			REFERENCES [dbo].[AspNetUsers] ([Id])
			ON DELETE CASCADE
			
			ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]



IF  EXISTS (SELECT *  FROM sys.foreign_keys WHERE name = (N'FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId') AND parent_object_id = OBJECT_ID(N'dbo.AspNetUserLogins'))
BEGIN
			ALTER TABLE dbo.AspNetUserLogins DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
end
			ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
			REFERENCES [dbo].[AspNetUsers] ([Id])
			ON DELETE CASCADE
			
			ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]



IF  EXISTS (SELECT *  FROM sys.foreign_keys WHERE name = (N'FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId') AND parent_object_id = OBJECT_ID(N'dbo.AspNetUserRoles'))
BEGIN
			ALTER TABLE dbo.AspNetUserRoles DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
end


			ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
			REFERENCES [dbo].[AspNetRoles] ([Id])
			ON DELETE CASCADE
			
			ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]


IF  EXISTS (SELECT *  FROM sys.foreign_keys WHERE name = (N'FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId') AND parent_object_id = OBJECT_ID(N'dbo.AspNetUserRoles'))
BEGIN
			ALTER TABLE dbo.AspNetUserRoles DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
end

			ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
			REFERENCES [dbo].[AspNetUsers] ([Id])
			ON DELETE CASCADE
			
			ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]



IF  EXISTS (SELECT *  FROM sys.foreign_keys WHERE name = (N'FK_CompanyOrder_Company') AND parent_object_id = OBJECT_ID(N'dbo.CompanyOrder'))
BEGIN
			ALTER TABLE dbo.CompanyOrder DROP CONSTRAINT [FK_CompanyOrder_Company]
end

			ALTER TABLE [dbo].[CompanyOrder]  WITH CHECK ADD  CONSTRAINT [FK_CompanyOrder_Company] FOREIGN KEY([CompanyId])
			REFERENCES [dbo].[Company] ([CompanyId])
			
			ALTER TABLE [dbo].[CompanyOrder] CHECK CONSTRAINT [FK_CompanyOrder_Company]



IF  EXISTS (SELECT *  FROM sys.foreign_keys WHERE name = (N'FK_CompanyOrder_OrderHeader') AND parent_object_id = OBJECT_ID(N'dbo.CompanyOrder'))
BEGIN
			ALTER TABLE dbo.CompanyOrder DROP CONSTRAINT [FK_CompanyOrder_OrderHeader]
end

			ALTER TABLE [dbo].[CompanyOrder]  WITH CHECK ADD  CONSTRAINT [FK_CompanyOrder_OrderHeader] FOREIGN KEY([SalesOrderId])
			REFERENCES [dbo].[OrderHeader] ([OrderNumber])
			
			ALTER TABLE [dbo].[CompanyOrder] CHECK CONSTRAINT [FK_CompanyOrder_OrderHeader]


IF  EXISTS (SELECT *  FROM sys.foreign_keys WHERE name = (N'FK_Enduser_Company') AND parent_object_id = OBJECT_ID(N'dbo.Enduser'))
BEGIN
			ALTER TABLE dbo.Enduser DROP CONSTRAINT [FK_Enduser_Company]
end

			ALTER TABLE [dbo].[Enduser]  WITH CHECK ADD  CONSTRAINT [FK_Enduser_Company] FOREIGN KEY([CompanyId])
			REFERENCES [dbo].[Company] ([CompanyId])
			
			ALTER TABLE [dbo].[Enduser] CHECK CONSTRAINT [FK_Enduser_Company]


IF  EXISTS (SELECT *  FROM sys.foreign_keys WHERE name = (N'FK_OrderLine_OrderHeader') AND parent_object_id = OBJECT_ID(N'dbo.OrderLine'))
BEGIN
			ALTER TABLE dbo.OrderLine DROP CONSTRAINT [FK_OrderLine_OrderHeader]
end

			ALTER TABLE [dbo].[OrderLine]  WITH CHECK ADD  CONSTRAINT [FK_OrderLine_OrderHeader] FOREIGN KEY([OrderNumber])
			REFERENCES [dbo].[OrderHeader] ([OrderNumber])
			
			ALTER TABLE [dbo].[OrderLine] CHECK CONSTRAINT [FK_OrderLine_OrderHeader]
	
	

/****** Object:  StoredProcedure [dbo].[procGetCompanySalesOrders]    Script Date: 6/12/2018 11:32:59 AM ******/

IF OBJECT_ID('procGetCompanySalesOrders') IS not NULL

/****** Object:  StoredProcedure [dbo].[procGetCompanySalesOrders]    Script Date: 7/26/2018 5:06:23 PM ******/
DROP PROCEDURE [dbo].[procGetCompanySalesOrders]

GO

/****** Object:  StoredProcedure [dbo].[procGetCompanySalesOrders]    Script Date: 7/26/2018 5:06:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	CREATE procedure [dbo].[procGetCompanySalesOrders]
	
		@CompanyId numeric(18,0),
		@EndUser nvarchar(100),
		@SalesOrderNo nvarchar(100),
		@rowCount int,
		@Pageno int
	
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
	

GO



	
	
	/****** Object:  StoredProcedure [dbo].[procGetSubscriptionSummary]    Script Date: 6/12/2018 11:32:59 AM ******/
	
IF OBJECT_ID('procGetSubscriptionSummary') IS not NULL

/****** Object:  StoredProcedure [dbo].[procGetSubscriptionSummary]    Script Date: 7/26/2018 5:35:19 PM ******/
DROP PROCEDURE [dbo].[procGetSubscriptionSummary]
GO

/****** Object:  StoredProcedure [dbo].[procGetSubscriptionSummary]    Script Date: 7/26/2018 5:35:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[procGetSubscriptionSummary]
  @PageSize INT, 
  @PageNum  INT,
  @CompanyName nvarchar(50),
  @Domain nvarchar(50),
  @ProductName nvarchar(50),
  @ResellerPO nvarchar(50),
  @OrderNumber nvarchar(50),
  @EditProductStatus bit 
	as
begin
if(@EditProductStatus = 0)
begin
;WITH TempResult AS(

    SELECT (case when isnull(SSD.Domain,'') = '' then OH.Domain else SSD.Domain End) Domain,SSD.OrderNumber,SSD.SkuName,SSD.SKU,isnull(OL.Quantity,'') Quantity,SSD.SubscriptionId,
		SSD.Company,SSD.MappingStatus,isnull(OL.LineStatus,'') LineStatus,SSD.CreatedDate,isnull(OH.PONumber,'')PONumber,isnull(OH.[Status],'')[Status],isnull(OH.OrderDate,'')OrderDate,
		SSD.UnitPrice,SSD.MarkUpPercentage,SSD.SeatLimit,SSD.TaxStatus,SSD.SeatLimitStartTime,SSD.SeatLimitEndTime,SSD.SeatCounter,SSD.CurrencySymbol

    FROM SubscriptionSummaryDetail SSD left join OrderHeader OH on SSD.OrderNumber=OH.OrderNumber left join OrderLine OL on SSD.OrderNumber = OL.OrderNumber and SSD.SKU = OL.Sku

	where (Isnull(@CompanyName,'') = '' or SSD.Company = @CompanyName) and
	(ISnull(@Domain,'')= '' or SSD.Domain like '%' + @Domain + '%') and
	(ISnull(@ProductName,'')= '' or SSD.SkuName like '%' + @ProductName + '%') and
	(ISnull(@ResellerPO,'')= '' or OH.PONumber like '%' + @ResellerPO + '%') and
	(ISnull(@OrderNumber,'')= '' or SSD.OrderNumber like '%' + @OrderNumber + '%') and 
	ISNULL(SSD.SkuName,'') not like '%Azure%' and ISNULL(SSD.UnitPrice,'0') <> '0' and
	ISNULL(OH.[Status],'') <> 'cancelled'
),
 TempCount AS (
    SELECT COUNT(*) AS MaxRows FROM TempResult
)
SELECT  Domain,OrderNumber,SkuName,SKU,Quantity,SubscriptionId,Company,MappingStatus,LineStatus,PONumber,[Status],OrderDate,CreatedDate,MaxRows,UnitPrice,MarkUpPercentage,SeatLimit,
   TaxStatus,SeatLimitStartTime,SeatLimitEndTime,SeatCounter,CurrencySymbol

FROM TempResult, TempCount

ORDER BY TempResult.MappingStatus
    OFFSET (@PageNum-1)*@PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY

End
if(@EditProductStatus = 1)
begin
;WITH TempResult AS(
	SELECT 
    * 
	FROM
    (
         SELECT (case when isnull(SSD.Domain,'') = '' then OH.Domain else SSD.Domain End) Domain,SSD.OrderNumber,SSD.SkuName,SSD.SKU,isnull(OL.Quantity,'') Quantity,SSD.SubscriptionId,SSD.Company,SSD.MappingStatus,isnull(OL.LineStatus,'') LineStatus,SSD.CreatedDate,isnull(OH.PONumber,'')PONumber,isnull(OH.[Status],'')[Status],isnull(OH.OrderDate,'')OrderDate,isnull(SSD.UnitPrice,'')UnitPrice,isnull(SSD.SalesPrice,'')SalesPrice,isnull(SSD.MarkUpPercentage,'')MarkUpPercentage,isnull(SSD.SeatLimit,'')SeatLimit,
         ROW_NUMBER() OVER (PARTITION BY SSD.SkuName ORDER BY SSD.SkuName DESC) rownumber 
      
         FROM SubscriptionSummaryDetail SSD left join OrderHeader OH on SSD.OrderNumber=OH.OrderNumber left join OrderLine OL on SSD.OrderNumber = OL.OrderNumber and SSD.SKU = OL.Sku
		 where (Isnull(@CompanyName,'') = '' or SSD.Company = @CompanyName) and
	(ISnull(@Domain,'')= '' or SSD.Domain like '%' + @Domain + '%') and
	(ISnull(@ProductName,'')= '' or SSD.SkuName like '%' + @ProductName + '%') and
	(ISnull(@ResellerPO,'')= '' or OH.PONumber like '%' + @ResellerPO + '%') and
	(ISnull(@OrderNumber,'')= '' or SSD.OrderNumber like '%' + @OrderNumber + '%') and 
	ISNULL(SSD.SkuName,'') not like '%Azure%' and ISNULL(SSD.UnitPrice,'0') <> '0' and
	ISNULL(OH.[Status],'') <> 'cancelled') a 
	WHERE
    rownumber = 1
),TempCount AS (
    SELECT COUNT(*) AS MaxRows FROM TempResult
)
	SELECT  Domain,OrderNumber,SkuName,SKU,Quantity,SubscriptionId,Company,MappingStatus,LineStatus,PONumber,[Status],OrderDate,CreatedDate,MaxRows,UnitPrice,SalesPrice,MarkUpPercentage,SeatLimit
FROM TempResult, TempCount
End
End


GO

IF OBJECT_ID('procGetUsers') IS not NULL
/****** Object:  StoredProcedure [dbo].[procGetUsers]    Script Date: 7/27/2018 2:35:41 PM ******/
DROP PROCEDURE [dbo].[procGetUsers]
GO

/****** Object:  StoredProcedure [dbo].[procGetUsers]    Script Date: 7/27/2018 2:35:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[procGetUsers]
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


GO

IF OBJECT_ID('procGetUserSubscriptions') IS not NULL
/****** Object:  StoredProcedure [dbo].[procGetUserSubscriptions]    Script Date: 7/27/2018 2:38:28 PM ******/
DROP PROCEDURE [dbo].[procGetUserSubscriptions]
GO

/****** Object:  StoredProcedure [dbo].[procGetUserSubscriptions]    Script Date: 7/27/2018 2:38:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[procGetUserSubscriptions]

  @PageSize INT, 
  @PageNum  INT,
  @EndUserEmail nvarchar(50),
  @EndUserName nvarchar(50),
  @ProductName nvarchar(50)
	as
begin
 declare @CompanyDetail as table(companyId nvarchar(50))
 declare @CompanyDetail1 as table(companyName nvarchar(50))
  insert into @CompanyDetail
  Select companyid from Enduser where Email = @EndUserEmail
 

  Insert into @CompanyDetail1
  select C.name from Company C inner join @CompanyDetail CD on CD.CompanyId = C.CompanyId
;WITH TempResult AS(
  SELECT SSD.Domain,SSD.OrderNumber,SSD.SkuName,SSD.SKU,isnull(OL.Quantity,'') Quantity,SSD.SubscriptionId,SSD.Company,SSD.MappingStatus,SSD.LineStatus,SSD.CreatedDate,OH.PONumber,OH.[Status],
  OH.OrderDate,SSD.UnitPrice,SSD.MarkUpPercentage,
		isnull(SSD.SeatLimit,'')SeatLimit,isnull(SSD.TaxStatus,'')TaxStatus,isnull(SSD.SeatLimitStartTime,'')SeatLimitStartTime,isnull(SeatLimitEndTime,'')SeatLimitEndTime,
		isnull(SSD.SeatCounter,'')SeatCounter,isnull(SSD.CurrencySymbol,'')CurrencySymbol
    FROM SubscriptionSummaryDetail SSD left join OrderLine OL on SSD.OrderNumber = OL.OrderNumber and SSD.SKU = OL.Sku
	inner Join @CompanyDetail1 cd1 on cd1.companyName = ssd.Company
	left join OrderHeader OH on SSD.OrderNumber=OH.OrderNumber 
	where
	(ISnull(@ProductName,'')= '' or SSD.SkuName like '%' + @ProductName + '%') and
	SSD.MappingStatus = 'MAPPED' and
    OH.[Status] <> 'cancelled'
	), TempCount AS (
    SELECT COUNT(*) AS MaxRows FROM TempResult
)
SELECT  Domain,OrderNumber,SkuName,SKU,Quantity,SubscriptionId,Company,MappingStatus,LineStatus,PONumber,[Status],OrderDate,CreatedDate,MaxRows,UnitPrice,MarkUpPercentage,SeatLimit,
   TaxStatus,SeatLimitStartTime,SeatLimitEndTime,SeatCounter,CurrencySymbol
FROM TempResult, TempCount
ORDER BY TempResult.MappingStatus
    OFFSET (@PageNum-1)*@PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY
end
GO


IF OBJECT_ID('procInsertIntoLogs') IS not NULL
/****** Object:  StoredProcedure [dbo].[procInsertIntoLogs]    Script Date: 7/27/2018 2:39:25 PM ******/
DROP PROCEDURE [dbo].[procInsertIntoLogs]
GO

/****** Object:  StoredProcedure [dbo].[procInsertIntoLogs]    Script Date: 7/27/2018 2:39:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[procInsertIntoLogs]
(
@message nvarchar(max),
@ErrorCode nvarchar(max),
@Result nvarchar(max),
@Key int,
@IsSuccess bit,
@IsValid bit,
@TimeStamp datetime,
@Browser nvarchar(max),
@CurrentExecutionFilePath nvarchar(max),
@RequestType nvarchar(max),
@UserHostAddress nvarchar(max),
@UserHostName nvarchar(max)

)
as
begin
insert into Logs([Message],ErrorCode,Result,[Key],IsSuccess,IsValid,[DateTime],Browser,CurrentExecutionFilePath,RequestType,
UserHostAddress,UserHostName) values (@message,@ErrorCode,@Result,@Key,@IsSuccess,@IsValid,@TimeStamp,@Browser,@CurrentExecutionFilePath,
@RequestType,@UserHostAddress,@UserHostName)
end

GO


IF OBJECT_ID('procIsUserAuthorizeToIncreaseSeat') IS not NULL
/****** Object:  StoredProcedure [dbo].[procIsUserAuthorizeToIncreaseSeat]    Script Date: 7/27/2018 2:40:29 PM ******/
DROP PROCEDURE [dbo].[procIsUserAuthorizeToIncreaseSeat]
GO

/****** Object:  StoredProcedure [dbo].[procIsUserAuthorizeToIncreaseSeat]    Script Date: 7/27/2018 2:40:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[procIsUserAuthorizeToIncreaseSeat]
(
 @Quantity nvarchar(max),
 @SKU nvarchar(50),
 @SeatCounter int,
 @OrderNumber nvarchar(50),
 @OriginalQuantity int
)
as

Begin
declare @Response as bit,@counter int,@SeatLimitEndTime dateTime,@TemporarySeatLimit int,@SeatLimit int

Select @counter = ISNULL(SSD.SeatCounter,0) , @TemporarySeatLimit = ISNULL(TemporarySeatLimit,0),@SeatLimit = ISNULL(SeatLimit,0),@SeatLimitEndTime = ISNULL(SeatLimitEndTime,0) from SubscriptionSummaryDetail SSD where SSD.OrderNumber = @OrderNumber and SSD.SKU = @SKU 

if(getdate() > ISNULL(@SeatLimitEndTime,0))
begin
				Update SubscriptionSummaryDetail
				set SeatCounter = 0,TemporarySeatLimit = @SeatLimit
				where OrderNumber = @OrderNumber and SKU = @SKU 
			
			set @counter = 0
end
if(@counter = 0 )
Begin
	if(@Quantity >@TemporarySeatLimit)
	begin
	 set @Response = 0
	end
	else
	begin
	Update SubscriptionSummaryDetail
	set SeatLimitStartTime = GETDATE(),SeatLimitEndTime = SeatLimitStartTime + 1,SeatCounter = @counter + 1
	where OrderNumber = @OrderNumber and SKU = @SKU 

	set @Response = 1
	end
End
Else
BEgin
	if(@Quantity > @TemporarySeatLimit)
		Begin
		set @Response = 0 
		End
	Else
	Begin
		select @SeatLimitEndTime = ISNULL(SSD.SeatLimitEndTime,0) from SubscriptionSummaryDetail SSD where SSD.OrderNumber = @OrderNumber and SSD.SKU = @SKU 
		if(getDate() > @SeatLimitEndTime)
		begin
		set @Response = 0
		end
		Begin
				Update SubscriptionSummaryDetail
				set SeatCounter = @counter + 1
				where OrderNumber = @OrderNumber and SKU = @SKU 
			
			set @Response = 1
		End
	ENd
ENd

select Cast(@Response as bit) 'Response'
End
GO

IF OBJECT_ID('procXmlDeleteCompanyOrderMapping') IS not NULL

/****** Object:  StoredProcedure [dbo].[procXmlDeleteCompanyOrderMapping]    Script Date: 7/27/2018 2:41:13 PM ******/
DROP PROCEDURE [dbo].[procXmlDeleteCompanyOrderMapping]
GO

/****** Object:  StoredProcedure [dbo].[procXmlDeleteCompanyOrderMapping]    Script Date: 7/27/2018 2:41:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[procXmlDeleteCompanyOrderMapping]
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
GO

IF OBJECT_ID('procXmlGetOrders') IS not NULL

/****** Object:  StoredProcedure [dbo].[procXmlGetOrders]    Script Date: 7/27/2018 2:42:03 PM ******/
DROP PROCEDURE [dbo].[procXmlGetOrders]
GO

/****** Object:  StoredProcedure [dbo].[procXmlGetOrders]    Script Date: 7/27/2018 2:42:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[procXmlGetOrders]
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
GO

IF OBJECT_ID('procXmlInsertCompanies') IS not NULL
/****** Object:  StoredProcedure [dbo].[procXmlInsertCompanies]    Script Date: 7/27/2018 2:42:39 PM ******/
DROP PROCEDURE [dbo].[procXmlInsertCompanies]
GO

/****** Object:  StoredProcedure [dbo].[procXmlInsertCompanies]    Script Date: 7/27/2018 2:42:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




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
GO

IF OBJECT_ID('procXmlRemoveEndUser') IS not NULL

/****** Object:  StoredProcedure [dbo].[procXmlRemoveEndUser]    Script Date: 7/27/2018 2:43:14 PM ******/
DROP PROCEDURE [dbo].[procXmlRemoveEndUser]
GO

/****** Object:  StoredProcedure [dbo].[procXmlRemoveEndUser]    Script Date: 7/27/2018 2:43:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[procXmlRemoveEndUser] 
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
GO


IF OBJECT_ID('procXmlUpsertProducts') IS not NULL

/****** Object:  StoredProcedure [dbo].[procXmlUpsertProducts]    Script Date: 7/27/2018 2:43:44 PM ******/
DROP PROCEDURE [dbo].[procXmlUpsertProducts]
GO

/****** Object:  StoredProcedure [dbo].[procXmlUpsertProducts]    Script Date: 7/27/2018 2:43:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[procXmlUpsertProducts]
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
GO













	
	
	
		

	
	