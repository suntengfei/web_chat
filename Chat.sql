USE [Chat]
GO
/****** 对象:  Table [dbo].[Chat_Users]    脚本日期: 10/01/2011 16:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat_Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[UserEmail] [nvarchar](50) NOT NULL,
	[UserPassWord] [nvarchar](100) NOT NULL,
	[CreateTime] [datetime] NULL,
	[ModifyTime] [datetime] NULL,
	[CreaterID] [int] NULL,
	[ModifierID] [int] NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_Chat_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'unique' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Chat_Users', @level2type=N'COLUMN',@level2name=N'UserEmail'
GO
/****** 对象:  Table [dbo].[Chat_Friends]    脚本日期: 10/01/2011 16:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat_Friends](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HostID] [int] NOT NULL,
	[FriendID] [int] NULL,
	[HandleOrNot] [bit] NOT NULL,
 CONSTRAINT [PK_Chat_Friends] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Chat_OffMessage]    脚本日期: 10/01/2011 16:00:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat_OffMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SenderID] [int] NOT NULL,
	[ReceiverID] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Chat_OffMessage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  StoredProcedure [dbo].[Chat_Message]    脚本日期: 10/01/2011 16:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*主要涉及Chat_OffMessage表的相关操作*/
CREATE PROC [dbo].[Chat_Message]
@mFunctionName nvarchar(30)   =null,
@mSenderEmail nvarchar(50)    =null,
@mReceiverEmail nvarchar(50)  =null,
@mTime datetime               =null,
@mMessage nvarchar(500)       =null
AS
    DECLARE @mSenderID int
    DECLARE @mReceiverID int
	SET @mSenderID=(SELECT UserID FROM Chat_Users
        WHERE UserEmail=@mSenderEmail)
    SET @mReceiverID=(SELECT UserID FROM Chat_Users
        WHERE UserEmail=@mReceiverEmail)


    
	IF(@mFunctionName='InSertInToOff')             ----插入离线消息
    BEGIN
        INSERT INTO Chat_OffMessage
        (SenderID,ReceiverID,Time,Message)
        VALUES
        (@mSenderID,@mReceiverID,@mTime,@mMessage)
	END


    
	IF(@mFunctionName='GetOffFriends')             --获得发送离线消息的好友账号
	BEGIN
        SELECT USerEmail  AS 'SenderEmail'
	    FROM Chat_OffMessage inner join Chat_Users 
	    ON Chat_Users.UserID =Chat_OffMessage.SenderID
	    WHERE ReceiverID=@mReceiverID 
	END
   
    IF(@mFunctionName='GetOffMessage')             --获取离线消息
	BEGIN
        SELECT UserEmail 'SenderEmail', @mReceiverEmail 'ReceiverEmail', Time,Message 
        FROM Chat_OffMessage inner join Chat_Users
        ON UserID=SenderID
		WHERE ReceiverID=@mReceiverID 
    END



    IF(@mFunctionName='DeleteOffMessage')             --删除离线消息
	BEGIN
        DELETE FROM Chat_OffMessage 
		WHERE ReceiverID=@mReceiverID 
    END
GO
/****** 对象:  StoredProcedure [dbo].[Chat_Friend]    脚本日期: 10/01/2011 16:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*主要涉及Chat_Friends表的相关操作*/
 CREATE PROC [dbo].[Chat_Friend]
 @mFunctionName nvarchar(30)   =null,
 @mHostEmail nvarchar(50)      =null,
 @mFriendEmail nvarchar(50)    =null,
 @mIsOrNot bit                 =null
    
 AS
	DECLARE @mHostID int
    DECLARE @mFriendID int
    DECLARE @mHandleOrNot bit
    SET @mHostID=(SELECT UserID FROM Chat_Users
           WHERE UserEmail=@mHostEmail)
    SET @mFriendID=(SELECT UserID FROM Chat_Users
           WHERE UserEmail=@mFriendEmail)


	IF(@mFunctionName='CheckInsertToFr')               --检测好友列表中是否有要查找的用户
	BEGIN
	    SELECT count(*) FROM Chat_Friends
	    WHERE HostID=@mHostID and FriendID=@mFriendID
	END


    IF(@mFunctionName='InsertIntoFr' )                 ---查找添加好友，未处理
    BEGIN
	    INSERT INTO Chat_Friends
        (HostID,FriendID,HandleOrNot)
        VALUES
        (@mHostID,@mFriendID,0)
		
    END
    

	IF(@mFunctionName='GetAddMessage')                ---获取添加信息
	BEGIN
	    SELECT UserEmail,UserName FROM Chat_Users 
               inner join Chat_Friends ON UserID=HostID
	    WHERE FriendID=@mHostID and HandleOrNot=0
	END


    IF(@mFunctionName='AdmmitOrNot1')                ---同意添加好友
    BEGIN
	    
	    INSERT INTO Chat_Friends
        (HostID,FriendID,HandleOrNot)
		VALUES
		(@mHostID,@mFriendID,1)
        UPDATE Chat_Friends
        SET  HandleOrNot=1
        WHERE (FriendID=@mHostID and HostID=@mFriendID) and HandleOrNot=0
	END
	

	IF(@mFunctionName='AdmmitOrNot2')               ---不同意添加好友
	BEGIN
        DELETE FROM Chat_Friends
	    WHERE FriendID=@mHostID and HostID=@mFriendID and HandleOrNot=0
	END


	IF(@mFunctionName='GetFriends')                ---获取好友列表
	BEGIN
	   /*SELECT UserName,UserEmail
		FROM Chat_Users
		WHERE  UserID in (SELECT FriendID 
			FROM Chat_Friends)
			AND UserEmail=@mHostEmail
	    END*/
        SELECT UserEmail,UserName
	    FROM Chat_Users
	    WHERE UserID in(SELECT FriendID FROM Chat_Friends
	          WHERE HostID=@mHostID and HandleOrNot=1)
	END
GO
/****** 对象:  StoredProcedure [dbo].[Chat_User]    脚本日期: 10/01/2011 16:00:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* 主要涉及Chat_Users表的相关操作*/
 CREATE PROC [dbo].[Chat_User]
 @mFunctionName nvarchar(30)  = null,
 @mUserEmail nvarchar(50) = null,  
 @mUserPassword  nvarchar(100) = null,
 @mIsOrNot bit = null,
 @mUserName  nvarchar(20) = null,
 @mCreateTime datetime = null,
 @mModifyTime datetime = null,
 @mCreaterID int = null,
 @mModifierID int = null,
 @mRemark nvarchar(200) = null,
 @mNuserPassword nvarchar(100) = null
 AS	
	DECLARE @mUserID int


    IF(@mFunctionName='Check')               ----登录时检测是否匹配
    BEGIN
        SELECT count(*) FROM Chat_Users
	    WHERE UserEmail=@mUserEmail and UserPassWord=@mUserPassword
    END
	
    
    
	IF(@mFunctionName='CheckInsert')         ----注册时检测账号是否重复
	BEGIN
	    SELECT Count(*) FROM Chat_Users
	    WHERE UserEmail=@mUserEmail
	END
    

    IF(@mFunctionName='Insert')              ---注册插入用户信息
    BEGIN
	SET NOCOUNT OFF
		
        INSERT INTO Chat_Users
       (UserName,UserEmail,UserPassWord,CreateTime,ModifyTime,CreaterID,ModifierID,Remark) 
	    VALUES
        (@mUserName,@mUserEmail,@mUserPassword,@mCreateTime,@mModifyTime,@mCreaterID,@mModifierID,@mRemark)
		SET @mUserID=(SELECT UserID FROM Chat_Users WHERE UserEmail=@mUserEmail)
	    INSERT INTO Chat_Friends 
		(HostID,FriendID,HandleOrNot)
		VALUES
		(@mUserID,@mUserID,1)
    END
    
     
   
    IF(@mFunctionName='SearchOne')           ---搜索并返回某一个用户的信息
    BEGIN
        SELECT UserEmail,UserName,Remark
        FROM Chat_Users
	    WHERE UserEmail=@mUserEmail
    END


    IF(@mFunctionName='SearchName')           ---搜索并返回某一个用户的用户名
    BEGIN
        SELECT UserName
        FROM Chat_Users
	    WHERE UserEmail=@mUserEmail
    END
    

	IF(@mFunctionName='GetPassword')          ---获取用户密码
	BEGIN
	    SELECT UserPassword FROM Chat_Users
	    WHERE UserEmail=@mUserEmail
	END


	IF(@mFunctionName='Update')               ---更改个人信息
	BEGIN
	   UPDATE Chat_Users 
	   SET UserName=@mUserName,
		   Remark=@mRemark,
		   ModifyTime=@mModifyTime,
		   ModifierID=@mModifierID
	       WHERE UserEmail=@mUserEmail
	END


	IF(@mFunctionName='UpdatePassword')       ---更改密码
	BEGIN
	    UPDATE Chat_Users 
	    SET UserPassword=@mNuserPassword
	    WHERE UserEmail=@mUserEmail
	END


	IF(@mFunctionName='SearchAll')            ---搜索所有用户信息
	BEGIN				  
	    SELECT UserEmail,UserName,Remark 
	    FROM Chat_Users
	END
GO
/****** 对象:  ForeignKey [FK_Chat_Friends_Chat_Friends]    脚本日期: 10/01/2011 16:00:48 ******/
ALTER TABLE [dbo].[Chat_Friends]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Friends_Chat_Friends] FOREIGN KEY([ID])
REFERENCES [dbo].[Chat_Friends] ([ID])
GO
ALTER TABLE [dbo].[Chat_Friends] CHECK CONSTRAINT [FK_Chat_Friends_Chat_Friends]
GO
/****** 对象:  ForeignKey [FK_Chat_Friends_Chat_Users]    脚本日期: 10/01/2011 16:00:48 ******/
ALTER TABLE [dbo].[Chat_Friends]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Friends_Chat_Users] FOREIGN KEY([HostID])
REFERENCES [dbo].[Chat_Users] ([UserID])
GO
ALTER TABLE [dbo].[Chat_Friends] CHECK CONSTRAINT [FK_Chat_Friends_Chat_Users]
GO
/****** 对象:  ForeignKey [FK_Chat_Friends_Chat_Users1]    脚本日期: 10/01/2011 16:00:49 ******/
ALTER TABLE [dbo].[Chat_Friends]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Friends_Chat_Users1] FOREIGN KEY([FriendID])
REFERENCES [dbo].[Chat_Users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Chat_Friends] CHECK CONSTRAINT [FK_Chat_Friends_Chat_Users1]
GO
/****** 对象:  ForeignKey [FK_Chat_OffMessage_Chat_OffMessage]    脚本日期: 10/01/2011 16:00:50 ******/
ALTER TABLE [dbo].[Chat_OffMessage]  WITH CHECK ADD  CONSTRAINT [FK_Chat_OffMessage_Chat_OffMessage] FOREIGN KEY([ID])
REFERENCES [dbo].[Chat_OffMessage] ([ID])
GO
ALTER TABLE [dbo].[Chat_OffMessage] CHECK CONSTRAINT [FK_Chat_OffMessage_Chat_OffMessage]
GO
/****** 对象:  ForeignKey [FK_Chat_OffMessage_Chat_Users]    脚本日期: 10/01/2011 16:00:51 ******/
ALTER TABLE [dbo].[Chat_OffMessage]  WITH CHECK ADD  CONSTRAINT [FK_Chat_OffMessage_Chat_Users] FOREIGN KEY([ReceiverID])
REFERENCES [dbo].[Chat_Users] ([UserID])
GO
ALTER TABLE [dbo].[Chat_OffMessage] CHECK CONSTRAINT [FK_Chat_OffMessage_Chat_Users]
GO
/****** 对象:  ForeignKey [FK_Chat_OffMessage_Chat_Users2]    脚本日期: 10/01/2011 16:00:51 ******/
ALTER TABLE [dbo].[Chat_OffMessage]  WITH CHECK ADD  CONSTRAINT [FK_Chat_OffMessage_Chat_Users2] FOREIGN KEY([SenderID])
REFERENCES [dbo].[Chat_Users] ([UserID])
GO
ALTER TABLE [dbo].[Chat_OffMessage] CHECK CONSTRAINT [FK_Chat_OffMessage_Chat_Users2]
GO
