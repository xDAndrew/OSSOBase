
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/04/2017 09:09:41
-- Generated from EDMX file: c:\users\work\documents\visual studio 2013\Projects\OSSOBase\Client\Model\EF\DataModel.edmx
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
IF OBJECT_ID(N'[dbo].[FK_TSOGroupTSO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TSOSet] DROP CONSTRAINT [FK_TSOGroupTSO];
GO
IF OBJECT_ID(N'[dbo].[FK_EquipmentLimb]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LimbSet] DROP CONSTRAINT [FK_EquipmentLimb];
GO
IF OBJECT_ID(N'[dbo].[FK_PKPCards]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CardsSet] DROP CONSTRAINT [FK_PKPCards];
GO
IF OBJECT_ID(N'[dbo].[FK_EquipmentCards]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CardsSet] DROP CONSTRAINT [FK_EquipmentCards];
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
IF OBJECT_ID(N'[dbo].[TSOGroupSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TSOGroupSet];
GO
IF OBJECT_ID(N'[dbo].[TSOSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TSOSet];
GO
IF OBJECT_ID(N'[dbo].[PKPSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PKPSet];
GO
IF OBJECT_ID(N'[dbo].[EquipmentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EquipmentSet];
GO
IF OBJECT_ID(N'[dbo].[LimbSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LimbSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CardsSet'
CREATE TABLE [dbo].[CardsSet] (
    [Cards_ID] int IDENTITY(1,1) NOT NULL,
    [Users_ID] int  NOT NULL,
    [Object_ID] int  NOT NULL,
    [MakeDate] datetime  NOT NULL,
    [PKP_ID] int  NOT NULL,
    [Equipment_ID] int  NOT NULL
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
    [Type] tinyint  NOT NULL
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

-- Creating table 'TSOGroupSet'
CREATE TABLE [dbo].[TSOGroupSet] (
    [TSOGroup_ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Amount] float  NOT NULL,
    [Type] tinyint  NOT NULL,
    [Visible] bit  NOT NULL
);
GO

-- Creating table 'TSOSet'
CREATE TABLE [dbo].[TSOSet] (
    [TSO_ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Visible] bit  NOT NULL,
    [TSOGroup_ID] int  NOT NULL
);
GO

-- Creating table 'PKPSet'
CREATE TABLE [dbo].[PKPSet] (
    [PKP_ID] int IDENTITY(1,1) NOT NULL,
    [Name] int  NOT NULL,
    [Serial] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Modules] varbinary(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Amount] float  NOT NULL
);
GO

-- Creating table 'EquipmentSet'
CREATE TABLE [dbo].[EquipmentSet] (
    [Equipment_ID] int IDENTITY(1,1) NOT NULL,
    [Data] varbinary(max)  NOT NULL,
    [Amount] float  NOT NULL
);
GO

-- Creating table 'LimbSet'
CREATE TABLE [dbo].[LimbSet] (
    [Limb_ID] int IDENTITY(1,1) NOT NULL,
    [Number] tinyint  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Data] varbinary(max)  NOT NULL,
    [Equipment_ID] int  NOT NULL
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

-- Creating primary key on [TSOGroup_ID] in table 'TSOGroupSet'
ALTER TABLE [dbo].[TSOGroupSet]
ADD CONSTRAINT [PK_TSOGroupSet]
    PRIMARY KEY CLUSTERED ([TSOGroup_ID] ASC);
GO

-- Creating primary key on [TSO_ID] in table 'TSOSet'
ALTER TABLE [dbo].[TSOSet]
ADD CONSTRAINT [PK_TSOSet]
    PRIMARY KEY CLUSTERED ([TSO_ID] ASC);
GO

-- Creating primary key on [PKP_ID] in table 'PKPSet'
ALTER TABLE [dbo].[PKPSet]
ADD CONSTRAINT [PK_PKPSet]
    PRIMARY KEY CLUSTERED ([PKP_ID] ASC);
GO

-- Creating primary key on [Equipment_ID] in table 'EquipmentSet'
ALTER TABLE [dbo].[EquipmentSet]
ADD CONSTRAINT [PK_EquipmentSet]
    PRIMARY KEY CLUSTERED ([Equipment_ID] ASC);
GO

-- Creating primary key on [Limb_ID] in table 'LimbSet'
ALTER TABLE [dbo].[LimbSet]
ADD CONSTRAINT [PK_LimbSet]
    PRIMARY KEY CLUSTERED ([Limb_ID] ASC);
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

-- Creating foreign key on [TSOGroup_ID] in table 'TSOSet'
ALTER TABLE [dbo].[TSOSet]
ADD CONSTRAINT [FK_TSOGroupTSO]
    FOREIGN KEY ([TSOGroup_ID])
    REFERENCES [dbo].[TSOGroupSet]
        ([TSOGroup_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TSOGroupTSO'
CREATE INDEX [IX_FK_TSOGroupTSO]
ON [dbo].[TSOSet]
    ([TSOGroup_ID]);
GO

-- Creating foreign key on [Equipment_ID] in table 'LimbSet'
ALTER TABLE [dbo].[LimbSet]
ADD CONSTRAINT [FK_EquipmentLimb]
    FOREIGN KEY ([Equipment_ID])
    REFERENCES [dbo].[EquipmentSet]
        ([Equipment_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EquipmentLimb'
CREATE INDEX [IX_FK_EquipmentLimb]
ON [dbo].[LimbSet]
    ([Equipment_ID]);
GO

-- Creating foreign key on [PKP_ID] in table 'CardsSet'
ALTER TABLE [dbo].[CardsSet]
ADD CONSTRAINT [FK_PKPCards]
    FOREIGN KEY ([PKP_ID])
    REFERENCES [dbo].[PKPSet]
        ([PKP_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PKPCards'
CREATE INDEX [IX_FK_PKPCards]
ON [dbo].[CardsSet]
    ([PKP_ID]);
GO

-- Creating foreign key on [Equipment_ID] in table 'CardsSet'
ALTER TABLE [dbo].[CardsSet]
ADD CONSTRAINT [FK_EquipmentCards]
    FOREIGN KEY ([Equipment_ID])
    REFERENCES [dbo].[EquipmentSet]
        ([Equipment_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EquipmentCards'
CREATE INDEX [IX_FK_EquipmentCards]
ON [dbo].[CardsSet]
    ([Equipment_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------