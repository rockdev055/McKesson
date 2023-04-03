USE [Test] -- use YOUR_DB_NAME
GO

/****** Object:  Table [dbo].[tblDemoApp]    Script Date: 3/30/2023 1:34:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblDemoApp](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](200) NULL,
	[Time] [time](7) NULL,
 CONSTRAINT [PK_tblDemoApp] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

