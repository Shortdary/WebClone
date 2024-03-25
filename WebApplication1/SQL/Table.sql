CREATE TABLE [dbo].[Board](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[board_name] [nvarchar](20) NULL,
	[board_name_eng] [nchar](20) NULL,
	[description] [nvarchar](30) NULL,
 CONSTRAINT [PK_Board] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO







CREATE TABLE [dbo].[Comment](
	[id]				INT			IDENTITY(1,1) NOT NULL,
	[post_id]			INT NOT NULL,
	[comment]			NVARCHAR(50) NOT NULL,
	[like_count]		INT NOT NULL,
	[created_time]		DATETIME NOT NULL,
	[created_uid]		INT NOT NULL,
	[parent_comment_id] INT NULL,
	[is_deleted]		INT NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [is_deleted_default_value]  DEFAULT ((0)) FOR [is_deleted]
GO






CREATE TABLE [dbo].[Post](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[board_id] [int] NOT NULL,
	[subject] [nvarchar](30) NOT NULL,
	[comment_count] [int] NOT NULL,
	[view_count] [int] NOT NULL,
	[like_count] [int] NOT NULL,
	[created_time] [datetime2](7) NULL,
	[created_uid] [int] NOT NULL,
	[detail] [nvarchar](max) NOT NULL,
	[is_deleted] [bit] NULL,
	[updated_uid] [int] NULL,
	[updated_time] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO






CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login_id] [nchar](20) NOT NULL,
	[password] [nchar](100) NULL,
	[nickname] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nickname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[login_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO