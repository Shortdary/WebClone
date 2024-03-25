-- =============================================
-- Author:		kkh
-- Create date: 2024-03-15
-- Description:	get user by login credentials
-- =============================================
ALTER PROCEDURE [dbo].[spSelectUserByLoginCredentials] 
	@login_id nchar(20),
	@password nchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
	id,
	nickname

	FROM [dbo].[User] 
	WHERE login_id=@login_id AND password=@password
END
GO





-- =============================================
-- Author:		kkh
-- Create date: 2024-02-27
-- Description:	insert post
-- =============================================
ALTER PROCEDURE [dbo].[spInsertPost]
	-- Add the parameters for the stored procedure here
	@id int output
	,@board_name_eng nchar(20) output
	,@board_id int
	,@subject nvarchar(30)
	,@detail nvarchar(max)
	,@created_uid int
AS
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
INSERT INTO [dbo].[Post]
           ([board_id]
           ,[subject]
		   ,[detail]
           ,[comment_count]
           ,[view_count]
           ,[like_count]
           ,[created_time]
           ,[created_uid])
     VALUES
           (@board_id
           ,@subject
		   ,@detail
           ,0
           ,0
           ,0
           ,GETUTCDATE()
           ,@created_uid)

	SET @id = SCOPE_IDENTITY()
	SELECT @board_name_eng = [board_name_eng] FROM Board WHERE id = @board_id
END
GO


-- =============================================
-- Author:		kkh
-- Create date: 2024-03-21
-- Description:	select all posts 
-- =============================================
ALTER PROCEDURE [dbo].[spSelectAllPosts] 
	-- Add the parameters for the stored procedure here
	@page_number int = 1
	,@page_size int = 2
AS	
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM
	(
		SELECT [Post].[id]
			  ,[board_id]
			  ,[subject]
			  ,[comment_count]
			  ,[view_count]
			  ,[like_count]
			  ,[created_time]
			  ,[created_uid]
			  ,[User].[nickname]
			  ,[Board].[board_name]
			  ,[Board].[board_name_eng]
			  ,ROW_NUMBER() OVER (ORDER BY [Post].[id] DESC) AS row_num
		  FROM [dbo].[Post]
		  INNER JOIN [dbo].[Board] ON Post.board_id=[dbo].[Board].id
		  INNER JOIN [dbo].[User] ON Post.created_uid=[dbo].[User].id
		  WHERE [Post].[is_deleted] = 0
	) AS Sub
	WHERE row_num BETWEEN (@page_number - 1) * @page_size + 1 AND @page_number * @page_size;

	  
	 SELECT COUNT(*) AS total_row_count FROM [dbo].[Post]
	 WHERE [Post].[is_deleted] = 0;
END
GO


-- =============================================
-- Author:		kkh
-- Create date: 2024-02-27
-- Description:	select popular posts 
-- =============================================
ALTER PROCEDURE [dbo].[spSelectPopularPosts] 
	-- Add the parameters for the stored procedure here
	@page_number int = 1
	,@page_size int = 1
AS	
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM
	(
		SELECT [Post].[id]
			  ,[board_id]
			  ,[subject]
			  ,[comment_count]
			  ,[view_count]
			  ,[like_count]
			  ,[created_time]
			  ,[created_uid]
			  ,[User].[nickname]
			  ,[Board].[board_name]
			  ,[Board].[board_name_eng]
			  ,ROW_NUMBER() OVER (ORDER BY [Post].[id] DESC) AS row_num
		  FROM [dbo].[Post]
		  INNER JOIN [dbo].[Board] ON Post.board_id=[dbo].[Board].id
		  INNER JOIN [dbo].[User] ON Post.created_uid=[dbo].[User].id
		  WHERE [like_count] >= 5 AND [Post].[is_deleted] = 0
	) AS Sub
	WHERE row_num BETWEEN (@page_number - 1) * @page_size + 1 AND @page_number * @page_size;

	  
	 SELECT COUNT(*) AS total_row_count FROM [dbo].[Post]
	 WHERE [like_count] >= 5 AND [Post].[is_deleted] = 0;
END
GO


-- =============================================
-- Author:		kkh
-- Create date: 2024-02-29
-- Description:	select post detail 
-- =============================================
ALTER PROCEDURE [dbo].[spSelectPostDetail] 
	-- Add the parameters for the stored procedure here
	@id int
AS	
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Post].[id]
		  ,[board_id]
		  ,[subject]
		  ,[detail]
		  ,[comment_count]
		  ,[view_count]
		  ,[like_count]
		  ,[created_time]
		  ,[created_uid]
		  ,[User].[nickname]
		  ,[Board].[board_name]
		  ,[Board].[board_name_eng]
	  FROM [dbo].[Post]
	  INNER JOIN [dbo].[Board] ON Post.board_id=[dbo].[Board].id
	  INNER JOIN [dbo].[User] ON Post.created_uid=[dbo].[User].id
	  WHERE [Post].[id] = @id
END
GO


-- =============================================
-- Author:		kkh
-- Create date: 2024-02-27
-- Description:	select all posts by board id
-- =============================================
ALTER PROCEDURE [dbo].[spSelectPostsByBoardId]
	-- Add the parameters for the stored procedure here
	@board_id int
	,@page_number int = 1
	,@page_size int = 1
AS
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM
	(
		SELECT [Post].[id]
			  ,[board_id]
			  ,[subject]
			  ,[comment_count]
			  ,[view_count]
			  ,[like_count]
			  ,[created_time]
			  ,[created_uid]
			  ,[User].[nickname]
			  ,[Board].[board_name]
			  ,[Board].[board_name_eng]
			  ,ROW_NUMBER() OVER (ORDER BY [Post].[id] DESC) AS row_num
			  FROM [dbo].[Post]
			  INNER JOIN [dbo].[Board] ON Post.board_id=[dbo].[Board].id
			  INNER JOIN [dbo].[User] ON Post.created_uid=[dbo].[User].id
			  WHERE board_id = @board_id AND [Post].[is_deleted] = 0
	) AS Sub
	WHERE row_num BETWEEN (@page_number - 1) * @page_size + 1 AND @page_number * @page_size;

	SELECT COUNT(*) AS total_row_count FROM [dbo].[Post]
	WHERE board_id = @board_id AND [Post].[is_deleted] = 0;
	  
END
GO


-- =============================================
-- Author:		kkh
-- Create date: 2024-03-19
-- Description:	update post
-- =============================================
ALTER PROCEDURE [dbo].[spUpdatePost]
	-- Add the parameters for the stored procedure here
	 @id int
	,@subject nvarchar(30)
	,@detail nvarchar(max)
	,@updated_uid int
AS
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Post]
	SET	subject = @subject
		, detail = @detail
		, updated_uid = @updated_uid
		, updated_time = GETUTCDATE()
	WHERE id = @id
END
GO


-- =============================================
-- Author:		kkh
-- Create date: 2024-03-19
-- Description:	delete post
-- =============================================
ALTER PROCEDURE [dbo].[spDeletePost]
	-- Add the parameters for the stored procedure here
	@id int
AS
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Post]
	SET	is_deleted = 'true'
	WHERE id = @id
END
GO





-- =============================================
-- Author:		kkh
-- Create date: 2024-03-23
-- Description:	insert comment
-- =============================================
ALTER PROCEDURE [dbo].[spInsertComment]
	-- Add the parameters for the stored procedure here
	@post_id int
	,@comment nvarchar(50)
	,@created_uid int
	,@parent_comment_id int = NULL
AS
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
INSERT INTO [dbo].[Comment]
			([post_id]
			 ,[comment]
			 ,[like_count]
			 ,[created_time]
			 ,[created_uid]
			 ,[parent_comment_id]
			 ,[is_deleted]
			)
     VALUES
           (@post_id
           ,@comment
		   ,0
           ,GETUTCDATE()
           ,@created_uid
           ,@parent_comment_id
           ,0)

UPDATE [dbo].[Post] 
SET [comment_count] = [comment_count] + 1 
WHERE [id] = @post_id

END
GO


-- =============================================
-- Author:		kkh
-- Create date: 2024-02-27
-- Description:	select all comments
-- =============================================
ALTER PROCEDURE [dbo].[spSelectCommentsByPostId] 
	-- Add the parameters for the stored procedure here
	@post_id int
AS
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	
SELECT [Comment].[id]
		,[post_id]
		,[comment]
		,[like_count]
		,[created_time]
		,[created_uid]
		,[parent_comment_id]
		,[nickname]
FROM Comment
		INNER JOIN [dbo].[User] ON Comment.created_uid=[dbo].[User].id
WHERE post_id = 2
ORDER BY
		CASE WHEN parent_comment_id IS NULL 
			THEN created_time 
			ELSE (SELECT created_time 
					FROM Comment c2 
					WHERE c2.id = Comment.parent_comment_id) 
			END ASC,
		CASE WHEN parent_comment_id IS NULL THEN NULL ELSE created_time END ASC;

END
GO





-- =============================================
-- Author:		kkh
-- Create date: 2024-03-07
-- Description:	get board infos by board id
-- =============================================
ALTER   PROCEDURE [dbo].[spSelectBoardInfoByBoardId]
	-- Add the parameters for the stored procedure here
	@board_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Board WHERE id = @board_id
END
GO