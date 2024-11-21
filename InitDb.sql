-- ConnectionString is "Server=.;Connection Timeout=30;Command Timeout=30;Persist Security Info=False;TrustServerCertificate=True;Integrated Security=True;Initial Catalog=FeedbackDb"
-- Please create a database named "FeedbackDb" before running this script
-- This script will create a table named "Feedbacks" in the "FeedbackDb" database

CREATE DATABASE FeedbackDb;
GO

USE FeedbackDb;
GO

CREATE TABLE Feedbacks (
    FeedbackID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100) NOT NULL,
    FeedbackMessage NVARCHAR(500) NOT NULL,
    DateSubmitted DATETIME DEFAULT GETDATE() NOT NULL
);
