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

	FROM [dbo].[ApplicationUser] 
	WHERE login_id=@login_id AND password=@password
END
GO


-- =============================================
-- Author:		kkh
-- Create date: 2024-03-29
-- Description:	get user list
-- =============================================
ALTER PROCEDURE [dbo].[spSelectAllUsers] 
	@page_number int = 1
	,@page_size int = 2
	,@search_target nvarchar(20)
	,@search_keyword nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @main_query NVARCHAR(MAX);
	DECLARE @count_query NVARCHAR(MAX);
	DECLARE @main_query_filter NVARCHAR(MAX) = '';

	SET @count_query = 'SELECT COUNT(*) AS total_row_count FROM [dbo].[ApplicationUser] WHERE 1=1'

	IF @search_target = 'id' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = ' AND id = TRY_CONVERT(INT, @search_keyword)';
        SET @count_query = @count_query + ' AND id = TRY_CONVERT(INT, @search_keyword);';
    END

	IF @search_target = 'nickname' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = ' AND nickname LIKE ''%' + @search_keyword + '%''';
        SET @count_query = @count_query + ' AND nickname LIKE ''%' + @search_keyword + '%'';';
    END

	SET @main_query = 
	'
	SELECT * FROM (
		SELECT
		id,
		nickname
		,ROW_NUMBER() OVER (ORDER BY [ApplicationUser].[id] ASC) AS row_num
		FROM [dbo].[ApplicationUser]
		WHERE  1=1 ' + @main_query_filter + '
	) AS Sub
	WHERE row_num BETWEEN (@page_number - 1) * @page_size + 1 AND @page_number * @page_size;
	'

	EXEC sp_executesql @main_query, N'@page_number int, @page_size int, @search_target nvarchar(20), @search_keyword nvarchar(50)', @page_number, @page_size, @search_target, @search_keyword;
	EXEC sp_executesql @count_query, N'@search_target nvarchar(20), @search_keyword nvarchar(50)', @search_target, @search_keyword;
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
	,@search_target nvarchar(20)
	,@search_keyword nvarchar(50)
AS	
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @main_query NVARCHAR(MAX);
	DECLARE @count_query NVARCHAR(MAX);
	DECLARE @main_query_filter NVARCHAR(MAX) = '';

    -- Insert statements for procedure here
	SET @count_query = 
	'
	SELECT COUNT(*) AS total_row_count 
	FROM [dbo].[Post]
	INNER JOIN [dbo].[ApplicationUser] ON [Post].[created_uid]=[dbo].[ApplicationUser].[id]
	WHERE [Post].[is_deleted] = 0
	';

	IF @search_target = 'id' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = + ' AND [Post].[id] = TRY_CONVERT(INT, @search_keyword)';
        SET @count_query = @count_query + ' AND [Post].[id] = TRY_CONVERT(INT, @search_keyword);';
    END;

	IF @search_target = 'nickname' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = ' AND nickname LIKE ''%' + @search_keyword + '%''';
        SET @count_query = @count_query + ' AND nickname LIKE ''%' + @search_keyword + '%'';';
    END;

	SET @main_query =
	'
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
			  ,[ApplicationUser].[nickname]
			  ,[Board].[board_name]
			  ,[Board].[board_name_eng]
			  ,ROW_NUMBER() OVER (ORDER BY [Post].[id] DESC) AS row_num
		  FROM [dbo].[Post]
		  INNER JOIN [dbo].[Board] ON Post.board_id=[dbo].[Board].[id]
		  INNER JOIN [dbo].[ApplicationUser] ON [Post].[created_uid]=[dbo].[ApplicationUser].[id]
		  WHERE [Post].[is_deleted] = 0' + @main_query_filter + '
	) AS Sub
	WHERE row_num BETWEEN (@page_number - 1) * @page_size + 1 AND @page_number * @page_size;
	';

	EXEC sp_executesql @main_query, N'@page_number int, @page_size int, @search_target nvarchar(20), @search_keyword nvarchar(50)', @page_number, @page_size, @search_target, @search_keyword;
	EXEC sp_executesql @count_query, N'@search_target nvarchar(20), @search_keyword nvarchar(50)', @search_target, @search_keyword;

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
	,@page_size int = 2
	,@search_target nvarchar(20)
	,@search_keyword nvarchar(50)
AS	
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @main_query NVARCHAR(MAX);
	DECLARE @count_query NVARCHAR(MAX);
	DECLARE @main_query_filter NVARCHAR(MAX) = '';
    -- Insert statements for procedure here
	SET @count_query = 
	'
	SELECT COUNT(*) AS total_row_count 
	FROM [dbo].[Post]
	INNER JOIN [dbo].[ApplicationUser] ON [Post].[created_uid]=[dbo].[ApplicationUser].[id]
	WHERE [like_count] >= 5 AND [Post].[is_deleted] = 0
	'

	IF @search_target = 'id' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = ' AND [Post].[id] = TRY_CONVERT(INT, @search_keyword)';
        SET @count_query = @count_query + ' AND [Post].[id] = TRY_CONVERT(INT, @search_keyword)';
    END

	IF @search_target = 'nickname' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = ' AND nickname LIKE ''%' + @search_keyword + '%''';
        SET @count_query = @count_query + ' AND nickname LIKE ''%' + @search_keyword + '%''';
    END

	SET @main_query =
	'
	SELECT * FROM
	(
		SELECT [Post].[id]
			  ,[Post].[board_id]
			  ,[Post].[subject]
			  ,[Post].[comment_count]
			  ,[Post].[view_count]
			  ,[Post].[like_count]
			  ,[Post].[created_time]
			  ,[Post].[created_uid]
			  ,[ApplicationUser].[nickname]
			  ,[Board].[board_name]
			  ,[Board].[board_name_eng]
			  ,ROW_NUMBER() OVER (ORDER BY [Post].[id] DESC) AS row_num
		  FROM [dbo].[Post]
		  INNER JOIN [dbo].[Board] ON [Post].[board_id]=[dbo].[Board].[id]
		  INNER JOIN [dbo].[ApplicationUser] ON [Post].[created_uid]=[dbo].[ApplicationUser].[id]
		  WHERE [like_count] >= 5 AND [Post].[is_deleted] = 0' + @main_query_filter +'
	) AS Sub
	WHERE row_num BETWEEN (@page_number - 1) * @page_size + 1 AND @page_number * @page_size
	'

	EXEC sp_executesql @main_query, N'@page_number int, @page_size int, @search_target nvarchar(20), @search_keyword nvarchar(50)', @page_number, @page_size, @search_target, @search_keyword;
	EXEC sp_executesql @count_query, N'@search_target nvarchar(20), @search_keyword nvarchar(50)', @search_target, @search_keyword;
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
		  ,[ApplicationUser].[nickname]
		  ,[Board].[board_name]
		  ,[Board].[board_name_eng]
	  FROM [dbo].[Post]
	  INNER JOIN [dbo].[Board] ON Post.board_id=[dbo].[Board].id
	  INNER JOIN [dbo].[ApplicationUser] ON Post.created_uid=[dbo].[ApplicationUser].id
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
	,@page_size int = 2
	,@search_target nvarchar(20)
	,@search_keyword nvarchar(50)
AS
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @main_query NVARCHAR(MAX);
	DECLARE @count_query NVARCHAR(MAX);
	DECLARE @main_query_filter NVARCHAR(MAX) = '';

	SET @count_query = 
	'
	SELECT COUNT(*) AS total_row_count 
	FROM [dbo].[Post]
	INNER JOIN [dbo].[ApplicationUser] ON [Post].[created_uid]=[dbo].[ApplicationUser].[id]
	WHERE board_id = @board_id AND [Post].[is_deleted] = 0
	'

	IF @search_target = 'id' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = ' AND [Post].[id] = TRY_CONVERT(INT, @search_keyword)';
        SET @count_query = @count_query + ' AND [Post].[id] = TRY_CONVERT(INT, @search_keyword)';
    END

	IF @search_target = 'nickname' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = ' AND [ApplicationUser].[nickname] LIKE ''%' + @search_keyword + '%''';
        SET @count_query = @count_query + ' AND [ApplicationUser].[nickname] LIKE ''%' + @search_keyword + '%''';
    END

    -- Insert statements for procedure here
	SET @main_query =
	'
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
			  ,[ApplicationUser].[nickname]
			  ,[Board].[board_name]
			  ,[Board].[board_name_eng]
			  ,ROW_NUMBER() OVER (ORDER BY [Post].[id] DESC) AS row_num
			  FROM [dbo].[Post]
			  INNER JOIN [dbo].[Board] ON Post.board_id=[dbo].[Board].id
			  INNER JOIN [dbo].[ApplicationUser] ON Post.created_uid=[dbo].[ApplicationUser].id
			  WHERE [board_id] = @board_id AND [Post].[is_deleted] = 0' + @main_query_filter + '
	) AS Sub
	WHERE row_num BETWEEN (@page_number - 1) * @page_size + 1 AND @page_number * @page_size
	'

	EXEC sp_executesql @main_query, N'@board_id int, @page_number int, @page_size int, @search_target nvarchar(20), @search_keyword nvarchar(50)', @board_id, @page_number, @page_size, @search_target, @search_keyword;
	EXEC sp_executesql @count_query, N'@board_id int, @search_target nvarchar(20), @search_keyword nvarchar(50)', @board_id, @search_target, @search_keyword;
	  
END
GO

USE [Copycat]
GO

/****** Object:  StoredProcedure [dbo].[spSelectPostsByBoardId]    Script Date: 2024-03-30 ¿ÀÀü 1:42:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		kkh
-- Create date: 2024-03-30
-- Description:	select all posts by user id
-- =============================================
ALTER PROCEDURE [dbo].[spSelectPostsByUserId]
	-- Add the parameters for the stored procedure here
	@user_id int
	,@page_number int = 1
	,@page_size int = 2
	,@search_target nvarchar(20)
	,@search_keyword nvarchar(50)
AS
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @main_query NVARCHAR(MAX);
	DECLARE @count_query NVARCHAR(MAX);
	DECLARE @main_query_filter NVARCHAR(MAX) = '';

	SET @count_query = 
	'
	SELECT COUNT(*) AS total_row_count 
	FROM [dbo].[Post]
	WHERE created_uid = @user_id 
	'

	IF @search_target = 'id' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = ' AND [Post].[id] = TRY_CONVERT(INT, @search_keyword)';
        SET @count_query = @count_query + ' AND [Post].[id] = TRY_CONVERT(INT, @search_keyword)';
    END

	IF @search_target = 'subject' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = ' AND subject LIKE ''%' + @search_keyword + '%''';
        SET @count_query = @count_query + ' AND subject LIKE ''%' + @search_keyword + '%''';
    END

    -- Insert statements for procedure here
	SET @main_query =
	'
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
			  ,[ApplicationUser].[nickname]
			  ,[Board].[board_name]
			  ,[Board].[board_name_eng]
			  ,ROW_NUMBER() OVER (ORDER BY [Post].[id] DESC) AS row_num
			  FROM [dbo].[Post]
			  INNER JOIN [dbo].[Board] ON Post.board_id=[dbo].[Board].id
			  INNER JOIN [dbo].[ApplicationUser] ON Post.created_uid=[dbo].[ApplicationUser].id
			  WHERE [created_uid] = @user_id' + @main_query_filter + '
	) AS Sub
	WHERE row_num BETWEEN (@page_number - 1) * @page_size + 1 AND @page_number * @page_size
	'

	EXEC sp_executesql @main_query, N'@user_id int, @page_number int, @page_size int, @search_target nvarchar(20), @search_keyword nvarchar(50)', @user_id, @page_number, @page_size, @search_target, @search_keyword;
	EXEC sp_executesql @count_query, N'@user_id int, @search_target nvarchar(20), @search_keyword nvarchar(50)', @user_id, @search_target, @search_keyword;
	  
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
		,[is_deleted]
FROM Comment
		INNER JOIN [dbo].[ApplicationUser] ON Comment.created_uid=[dbo].[ApplicationUser].id
WHERE post_id = @post_id
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
-- Create date: 2024-03-30
-- Description:	select all comments by user id
-- =============================================
ALTER PROCEDURE [dbo].[spSelectCommentListByUserId] 
	-- Add the parameters for the stored procedure here
	@user_id int
	,@page_number int = 1
	,@page_size int = 2
	,@search_target nvarchar(20)
	,@search_keyword nvarchar(50)
AS
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @main_query NVARCHAR(MAX);
	DECLARE @count_query NVARCHAR(MAX);
	DECLARE @main_query_filter NVARCHAR(MAX) = '';

	SET @count_query = 
	'
	SELECT COUNT(*) AS total_row_count FROM Comment WHERE created_uid = @user_id
	'

	IF @search_target = 'id' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = ' AND [Comment].[id] = TRY_CONVERT(INT, @search_keyword)';
        SET @count_query = @count_query + ' AND [Comment].[id] = TRY_CONVERT(INT, @search_keyword)';
    END

	IF @search_target = 'comment' AND @search_keyword IS NOT NULL
    BEGIN
        SET @main_query_filter = ' AND comment LIKE ''%' + @search_keyword + '%''';
        SET @count_query = @count_query + ' AND comment LIKE ''%' + @search_keyword + '%''';
    END

    -- Insert statements for procedure here
	SET @main_query =
	'
	SELECT * FROM
	(
		SELECT [Comment].[id]
				,[post_id]
				,[comment]
				,[like_count]
				,[created_time]
				,[created_uid]
				,[parent_comment_id]
				,[nickname]
				,[is_deleted]
			    ,ROW_NUMBER() OVER (ORDER BY [Comment].[id] DESC) AS row_num
		FROM Comment
				INNER JOIN [dbo].[ApplicationUser] ON Comment.created_uid=[dbo].[ApplicationUser].id
		WHERE created_uid = @user_id' + @main_query_filter +' 
	) AS Sub
	WHERE row_num BETWEEN (@page_number - 1) * @page_size + 1 AND @page_number * @page_size;
	'
	
	EXEC sp_executesql @main_query, N'@user_id int, @page_number int, @page_size int, @search_target nvarchar(20), @search_keyword nvarchar(50)', @user_id, @page_number, @page_size, @search_target, @search_keyword;
	EXEC sp_executesql @count_query, N'@user_id int, @search_target nvarchar(20), @search_keyword nvarchar(50)', @user_id, @search_target, @search_keyword;
END
GO


-- =============================================
-- Author:		kkh
-- Create date: 2024-03-27
-- Description:	delete comment
-- =============================================
ALTER PROCEDURE [dbo].[spDeleteComment]
	-- Add the parameters for the stored procedure here
	@comment_id int
AS
BEGIN
	-- BEGIN NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [dbo].[Comment] 
SET [is_deleted] = 'true' 
WHERE [id] = @comment_id

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



-- =============================================
-- Author:		kkh
-- Create date: 2024-04-04
-- Description:	insert user
-- =============================================
ALTER PROCEDURE [dbo].[spInsertUser] 
	@login_id nchar(20),
	@password nvarchar(50),
	@password_salt nvarchar(50),
	@nickname nchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @new_user_id int;
	DECLARE @new_role_id int; 

	INSERT INTO [dbo].[ApplicationUser]
			([login_id]
			 ,[password]
			 ,[password_salt]
			 ,[nickname]
			)
    VALUES
           (@login_id
			,@password
			,@password_salt
			,@nickname)

	SET @new_user_id = SCOPE_IDENTITY();

	INSERT INTO [dbo].[Role]
		([role_name])
	VALUES 
		('Member');

	SET @new_user_id = SCOPE_IDENTITY();


	INSERT INTO [dbo].[ApplicationUser_Role]
		([user_id], [role_id])
	VALUES 
		(@new_user_id, @new_role_id);
END
GO
