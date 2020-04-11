
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/05/2020 16:04:25
-- Generated from EDMX file: C:\Users\Valentin\Desktop\Photo_Classifier\ClassLibrary1\MODEL\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Photo_Classifier];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Pics_Vids]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pics_Vids];
GO
IF OBJECT_ID(N'[dbo].[Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tags];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Pics_Vids'
CREATE TABLE [dbo].[Pics_Vids] (
    [Unique_Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Full_Path] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Size] float  NOT NULL,
    [Date_Created] datetime  NOT NULL,
    [Date_Modified] datetime  NOT NULL,
    [Values] nvarchar(max)  NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [Tag_Id] int IDENTITY(1,1) NOT NULL,
    [Tag_Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Unique_Id] in table 'Pics_Vids'
ALTER TABLE [dbo].[Pics_Vids]
ADD CONSTRAINT [PK_Pics_Vids]
    PRIMARY KEY CLUSTERED ([Unique_Id] ASC);
GO

-- Creating primary key on [Tag_Id] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([Tag_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------