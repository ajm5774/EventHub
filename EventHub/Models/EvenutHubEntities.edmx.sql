
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/23/2014 15:16:19
-- Generated from EDMX file: C:\Users\Andrew\documents\visual studio 2013\Projects\EventHub\EventHub\Models\EvenutHubEntities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-EventHub-20141011031729];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AspNetUserComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_AspNetUserComment];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserNotification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications] DROP CONSTRAINT [FK_AspNetUserNotification];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUsersNotifications1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications] DROP CONSTRAINT [FK_AspNetUsersNotifications1];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUsersSubscribers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupSubscriptions] DROP CONSTRAINT [FK_AspNetUsersSubscribers];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUsersUserEventNotifications]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEventNotifications] DROP CONSTRAINT [FK_AspNetUsersUserEventNotifications];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_User_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_User_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_EventsComments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_EventsComments];
GO
IF OBJECT_ID(N'[dbo].[FK_EventsEventPictures]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventPictures] DROP CONSTRAINT [FK_EventsEventPictures];
GO
IF OBJECT_ID(N'[dbo].[FK_EventsUserEventNotifications]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEventNotifications] DROP CONSTRAINT [FK_EventsUserEventNotifications];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupsEvents]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_GroupsEvents];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupsSubscribers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupSubscriptions] DROP CONSTRAINT [FK_GroupsSubscribers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[EventPictures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventPictures];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[GroupSubscriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupSubscriptions];
GO
IF OBJECT_ID(N'[dbo].[Notifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications];
GO
IF OBJECT_ID(N'[dbo].[Schools]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Schools];
GO
IF OBJECT_ID(N'[dbo].[UserEventNotifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserEventNotifications];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL,
    [User_Id] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [UserId] nvarchar(128)  NOT NULL,
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [UserName] nvarchar(max)  NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [Discriminator] nvarchar(128)  NOT NULL,
    [FirstName] nvarchar(256)  NULL,
    [LastName] nvarchar(256)  NULL,
    [PicturePath] nvarchar(max)  NOT NULL,
    [SchoolId] int  NOT NULL
);
GO

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [EventId] int  NOT NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'EventPictures'
CREATE TABLE [dbo].[EventPictures] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PicturePath] nvarchar(max)  NOT NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NOT NULL,
    [Place] nvarchar(max)  NOT NULL,
    [GroupId] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [PicturePath] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'GroupSubscriptions'
CREATE TABLE [dbo].[GroupSubscriptions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsAdministrator] bit  NOT NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL,
    [GroupId] int  NOT NULL
);
GO

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [Viewed] bit  NOT NULL,
    [NotificationType] smallint  NOT NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL,
    [AspNetUserId1] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Schools'
CREATE TABLE [dbo].[Schools] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserEventNotifications'
CREATE TABLE [dbo].[UserEventNotifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AllowNotifications] bit  NOT NULL,
    [EventsId] int  NOT NULL,
    [AspNetUsersId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId], [LoginProvider], [ProviderKey] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([UserId], [LoginProvider], [ProviderKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventPictures'
ALTER TABLE [dbo].[EventPictures]
ADD CONSTRAINT [PK_EventPictures]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GroupSubscriptions'
ALTER TABLE [dbo].[GroupSubscriptions]
ADD CONSTRAINT [PK_GroupSubscriptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Schools'
ALTER TABLE [dbo].[Schools]
ADD CONSTRAINT [PK_Schools]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserEventNotifications'
ALTER TABLE [dbo].[UserEventNotifications]
ADD CONSTRAINT [PK_UserEventNotifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_User_Id]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_User_Id'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_User_Id]
ON [dbo].[AspNetUserClaims]
    ([User_Id]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUserId] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [FK_AspNetUsersNotifications1]
    FOREIGN KEY ([AspNetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUsersNotifications1'
CREATE INDEX [IX_FK_AspNetUsersNotifications1]
ON [dbo].[Notifications]
    ([AspNetUserId]);
GO

-- Creating foreign key on [SchoolId] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [FK_AspNetUsersSchools]
    FOREIGN KEY ([SchoolId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUsersSchools'
CREATE INDEX [IX_FK_AspNetUsersSchools]
ON [dbo].[AspNetUsers]
    ([SchoolId]);
GO

-- Creating foreign key on [AspNetUserId] in table 'GroupSubscriptions'
ALTER TABLE [dbo].[GroupSubscriptions]
ADD CONSTRAINT [FK_AspNetUsersSubscribers]
    FOREIGN KEY ([AspNetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUsersSubscribers'
CREATE INDEX [IX_FK_AspNetUsersSubscribers]
ON [dbo].[GroupSubscriptions]
    ([AspNetUserId]);
GO

-- Creating foreign key on [AspNetUsersId] in table 'UserEventNotifications'
ALTER TABLE [dbo].[UserEventNotifications]
ADD CONSTRAINT [FK_AspNetUsersUserEventNotifications]
    FOREIGN KEY ([AspNetUsersId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUsersUserEventNotifications'
CREATE INDEX [IX_FK_AspNetUsersUserEventNotifications]
ON [dbo].[UserEventNotifications]
    ([AspNetUsersId]);
GO

-- Creating foreign key on [EventId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_EventsComments]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventsComments'
CREATE INDEX [IX_FK_EventsComments]
ON [dbo].[Comments]
    ([EventId]);
GO

-- Creating foreign key on [EventId] in table 'EventPictures'
ALTER TABLE [dbo].[EventPictures]
ADD CONSTRAINT [FK_EventsEventPictures]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventsEventPictures'
CREATE INDEX [IX_FK_EventsEventPictures]
ON [dbo].[EventPictures]
    ([EventId]);
GO

-- Creating foreign key on [EventsId] in table 'UserEventNotifications'
ALTER TABLE [dbo].[UserEventNotifications]
ADD CONSTRAINT [FK_EventsUserEventNotifications]
    FOREIGN KEY ([EventsId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventsUserEventNotifications'
CREATE INDEX [IX_FK_EventsUserEventNotifications]
ON [dbo].[UserEventNotifications]
    ([EventsId]);
GO

-- Creating foreign key on [GroupId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_GroupsEvents]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupsEvents'
CREATE INDEX [IX_FK_GroupsEvents]
ON [dbo].[Events]
    ([GroupId]);
GO

-- Creating foreign key on [GroupId] in table 'GroupSubscriptions'
ALTER TABLE [dbo].[GroupSubscriptions]
ADD CONSTRAINT [FK_GroupsSubscribers]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupsSubscribers'
CREATE INDEX [IX_FK_GroupsSubscribers]
ON [dbo].[GroupSubscriptions]
    ([GroupId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [AspNetUserId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_AspNetUserComment]
    FOREIGN KEY ([AspNetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserComment'
CREATE INDEX [IX_FK_AspNetUserComment]
ON [dbo].[Comments]
    ([AspNetUserId]);
GO

-- Creating foreign key on [AspNetUserId1] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [FK_AspNetUserNotification]
    FOREIGN KEY ([AspNetUserId1])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserNotification'
CREATE INDEX [IX_FK_AspNetUserNotification]
ON [dbo].[Notifications]
    ([AspNetUserId1]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------