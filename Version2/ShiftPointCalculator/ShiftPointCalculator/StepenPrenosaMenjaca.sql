CREATE TABLE dbo.StepenPrenosaMenjaca
(
	Id int IDENTITY(1,1) NOT NULL,
	VoziloId int NOT NULL,
	RedniBroj int NOT NULL,
	PrenosniOdnos decimal(4, 3) NOT NULL,
 CONSTRAINT PK_StepenPrenosaMenjaca PRIMARY KEY CLUSTERED 
(
	Id ASC
);