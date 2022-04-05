CREATE DATABASE Pantry

USE Pantry

CREATE TABLE Pantries
(
 PantryID int IDENTITY (1,1) PRIMARY KEY,
 PantryDesc nvarchar(50)
);

CREATE TABLE Packages
(
 PackageID int IDENTITY (1,1) PRIMARY KEY,
 PackageDesc nvarchar(50),
 PackageInteg int NOT NULL,
 PantryID int NOT NULL FOREIGN KEY REFERENCES Pantries(PantryID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Products
( 
 ProductID int IDENTITY (1,1) PRIMARY KEY,
 ProductDesc nvarchar(50),
 PackageID int NOT NULL FOREIGN KEY REFERENCES Packages(PackageID) ON DELETE CASCADE ON UPDATE CASCADE
);