CREATE TABLE [dbo].[Usuario]
(
	[Codigo] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nome] NVARCHAR(250) NOT NULL, 
    [Login] NVARCHAR(50) NOT NULL, 
    [Senha] NVARCHAR(50) NOT NULL, 
    [Tipo] INT NOT NULL
)
