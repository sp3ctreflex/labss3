-- Zajecia 6IIZ
INSERT INTO Zajecia (PrzedmiotId,ProwadzacyId,SalaId,FormaZajecId,DzienTygodnia,GodzinaRozpoczecia,GodzinaZakonczenia) VALUES
(1,1,1,1,'Piatek','08:00','09:30'),
(1,2,2,1,'Piatek','10:00','11:30'),
(2,3,3,3,'Sobota','08:00','09:30'),
(2,4,4,2,'Niedziela','09:00','10:30');

-- Zajecia 5IIS
INSERT INTO Zajecia (PrzedmiotId,ProwadzacyId,SalaId,FormaZajecId,DzienTygodnia,GodzinaRozpoczecia,GodzinaZakonczenia) VALUES
(3,1,5,1,'Poniedzialek','08:00','09:30'),
(4,2,6,2,'Poniedzialek','09:30','11:00'),
(3,3,5,1,'Wtorek','08:00','09:30'),
(4,4,6,3,'Wtorek','09:30','11:00'),
(3,1,5,1,'Sroda','08:00','09:30'),
(4,2,6,2,'Sroda','09:30','11:00'),
(3,3,5,3,'Czwartek','08:00','09:30'),
(4,4,6,2,'Czwartek','09:30','11:00'),
(3,1,5,1,'Piatek','08:00','09:30'),
(4,2,6,2,'Piatek','09:30','11:00');

-- Zajecia 4IMS
INSERT INTO Zajecia (PrzedmiotId,ProwadzacyId,SalaId,FormaZajecId,DzienTygodnia,GodzinaRozpoczecia,GodzinaZakonczenia) VALUES
(5,1,7,1,'Poniedzialek','10:00','11:30'),
(6,2,8,2,'Poniedzialek','11:30','13:00'),
(5,3,7,3,'Wtorek','10:00','11:30'),
(6,4,8,2,'Wtorek','11:30','13:00'),
(5,1,7,3,'Sroda','10:00','11:30'),
(6,2,8,2,'Sroda','11:30','13:00'),
(5,3,7,1,'Czwartek','10:00','11:30'),
(6,4,8,2,'Czwartek','11:30','13:00'),
(5,1,7,3,'Piatek','10:00','11:30'),
(6,2,8,2,'Piatek','11:30','13:00');

-- Zajecia 4IMZ
INSERT INTO Zajecia (PrzedmiotId,ProwadzacyId,SalaId,FormaZajecId,DzienTygodnia,GodzinaRozpoczecia,GodzinaZakonczenia) VALUES
(7,1,9,1,'Piatek','12:00','13:30'),
(8,2,10,1,'Piatek','14:00','15:30'),
(7,3,11,3,'Sobota','08:00','09:30'),
(8,4,12,2,'Niedziela','09:00','10:30');


INSERT INTO Zajecia_Grupy (ZajeciaId, GrupaId) VALUES
-- 6IIZ
(1,1),      
(2,2),      
(3,1),(3,2),
(4,1),(4,2),

-- 5IIS
(5,1),           
(6,1),(6,2),     
(7,2),           
(8,1),(8,2),     
(9,1),           
(10,2),          

-- 4IMS
(11,1),(11,2),
(12,1),(12,2),
(13,1),       
(14,1),(14,2),
(15,1),       
(16,1),(16,2),
(17,1),(17,2),
(18,2),       

-- 4IMZ
(19,1),          
(20,2),          
(21,1),(21,2),   
(22,1),(22,2),   
(23,1),(23,2),   
(24,1),          
(25,2),          
(26,1),          
(27,1),(27,2);   

-- usuwanie kolizji
DELETE FROM Zajecia_Grupy
WHERE ZajeciaId = 21;
DELETE FROM Zajecia
WHERE ZajeciaId = 21;
DELETE FROM Zajecia_Grupy
WHERE ZajeciaId = 13;
DELETE FROM Zajecia
WHERE ZajeciaId = 13;
DELETE FROM Zajecia_Grupy
WHERE ZajeciaId = 23;
DELETE FROM Zajecia
WHERE ZajeciaId = 23;
DELETE FROM Zajecia_Grupy
WHERE ZajeciaId = 27;
DELETE FROM Zajecia
WHERE ZajeciaId = 27;
DELETE FROM Zajecia_Grupy
WHERE ZajeciaId = 15;
DELETE FROM Zajecia
WHERE ZajeciaId = 15;
DELETE FROM Zajecia_Grupy
WHERE ZajeciaId = 17;
DELETE FROM Zajecia
WHERE ZajeciaId = 17;
DELETE FROM Zajecia_Grupy
WHERE ZajeciaId = 14;
DELETE FROM Zajecia
WHERE ZajeciaId = 14;