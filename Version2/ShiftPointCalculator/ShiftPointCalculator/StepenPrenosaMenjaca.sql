CREATE TABLE dbo.StepenPrenosaMenjaca
(
	Id int IDENTITY(1,1) NOT NULL,
	VoziloId int NOT NULL,
	RedniBroj int NOT NULL,
	PrenosniOdnos decimal(4, 3) NOT NULL,
 CONSTRAINT PK_StepenPrenosaMenjaca PRIMARY KEY CLUSTERED 
(
	Id ASC
)
)
GO
CREATE UNIQUE INDEX IX_StepenPrenosaMenjaca_UNIQUE ON dbo.StepenPrenosaMenjaca (VoziloId, RedniBroj)
GO
ALTER TABLE dbo.StepenPrenosaMenjaca ADD CONSTRAINT FK_StepenPrenosaMenjaca_Vozilo FOREIGN KEY(VoziloId)
REFERENCES dbo.Vozilo (Id)
GO
ALTER TABLE dbo.StepenPrenosaMenjaca ADD CONSTRAINT CK_StepenPrenosaMenjaca_RedniBroj CHECK
(
	RedniBroj IN (1, 2, 3, 4, 5, 6) 
)
GO