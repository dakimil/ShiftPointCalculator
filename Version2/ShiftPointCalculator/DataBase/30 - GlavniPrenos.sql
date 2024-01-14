CREATE TABLE dbo.GlavniPrenos(
	Id INT IDENTITY(1,1) NOT NULL,
	VoziloId INT NOT NULL,
	PrenosniOdnos DECIMAL(4, 3) NOT NULL,
	CONSTRAINT PK_PrenosniOdnosUDiferncijalu PRIMARY KEY CLUSTERED(
		Id ASC
	)
)
GO

CREATE UNIQUE INDEX IX_GlavniPrenos_UNIQUE ON dbo.GlavniPrenos(VoziloId)
GO

ALTER TABLE dbo.GlavniPrenos ADD CONSTRAINT FK_GlavniPrenos_Vozilo FOREIGN KEY(VoziloId)
REFERENCES dbo.Vozilo (Id)
GO