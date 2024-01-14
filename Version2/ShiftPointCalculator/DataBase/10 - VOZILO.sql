CREATE TABLE dbo.Vozilo(
	Id INT IDENTITY(1,1) NOT NULL, --SQL SERVER SAM DODELJUJE SLEDECI BROJ
	NazivVozila NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_VOZILO PRIMARY KEY CLUSTERED(
		Id ASC
	)
) ON [PRIMARY] --DEFAULT OPCIJA, IMA VEZE SA RASPOREDJIVANJEM FAJLOVA PO DISKOVIMA
GO

CREATE UNIQUE INDEX IX_Vozilo_Unique ON dbo.Vozilo (NazivVozila)
GO