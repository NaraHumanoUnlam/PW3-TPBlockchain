USE [SmartContracts]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 8/11/2023 12:56:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[senderAddress] [nvarchar](max) NULL,
	[recieverAddress] [nvarchar](max) NULL,
	[senderBalance] [numeric](30, 2) NULL,
	[receiverBalance] [numeric](30, 2) NULL,
	[quantityTokens] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO