CREATE TABLE [dbo].[Questao]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Pergunta] NVARCHAR(MAX) NOT NULL, 
    [Resposta] NVARCHAR(MAX) NOT NULL, 
    [PerguntadoPor] INT NULL, 
    [RespondidoPor] INT NULL
)
