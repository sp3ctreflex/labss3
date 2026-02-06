-- Kierunki
INSERT INTO Kierunek (Nazwa) VALUES 
('6IIZ'), -- Informatyka zaocznie 
('5IIS'),
('4IMS'), -- Matematyka stacjonarnie 
('4IMZ');

-- Grupy
INSERT INTO Grupa (KierunekId, Nazwa) VALUES 
-- 6IIZ
(1,'1'),(1,'2'),
-- 5IIS
(2,'1'),(2,'2'),
-- 4IMS
(3,'1'),(3,'2'),
-- 4IMZ
(4,'1'),(4,'2');

-- Prowadzacy
INSERT INTO Prowadzacy (Imie, Nazwisko) VALUES
('Jan','Kowalski'),
('Anna','Nowak'),
('Piotr','Zielinski'),
('Maria','Wójcik'),
('Tomasz','Lewandowski');

-- Sale
INSERT INTO Sala (Nazwa, Pojemnosc) VALUES
('A101',30),('A102',25),('B201',30),('B202',25),
('C301',35),('C302',30),('D401',20),('D402',20),
('E501',40),('E502',35),('F601',25),('F602',25);

-- Przedmioty
INSERT INTO Przedmiot (KierunekId, Nazwa) VALUES
-- 6IIZ
(1,'Analiza Matematyczna'),
(1,'Programowanie Obiektowe'),
-- 5IIS
(2,'Algorytmy i Struktury Danych'),
(2,'Bazy Danych'),
-- 4IMS
(3,'Rachunek Różniczkowy'),
(3,'Algebra Linearna'),
-- 4IMZ
(4,'Statystyka'),
(4,'Analiza Matematyczna');

-- Forma zajęć
INSERT INTO FormaZajec (Nazwa) VALUES 
('Wyklad'),('Laboratorium'),('Projekt');