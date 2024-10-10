using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data;

namespace LINQ
{
    internal class Program
    {
        public static DataClasses2DataContext dataClasses;
        public static void SelectAllCountriesInfo()
        {
            var joinRequest = from country in dataClasses.Country
                              join capitalCountry in dataClasses.CapitalsOfCountries on country.ID equals capitalCountry.CountryId
                              join partOfWorld in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals partOfWorld.ID
                              join sities in dataClasses.BigSities on country.ID equals sities.CountryId
                              select new { Name = country.Name, PartOfWorld = partOfWorld.Name, Sity = sities.Name, CountOfPersons = country.TotalCountOfPersons, Square = country.SquareOfCountry };
            foreach (var i in joinRequest)
            {
                Console.WriteLine($"{i.Name} {i.PartOfWorld} {i.Sity} {i.CountOfPersons} {i.Square}");
            }
        }
        public static void SelectNamesOfCountries()
        {
            var selectRequest = from country in dataClasses.Country
                                select country.Name;
            Console.WriteLine("Имена стран:");
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectNamesOfCapitals()
        {
            var selectRequest = from capital in dataClasses.CapitalsOfCountries
                                join sities in dataClasses.BigSities on capital.SityId equals sities.ID
                                select new { Name = sities.Name };
            Console.WriteLine("Имена cтолиц:");
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i.Name}");
            }
        }
        public static void SelectNamesOfSities(string country)
        {
            var selectRequest = from bigSities in dataClasses.BigSities
                                join countries in dataClasses.Country on bigSities.CountryId equals countries.ID
                                where countries.Name == country
                                select new { Name = bigSities.Name };
            Console.WriteLine($"Имена больших городов страны {country}:");
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i.Name}");
            }
        }
        public static void SelectCountryWherePersonsMoreThanFiveMillions()
        {
            var selectRequest = from bigSities in dataClasses.BigSities
                                join capitals in dataClasses.CapitalsOfCountries on bigSities.ID equals capitals.SityId
                                where bigSities.CountOfPersons > 50000 //5 миллионов человек у меня нигде нет
                                select new { Name = bigSities.Name, CountOfPersons = bigSities.CountOfPersons };
            Console.WriteLine($"Столицы с населением больше 50000:");
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i.Name} {i.CountOfPersons}");
            }
        }
        public static void SelectAllAsianCountries()
        {
            var selectRequest = from countries in dataClasses.Country
                                join partOfWorld in dataClasses.PartsOfTheWorld on countries.PartOfTheWorldId equals partOfWorld.ID
                                select new { CountryName = countries.Name, PartName = partOfWorld.Name };
            selectRequest = selectRequest.Where(data => data.PartName == "asia");
            Console.WriteLine($"Государства из азии: ");
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i.CountryName}");
            }
        }
        public static void SelectCountryWithSelectedSquare(int square)
        {
            var selectRequest = dataClasses.Country.Where(data => data.SquareOfCountry > square).Select(data => data.Name);
            Console.WriteLine($"Государства где площадь больше {square}: ");
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectCapitalsWhereAandP()
        {
            var selectRequest = from bigsities in dataClasses.BigSities
                                join capitals in dataClasses.CapitalsOfCountries on bigsities.ID equals capitals.SityId
                                select new { Name = bigsities.Name };
            selectRequest = selectRequest.Where(data => data.Name.Contains("a") && data.Name.Contains("p"));

            Console.WriteLine($"Столицы в названиях которых есть буквы a и p: ");
            foreach(var i in selectRequest)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectCapitalsWhoStartedWithK()
        {
            var selectRequest = from bigsities in dataClasses.BigSities
                                join capitals in dataClasses.CapitalsOfCountries on bigsities.ID equals capitals.SityId
                                select new { Name = bigsities.Name };
            selectRequest = selectRequest.Where(data => data.Name.StartsWith("k"));

            Console.WriteLine($"Столицы, названия которых начинаются с K:  ");
            foreach(var i in selectRequest)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectCountryWithSelectedSquareRange(int square1, int square2)
        {
            var selectRequest = dataClasses.Country.Where(data => data.SquareOfCountry > square1 && data.SquareOfCountry < square2).Select(data => data.Name);
            Console.WriteLine($"Государства в диапазоне по площади {square1} - {square2}: ");
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectCountryWithSelectedCountOfPersons(int countOfPersons)
        {
            var selectRequest = dataClasses.Country.Where(data => data.TotalCountOfPersons > countOfPersons).Select(data => data.Name);
            Console.WriteLine($"Государства где количество людей больше {countOfPersons}: ");
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectTopFiveCountriesPerSquare()
        {
            var selectRequest = dataClasses.Country.OrderBy(data => data.SquareOfCountry).Select(data => data.Name).Take(5);
            Console.WriteLine("Топ 5 стран по площади: ");
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectTopFiveCountriesPerCountOfPeoples()
        {
            var selectRequest = dataClasses.Country.OrderBy(data => data.TotalCountOfPersons).Select(data => data.Name).Take(5);
            Console.WriteLine("Топ 5 стран по количеству людей: ");
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectCountryWithMaxSquare()
        {
            int maxSquare = dataClasses.Country.Max(data => data.SquareOfCountry);
            var selectedCountryWithMaxSquare = dataClasses.Country.Where(data => data.SquareOfCountry == maxSquare).Select(data => data.Name);
            Console.WriteLine("Страна с самой большой площадью: ");
            foreach (var i in selectedCountryWithMaxSquare)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectCountryWithMaxCountOFPersons()
        {
            var capitals = from bigSities in dataClasses.BigSities
                           join capital in dataClasses.CapitalsOfCountries on bigSities.ID equals capital.SityId
                           select new { Name = bigSities.Name, CountOfPersons = bigSities.CountOfPersons };
            int maxCount = capitals.Max(data => data.CountOfPersons);
            var selectedCapital = capitals.Where(data => data.CountOfPersons == maxCount).Select(data => data.Name);
            Console.WriteLine("Столица с наибольшим количеством людей: ");
            foreach (var i in selectedCapital)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectCountryWithMinSquareInEurope()
        {
            int minSquare = dataClasses.Country.Min(data => data.SquareOfCountry);
            var selectedCountry = from country in dataClasses.Country
                                  join partOfWorld in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals partOfWorld.ID
                                  where country.SquareOfCountry == minSquare && partOfWorld.Name == "Europe"
                                  select new { Name = country.Name, partOfWorld = partOfWorld.Name };
            Console.WriteLine("Страны с самой маленькой площадью в Европе: ");
            foreach (var i in selectedCountry)
            {
                Console.WriteLine($"{i.Name}");
            }
        }
        public static void SelectAvgCountOfPersonsInEurope()
        {
            var selectedCountry = from country in dataClasses.Country
                                  join partOfWorld in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals partOfWorld.ID
                                  where partOfWorld.Name == "Europe"
                                  select new {CountOfPersons = country.TotalCountOfPersons };
            double avgCount = selectedCountry.Average(data => data.CountOfPersons);
            Console.WriteLine($"Средняя площадь стран европы: {avgCount} ");
        }
        public static void SelectTopThreeSitiesPerCountOfPersonsPerSelectedCountry(string userCountry)
        {
            var selectedCountry = from country in dataClasses.Country
                                  join bigSities in dataClasses.BigSities on country.ID equals bigSities.CountryId
                                  where country.Name == userCountry
                                  select new { Name = bigSities.Name , CountOfPersons = bigSities.CountOfPersons};
            var topThreeSities = selectedCountry.OrderBy(data => data.CountOfPersons).Select(data => data).Take(5);
            Console.WriteLine($"Топ 3 городов по количеству жителей");
            foreach(var i in topThreeSities)
            {
                Console.WriteLine($"{i.Name}");
            }
        }
        public static void SelectSummaryCountOfCountries(string userCountry)
        {
            int sum = dataClasses.Country.Count();
            Console.WriteLine($"Суммарное количество стран: {sum}");
        }
        public static void SelectPartOfThe()
        {
            var selectedCountry = from country in dataClasses.Country
                                  join partsOfTheWorld in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals partsOfTheWorld.ID
                                  select new { Name = partsOfTheWorld.Name, CountryName = country.Name};

            var group = selectedCountry.GroupBy(select => select.Name).ToList();
            var tmp = group.Select(x => x);
            Console.WriteLine($"Топ 3 городов по количеству жителей");
            foreach (var i in tmp)
            {
                Console.WriteLine($"{i}");
            }
        }
        static void Main(string[] args)
        {
            dataClasses = new DataClasses2DataContext();
            //SelectAllCountriesInfo();
            //SelectNamesOfCountries();
            //SelectNamesOfCapitals();
            //SelectNamesOfSities("China");
            //SelectCountryWherePersonsMoreThanFiveMillions();
            //SelectAllAsianCountries();
            //SelectCountryWithSelectedSquare(10000);
            //SelectCapitalsWhereAandP();
            //SelectCapitalsWhoStartedWithK();
            //SelectCountryWithSelectedSquareRange(10000, 123123);
            //SelectCountryWithSelectedCountOfPersons(1000);
            //SelectTopFiveCountriesPerSquare();
            //SelectTopFiveCountriesPerCountOfPeoples();
            //SelectCountryWithMaxSquare();
            //SelectCountryWithMaxCountOFPersons();
            //SelectCountryWithMinSquareInEurope();
            //SelectAvgCountOfPersonsInEurope();
            //SelectTopThreeSitiesPerCountOfPersonsPerSelectedCountry("China");
            SelectPartOfThe();
        }
    }
}
