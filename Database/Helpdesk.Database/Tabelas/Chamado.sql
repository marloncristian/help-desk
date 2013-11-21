CREATE TABLE [dbo].[Chamado]
(
	[Codigo] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Status] INT NOT NULL, 
    [Abertura] DATETIME NOT NULL, 
    [Assunto] NVARCHAR(250) NOT NULL, 
    [Problema] TEXT NOT NULL, 
    [Chaves] TEXT NULL, 
    [Solucao] INT NULL, 
    [Usuario] INT NULL, 
    CONSTRAINT [FK_Chamado_Solucao] FOREIGN KEY (Solucao) REFERENCES [Solucao]([Codigo]), 
    CONSTRAINT [FK_Chamado_Usuario] FOREIGN KEY (Usuario) REFERENCES [Usuario]([Codigo])
)
