CREATE TABLE dbo.PrenosniOdnosUDiferncijalu(
	Id INT IDENTITY(1,1) NOT NULL,
	VoziloId INT NOT NULL,
	PrenosniOdnos DECIMAL(4, 3) NOT NULL,
	CONSTRAINT PK_PrenosniOdnosUDiferncijalu PRIMARY KEY CLUSTERED(
		Id ASC
	)
)
GO

CREATE UNIQUE INDEX IX_PrenosniOdnosUDiferencijalu_UNIQUE ON dbo.PrenosniOdnosUDiferncijalu(VoziloId)
GO

ALTER TABLE dbo.PrenosniOdnosUDiferencijalu ADD CONSTRAINT FK_PrenosniOdnosUDiferencijalu_Vozilo FOREIGN KEY(VoziloId)
REFERENCES dbo.Vozilo (Id)
GO