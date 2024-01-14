CREATE TABLE dbo.PoluprecnikTocka(
	Id INT IDENTITY(1,1) NOT NULL,
	VoziloId INT NOT NULL,
	Poluprecnik INT NOT NULL,
	CONSTRAINT PK_PoluprecnikTocka PRIMARY KEY CLUSTERED(
		Id ASC
	)
)
GO

CREATE UNIQUE INDEX IX_PoluprecnikTocka_UNIQUE ON dbo.PoluprecnikTocka (VoziloId)
GO

ALTER TABLE dbo.PoluprecnikTocka ADD CONSTRAINT FK_PoluprecnikTocka_Vozilo FOREIGN KEY(VoziloId)
REFERENCES dbo.Vozilo (Id)
GO