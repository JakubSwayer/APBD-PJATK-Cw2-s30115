using LinqConsoleLab.PL.Data;

namespace LinqConsoleLab.PL.Exercises;

public sealed class ZadaniaLinq
{
    /// <summary>
    /// Zadanie:
    /// Wyszukaj wszystkich studentów mieszkających w Warsaw.
    /// Zwróć numer indeksu, pełne imię i nazwisko oraz miasto.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko, Miasto
    /// FROM Studenci
    /// WHERE Miasto = 'Warsaw';
    /// </summary>
    public IEnumerable<string> Zadanie01_StudenciZWarszawy()
    {
        return DaneUczelni.Studenci.Where(student => student.Miasto.Equals("Warsaw")).Select(student => $"{student.Id} {student.Imie} {student.Nazwisko} {student.Miasto}");
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj listę adresów e-mail wszystkich studentów.
    /// Użyj projekcji, tak aby w wyniku nie zwracać całych obiektów.
    ///
    /// SQL:
    /// SELECT Email
    /// FROM Studenci;
    /// </summary>
    public IEnumerable<string> Zadanie02_AdresyEmailStudentow()
    {
        return DaneUczelni.Studenci.Select(student => student.Email);
    }

    /// <summary>
    /// Zadanie:
    /// Posortuj studentów alfabetycznie po nazwisku, a następnie po imieniu.
    /// Zwróć numer indeksu i pełne imię i nazwisko.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko
    /// FROM Studenci
    /// ORDER BY Nazwisko, Imie;
    /// </summary>
    public IEnumerable<string> Zadanie03_StudenciPosortowani()
    {
        return DaneUczelni.Studenci
            .OrderBy(student => student.Nazwisko)
            .ThenBy(student => student.Imie)
            .Select(student => $"{student.Id} {student.Imie} {student.Nazwisko}");
    }

    /// <summary>
    /// Zadanie:
    /// Znajdź pierwszy przedmiot z kategorii Analytics.
    /// Jeżeli taki przedmiot nie istnieje, zwróć komunikat tekstowy.
    ///
    /// SQL:
    /// SELECT TOP 1 Nazwa, DataStartu
    /// FROM Przedmioty
    /// WHERE Kategoria = 'Analytics';
    /// </summary>
    public IEnumerable<string> Zadanie04_PierwszyPrzedmiotAnalityczny()
    {
        // return DaneUczelni.Przedmioty.Select(item => item.Nazwa + item.DataStartu).
        //     FirstOrDefault(przedmiot => przedmiot.Kategoria == "Analytics");

        var result = DaneUczelni.Przedmioty
            .FirstOrDefault(przedmiot => przedmiot.Kategoria == "Analytics");
        
        if (result == null)
        {
            return new List<string> {"No object has been found"};
        }
        else
        {
            List<string> ans = new List<string>();
            ans.Add($"{result.Nazwa} + {result.DataStartu}");
            return ans;
        }

    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy w danych istnieje przynajmniej jeden nieaktywny zapis.
    /// Zwróć jedno zdanie z odpowiedzią True/False albo Tak/Nie.
    ///
    /// SQL:
    /// SELECT CASE WHEN EXISTS (
    ///     SELECT 1
    ///     FROM Zapisy
    ///     WHERE CzyAktywny = 0
    /// ) THEN 1 ELSE 0 END;
    /// </summary>
    public IEnumerable<string> Zadanie05_CzyIstniejeNieaktywneZapisanie()
    {
        var result = DaneUczelni.Zapisy.Any(zapis => zapis.CzyAktywny.Equals(false));
        return new List<string>(){$"{result}"};
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy każdy prowadzący ma uzupełnioną nazwę katedry.
    /// Warto użyć metody, która weryfikuje warunek dla całej kolekcji.
    ///
    /// SQL:
    /// SELECT CASE WHEN COUNT(*) = COUNT(Katedra)
    /// THEN 1 ELSE 0 END
    /// FROM Prowadzacy;
    /// </summary>
    public IEnumerable<string> Zadanie06_CzyWszyscyProwadzacyMajaKatedre()
    {
        var result = DaneUczelni.Prowadzacy.All(prowadzacy => !string.IsNullOrWhiteSpace(prowadzacy.Katedra));
        return new List<string>(){$"{result}"};
    }

    /// <summary>
    /// Zadanie:
    /// Policz, ile aktywnych zapisów znajduje się w systemie.
    ///
    /// SQL:
    /// SELECT COUNT(*)
    /// FROM Zapisy
    /// WHERE CzyAktywny = 1;
    /// </summary>
    public IEnumerable<string> Zadanie07_LiczbaAktywnychZapisow()
    {
        var result = DaneUczelni.Zapisy.Count(zapis => zapis.CzyAktywny.Equals(true));
        return new List<string>(){$"{result}"};
    }

    /// <summary>
    /// Zadanie:
    /// Pobierz listę unikalnych miast studentów i posortuj ją rosnąco.
    ///
    /// SQL:
    /// SELECT DISTINCT Miasto
    /// FROM Studenci
    /// ORDER BY Miasto;
    /// </summary>
    public IEnumerable<string> Zadanie08_UnikalneMiastaStudentow()
    {
        var result = DaneUczelni.Studenci.Select(student => student.Miasto).Distinct().OrderBy(s => s);
        return result;

    }

    /// <summary>
    /// Zadanie:
    /// Zwróć trzy najnowsze zapisy na przedmioty.
    /// W wyniku pokaż datę zapisu, identyfikator studenta i identyfikator przedmiotu.
    ///
    /// SQL:
    /// SELECT TOP 3 DataZapisu, StudentId, PrzedmiotId
    /// FROM Zapisy
    /// ORDER BY DataZapisu DESC;
    /// </summary>
    public IEnumerable<string> Zadanie09_TrzyNajnowszeZapisy()
    {
        var result = DaneUczelni.Zapisy.OrderByDescending(zapis => zapis.DataZapisu).Take(3)
            .Select(zapis => $"{zapis.DataZapisu} + {zapis.StudentId} + {zapis.PrzedmiotId}");
        return result;
    }

    /// <summary>
    /// Zadanie:
    /// Zaimplementuj prostą paginację dla listy przedmiotów.
    /// Załóż stronę o rozmiarze 2 i zwróć drugą stronę danych.
    ///
    /// SQL:
    /// SELECT Nazwa, Kategoria
    /// FROM Przedmioty
    /// ORDER BY Nazwa
    /// OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY;
    /// </summary>
    public IEnumerable<string> Zadanie10_DrugaStronaPrzedmiotow()
    {
        var result = DaneUczelni.Przedmioty
            .OrderBy(przedmiot => przedmiot.Nazwa)
            .Skip(2).Take(2)
            .Select(przedmiot => $"{przedmiot.Nazwa} {przedmiot.Kategoria}");
        return result;
    }

    /// <summary>
    /// Zadanie:
    /// Połącz studentów z zapisami po StudentId.
    /// Zwróć pełne imię i nazwisko studenta oraz datę zapisu.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, z.DataZapisu
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId;
    /// </summary>
    public IEnumerable<string> Zadanie11_PolaczStudentowIZapisy()
    {
        var result = DaneUczelni.Studenci.Join(
            DaneUczelni.Zapisy,
            student => student.Id,
            zapis => zapis.StudentId,
            (stu, zap) => $"{stu.Imie} {stu.Nazwisko} {zap.DataZapisu}" 
        );
    
        return result;
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj wszystkie pary student-przedmiot na podstawie zapisów.
    /// Użyj podejścia, które pozwoli spłaszczyć dane do jednej sekwencji wyników.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, p.Nazwa
    /// FROM Zapisy z
    /// JOIN Studenci s ON s.Id = z.StudentId
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId;
    /// </summary>
    public IEnumerable<string> Zadanie12_ParyStudentPrzedmiot()
    {
        var result = DaneUczelni.Studenci.Join(
            DaneUczelni.Zapisy,
            student => student.Id,
            zapis => zapis.StudentId,
            (s, z) => new { Student = s, PrzedmiotID = z.PrzedmiotId }
        ).Join(
            DaneUczelni.Przedmioty,
            studentZapis => studentZapis.PrzedmiotID,
            przedmiot => przedmiot.Id,
            ((studentZapis, przedmiot) =>
                $"{studentZapis.Student.Imie} {studentZapis.Student.Nazwisko} {przedmiot.Nazwa}")
        );

        return result;
    }

    /// <summary>
    /// Zadanie:
    /// Pogrupuj zapisy według przedmiotu i zwróć nazwę przedmiotu oraz liczbę zapisów.
    ///
    /// SQL:
    /// SELECT p.Nazwa, COUNT(*)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie13_GrupowanieZapisowWedlugPrzedmiotu()
    {
        var result = DaneUczelni.Zapisy.Join(
                DaneUczelni.Przedmioty,
                zapis => zapis.PrzedmiotId,
                przedmiot => przedmiot.Id,
                (zap, przed) => new { Zapis = zap, nazwaPrzedmiotu = przed.Nazwa })
            .GroupBy(zapisPrzedmiot => zapisPrzedmiot.nazwaPrzedmiotu)
            .Select(grupa => $"{grupa.Key} {grupa.Count()}");
        return result;
    }

    /// <summary>
    /// Zadanie:
    /// Oblicz średnią ocenę końcową dla każdego przedmiotu.
    /// Pomiń rekordy, w których ocena końcowa ma wartość null.
    ///
    /// SQL:
    /// SELECT p.Nazwa, AVG(z.OcenaKoncowa)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie14_SredniaOcenaNaPrzedmiot()
    {
        var result = DaneUczelni.Zapisy
            .Where(z => z.OcenaKoncowa != null)
            .Join(
                DaneUczelni.Przedmioty,
                z => z.PrzedmiotId,
                p => p.Id,
                (z, p) => new { p.Nazwa, z.OcenaKoncowa }
            )
            .GroupBy(x => x.Nazwa)
            .Select(g => $"{g.Key}: {g.Average(x => x.OcenaKoncowa):F2}");

        return result;
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego prowadzącego policz liczbę przypisanych przedmiotów.
    /// W wyniku zwróć pełne imię i nazwisko oraz liczbę przedmiotów.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, COUNT(p.Id)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie15_ProwadzacyILiczbaPrzedmiotow()
    {
        var result = DaneUczelni.Prowadzacy
            .GroupJoin(
                DaneUczelni.Przedmioty,
                prow => prow.Id,
                przed => przed.ProwadzacyId,
                (prow, przedmioty) => new 
                { 
                    Imie = prow.Imie, 
                    Nazwisko = prow.Nazwisko, 
                    Liczba = przedmioty.Count() 
                }
            )
            .Select(x => $"{x.Imie} {x.Nazwisko}: {x.Liczba}");

        return result;
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego studenta znajdź jego najwyższą ocenę końcową.
    /// Pomiń studentów, którzy nie mają jeszcze żadnej oceny.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, MAX(z.OcenaKoncowa)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY s.Imie, s.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie16_NajwyzszaOcenaKazdegoStudenta()
    {
        var result = DaneUczelni.Studenci.Join(
                DaneUczelni.Zapisy,
                student => student.Id,
                zapis => zapis.StudentId,
                (stud, zap) => new { Student = stud, ocena = zap.OcenaKoncowa }
            )
            .Where(sm => sm.ocena != null)
            .GroupBy(x => new { x.Student.Imie, x.Student.Nazwisko })
            .Select(g => $"{g.Key.Imie} {g.Key.Nazwisko}: {g.Max(x => x.ocena)}");

        return result;
    }

    /// <summary>
    /// Wyzwanie:
    /// Znajdź studentów, którzy mają więcej niż jeden aktywny zapis.
    /// Zwróć pełne imię i nazwisko oraz liczbę aktywnych przedmiotów.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Imie, s.Nazwisko
    /// HAVING COUNT(*) > 1;
    /// </summary>
    public IEnumerable<string> Wyzwanie01_StudenciZWiecejNizJednymAktywnymPrzedmiotem()
    {
        var result = DaneUczelni.Studenci
            .Join(
                DaneUczelni.Zapisy,
                s => s.Id,
                z => z.StudentId,
                (s, z) => new { s.Imie, s.Nazwisko, z.CzyAktywny }
            )
            .Where(x => x.CzyAktywny)
            .GroupBy(x => new { x.Imie, x.Nazwisko })
            .Where(g => g.Count() > 1)
            .Select(g => $"{g.Key.Imie} {g.Key.Nazwisko}: {g.Count()}");

        return result;
    }

    /// <summary>
    /// Wyzwanie:
    /// Wypisz przedmioty startujące w kwietniu 2026, dla których żaden zapis nie ma jeszcze oceny końcowej.
    ///
    /// SQL:
    /// SELECT p.Nazwa
    /// FROM Przedmioty p
    /// JOIN Zapisy z ON p.Id = z.PrzedmiotId
    /// WHERE MONTH(p.DataStartu) = 4 AND YEAR(p.DataStartu) = 2026
    /// GROUP BY p.Nazwa
    /// HAVING SUM(CASE WHEN z.OcenaKoncowa IS NOT NULL THEN 1 ELSE 0 END) = 0;
    /// </summary>
    public IEnumerable<string> Wyzwanie02_PrzedmiotyStartujaceWKwietniuBezOcenKoncowych()
    {
        var result = DaneUczelni.Przedmioty
            .Where(p => p.DataStartu.Month == 4 && p.DataStartu.Year == 2026)
            .Join(
                DaneUczelni.Zapisy,
                p => p.Id,
                z => z.PrzedmiotId,
                (p, z) => new { p.Nazwa, z.OcenaKoncowa }
            )
            .GroupBy(x => x.Nazwa)
            .Where(g => g.All(x => x.OcenaKoncowa == null))
            .Select(g => g.Key);

        return result;
    }

    /// <summary>
    /// Wyzwanie:
    /// Oblicz średnią ocen końcowych dla każdego prowadzącego na podstawie wszystkich jego przedmiotów.
    /// Pomiń brakujące oceny, ale pozostaw samych prowadzących w wyniku.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, AVG(z.OcenaKoncowa)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// LEFT JOIN Zapisy z ON z.PrzedmiotId = p.Id
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Wyzwanie03_ProwadzacyISredniaOcenNaIchPrzedmiotach()
    {
        var query = 
            from pr in DaneUczelni.Prowadzacy
            join p in DaneUczelni.Przedmioty on pr.Id equals p.ProwadzacyId into przedmiotyProwadzacego
            from p in przedmiotyProwadzacego.DefaultIfEmpty() 
            join z in DaneUczelni.Zapisy on p?.Id equals z.PrzedmiotId into zapisyPrzedmiotu
            from z in zapisyPrzedmiotu.DefaultIfEmpty()       
            where z != null && z.OcenaKoncowa != null
            group z by new { pr.Imie, pr.Nazwisko } into g
            select $"{g.Key.Imie} {g.Key.Nazwisko}: {g.Average(x => x.OcenaKoncowa):F2}";

        return query;
    }

    /// <summary>
    /// Wyzwanie:
    /// Pokaż miasta studentów oraz liczbę aktywnych zapisów wykonanych przez studentów z danego miasta.
    /// Posortuj wynik malejąco po liczbie aktywnych zapisów.
    ///
    /// SQL:
    /// SELECT s.Miasto, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Miasto
    /// ORDER BY COUNT(*) DESC;
    /// </summary>
    public IEnumerable<string> Wyzwanie04_MiastaILiczbaAktywnychZapisow()
    {
        return DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy, s => s.Id, z => z.StudentId, (s, z) => new { s.Miasto, z.CzyAktywny })
            .Where(x => x.CzyAktywny)
            .GroupBy(x => x.Miasto)
            .OrderByDescending(g => g.Count())
            .Select(g => $"{g.Key}: {g.Count()}");
    }

    private static NotImplementedException Niezaimplementowano(string nazwaMetody)
    {
        return new NotImplementedException(
            $"Uzupełnij metodę {nazwaMetody} w pliku Exercises/ZadaniaLinq.cs i uruchom polecenie ponownie.");
    }
}
