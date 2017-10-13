
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/13/2017 09:08:59
-- Generated from EDMX file: C:\Users\Work\documents\visual studio 2013\Projects\OSSOBase\Client\Model\EF\DataModel.edmx
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
IF OBJECT_ID(N'[dbo].[FK_TSOGroupTSO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TSOSet] DROP CONSTRAINT [FK_TSOGroupTSO];
GO
IF OBJECT_ID(N'[dbo].[FK_PKPPKPModels]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PKPSet] DROP CONSTRAINT [FK_PKPPKPModels];
GO
IF OBJECT_ID(N'[dbo].[FK_ModulesPKP_Modules]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PKP_ModulesSet] DROP CONSTRAINT [FK_ModulesPKP_Modules];
GO
IF OBJECT_ID(N'[dbo].[FK_PKPPKP_Modules]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PKP_ModulesSet] DROP CONSTRAINT [FK_PKPPKP_Modules];
GO
IF OBJECT_ID(N'[dbo].[FK_TSOEquipment_TSO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cards_TSOSet] DROP CONSTRAINT [FK_TSOEquipment_TSO];
GO
IF OBJECT_ID(N'[dbo].[FK_CardsBranch]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BranchSet] DROP CONSTRAINT [FK_CardsBranch];
GO
IF OBJECT_ID(N'[dbo].[FK_CardsCards_TSO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cards_TSOSet] DROP CONSTRAINT [FK_CardsCards_TSO];
GO
IF OBJECT_ID(N'[dbo].[FK_CardsObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ObjectSet] DROP CONSTRAINT [FK_CardsObject];
GO
IF OBJECT_ID(N'[dbo].[FK_CardsPKP]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PKPSet] DROP CONSTRAINT [FK_CardsPKP];
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
IF OBJECT_ID(N'[dbo].[BranchSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BranchSet];
GO
IF OBJECT_ID(N'[dbo].[PKPModelsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PKPModelsSet];
GO
IF OBJECT_ID(N'[dbo].[ModulesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModulesSet];
GO
IF OBJECT_ID(N'[dbo].[PKP_ModulesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PKP_ModulesSet];
GO
IF OBJECT_ID(N'[dbo].[Cards_TSOSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cards_TSOSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CardsSet'
CREATE TABLE [dbo].[CardsSet] (
    [Cards_ID] int IDENTITY(1,1) NOT NULL,
    [Users_ID] int  NOT NULL,
    [MakeDate] datetime  NOT NULL,
    [Amount] float  NOT NULL,
    [OwnerView] nvarchar(max)  NOT NULL,
    [ObjectView] nvarchar(max)  NOT NULL,
    [AddressView] nvarchar(max)  NOT NULL,
    [PKPView] nvarchar(max)  NOT NULL
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
    [Streets_ID] int  NOT NULL,
    [Cards_ID] int  NOT NULL
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
    [Visible] bit  NOT NULL
);
GO

-- Creating table 'TSOSet'
CREATE TABLE [dbo].[TSOSet] (
    [TSO_ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Visible] bit  NOT NULL,
    [Group_ID] int  NOT NULL
);
GO

-- Creating table 'PKPSet'
CREATE TABLE [dbo].[PKPSet] (
    [PKP_ID] int IDENTITY(1,1) NOT NULL,
    [Serial] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [UUAmount] float  NOT NULL,
    [PKPModels_ID] int  NOT NULL,
    [Cards_ID] int  NOT NULL
);
GO

-- Creating table 'BranchSet'
CREATE TABLE [dbo].[BranchSet] (
    [Branch_ID] int IDENTITY(1,1) NOT NULL,
    [Number] tinyint  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Data] varbinary(max)  NOT NULL,
    [Cards_ID] int  NOT NULL
);
GO

-- Creating table 'PKPModelsSet'
CREATE TABLE [dbo].[PKPModelsSet] (
    [PKPModels_ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Visible] bit  NOT NULL
);
GO

-- Creating table 'ModulesSet'
CREATE TABLE [dbo].[ModulesSet] (
    [Modules_ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Visible] bit  NOT NULL,
    [UUCount] float  NOT NULL
);
GO

-- Creating table 'PKP_ModulesSet'
CREATE TABLE [dbo].[PKP_ModulesSet] (
    [PKP_Modules_ID] int IDENTITY(1,1) NOT NULL,
    [Modules_ID] int  NOT NULL,
    [PKP_ID] int  NOT NULL,
    [Count] int  NOT NULL
);
GO

-- Creating table 'Cards_TSOSet'
CREATE TABLE [dbo].[Cards_TSOSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TSO_ID] int  NOT NULL,
    [Number] tinyint  NOT NULL,
    [Cards_ID] int  NOT NULL
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

-- Creating primary key on [Branch_ID] in table 'BranchSet'
ALTER TABLE [dbo].[BranchSet]
ADD CONSTRAINT [PK_BranchSet]
    PRIMARY KEY CLUSTERED ([Branch_ID] ASC);
GO

-- Creating primary key on [PKPModels_ID] in table 'PKPModelsSet'
ALTER TABLE [dbo].[PKPModelsSet]
ADD CONSTRAINT [PK_PKPModelsSet]
    PRIMARY KEY CLUSTERED ([PKPModels_ID] ASC);
GO

-- Creating primary key on [Modules_ID] in table 'ModulesSet'
ALTER TABLE [dbo].[ModulesSet]
ADD CONSTRAINT [PK_ModulesSet]
    PRIMARY KEY CLUSTERED ([Modules_ID] ASC);
GO

-- Creating primary key on [PKP_Modules_ID] in table 'PKP_ModulesSet'
ALTER TABLE [dbo].[PKP_ModulesSet]
ADD CONSTRAINT [PK_PKP_ModulesSet]
    PRIMARY KEY CLUSTERED ([PKP_Modules_ID] ASC);
GO

-- Creating primary key on [Id] in table 'Cards_TSOSet'
ALTER TABLE [dbo].[Cards_TSOSet]
ADD CONSTRAINT [PK_Cards_TSOSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
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

-- Creating foreign key on [Group_ID] in table 'TSOSet'
ALTER TABLE [dbo].[TSOSet]
ADD CONSTRAINT [FK_TSOGroupTSO]
    FOREIGN KEY ([Group_ID])
    REFERENCES [dbo].[TSOGroupSet]
        ([TSOGroup_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TSOGroupTSO'
CREATE INDEX [IX_FK_TSOGroupTSO]
ON [dbo].[TSOSet]
    ([Group_ID]);
GO

-- Creating foreign key on [PKPModels_ID] in table 'PKPSet'
ALTER TABLE [dbo].[PKPSet]
ADD CONSTRAINT [FK_PKPPKPModels]
    FOREIGN KEY ([PKPModels_ID])
    REFERENCES [dbo].[PKPModelsSet]
        ([PKPModels_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PKPPKPModels'
CREATE INDEX [IX_FK_PKPPKPModels]
ON [dbo].[PKPSet]
    ([PKPModels_ID]);
GO

-- Creating foreign key on [Modules_ID] in table 'PKP_ModulesSet'
ALTER TABLE [dbo].[PKP_ModulesSet]
ADD CONSTRAINT [FK_ModulesPKP_Modules]
    FOREIGN KEY ([Modules_ID])
    REFERENCES [dbo].[ModulesSet]
        ([Modules_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ModulesPKP_Modules'
CREATE INDEX [IX_FK_ModulesPKP_Modules]
ON [dbo].[PKP_ModulesSet]
    ([Modules_ID]);
GO

-- Creating foreign key on [PKP_ID] in table 'PKP_ModulesSet'
ALTER TABLE [dbo].[PKP_ModulesSet]
ADD CONSTRAINT [FK_PKPPKP_Modules]
    FOREIGN KEY ([PKP_ID])
    REFERENCES [dbo].[PKPSet]
        ([PKP_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PKPPKP_Modules'
CREATE INDEX [IX_FK_PKPPKP_Modules]
ON [dbo].[PKP_ModulesSet]
    ([PKP_ID]);
GO

-- Creating foreign key on [TSO_ID] in table 'Cards_TSOSet'
ALTER TABLE [dbo].[Cards_TSOSet]
ADD CONSTRAINT [FK_TSOEquipment_TSO]
    FOREIGN KEY ([TSO_ID])
    REFERENCES [dbo].[TSOSet]
        ([TSO_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TSOEquipment_TSO'
CREATE INDEX [IX_FK_TSOEquipment_TSO]
ON [dbo].[Cards_TSOSet]
    ([TSO_ID]);
GO

-- Creating foreign key on [Cards_ID] in table 'BranchSet'
ALTER TABLE [dbo].[BranchSet]
ADD CONSTRAINT [FK_CardsBranch]
    FOREIGN KEY ([Cards_ID])
    REFERENCES [dbo].[CardsSet]
        ([Cards_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CardsBranch'
CREATE INDEX [IX_FK_CardsBranch]
ON [dbo].[BranchSet]
    ([Cards_ID]);
GO

-- Creating foreign key on [Cards_ID] in table 'Cards_TSOSet'
ALTER TABLE [dbo].[Cards_TSOSet]
ADD CONSTRAINT [FK_CardsCards_TSO]
    FOREIGN KEY ([Cards_ID])
    REFERENCES [dbo].[CardsSet]
        ([Cards_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CardsCards_TSO'
CREATE INDEX [IX_FK_CardsCards_TSO]
ON [dbo].[Cards_TSOSet]
    ([Cards_ID]);
GO

-- Creating foreign key on [Cards_ID] in table 'ObjectSet'
ALTER TABLE [dbo].[ObjectSet]
ADD CONSTRAINT [FK_CardsObject]
    FOREIGN KEY ([Cards_ID])
    REFERENCES [dbo].[CardsSet]
        ([Cards_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CardsObject'
CREATE INDEX [IX_FK_CardsObject]
ON [dbo].[ObjectSet]
    ([Cards_ID]);
GO

-- Creating foreign key on [Cards_ID] in table 'PKPSet'
ALTER TABLE [dbo].[PKPSet]
ADD CONSTRAINT [FK_CardsPKP]
    FOREIGN KEY ([Cards_ID])
    REFERENCES [dbo].[CardsSet]
        ([Cards_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CardsPKP'
CREATE INDEX [IX_FK_CardsPKP]
ON [dbo].[PKPSet]
    ([Cards_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------