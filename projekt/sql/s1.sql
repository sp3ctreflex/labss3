-- 1. Kierunek
CREATE TABLE Kierunek (
    KierunekId INT IDENTITY(1,1) PRIMARY KEY,
    Nazwa NVARCHAR(50) NOT NULL
);

-- 2. Grupa
CREATE TABLE Grupa (
    GrupaId INT IDENTITY(1,1) PRIMARY KEY,
    KierunekId INT NOT NULL,
    Nazwa NVARCHAR(20) NOT NULL,
    FOREIGN KEY (KierunekId) REFERENCES Kierunek(KierunekId)
);

-- 3. Prowadzacy
CREATE TABLE Prowadzacy (
    ProwadzacyId INT IDENTITY(1,1) PRIMARY KEY,
    Imie NVARCHAR(50) NOT NULL,
    Nazwisko NVARCHAR(50) NOT NULL
);

-- 4. Sala
CREATE TABLE Sala (
    SalaId INT IDENTITY(1,1) PRIMARY KEY,
    Nazwa NVARCHAR(20) NOT NULL,
    Pojemnosc INT NOT NULL
);

-- 5. Przedmiot
CREATE TABLE Przedmiot (
    PrzedmiotId INT IDENTITY(1,1) PRIMARY KEY,
    KierunekId INT NOT NULL,
    Nazwa NVARCHAR(100) NOT NULL,
    FOREIGN KEY (KierunekId) REFERENCES Kierunek(KierunekId)
);

-- 6. FormaZajec
CREATE TABLE FormaZajec (
    FormaZajecId INT IDENTITY(1,1) PRIMARY KEY,
    Nazwa NVARCHAR(20) NOT NULL -- np. "Laboratorium", "Wyklad", "Projekt"
);

-- 7. Zajecia
CREATE TABLE Zajecia (
    ZajeciaId INT IDENTITY(1,1) PRIMARY KEY,
    PrzedmiotId INT NOT NULL,
    ProwadzacyId INT NOT NULL,
    SalaId INT NOT NULL,
    FormaZajecId INT NOT NULL,
    DzienTygodnia NVARCHAR(20) NOT NULL,
    GodzinaRozpoczecia TIME NOT NULL,
    GodzinaZakonczenia TIME NOT NULL,
    FOREIGN KEY (PrzedmiotId) REFERENCES Przedmiot(PrzedmiotId),
    FOREIGN KEY (ProwadzacyId) REFERENCES Prowadzacy(ProwadzacyId),
    FOREIGN KEY (SalaId) REFERENCES Sala(SalaId),
    FOREIGN KEY (FormaZajecId) REFERENCES FormaZajec(FormaZajecId)
);

-- 8. Zajecia_Grupy
CREATE TABLE Zajecia_Grupy (
    ZajeciaGrupyId INT IDENTITY(1,1) PRIMARY KEY,
    ZajeciaId INT NOT NULL,
    GrupaId INT NOT NULL,
    FOREIGN KEY (ZajeciaId) REFERENCES Zajecia(ZajeciaId),
    FOREIGN KEY (GrupaId) REFERENCES Grupa(GrupaId)
);