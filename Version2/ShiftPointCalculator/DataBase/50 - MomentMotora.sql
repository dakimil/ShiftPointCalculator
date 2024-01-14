CREATE TABLE dbo.MomentMotora(
	Id INT IDENTITY(1,1) NOT NULL,
	VoziloID INT NOT NULL,
	BrojObrtaja INT NOT NULL,
	MomentPriObrtajima DECIMAL(4,1) NOT NULL,
	CONSTRAINT PK_MomentMotora PRIMARY KEY CLUSTERED(
		Id ASC
	)
)
GO

CREATE UNIQUE INDEX IX_MomentMotora_UNIQUE ON dbo.MomentMotora (VoziloId, BrojObrtaja)
GO

ALTER TABLE dbo.MomentMotora ADD CONSTRAINT FK_MomentMotora_Vozilo FOREIGN KEY(VoziloID)
REFERENCES dbo.Vozilo (Id)
GO