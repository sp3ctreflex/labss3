-- Kolizje sal
SELECT 
    z1.SalaId, s.Nazwa AS Sala,
    z1.DzienTygodnia,
    z1.ZajeciaId AS Zajecia1,
    z2.ZajeciaId AS Zajecia2,
    z1.GodzinaRozpoczecia AS Start1, z1.GodzinaZakonczenia AS End1,
    z2.GodzinaRozpoczecia AS Start2, z2.GodzinaZakonczenia AS End2
FROM Zajecia z1
JOIN Zajecia z2
    ON z1.SalaId = z2.SalaId
    AND z1.DzienTygodnia = z2.DzienTygodnia
    AND z1.ZajeciaId < z2.ZajeciaId
    AND z1.GodzinaRozpoczecia < z2.GodzinaZakonczenia
    AND z1.GodzinaZakonczenia > z2.GodzinaRozpoczecia
JOIN Sala s ON s.SalaId = z1.SalaId
ORDER BY z1.SalaId, z1.DzienTygodnia;

-- Kolizje grup
SELECT 
    zg1.GrupaId, g.Nazwa AS Grupa,
    z1.DzienTygodnia,
    z1.ZajeciaId AS Zajecia1,
    z2.ZajeciaId AS Zajecia2,
    z1.GodzinaRozpoczecia AS Start1, z1.GodzinaZakonczenia AS End1,
    z2.GodzinaRozpoczecia AS Start2, z2.GodzinaZakonczenia AS End2
FROM Zajecia_Grupy zg1
JOIN Zajecia_Grupy zg2
    ON zg1.GrupaId = zg2.GrupaId
    AND zg1.ZajeciaId < zg2.ZajeciaId
JOIN Zajecia z1 ON z1.ZajeciaId = zg1.ZajeciaId
JOIN Zajecia z2 ON z2.ZajeciaId = zg2.ZajeciaId
JOIN Grupa g ON g.GrupaId = zg1.GrupaId
WHERE z1.DzienTygodnia = z2.DzienTygodnia
AND z1.GodzinaRozpoczecia < z2.GodzinaZakonczenia
AND z1.GodzinaZakonczenia > z2.GodzinaRozpoczecia
ORDER BY zg1.GrupaId, z1.DzienTygodnia;