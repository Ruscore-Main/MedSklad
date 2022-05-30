
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/26/2022 20:29:02
-- Generated from EDMX file: C:\Users\Ruslan\source\repos\MedStorage\MedStorage\DataBaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [C:\USERS\RUSLAN\DOCUMENTS\MEDSTORAGEDB.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProductStorage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductSet] DROP CONSTRAINT [FK_ProductStorage];
GO
IF OBJECT_ID(N'[dbo].[FK_NacladnayaNacladnayaItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NacladnayaItemSet] DROP CONSTRAINT [FK_NacladnayaNacladnayaItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductNacladnayaItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NacladnayaItemSet] DROP CONSTRAINT [FK_ProductNacladnayaItem];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeNacladnaya]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NacladnayaSet] DROP CONSTRAINT [FK_EmployeeNacladnaya];
GO
IF OBJECT_ID(N'[dbo].[FK_NacladnayaCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerSet] DROP CONSTRAINT [FK_NacladnayaCustomer];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSet];
GO
IF OBJECT_ID(N'[dbo].[StorageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StorageSet];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSet];
GO
IF OBJECT_ID(N'[dbo].[CustomerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerSet];
GO
IF OBJECT_ID(N'[dbo].[NacladnayaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NacladnayaSet];
GO
IF OBJECT_ID(N'[dbo].[NacladnayaItemSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NacladnayaItemSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] int  NOT NULL,
    [ProductWeigth] int  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Manufacturer] nvarchar(max)  NOT NULL,
    [ExpirationDate] nvarchar(max)  NOT NULL,
    [StoragePlace] nvarchar(max)  NOT NULL,
    [Amount] int  NOT NULL,
    [Storage_Id] int  NULL
);
GO

-- Creating table 'StorageSet'
CREATE TABLE [dbo].[StorageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Capacity] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EmployeeSet'
CREATE TABLE [dbo].[EmployeeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NOT NULL,
    [Post] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CustomerSet'
CREATE TABLE [dbo].[CustomerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Nacladnaya_Id] int  NULL
);
GO

-- Creating table 'NacladnayaSet'
CREATE TABLE [dbo].[NacladnayaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderDate] nvarchar(max)  NULL,
    [Sum] int  NULL,
    [EmployeeId] int  NULL
);
GO

-- Creating table 'NacladnayaItemSet'
CREATE TABLE [dbo].[NacladnayaItemSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Amount] int  NOT NULL,
    [NacladnayaId] int  NULL,
    [ProductId] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StorageSet'
ALTER TABLE [dbo].[StorageSet]
ADD CONSTRAINT [PK_StorageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [PK_EmployeeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [PK_CustomerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NacladnayaSet'
ALTER TABLE [dbo].[NacladnayaSet]
ADD CONSTRAINT [PK_NacladnayaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NacladnayaItemSet'
ALTER TABLE [dbo].[NacladnayaItemSet]
ADD CONSTRAINT [PK_NacladnayaItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Storage_Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [FK_ProductStorage]
    FOREIGN KEY ([Storage_Id])
    REFERENCES [dbo].[StorageSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductStorage'
CREATE INDEX [IX_FK_ProductStorage]
ON [dbo].[ProductSet]
    ([Storage_Id]);
GO

-- Creating foreign key on [NacladnayaId] in table 'NacladnayaItemSet'
ALTER TABLE [dbo].[NacladnayaItemSet]
ADD CONSTRAINT [FK_NacladnayaNacladnayaItem]
    FOREIGN KEY ([NacladnayaId])
    REFERENCES [dbo].[NacladnayaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NacladnayaNacladnayaItem'
CREATE INDEX [IX_FK_NacladnayaNacladnayaItem]
ON [dbo].[NacladnayaItemSet]
    ([NacladnayaId]);
GO

-- Creating foreign key on [ProductId] in table 'NacladnayaItemSet'
ALTER TABLE [dbo].[NacladnayaItemSet]
ADD CONSTRAINT [FK_ProductNacladnayaItem]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductNacladnayaItem'
CREATE INDEX [IX_FK_ProductNacladnayaItem]
ON [dbo].[NacladnayaItemSet]
    ([ProductId]);
GO

-- Creating foreign key on [EmployeeId] in table 'NacladnayaSet'
ALTER TABLE [dbo].[NacladnayaSet]
ADD CONSTRAINT [FK_EmployeeNacladnaya]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeNacladnaya'
CREATE INDEX [IX_FK_EmployeeNacladnaya]
ON [dbo].[NacladnayaSet]
    ([EmployeeId]);
GO

-- Creating foreign key on [Nacladnaya_Id] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [FK_NacladnayaCustomer]
    FOREIGN KEY ([Nacladnaya_Id])
    REFERENCES [dbo].[NacladnayaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NacladnayaCustomer'
CREATE INDEX [IX_FK_NacladnayaCustomer]
ON [dbo].[CustomerSet]
    ([Nacladnaya_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------