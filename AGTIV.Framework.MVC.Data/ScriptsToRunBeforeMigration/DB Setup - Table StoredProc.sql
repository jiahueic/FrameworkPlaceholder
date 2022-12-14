/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 17-Aug-20 5:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[Split]
 (@List varchar(MAX))

RETURNS @Results table
 (Item varchar(MAX))

AS

begin
 declare @IndexStart int
 declare @IndexEnd int
 declare @Length  int
 declare @Delim  char(1)
 declare @Word  varchar(1000)

 set @IndexStart = 1 
 set @IndexEnd = 0

 set @Length = len(@List) 
 set @Delim = ','
 
 while @IndexStart <= @Length
      begin
  
  set @IndexEnd = charindex(@Delim, @List, @IndexStart)
  
  if @IndexEnd = 0
   set @IndexEnd = @Length + 1
  
  set @Word = substring(@List, @IndexStart, @IndexEnd - @IndexStart)
  
  set @IndexStart = @IndexEnd + 1
  
  INSERT INTO @Results
   SELECT @Word
      end
 
 return
end
GO
/****** Object:  Table [dbo].[d_tblAction]    Script Date: 17-Aug-20 5:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[d_tblAction](
	[ActionID] [int] IDENTITY(1,1) NOT NULL,
	[ActionName] [varchar](50) NULL,
	[CurStepID] [int] NULL,
	[NextStepID] [int] NULL,
	[MinSlot] [int] NULL,
	[ProcessID] [int] NULL,
 CONSTRAINT [PK_d_tblAction_ActionID] PRIMARY KEY CLUSTERED 
(
	[ActionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[d_tblProcess]    Script Date: 17-Aug-20 5:59:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[d_tblProcess](
	[ProcessID] [int] IDENTITY(1,1) NOT NULL,
	[ProcessName] [varchar](50) NOT NULL,
	[EncryptParam] [bit] NULL,
 CONSTRAINT [PK_d_tblProcess_ProcessID] PRIMARY KEY CLUSTERED 
(
	[ProcessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[d_tblStep]    Script Date: 17-Aug-20 5:59:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[d_tblStep](
	[StepID] [int] IDENTITY(1,1) NOT NULL,
	[StepName] [varchar](50) NOT NULL,
	[InternalStepName] [varchar](50) NOT NULL,
	[StepOrder] [int] NOT NULL,
	[DueDateDay] [int] NOT NULL,
	[EmailNotification] [bit] NOT NULL,
	[EmailToAssignee] [bit] NOT NULL,
	[EmailToOriginator] [bit] NOT NULL,
	[EmailCCOriginator] [bit] NOT NULL,
	[EmailNotificationSubject] [text] NOT NULL,
	[EmailNotificationBody] [text] NOT NULL,
	[TaskURL] [text] NOT NULL,
	[LastStep] [bit] NOT NULL,
	[EmailOnlyStep] [bit] NULL,
	[CodeOnlyStep] [bit] NULL,
	[AssemblyName] [varchar](200) NULL,
	[ClassName] [varchar](200) NULL,
	[MethodName] [varchar](200) NULL,
	[ProcessID] [int] NULL,
 CONSTRAINT [PK_d_tblStep_StepID] PRIMARY KEY CLUSTERED 
(
	[StepID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ELMAH_Error]    Script Date: 17-Aug-20 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELMAH_Error](
	[ErrorId] [uniqueidentifier] NOT NULL,
	[Application] [nvarchar](60) NOT NULL,
	[Host] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](60) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[TimeUtc] [datetime] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL,
	[AllXml] [ntext] NOT NULL,
 CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[i_tblAction]    Script Date: 17-Aug-20 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[i_tblAction](
	[ActionID] [int] IDENTITY(1,1) NOT NULL,
	[ActionName] [varchar](50) NOT NULL,
	[CurStepTemplateID] [int] NOT NULL,
	[NextStepTemplateID] [int] NOT NULL,
	[MinSlot] [int] NOT NULL,
	[ProcessID] [int] NOT NULL,
	[SlotCounter] [int] NOT NULL,
 CONSTRAINT [PK_i_tblActionp_ActionID] PRIMARY KEY CLUSTERED 
(
	[ActionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[i_tblActioner]    Script Date: 17-Aug-20 5:59:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[i_tblActioner](
	[ActionerID] [int] IDENTITY(1,1) NOT NULL,
	[ActionerLogin] [varchar](50) NOT NULL,
	[ActionerName] [varchar](50) NOT NULL,
	[ActionerEmail] [varchar](100) NOT NULL,
	[StepTemplateID] [int] NOT NULL,
	[ProcessID] [int] NULL,
 CONSTRAINT [PK_i_tblActioner_ActionerID] PRIMARY KEY CLUSTERED 
(
	[ActionerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[i_tblDelegation]    Script Date: 17-Aug-20 5:59:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[i_tblDelegation](
	[DelegationID] [int] IDENTITY(1,1) NOT NULL,
	[DelegationFromLogin] [varchar](50) NOT NULL,
	[DelegationFromName] [varchar](50) NULL,
	[DelegationFromEmail] [varchar](100) NULL,
	[DelegationToLogin] [varchar](50) NOT NULL,
	[DelegationToName] [varchar](50) NULL,
	[DelegationToEmail] [varchar](100) NULL,
	[DelegationStartDate] [datetime] NOT NULL,
	[DelegationEndDate] [datetime] NOT NULL,
	[ProcessTemplateID] [int] NULL,
	[ApplicationName] [varchar](100) NULL,
	[Active] [bit] NOT NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_i_tblDelegation_DelegationID] PRIMARY KEY CLUSTERED 
(
	[DelegationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[i_tblProcess]    Script Date: 17-Aug-20 5:59:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[i_tblProcess](
	[ProcessID] [int] IDENTITY(1,1) NOT NULL,
	[ProcessTemplateID] [int] NULL,
	[ProcessName] [varchar](50) NOT NULL,
	[ApplicationName] [varchar](100) NULL,
	[ReferenceKey] [varchar](50) NOT NULL,
	[OriginatorLogin] [varchar](50) NOT NULL,
	[OriginatorName] [varchar](50) NOT NULL,
	[OriginatorEmail] [varchar](100) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[CompletionDate] [datetime] NULL,
	[CurStepTemplateID] [int] NOT NULL,
	[EmailHost] [varchar](50) NULL,
	[EmailPort] [int] NULL,
	[EmailFrom] [varchar](50) NULL,
	[KeywordsXML] [text] NULL,
	[EncryptParam] [bit] NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PL_i_tblProcess_ProcessID] PRIMARY KEY CLUSTERED 
(
	[ProcessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[i_tblStep]    Script Date: 17-Aug-20 5:59:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[i_tblStep](
	[StepID] [int] IDENTITY(1,1) NOT NULL,
	[StepName] [varchar](50) NOT NULL,
	[InternalStepName] [varchar](50) NOT NULL,
	[StepOrder] [int] NOT NULL,
	[DueDateDay] [int] NOT NULL,
	[EmailNotification] [bit] NOT NULL,
	[EmailToAssignee] [bit] NOT NULL,
	[EmailToOriginator] [bit] NOT NULL,
	[EmailCCOriginator] [bit] NOT NULL,
	[EmailAdditionalTo] [text] NULL,
	[EmailAdditionalCC] [text] NULL,
	[EmailNotificationSubject] [text] NOT NULL,
	[EmailNotificationBody] [text] NOT NULL,
	[EmailNotificationSubjectTemplate] [text] NULL,
	[EmailNotificationBodyTemplate] [text] NULL,
	[TaskURL] [text] NOT NULL,
	[TaskURLTemplate] [text] NULL,
	[LastStep] [bit] NOT NULL,
	[EmailOnlyStep] [bit] NULL,
	[CodeOnlyStep] [bit] NULL,
	[AssemblyName] [varchar](200) NULL,
	[ClassName] [varchar](200) NULL,
	[MethodName] [varchar](200) NULL,
	[ProcessID] [int] NOT NULL,
	[StepTemplateID] [int] NOT NULL,
 CONSTRAINT [PL_i_tblStep_StepID] PRIMARY KEY CLUSTERED 
(
	[StepID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[i_tblTask]    Script Date: 17-Aug-20 5:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[i_tblTask](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[AssigneeLogin] [varchar](50) NOT NULL,
	[AssigneeName] [varchar](50) NOT NULL,
	[AssigneeEmail] [varchar](100) NOT NULL,
	[AssignedDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[ActionedDate] [datetime] NULL,
	[ActionID] [int] NULL,
	[StepTemplateID] [int] NOT NULL,
	[ProcessID] [int] NOT NULL,
	[ActionedBy] [varchar](50) NULL,
	[ActionedByName] [varchar](50) NULL,
	[Status] [varchar](20) NULL,
	[Comments] [text] NULL,
	[ExtendedDays] [float] NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [varchar](50) NULL,
	[EmailSent] [bit] NULL,
 CONSTRAINT [PL_i_tblTask_TaskID] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[i_tblWorkflowLog]    Script Date: 17-Aug-20 5:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[i_tblWorkflowLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LogType] [varchar](50) NULL,
	[LogDescription] [text] NULL,
	[LogDate] [datetime] NULL,
	[LogBy] [varchar](50) NULL,
	[LogByName] [varchar](50) NULL,
	[ProcessID] [int] NULL,
 CONSTRAINT [PL_i_tblWorkflowLog_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [DF_ELMAH_Error_ErrorId]  DEFAULT (newid()) FOR [ErrorId]
GO
ALTER TABLE [dbo].[i_tblAction] ADD  CONSTRAINT [DF_i_tblAction_SlotCounter]  DEFAULT ((0)) FOR [SlotCounter]
GO
ALTER TABLE [dbo].[i_tblTask] ADD  CONSTRAINT [DF_i_tblTask_ExtendedDays]  DEFAULT ((0)) FOR [ExtendedDays]
GO
/****** Object:  StoredProcedure [dbo].[AG_ELMAH_GetErrors]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[AG_ELMAH_GetErrors]
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON
	declare @num1 int = (SELECT COUNT(*) FROM ELMAH_Error);
	declare @num2 int = @PageSize * (@PageIndex + 1);
    if (@num2 > @num1)
    begin
        set @num2 = @num1;
        set @PageSize = @num1 - @PageSize * (@num1 / @PageSize);
    end

    declare @sql nvarchar(max) = 'SELECT e.* FROM ( ' + 
    + 'SELECT TOP ' +
    + Convert(nvarchar(50), @PageSize) +
    + ' TimeUtc, ErrorId FROM (' +
    + 'SELECT TOP ' +
    + Convert(nvarchar(50), @num2) + 
    + ' TimeUtc, ErrorId FROM ELMAH_Error ' +
    + 'ORDER BY TimeUtc DESC, ErrorId DESC) x ' +
    + 'ORDER BY TimeUtc ASC, ErrorId ASC) AS i ' +
    + 'INNER JOIN Elmah_Error AS e ON i.ErrorId = e.ErrorId ' +
    + 'ORDER BY e.TimeUtc DESC, e.ErrorId DESC'

	print(@sql)
	exec(@sql)
	set @TotalCount = @num1
GO
/****** Object:  StoredProcedure [dbo].[CompleteAllTasks]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CompleteAllTasks]
	-- Add the parameters for the stored procedure here	
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    UPDATE i_tblTask
    SET [STATUS] = 'Completed'
    WHERE ProcessID = @ProcessID
    AND STATUS NOT IN ('Completed','Reassigned','Removed')
END
GO
/****** Object:  StoredProcedure [dbo].[CompleteOtherTasks]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CompleteOtherTasks]
	-- Add the parameters for the stored procedure here
	@TaskID int,
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    UPDATE i_tblTask
    SET [STATUS] = 'Completed'
    WHERE StepTemplateID IN
		(SELECT StepTemplateID FROM i_tblTask WHERE TaskID = @TaskID)
	AND ProcessID = @ProcessID
	AND STATUS NOT IN ('Completed','Reassigned','Removed')
END
GO
/****** Object:  StoredProcedure [dbo].[CompleteTask]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CompleteTask]
	-- Add the parameters for the stored procedure here
	@ActionedDate datetime,
	@ActionID int,
	@TaskID int,
	@ActionedBy varchar(50),
	@ActionedByName varchar(50),
	@Comments text,
	@LastUpdated datetime,
	@LastUpdatedBy varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    --Complete Task
	UPDATE i_tblTask 
	SET ActionedDate =@ActionedDate,
	ActionID =@ActionID,
	ActionedBy = @ActionedBy,
	ActionedByName = @ActionedByName,
	Status = 'Completed',
	Comments = @Comments,
	LastUpdated = @LastUpdated,
	LastUpdatedBy = @LastUpdatedBy
	WHERE TaskID = @TaskID
	AND [STATUS] = 'In Progress'
	
	--Increment Counter
	UPDATE i_tblAction
	SET SlotCounter = SlotCounter + 1
	WHERE ActionID = @ActionID
	
	--return result to indicate whether min slot has met
	SELECT CASE WHEN SlotCounter >= MinSlot THEN 1 else 0 END as COMPLETED
	FROM i_tblAction
	WHERE ActionID = @ActionID
END
GO
/****** Object:  StoredProcedure [dbo].[CreateTaskForStepTemplate]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateTaskForStepTemplate]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@StepTemplateID int,
	@LastUpdated datetime,
	@LastUpdatedBy varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @AssigneeLogin varchar(50)
	declare @AssigneeName varchar(50)
	declare @AssigneeEmail varchar(100)
	declare @AssignedDate datetime	
	declare @DueDate datetime
	declare @DueDateDay int
	
	set @AssignedDate = GETDATE()	
	set @DueDateDay = (SELECT DueDateDay FROM i_tblStep WHERE ProcessID=@ProcessID AND StepTemplateID=@StepTemplateID)
	set @DueDate = DateAdd(DAY,@DueDateDay,@AssignedDate)

	DECLARE @cursor CURSOR
	SET @cursor = CURSOR FOR
		SELECT ActionerName, ActionerLogin, ActionerEmail FROM i_tblActioner
		WHERE ProcessID=@ProcessID AND StepTemplateID=@StepTemplateID
		
		OPEN @cursor
		FETCH NEXT
		FROM @cursor INTO @AssigneeName, @AssigneeLogin, @AssigneeEmail
		
		WHILE @@FETCH_STATUS = 0
		BEGIN
		
		--perform insert here
		INSERT INTO i_tblTask(AssigneeName,AssigneeLogin,AssigneeEmail,AssignedDate,DueDate,ProcessID,StepTemplateID,Status,Created,CreatedBy,LastUpdated,LastUpdatedBy)
		VALUES (@AssigneeName,@AssigneeLogin,@AssigneeEmail,@AssignedDate,@DueDate,@ProcessID,@StepTemplateID,'In Progress',@LastUpdated,@LastUpdatedBy,@LastUpdated,@LastUpdatedBy)
		
		
		FETCH NEXT
		FROM @cursor INTO @AssigneeName, @AssigneeLogin
		END
	CLOSE @cursor
	DEALLOCATE @cursor

    -- Insert statements for procedure here
	
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteActionerInstanceForStep]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteActionerInstanceForStep]
	@StepTemplateID int,
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--DELETE FIRST THEN INSERT
	DELETE FROM i_tblActioner 
	WHERE StepTemplateID = @StepTemplateID
	AND ProcessID = @ProcessID
	    
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteDelegationInstance]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteDelegationInstance]
	-- Add the parameters for the stored procedure here
	@DelegationID int,	
	@LastUpdated datetime,
	@LastUpdatedBy varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE i_tblDelegation
	SET Active=0,
	LastUpdated=@LastUpdated,
	LastUpdatedBy=@LastUpdatedBy
	WHERE DelegationID=@DelegationID
END
GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorsXml]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON

    DECLARE @FirstTimeUTC DATETIME
    DECLARE @FirstSequence INT
    DECLARE @StartRow INT
    DECLARE @StartRowIndex INT

    SELECT 
        @TotalCount = COUNT(1) 
    FROM 
        [ELMAH_Error]
    WHERE 
        [Application] = @Application

    -- Get the ID of the first error for the requested page

    SET @StartRowIndex = @PageIndex * @PageSize + 1

    IF @StartRowIndex <= @TotalCount
    BEGIN

        SET ROWCOUNT @StartRowIndex

        SELECT  
            @FirstTimeUTC = [TimeUtc],
            @FirstSequence = [Sequence]
        FROM 
            [ELMAH_Error]
        WHERE   
            [Application] = @Application
        ORDER BY 
            [TimeUtc] DESC, 
            [Sequence] DESC

    END
    ELSE
    BEGIN

        SET @PageSize = 0

    END

    -- Now set the row count to the requested page size and get
    -- all records below it for the pertaining application.

    SET ROWCOUNT @PageSize

    SELECT 
        errorId     = [ErrorId], 
        application = [Application],
        host        = [Host], 
        type        = [Type],
        source      = [Source],
        message     = [Message],
        [user]      = [User],
        statusCode  = [StatusCode], 
        time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
    FROM 
        [ELMAH_Error] error
    WHERE
        [Application] = @Application
    AND
        [TimeUtc] <= @FirstTimeUTC
    AND 
        [Sequence] <= @FirstSequence
    ORDER BY
        [TimeUtc] DESC, 
        [Sequence] DESC
    FOR
        XML AUTO

GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorXml]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

    SET NOCOUNT ON

    SELECT 
        [AllXml]
    FROM 
        [ELMAH_Error]
    WHERE
        [ErrorId] = @ErrorId
    AND
        [Application] = @Application

GO
/****** Object:  StoredProcedure [dbo].[ELMAH_LogError]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_LogError]
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NTEXT,
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

    SET NOCOUNT ON

    INSERT
    INTO
        [ELMAH_Error]
        (
            [ErrorId],
            [Application],
            [Host],
            [Type],
            [Source],
            [Message],
            [User],
            [AllXml],
            [StatusCode],
            [TimeUtc]
        )
    VALUES
        (
            @ErrorId,
            @Application,
            @Host,
            @Type,
            @Source,
            @Message,
            @User,
            @AllXml,
            @StatusCode,
            @TimeUtc
        )

GO
/****** Object:  StoredProcedure [dbo].[GetActionInstanceByID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetActionInstanceByID]
	-- Add the parameters for the stored procedure here
	@ActionID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM i_tblAction
	WHERE ActionID=@ActionID
END
GO
/****** Object:  StoredProcedure [dbo].[GetActions]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetActions]
	-- Add the parameters for the stored procedure here
	@TaskID int, 
	@ProcessID int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM i_tblAction
	WHERE ProcessID= @ProcessID AND
	CurStepTemplateID IN
	(SELECT StepTemplateID FROM i_tblTask WHERE TaskID=@TaskID)
END
GO
/****** Object:  StoredProcedure [dbo].[GetActionTemplateByInstanceProcessID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetActionTemplateByInstanceProcessID]
	-- Add the parameters for the stored procedure here
	@InstanceProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	SELECT * 
	FROM d_tblAction 
	WHERE ProcessID = 
		(SELECT ProcessTemplateID 
		 FROM i_tblProcess 
		 WHERE ProcessID = @InstanceProcessID)
END
GO
/****** Object:  StoredProcedure [dbo].[GetActionTemplateByProcessID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetActionTemplateByProcessID]
	-- Add the parameters for the stored procedure here
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from d_tblAction
	Where ProcessID=@ProcessID	
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllDelegateesForUsers]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllDelegateesForUsers]
	-- Add the parameters for the stored procedure here
	@CommaSeparatedLogin varchar(5000)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT i_tblDelegation.*, d_tblProcess.ProcessName FROM i_tblDelegation left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	WHERE DelegationFromLogin IN (Select * FROM Split(@CommaSeparatedLogin))
	AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate
	AND Active='true'
	ORDER by DelegationStartDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllDelegateesForUsersByApplicationName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllDelegateesForUsersByApplicationName]
	-- Add the parameters for the stored procedure here
	@CommaSeparatedLogin varchar(5000),	
	@ApplicationName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT i_tblDelegation.*, d_tblProcess.ProcessName FROM i_tblDelegation left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	WHERE DelegationFromLogin IN (Select * FROM Split(@CommaSeparatedLogin))
	AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate
	AND ApplicationName = @ApplicationName
	AND Active='true'
	ORDER by DelegationStartDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllDelegations]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllDelegations]
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT i_tblDelegation.*, d_tblProcess.ProcessName FROM i_tblDelegation left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	where DelegationEndDate >= DateAdd(M,-3,GETDATE())
	AND Active = 'true'
	order by DelegationStartDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllDelegationsByApplicationName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllDelegationsByApplicationName]
	-- Add the parameters for the stored procedure here	
	@ApplicationName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT i_tblDelegation.*, d_tblProcess.ProcessName FROM i_tblDelegation left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	where DelegationEndDate >= DateAdd(M,-3,GETDATE())
	AND Active = 'true'
	AND ApplicationName = @ApplicationName
	order by DelegationStartDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllDelegationsByBatchByProcessNameApplicationName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllDelegationsByBatchByProcessNameApplicationName]
	-- Add the parameters for the stored procedure here	
	@ProcessName nvarchar(250),
	@ApplicationName varchar(100),
	@PageNumber int,
	@RowsPerPage int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

WITH tblDelegations AS
(
SELECT *, ROW_NUMBER() OVER(
ORDER BY DelegationStartDate) AS RowNumber
from (
    -- Insert statements for procedure here
	SELECT i_tblDelegation.*, d_tblProcess.ProcessName FROM i_tblDelegation left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	where DelegationEndDate >= DateAdd(M,-3,GETDATE())
	AND Active = 'true'
	AND (ProcessName = @ProcessName OR @ProcessName = '')
	AND (ApplicationName = @ApplicationName OR @ApplicationName = '')
	)
PC
) 
 
SELECT *
FROM tblDelegations
WHERE RowNumber >= @PageNumber*@RowsPerPage+1 AND RowNumber <= (@PageNumber+1)*@RowsPerPage
ORDER BY DelegationStartDate
END





--------


GO
/****** Object:  StoredProcedure [dbo].[GetAllDelegationsCountByProcessNameApplicationName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllDelegationsCountByProcessNameApplicationName]
	-- Add the parameters for the stored procedure here	
	@ProcessName nvarchar(250),
	@ApplicationName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT COUNT(*) as Count
    FROM
    (
    -- Insert statements for procedure here
	SELECT i_tblDelegation.*, d_tblProcess.ProcessName FROM i_tblDelegation left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	where DelegationEndDate >= DateAdd(M,-3,GETDATE())
	AND Active = 'true'
	AND (ProcessName = @ProcessName OR @ProcessName = '')
	AND (ApplicationName = @ApplicationName OR @ApplicationName = '')
	) Delegations
END





--------


GO
/****** Object:  StoredProcedure [dbo].[GetAllMyDelegations]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllMyDelegations]
	-- Add the parameters for the stored procedure here
	@Login varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT i_tblDelegation.*, d_tblProcess.ProcessName FROM i_tblDelegation left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	WHERE (DelegationFromLogin = @Login OR DelegationToLogin = @Login )
	AND DelegationEndDate >= DateAdd(M,-3,GETDATE())
	AND Active='true'
	ORDER by DelegationStartDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllMyDelegationsByApplicationName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllMyDelegationsByApplicationName]
	-- Add the parameters for the stored procedure here
	@Login varchar(50),
	@ApplicationName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT i_tblDelegation.*, d_tblProcess.ProcessName FROM i_tblDelegation left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	WHERE (DelegationFromLogin = @Login OR DelegationToLogin = @Login )
	AND DelegationEndDate >= DateAdd(M,-3,GETDATE())
	AND Active='true'
	AND ApplicationName = @ApplicationName
	ORDER by DelegationStartDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllMyDelegationsByBatchByProcessNameApplicationName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllMyDelegationsByBatchByProcessNameApplicationName]
	-- Add the parameters for the stored procedure here
	@Login varchar(50),
	@ProcessName nvarchar(250),
	@ApplicationName varchar(100),
	@PageNumber int,
	@RowsPerPage int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
WITH tblDelegations AS
(
SELECT *, ROW_NUMBER() OVER(
ORDER BY DelegationStartDate) AS RowNumber
from (
	-- Insert statements for procedure here
	SELECT i_tblDelegation.*, d_tblProcess.ProcessName 
	FROM i_tblDelegation 
	left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	WHERE (DelegationFromLogin = @Login OR DelegationToLogin = @Login )
	AND DelegationEndDate >= DateAdd(M,-3,GETDATE())
	AND Active='true'
	AND (ProcessName = @ProcessName OR @ProcessName = '')
	AND (ApplicationName = @ApplicationName OR @ApplicationName = '')
	)
PC
) 
 
SELECT *
FROM tblDelegations
WHERE RowNumber >= @PageNumber*@RowsPerPage+1 AND RowNumber <= (@PageNumber+1)*@RowsPerPage
ORDER BY DelegationStartDate
END





--------


GO
/****** Object:  StoredProcedure [dbo].[GetAllMyDelegationsCountByProcessNameApplicationName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllMyDelegationsCountByProcessNameApplicationName]
	-- Add the parameters for the stored procedure here
	@ActionerLogin varchar(50),
	@ProcessName nvarchar(250),
	@ApplicationName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT COUNT(*) as Count
    FROM
    (
    -- Insert statements for procedure here
	SELECT i_tblDelegation.*, d_tblProcess.ProcessName 
	FROM i_tblDelegation 
	left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	WHERE (DelegationFromLogin = @ActionerLogin OR DelegationToLogin = @ActionerLogin )
	AND DelegationEndDate >= DateAdd(M,-3,GETDATE())
	AND Active='true'
	AND (ProcessName = @ProcessName OR @ProcessName = '')
	AND (ApplicationName = @ApplicationName OR @ApplicationName = '')
	) Delegations
END





--------


GO
/****** Object:  StoredProcedure [dbo].[GetAllMyPendingTasks]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllMyPendingTasks]
	-- Add the parameters for the stored procedure here
	@ActionerLogin varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT i_tblProcess.ProcessID as ProcessID, i_tblProcess.ProcessName as ProcessName,i_tblProcess.OriginatorLogin as OriginatorLogin, i_tblProcess.ReferenceKey as ReferenceKey,
    i_tblProcess.OriginatorName as OriginatorName, i_tblProcess.StartDate as ProcessStartDate, i_tblProcess.KeywordsXML as KeywordsXML, i_tblProcess.EncryptParam as EncryptParam,
    i_tblProcess.ApplicationName as ApplicationName,
    i_tblStep.StepName as StepName, 
    i_tblStep.TaskURL as TaskURL,
    Task.TaskID as TaskID, Task.AssigneeLogin as AssigneeLogin,Task.AssigneeName as AssigneeName, Task.AssignedDate as AssigneeDate, Task.DueDate as DueDate, Task.ExtendedDays as ExtendedDays
    FROM
    (
		SELECT i_tblTask.* FROM i_tblTask join i_tblProcess on i_tblTask.ProcessID = i_tblProcess.ProcessID
		WHERE 
		(
		AssigneeLogin = @ActionerLogin
		-- Delegation
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is not null
				AND i_tblDelegation.ProcessTemplateID = i_tblProcess.ProcessTemplateID
				AND i_tblDelegation.ApplicationName = i_tblProcess.ApplicationName
				)
		-- Delegation backward compatibility
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is null
				)
		)
		AND STATUS NOT IN ('Completed','Reassigned','Removed')
	) Task
	JOIN i_tblProcess ON Task.ProcessID = i_tblProcess.ProcessID
	JOIN i_tblStep ON Task.StepTemplateID = i_tblStep.StepTemplateID
					AND Task.ProcessID = i_tblStep.ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllMyPendingTasksByBatch]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllMyPendingTasksByBatch]
	-- Add the parameters for the stored procedure here
	@ActionerLogin varchar(50),
	@PageNumber int,
	@RowsPerPage int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

;WITH tblTasks AS
(
SELECT *, ROW_NUMBER() OVER(
ORDER BY DueDate) AS RowNumber
from (

    -- Insert statements for procedure here
    SELECT i_tblProcess.ProcessID as ProcessID, i_tblProcess.ProcessName as ProcessName,i_tblProcess.OriginatorLogin as OriginatorLogin, i_tblProcess.ReferenceKey as ReferenceKey,
    i_tblProcess.OriginatorName as OriginatorName, i_tblProcess.StartDate as ProcessStartDate, i_tblProcess.KeywordsXML as KeywordsXML, i_tblProcess.EncryptParam as EncryptParam,
    i_tblProcess.ApplicationName as ApplicationName,
    i_tblStep.StepName as StepName, 
    i_tblStep.TaskURL as TaskURL,
    Task.TaskID as TaskID, Task.AssigneeLogin as AssigneeLogin,Task.AssigneeName as AssigneeName, Task.AssignedDate as AssigneeDate, Task.DueDate as DueDate, Task.ExtendedDays as ExtendedDays
    FROM
    (
		SELECT i_tblTask.* FROM i_tblTask join i_tblProcess on i_tblTask.ProcessID = i_tblProcess.ProcessID
		WHERE 
		(
		AssigneeLogin = @ActionerLogin
		-- Delegation
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is not null
				AND i_tblDelegation.ProcessTemplateID = i_tblProcess.ProcessTemplateID
				AND i_tblDelegation.ApplicationName = i_tblProcess.ApplicationName
				)
		-- Delegation backward compatibility
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is null
				)
		)
		AND STATUS NOT IN ('Completed','Reassigned','Removed')
	) Task
	JOIN i_tblProcess ON Task.ProcessID = i_tblProcess.ProcessID
	JOIN i_tblStep ON Task.StepTemplateID = i_tblStep.StepTemplateID
					AND Task.ProcessID = i_tblStep.ProcessID
)
PC
) 
 
SELECT *
FROM tblTasks
WHERE RowNumber >= @PageNumber*@RowsPerPage+1 AND RowNumber <= (@PageNumber+1)*@RowsPerPage
ORDER BY DueDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllMyPendingTasksByBatchByProcessNameApplicationName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllMyPendingTasksByBatchByProcessNameApplicationName]
	-- Add the parameters for the stored procedure here
	@ActionerLogin varchar(50),
	@ProcessName nvarchar(250),
	@ApplicationName nvarchar(250),
	@PageNumber int,
	@RowsPerPage int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

WITH tblTasks AS
(
SELECT *, ROW_NUMBER() OVER(
ORDER BY DueDate) AS RowNumber
from (

    -- Insert statements for procedure here
    SELECT i_tblProcess.ProcessID as ProcessID, i_tblProcess.ProcessName as ProcessName,i_tblProcess.OriginatorLogin as OriginatorLogin, i_tblProcess.ReferenceKey as ReferenceKey,
    i_tblProcess.OriginatorName as OriginatorName, i_tblProcess.StartDate as ProcessStartDate, i_tblProcess.KeywordsXML as KeywordsXML, i_tblProcess.EncryptParam as EncryptParam,
    i_tblProcess.ApplicationName as ApplicationName,
    i_tblStep.StepName as StepName, 
    i_tblStep.TaskURL as TaskURL,
    Task.TaskID as TaskID, Task.AssigneeLogin as AssigneeLogin,Task.AssigneeName as AssigneeName, Task.AssignedDate as AssigneeDate, Task.DueDate as DueDate, Task.ExtendedDays as ExtendedDays
    FROM
    (
		SELECT i_tblTask.* FROM i_tblTask join i_tblProcess on i_tblTask.ProcessID = i_tblProcess.ProcessID
		WHERE 
		(
		AssigneeLogin = @ActionerLogin
		AND (ProcessName = @ProcessName OR @ProcessName = '')
		AND (ApplicationName = @ApplicationName OR @ApplicationName = '')
		-- Delegation
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is not null
				AND i_tblDelegation.ProcessTemplateID = i_tblProcess.ProcessTemplateID
				AND i_tblDelegation.ApplicationName = i_tblProcess.ApplicationName
				AND (i_tblProcess.ProcessName = @ProcessName OR @ProcessName = '')
				AND (i_tblProcess.ApplicationName = @ApplicationName OR @ApplicationName = '')
				)
		-- Delegation backward compatibility
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is null
				)
		)
		AND STATUS NOT IN ('Completed','Reassigned','Removed')
	) Task
	JOIN i_tblProcess ON Task.ProcessID = i_tblProcess.ProcessID
	JOIN i_tblStep ON Task.StepTemplateID = i_tblStep.StepTemplateID
					AND Task.ProcessID = i_tblStep.ProcessID
)
PC
) 
 
SELECT *
FROM tblTasks
WHERE RowNumber >= @PageNumber*@RowsPerPage+1 AND RowNumber <= (@PageNumber+1)*@RowsPerPage
ORDER BY DueDate
END



--------


GO
/****** Object:  StoredProcedure [dbo].[GetAllMyPendingTasksByProcessName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllMyPendingTasksByProcessName]
	-- Add the parameters for the stored procedure here
	@ActionerLogin varchar(50),
	@ProcessName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT i_tblProcess.ProcessID as ProcessID, i_tblProcess.ProcessName as ProcessName,i_tblProcess.OriginatorLogin as OriginatorLogin, i_tblProcess.ReferenceKey as ReferenceKey,
    i_tblProcess.OriginatorName as OriginatorName, i_tblProcess.StartDate as ProcessStartDate, i_tblProcess.KeywordsXML as KeywordsXML, i_tblProcess.EncryptParam as EncryptParam,
    i_tblProcess.ApplicationName as ApplicationName,
    i_tblStep.StepName as StepName, 
    i_tblStep.TaskURL as TaskURL,
    Task.TaskID as TaskID, Task.AssigneeLogin as AssigneeLogin,Task.AssigneeName as AssigneeName, Task.AssignedDate as AssigneeDate, Task.DueDate as DueDate, Task.ExtendedDays as ExtendedDays
    FROM
    (
		SELECT i_tblTask.* FROM i_tblTask join i_tblProcess on i_tblTask.ProcessID = i_tblProcess.ProcessID
		WHERE 
		(
		AssigneeLogin = @ActionerLogin
		-- Delegation
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is not null
				AND i_tblDelegation.ProcessTemplateID = i_tblProcess.ProcessTemplateID
				AND i_tblDelegation.ApplicationName = i_tblProcess.ApplicationName
				)
		-- Delegation backward compatibility
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is null
				)
		)
		AND STATUS NOT IN ('Completed','Reassigned','Removed')
	) Task
	JOIN i_tblProcess ON Task.ProcessID = i_tblProcess.ProcessID
	JOIN i_tblStep ON Task.StepTemplateID = i_tblStep.StepTemplateID
					AND Task.ProcessID = i_tblStep.ProcessID
	WHERE i_tblProcess.ProcessName = @ProcessName
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllMyPendingTasksCount]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllMyPendingTasksCount]
	-- Add the parameters for the stored procedure here
	@ActionerLogin varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
    SELECT COUNT(*) as Count
    FROM
    (
		SELECT i_tblTask.* FROM i_tblTask join i_tblProcess on i_tblTask.ProcessID = i_tblProcess.ProcessID
		WHERE 
		(
		AssigneeLogin = @ActionerLogin
		-- Delegation
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is not null
				AND i_tblDelegation.ProcessTemplateID = i_tblProcess.ProcessTemplateID
				AND i_tblDelegation.ApplicationName = i_tblProcess.ApplicationName
				)
		-- Delegation backward compatibility
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is null
				)
		)
		AND STATUS NOT IN ('Completed','Reassigned','Removed')
	) Task
	JOIN i_tblProcess ON Task.ProcessID = i_tblProcess.ProcessID
	JOIN i_tblStep ON Task.StepTemplateID = i_tblStep.StepTemplateID
					AND Task.ProcessID = i_tblStep.ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllMyPendingTasksCountByProcessNameApplicationName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllMyPendingTasksCountByProcessNameApplicationName]
	-- Add the parameters for the stored procedure here
	@ActionerLogin varchar(50),
	@ProcessName nvarchar(250),
	@ApplicationName nvarchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
    SELECT COUNT(*) as Count
    FROM
    (
		SELECT i_tblTask.* FROM i_tblTask join i_tblProcess on i_tblTask.ProcessID = i_tblProcess.ProcessID
		WHERE 
		(
		AssigneeLogin = @ActionerLogin
		AND (ProcessName = @ProcessName OR @ProcessName = '')
		AND (ApplicationName = @ApplicationName OR @ApplicationName = '')
		-- Delegation
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
			JOIN d_tblProcess ON i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is not null
				AND i_tblDelegation.ProcessTemplateID = i_tblProcess.ProcessTemplateID
				AND i_tblDelegation.ApplicationName = i_tblProcess.ApplicationName
				AND (d_tblProcess.ProcessName = @ProcessName OR @ProcessName = '')
				)
		-- Delegation backward compatibility
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@ActionerLogin
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is null
				)
		)
		AND STATUS NOT IN ('Completed','Reassigned','Removed')
	) Task
	JOIN i_tblProcess ON Task.ProcessID = i_tblProcess.ProcessID
	JOIN i_tblStep ON Task.StepTemplateID = i_tblStep.StepTemplateID
					AND Task.ProcessID = i_tblStep.ProcessID
END





--------


GO
/****** Object:  StoredProcedure [dbo].[GetAllPendingTasksByProcessName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllPendingTasksByProcessName]
	-- Add the parameters for the stored procedure here	
	@ProcessName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT * FROM i_tblTask join i_tblStep ON i_tblTask.StepTemplateID = i_tblStep.StepTemplateID AND i_tblTask.ProcessID = i_tblStep.ProcessID
		join i_tblProcess on i_tblStep.ProcessID = i_tblProcess.ProcessID
		WHERE i_tblProcess.ProcessName=@ProcessName
		AND STATUS NOT IN ('Completed','Reassigned','Removed')	
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllPendingTasksByProcessNameByApplicationName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllPendingTasksByProcessNameByApplicationName]
	-- Add the parameters for the stored procedure here	
	@ProcessName varchar(50),
	@ApplicationName varchar (50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT * FROM i_tblTask join i_tblStep ON i_tblTask.StepTemplateID = i_tblStep.StepTemplateID AND i_tblTask.ProcessID = i_tblStep.ProcessID
		join i_tblProcess on i_tblStep.ProcessID = i_tblProcess.ProcessID
		WHERE i_tblProcess.ProcessName=@ProcessName and i_tblProcess.ApplicationName = @ApplicationName
		AND STATUS NOT IN ('Completed','Reassigned','Removed')	 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllProcessTemplate]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllProcessTemplate]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * FROM d_tblProcess
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllTasksByStepTemplateID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllTasksByStepTemplateID]
	-- Add the parameters for the stored procedure here
	@StepTemplateID int,
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT * FROM     
	(
		SELECT * FROM i_tblTask
		WHERE ProcessID=@ProcessID AND StepTemplateID = @StepTemplateID
		AND [STATUS] NOT IN ('Completed','Reassigned','Removed')
	) Task JOIN i_tblStep ON Task.StepTemplateID = i_tblStep.StepTemplateID
		AND Task.ProcessID = i_tblStep.ProcessID
	JOIN i_tblProcess ON i_tblProcess.ProcessID = Task.ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[GetCurrentStepID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCurrentStepID]
	-- Add the parameters for the stored procedure here
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT CurStepTemplateID FROM i_tblProcess
		WHERE ProcessID = @ProcessID	
END
GO
/****** Object:  StoredProcedure [dbo].[GetCurrentStepName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCurrentStepName]
	-- Add the parameters for the stored procedure here
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT StepName FROM 
    (
		SELECT * FROM i_tblProcess
		WHERE ProcessID = @ProcessID
	) P
	JOIN i_tblStep
	ON P.ProcessID = i_tblStep.ProcessID
	AND P.CurStepTemplateID = i_tblStep.StepTemplateID
END
GO
/****** Object:  StoredProcedure [dbo].[GetDelegationsForUserPeriod]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDelegationsForUserPeriod]
	-- Add the parameters for the stored procedure here
	@Login varchar(50),
	@StartDate datetime,
	@EndDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *, d_tblProcess.ProcessName FROM i_tblDelegation left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	WHERE DelegationFromLogin = @Login
	AND (DelegationStartDate Between @StartDate AND @EndDate
	OR DelegationEndDate Between @StartDate AND @EndDate
	OR @StartDate Between DelegationStartDate AND DelegationEndDate
	OR @EndDate Between DelegationStartDate AND DelegationEndDate)
	AND Active = 'true'
	ORDER by DelegationStartDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetDelegationsForUserPeriodByApplicationName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDelegationsForUserPeriodByApplicationName]
	-- Add the parameters for the stored procedure here
	@Login varchar(50),
	@StartDate datetime,
	@EndDate datetime,
	@ApplicationName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *, d_tblProcess.ProcessName FROM i_tblDelegation left join d_tblProcess on i_tblDelegation.ProcessTemplateID = d_tblProcess.ProcessID
	WHERE DelegationFromLogin = @Login
	AND (DelegationStartDate Between @StartDate AND @EndDate
	OR DelegationEndDate Between @StartDate AND @EndDate
	OR @StartDate Between DelegationStartDate AND DelegationEndDate
	OR @EndDate Between DelegationStartDate AND DelegationEndDate)
	AND Active = 'true'
	AND ApplicationName = @ApplicationName
	ORDER by DelegationStartDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetMyTask]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMyTask]
	-- Add the parameters for the stored procedure here
	@LoginName varchar(50),
	@TaskID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT Task.*, i_tblProcess.*,i_tblStep.StepName as StepName, i_tblStep.TaskURL as TaskURL
    FROM
    (
		SELECT i_tblTask.* FROM i_tblTask join i_tblProcess on i_tblTask.ProcessID = i_tblProcess.ProcessID
		WHERE 
		(
		AssigneeLogin = @LoginName
		-- Delegation
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@LoginName
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is not null
				AND i_tblDelegation.ProcessTemplateID = i_tblProcess.ProcessTemplateID
				AND i_tblDelegation.ApplicationName = i_tblProcess.ApplicationName
				)
		-- Delegation backward compatibility
		OR AssigneeLogin IN
			(Select DelegationFromLogin FROM i_tblDelegation 
				WHERE Active = 'true'
				AND DelegationToLogin=@LoginName
				AND GETDATE() BETWEEN DelegationStartDate AND DelegationEndDate 
				AND i_tblDelegation.ProcessTemplateID is null
				)
		)
		AND STATUS NOT IN ('Completed','Reassigned','Removed')
	) Task
	JOIN i_tblProcess ON Task.ProcessID = i_tblProcess.ProcessID
	JOIN i_tblStep ON Task.StepTemplateID = i_tblStep.StepTemplateID
					AND Task.ProcessID = i_tblStep.ProcessID
					AND Task.TaskID = @TaskID
END
GO
/****** Object:  StoredProcedure [dbo].[GetNextStepTemplateIDForAction]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetNextStepTemplateIDForAction]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@CurStepTemplateID int,
	@ActionName	varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT NextStepTemplateID FROM i_tblAction
	WHERE CurStepTemplateID = @CurStepTemplateID AND ProcessID = @ProcessID
	AND ActionName = @ActionName
END
GO
/****** Object:  StoredProcedure [dbo].[GetNextStepTemplateIDForDefaultAction]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetNextStepTemplateIDForDefaultAction]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@CurStepTemplateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT NextStepTemplateID FROM i_tblAction
	WHERE CurStepTemplateID = @CurStepTemplateID AND ProcessID = @ProcessID
	AND ActionName = 'Default'
END
GO
/****** Object:  StoredProcedure [dbo].[GetOverdueTasks]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetOverdueTasks]
	
AS
BEGIN
	
    -- Insert statements for procedure here
	SELECT *, datediff(DAY,DueDate,GETDATE()) as OverdueDays, datediff(DAY,AssignedDate,DueDate) as DueDateDay
	FROM i_tblTask join i_tblProcess on i_tblTask.ProcessID = i_tblProcess.ProcessID
	join i_tblStep on i_tblTask.StepTemplateID = i_tblStep.StepTemplateID AND i_tblStep.ProcessID = i_tblTask.ProcessID
	WHERE Status = 'In Progress'
	AND GETDATE() > DueDate
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetPendingTasksByProcessID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPendingTasksByProcessID]
	-- Add the parameters for the stored procedure here	
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT * FROM i_tblTask join i_tblStep ON i_tblTask.StepTemplateID = i_tblStep.StepTemplateID AND i_tblTask.ProcessID = i_tblStep.ProcessID
		join i_tblProcess on i_tblStep.ProcessID = i_tblProcess.ProcessID
		WHERE i_tblTask.ProcessID=@ProcessID
		AND STATUS NOT IN ('Completed','Reassigned','Removed')	
END
GO
/****** Object:  StoredProcedure [dbo].[GetProcessInstance]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProcessInstance]
	-- Add the parameters for the stored procedure here
	@processID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from i_tblProcess
	where ProcessID = @processID
END
GO
/****** Object:  StoredProcedure [dbo].[GetProcessTemplateByName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProcessTemplateByName] 
	-- Add the parameters for the stored procedure here
	@ProcessName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from d_tblProcess
	WHERE ProcessName=@ProcessName
END
GO
/****** Object:  StoredProcedure [dbo].[GetReferenceKey]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetReferenceKey]
	-- Add the parameters for the stored procedure here
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT ReferenceKey FROM 
		i_tblProcess
		WHERE ProcessID = @ProcessID	
END
GO
/****** Object:  StoredProcedure [dbo].[GetStepActionersInformationByStepTemplateID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStepActionersInformationByStepTemplateID] 
	-- Add the parameters for the stored procedure here
	@StepTemplateID int,
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    Select * FROM 
    i_tblProcess P
    JOIN
	(
		SELECT * FROM i_tblStep
		WHERE ProcessID=@ProcessID AND StepTemplateID = @StepTemplateID
	) S 
	ON P.ProcessID=S.ProcessID
	LEFT JOIN i_tblActioner A
	ON S.StepTemplateID=A.StepTemplateID AND S.ProcessID=A.ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[GetStepActionersInformationByTaskID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStepActionersInformationByTaskID] 
	-- Add the parameters for the stored procedure here
	@TaskID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    Select * FROM 
    i_tblProcess P
    JOIN
	(
		SELECT i_tblStep.* FROM i_tblStep JOIN
		(SELECT * FROM i_tblTask WHERE TaskID = @TaskID) Task
		ON i_tblStep.ProcessID = Task.ProcessID AND
		i_tblStep.StepTemplateID = Task.StepTemplateID
	) S 
	ON P.ProcessID=S.ProcessID
	LEFT JOIN i_tblActioner A
	ON S.StepTemplateID=A.StepTemplateID AND S.ProcessID=A.ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[GetStepByStepTemplateID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStepByStepTemplateID]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@StepTemplateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT * FROM i_tblStep
		WHERE ProcessID = @ProcessID	
		AND StepTemplateID = @StepTemplateID
END
GO
/****** Object:  StoredProcedure [dbo].[GetStepInstanceByInternalStepName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStepInstanceByInternalStepName]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@InternalStepName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT * FROM i_tblStep
		WHERE ProcessID = @ProcessID	
		AND InternalStepName = @InternalStepName
END
GO
/****** Object:  StoredProcedure [dbo].[GetStepTemplateAndInstanceByProcessID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStepTemplateAndInstanceByProcessID]
	-- Add the parameters for the stored procedure here
	@ProcessID int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT StepID,EmailNotificationSubject as InstanceSubject,EmailNotificationBody as InstanceBody,TaskURL as InstanceTask,
		EmailNotificationSubjectTemplate as TemplateSubject,EmailNotificationBodyTemplate as TemplateBody,TaskURLTemplate as TemplateTask
    FROM i_tblStep
		WHERE ProcessID = @ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[GetStepTemplateByProcessID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStepTemplateByProcessID]
	-- Add the parameters for the stored procedure here
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from d_tblStep
	Where ProcessID=@ProcessID
	Order By StepOrder
END
GO
/****** Object:  StoredProcedure [dbo].[GetStepTemplateIDByStepName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStepTemplateIDByStepName]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@InternalStepName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT * FROM i_tblStep
		WHERE ProcessID = @ProcessID	
		AND InternalStepName = @InternalStepName
END
GO
/****** Object:  StoredProcedure [dbo].[GetTaskInstance]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTaskInstance]
	-- Add the parameters for the stored procedure here
	@TaskInstanceID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM i_tblTask
	WHERE TaskID = @TaskInstanceID
END
GO
/****** Object:  StoredProcedure [dbo].[GetTasksByProcessID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTasksByProcessID]
	-- Add the parameters for the stored procedure here	
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT * FROM i_tblTask join i_tblStep ON i_tblTask.StepTemplateID = i_tblStep.StepTemplateID AND i_tblTask.ProcessID = i_tblStep.ProcessID
		join i_tblProcess on i_tblTask.ProcessID = i_tblProcess.ProcessID
		WHERE i_tblTask.ProcessID=@ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[GetWorkflowInstancesByReferenceKey]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWorkflowInstancesByReferenceKey]
	-- Add the parameters for the stored procedure here
	@ReferenceKey varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM i_tblProcess P join i_tblStep S on P.CurStepTemplateID = S.StepTemplateID AND P.ProcessID = S.ProcessID
	WHERE ReferenceKey = @ReferenceKey
	ORDER BY P.StartDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetWorkflowLog]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWorkflowLog]
	-- Add the parameters for the stored procedure here
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM i_tblWorkflowLog
	Where ProcessID=@ProcessID
	order by ID
END
GO
/****** Object:  StoredProcedure [dbo].[GetWorkflowTaskHistoryByProcessID]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWorkflowTaskHistoryByProcessID]
	-- Add the parameters for the stored procedure here
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT i_tblTask.ProcessID, ProcessName,ReferenceKey,InternalStepName,EncryptParam,
	StepName,AssigneeLogin,AssigneeName,AssignedDate,DueDate, Status,
	ActionedDate,ActionedBy,ActionedByName,ActionName, Comments,
	TaskURL, TaskID, ExtendedDays as ExtendedDays
	FROM 
	(	
		SELECT * FROM i_tblTask
		WHERE ProcessID=@ProcessID
	) i_tblTask
	LEFT JOIN i_tblAction ON i_tblTask.ActionID = i_tblAction.ActionID
	JOIN i_tblStep ON i_tblTask.ProcessID = i_tblStep.ProcessID 
	AND i_tblTask.StepTemplateID = i_tblstep.StepTemplateID
	JOIN i_tblprocess ON i_tblTask.ProcessID = i_tblProcess.ProcessID
	ORDER BY AssignedDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetWorkflowTemplateByProcessName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWorkflowTemplateByProcessName]
	-- Add the parameters for the stored procedure here
	@ProcessName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT * FROM
    (
		SELECT * FROM d_tblProcess WHERE ProcessName = @ProcessName
    ) P JOIN d_tblStep S on P.ProcessID = S.ProcessID
	
	Order By StepOrder
END
GO
/****** Object:  StoredProcedure [dbo].[IncrementSlotCounter]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IncrementSlotCounter]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@CurStepTemplateID int,
	@NextStepTemplateID int,
	@AddSlot int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
    Update i_tblAction 
    SET MinSlot = MinSlot + @AddSlot
    WHERE ActionID IN
    (
		SELECT i_tblAction.ActionID FROM i_tblAction join d_tblAction on i_tblAction.CurStepTemplateID = d_tblAction.CurStepID 
		AND i_tblAction.ActionName=d_tblAction.ActionName
		WHERE i_tblAction.ProcessID = @ProcessID
		and CurStepTemplateID = @CurStepTemplateID
		and NextStepTemplateID = @NextStepTemplateID
		and d_tblAction.MinSlot=0
    )
END







--------


GO
/****** Object:  StoredProcedure [dbo].[InsertActionerInstance]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertActionerInstance]
	-- Add the parameters for the stored procedure here
	@ActionerLogin varchar(50),
	@ActionerName varchar(50),
	@ActionerEmail varchar(100),
	@StepTemplateID int,
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO i_tblActioner(ActionerLogin,ActionerName,ActionerEmail,StepTemplateID,ProcessID)
	VALUES(@ActionerLogin,@ActionerName,@ActionerEmail,@StepTemplateID,@ProcessID)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertActionInstance]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertActionInstance]
	-- Add the parameters for the stored procedure here
	@ActionName varchar(50),
	@CurStepTemplateID int,
	@NextStepTemplateID int,
	@MinSlot int,
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO i_tblAction(ActionName,CurStepTemplateID,NextStepTemplateID,MinSlot,ProcessID)
	VALUES(@ActionName,@CurStepTemplateID,@NextStepTemplateID,@MinSlot,@ProcessID)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertDelegationInstance]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertDelegationInstance]
	-- Add the parameters for the stored procedure here
	@ProcessTemplateID int,
	@ApplicationName varchar(100),
	@DelegationFromLogin varchar(50),
	@DelegationFromName varchar(50),
	@DelegationFromEmail varchar(100),
	@DelegationToLogin varchar(50),	
	@DelegationToName varchar(50),	
	@DelegationToEmail varchar(100),
	@DelegationStartDate datetime,
	@DelegationEndDate datetime,
	@LastUpdated datetime,
	@LastUpdatedBy varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO i_tblDelegation(ProcessTemplateID,ApplicationName,DelegationFromLogin,DelegationFromName,DelegationFromEmail,DelegationToLogin,DelegationToName,DelegationToEmail,DelegationStartDate,DelegationEndDate,Active,Created,CreatedBy,LastUpdated,LastUpdatedBy)
	VALUES (@ProcessTemplateID,@ApplicationName,@DelegationFromLogin,@DelegationFromName,@DelegationFromEmail,@DelegationToLogin,@DelegationToName,@DelegationToEmail,@DelegationStartDate,@DelegationEndDate,1,@LastUpdated,@LastUpdatedBy,@LastUpdated,@LastUpdatedBy)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertGoToTaskInstance]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertGoToTaskInstance]
	-- Add the parameters for the stored procedure here
	@AssigneeLogin varchar(50),
	@AssigneeName varchar(50),
	@AssigneeEmail varchar(100),
	@Comments text,		
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @StepTemplateID int
	declare @ActionID int
	declare @now datetime	
	
	set @now = GetDate()
	
	Select @ActionID = A.ActionID, @StepTemplateID = CurStepTemplateID from
		(Select ActionID, CurStepTemplateID From i_tblAction 
		WHERE ProcessID = @ProcessID) A
	JOIN
		(Select StepTemplateID, StepName From i_tblStep
		WHERE ProcessID = @ProcessID
		AND StepName = 'GoTo'
		) S
	ON A.CurStepTemplateID = S.StepTemplateID		

    -- Insert statements for procedure here
	INSERT INTO i_tblTask(AssigneeLogin,AssigneeName,AssigneeEmail,AssignedDate,DueDate,StepTemplateID,ProcessID,Status,ActionID,ActionedBy,ActionedByName,ActionedDate,Comments,Created,CreatedBy,LastUpdated,LastUpdatedBy)
	VALUES (@AssigneeLogin,@AssigneeName,@AssigneeEmail,@now,@now,@StepTemplateID,@ProcessID,'Completed',@ActionID,@AssigneeLogin,@AssigneeName,@now,@Comments,@now,@AssigneeLogin,@now,@AssigneeLogin)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertProcessInstance]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertProcessInstance]
	-- Add the parameters for the stored procedure here
	@ProcessTemplateID int,
	@ProcessName varchar(50),
	@ApplicationName varchar(100),
	@ReferenceKey varchar(50),
	@OriginatorLogin varchar(50),
	@OriginatorName varchar(50),
	@OriginatorEmail varchar(50),
	@StartDate	datetime,
	@CurStepTemplateID  int,
	@EmailHost varchar(50),
	@EmailPort int,
	@EmailFrom varchar(50),
	@KeywordsXML text,
	@EncryptParam bit,
	@LastUpdated datetime,
	@LastUpdatedBy varchar(50)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
    -- Insert statements for procedure here
	INSERT INTO i_tblProcess(ProcessTemplateID,ProcessName,ApplicationName,ReferenceKey,CurStepTemplateID,OriginatorLogin,OriginatorName,OriginatorEmail,StartDate,EmailHost,EmailPort,EmailFrom,KeywordsXML,EncryptParam,Created,CreatedBy,LastUpdated,LastUpdatedBy)	
	VALUES(@ProcessTemplateID,@ProcessName,@ApplicationName,@ReferenceKey,@CurStepTemplateID,@OriginatorLogin,@OriginatorName,@OriginatorEmail,@StartDate,@EmailHost,@EmailPort,@EmailFrom,@KeywordsXML,@EncryptParam,@LastUpdated,@LastUpdatedBy,@LastUpdated,@LastUpdatedBy);
	
	SELECT SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[InsertStartTaskInstance]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertStartTaskInstance]
	-- Add the parameters for the stored procedure here
	@AssigneeLogin varchar(50),
	@AssigneeName varchar(50),
	@AssigneeEmail varchar(100),
	@Comments text,		
	@ProcessID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @StepTemplateID int
	declare @ActionID int
	declare @now datetime	
	
	set @now = GetDate()
	
	Select @ActionID = A.ActionID, @StepTemplateID = CurStepTemplateID from
		(Select ActionID, CurStepTemplateID From i_tblAction 
		WHERE ProcessID = @ProcessID) A
	JOIN
		(Select StepTemplateID, StepName From i_tblStep
		WHERE ProcessID = @ProcessID
		AND StepName = 'Start'
		) S
	ON A.CurStepTemplateID = S.StepTemplateID		

    -- Insert statements for procedure here
	INSERT INTO i_tblTask(AssigneeLogin,AssigneeName,AssigneeEmail,AssignedDate,DueDate,StepTemplateID,ProcessID,Status,ActionID,ActionedBy,ActionedByName,ActionedDate,Comments,Created,CreatedBy,LastUpdated,LastUpdatedBy)
	VALUES (@AssigneeLogin,@AssigneeName,@AssigneeEmail,@now,@now,@StepTemplateID,@ProcessID,'Completed',@ActionID,@AssigneeLogin,@AssigneeName,@now,@Comments,@now,@AssigneeLogin,@now,@AssigneeLogin)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertStepInstance]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertStepInstance]
	-- Add the parameters for the stored procedure here
	@StepName varchar(50),
	@InternalStepName varchar(50),
	@StepOrder int,
	@DueDateDay int,
	@EmailNotification bit,
	@EmailToAssignee bit,
	@EmailToOriginator bit,
	@EmailCCOriginator bit,
	@EmailAdditionalCC text,
	@EmailAdditionalTo text,
	@EmailNotificationSubject text,
	@EmailNotificationBody text,
	@EmailNotificationSubjectTemplate text,
	@EmailNotificationBodyTemplate text,
	@TaskURL text,
	@TaskURLTemplate text,
	@LastStep bit,
	@EmailOnlyStep bit,
	@CodeOnlyStep bit,
	@AssemblyName varchar(200),
	@ClassName varchar(200),
	@MethodName varchar(200),
	@ProcessID int,
	@StepTemplateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO i_tblStep(StepName,InternalStepName,StepOrder,DueDateDay,EmailNotification,EmailToAssignee,
			EmailToOriginator,EmailCCOriginator,EmailAdditionalCC,EmailAdditionalTo,EmailNotificationSubject,EmailNotificationBody,EmailNotificationSubjectTemplate,EmailNotificationBodyTemplate,TaskURL,TaskURLTemplate,LastStep,EmailOnlyStep,CodeOnlyStep,AssemblyName,ClassName,MethodName,ProcessID,StepTemplateID)
	VALUES(@StepName,@InternalStepName,@StepOrder,@DueDateDay,@EmailNotification,@EmailToAssignee,
			@EmailToOriginator,@EmailCCOriginator,@EmailAdditionalCC,@EmailAdditionalTo,@EmailNotificationSubject,@EmailNotificationBody,@EmailNotificationSubjectTemplate,@EmailNotificationBodyTemplate,@TaskURL,@TaskURLTemplate,@LastStep,@EmailOnlyStep,@CodeOnlyStep,@AssemblyName,@ClassName,@MethodName,@ProcessID,@StepTemplateID);
END
GO
/****** Object:  StoredProcedure [dbo].[InsertTaskInstance]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertTaskInstance]
	-- Add the parameters for the stored procedure here
	@AssigneeLogin varchar(50),
	@AssigneeName varchar(50),
	@AssigneeEmail varchar(100),
	@AssignedDate datetime,
	@DueDate datetime,
	@StepTemplateID int,
	@ProcessID int,
	@LastUpdated datetime,
	@LastUpdatedBy varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO i_tblTask(AssigneeLogin,AssigneeName,AssigneeEmail,AssignedDate,DueDate,StepTemplateID,ProcessID,Status,Created,CreatedBy,LastUpdated,LastUpdatedBy)
	VALUES (@AssigneeLogin,@AssigneeName,@AssigneeEmail,@AssignedDate,@DueDate,@StepTemplateID,@ProcessID,'In Progress',@LastUpdated,@LastUpdatedBy,@LastUpdated,@LastUpdatedBy)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertWorkflowLog]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertWorkflowLog]
	-- Add the parameters for the stored procedure here
	@LogType varchar(50),
	@LogDescription text,
	@LogDate DateTime,
	@LogBy varchar(50),
	@LogByName varchar(50),
	@ProcessID int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO i_tblWorkflowLog(LogType,LogDescription,LogDate,LogBy,LogByName,ProcessID)
	VALUES (@LogType,@LogDescription,@LogDate,@LogBy,@LogByName,@ProcessID)	
END
GO
/****** Object:  StoredProcedure [dbo].[ReassignTask]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ReassignTask]
	-- Add the parameters for the stored procedure here
	@ActionedDate datetime,	
	@TaskID int,
	@ActionedBy varchar(50),
	@ActionedByName varchar(50),	
	@NewAssigneeName varchar(50),
	@NewAssigneeLogin varchar(50),
	@NewAssigneeEmail varchar(50),
	@LastUpdated datetime,
	@LastUpdatedBy varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    --Complete Task as Reassigned
	UPDATE i_tblTask 
	SET ActionedDate =@ActionedDate,	
	ActionedBy = @ActionedBy,
	ActionedByName = @ActionedByName,
	[Status] = 'Reassigned',
	LastUpdated = @LastUpdated,
	LastUpdatedBy = @LastUpdatedBy
	WHERE TaskID = @TaskID	
	
	--Create New Task
	declare @curDate datetime
	set @curDate = GETDATE()
	
	INSERT INTO i_tblTask(AssigneeLogin,AssigneeName,AssigneeEmail,AssignedDate,DueDate,StepTemplateID,ProcessID,Status,Created,CreatedBy,LastUpdated,LastUpdatedBy)
	(
		SELECT @NewAssigneeLogin,@NewAssigneeName,@NewAssigneeEmail,@curDate,DueDate,StepTemplateID,ProcessID,'In Progress',@curDate,@ActionedBy,@curDate,@ActionedBy
		FROM i_tblTask
		WHERE TaskID=@TaskID
	)
	
END
GO
/****** Object:  StoredProcedure [dbo].[ResetMinSlotCounter]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ResetMinSlotCounter]	
	@ProcessID int,
	@CurStepTemplateID int,
	@NextStepTemplateID int,
	@MinSlot int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE i_tblAction
	SET MinSlot = @MinSlot
	WHERE ProcessID=@ProcessID
	AND CurStepTemplateID = @CurStepTemplateID
	AND NextStepTemplateID = @NextStepTemplateID
END


--------


GO
/****** Object:  StoredProcedure [dbo].[ResetSlotCounter]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ResetSlotCounter]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@CurStepTemplateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE i_tblAction
	SET SlotCounter = 0
	WHERE ProcessID=@ProcessID
	AND CurStepTemplateID = @CurStepTemplateID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEmailTemplateByInternalStepName]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateEmailTemplateByInternalStepName]
	-- Add the parameters for the stored procedure here
	@EmailSubject text,
	@EmailBody text,
	@ProcessName varchar(50),
	@InternalStepName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update d_tblStep 
	Set EmailNotificationSubject = @EmailSubject,
	EmailNotificationBody = @EmailBody
	Where ProcessID IN 
		(
			Select ProcessID from d_tblProcess
			WHERE ProcessName = @ProcessName			
		)
	AND InternalStepName = @InternalStepName
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProcessInstanceCompletionDate]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateProcessInstanceCompletionDate]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@CompletionDate datetime,
	@LastUpdated datetime,
	@LastUpdatedBy varchar(50)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE i_tblProcess
	SET CompletionDate = @CompletionDate,
	LastUpdated = @LastUpdated,
	LastUpdatedBy = @LastUpdatedBy
	WHERE ProcessID = @ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProcessInstanceCurrentStep]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateProcessInstanceCurrentStep]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@CurStepTemplateID int,
	@LastUpdated datetime,
	@LastUpdatedBy varchar(50)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE i_tblProcess
	SET CurStepTemplateID = @CurStepTemplateID,
	LastUpdated = @LastUpdated,
	LastUpdatedBy = @LastUpdatedBy
	WHERE ProcessID = @ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProcessInstanceKeywords]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateProcessInstanceKeywords]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@KeywordsXML text,
	@LastUpdated datetime,
	@LastUpdatedBy varchar(50)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE i_tblProcess
	SET KeywordsXML = @KeywordsXML,
	LastUpdated = @LastUpdated,
	LastUpdatedBy = @LastUpdatedBy
	WHERE ProcessID = @ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStepAdditionalEmail]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateStepAdditionalEmail]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@InternalStepName varchar(50),
	@AdditionalTo text,
	@AdditionalCC text
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE i_tblStep
	SET EmailAdditionalTo = @AdditionalTo,
	EmailAdditionalCC = @AdditionalCC
	WHERE InternalStepName = @InternalStepName
AND ProcessID = @ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStepDueDateDay]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateStepDueDateDay]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@InternalStepName varchar(50),
	@DueDateDay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE i_tblStep
	SET DueDateDay = @DueDateDay
	WHERE InternalStepName = @InternalStepName
AND ProcessID = @ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStepInstanceWithNewKeywords]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateStepInstanceWithNewKeywords]
	-- Add the parameters for the stored procedure here
	@EmailNotificationSubject text,
	@EmailNotificationBody text,
	@TaskURL text,
	@StepInstanceID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update i_tblStep
	SET EmailNotificationBody = @EmailNotificationBody,
	EmailNotificationSubject = @EmailNotificationSubject,
	TaskURL = @TaskURL
	WHERE
	StepID=@StepInstanceID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStepNotification]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateStepNotification]
	-- Add the parameters for the stored procedure here
	@ProcessID int,
	@InternalStepName varchar(50),
	@onoff bit	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE i_tblStep
	SET EmailNotification = @onoff
	WHERE InternalStepName = @InternalStepName
AND ProcessID = @ProcessID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTaskAsRemoved]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateTaskAsRemoved]
	-- Add the parameters for the stored procedure here
	@TaskID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE i_tblTask
	SET Status = 'Removed',
	ActionID = null
	WHERE TaskID = @TaskID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTaskDueDate]    Script Date: 17-Aug-20 5:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateTaskDueDate]
	-- Add the parameters for the stored procedure here
	@TaskID int,
	@DueDate datetime, 
	@ExtendedDays float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE i_tblTask
	SET DueDate = @DueDate,
	ExtendedDays = @ExtendedDays
	WHERE TaskID = @TaskID
END
GO
