
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/26/2017 12:31:48
-- Generated from EDMX file: C:\Users\Work\Documents\Visual Studio 2013\Projects\OSSOBase\Client\Model\EF\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OSSOBase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_StreetsObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ObjectSet] DROP CONSTRAINT [FK_StreetsObject];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersCards]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CardsSet] DROP CONSTRAINT [FK_UsersCards];
GO
IF OBJECT_ID(N'[dbo].[FK_ObjectCards]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CardsSet] DROP CONSTRAINT [FK_ObjectCards];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CardsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CardsSet];
GO
IF OBJECT_ID(N'[dbo].[ObjectSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ObjectSet];
GO
IF OBJECT_ID(N'[dbo].[StreetsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StreetsSet];
GO
IF OBJECT_ID(N'[dbo].[UsersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CardsSet'
CREATE TABLE [dbo].[CardsSet] (
    [Cards_ID] int IDENTITY(1,1) NOT NULL,
    [Users_ID] int  NOT NULL,
    [Object_ID] int  NOT NULL,
    [MakeDate] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ObjectSet'
CREATE TABLE [dbo].[ObjectSet] (
    [Object_ID] int IDENTITY(1,1) NOT NULL,
    [Owner] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Home] nvarchar(max)  NOT NULL,
    [Corp] nvarchar(max)  NOT NULL,
    [Room] nvarchar(max)  NOT NULL,
    [Streets_ID] int  NOT NULL
);
GO

-- Creating table 'StreetsSet'
CREATE TABLE [dbo].[StreetsSet] (
    [Streets_ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UsersSet'
CREATE TABLE [dbo].[UsersSet] (
    [Users_ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Place] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Cards_ID] in table 'CardsSet'
ALTER TABLE [dbo].[CardsSet]
ADD CONSTRAINT [PK_CardsSet]
    PRIMARY KEY CLUSTERED ([Cards_ID] ASC);
GO

-- Creating primary key on [Object_ID] in table 'ObjectSet'
ALTER TABLE [dbo].[ObjectSet]
ADD CONSTRAINT [PK_ObjectSet]
    PRIMARY KEY CLUSTERED ([Object_ID] ASC);
GO

-- Creating primary key on [Streets_ID] in table 'StreetsSet'
ALTER TABLE [dbo].[StreetsSet]
ADD CONSTRAINT [PK_StreetsSet]
    PRIMARY KEY CLUSTERED ([Streets_ID] ASC);
GO

-- Creating primary key on [Users_ID] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [PK_UsersSet]
    PRIMARY KEY CLUSTERED ([Users_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Streets_ID] in table 'ObjectSet'
ALTER TABLE [dbo].[ObjectSet]
ADD CONSTRAINT [FK_StreetsObject]
    FOREIGN KEY ([Streets_ID])
    REFERENCES [dbo].[StreetsSet]
        ([Streets_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StreetsObject'
CREATE INDEX [IX_FK_StreetsObject]
ON [dbo].[ObjectSet]
    ([Streets_ID]);
GO

-- Creating foreign key on [Users_ID] in table 'CardsSet'
ALTER TABLE [dbo].[CardsSet]
ADD CONSTRAINT [FK_UsersCards]
    FOREIGN KEY ([Users_ID])
    REFERENCES [dbo].[UsersSet]
        ([Users_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersCards'
CREATE INDEX [IX_FK_UsersCards]
ON [dbo].[CardsSet]
    ([Users_ID]);
GO

-- Creating foreign key on [Object_ID] in table 'CardsSet'
ALTER TABLE [dbo].[CardsSet]
ADD CONSTRAINT [FK_ObjectCards]
    FOREIGN KEY ([Object_ID])
    REFERENCES [dbo].[ObjectSet]
        ([Object_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ObjectCards'
CREATE INDEX [IX_FK_ObjectCards]
ON [dbo].[CardsSet]
    ([Object_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------