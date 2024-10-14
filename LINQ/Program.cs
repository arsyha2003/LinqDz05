using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data;
using System.Security.Policy;

namespace LINQ
{
    internal class Program
    {
        public static DataClasses1DataContext dataClasses;
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
        public static void SelectCountryByName(string countryName)
        {
            var selectRequest = from country in dataClasses.Country
                                join capitalCountry in dataClasses.CapitalsOfCountries on country.ID equals capitalCountry.CountryId
                                join partOfWorld in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals partOfWorld.ID
                                join sities in dataClasses.BigSities on country.ID equals sities.CountryId
                                where country.Name == countryName
                                select new { Name = country.Name, PartOfWorld = partOfWorld.Name, Sity = sities.Name, CountOfPersons = country.TotalCountOfPersons, Square = country.SquareOfCountry };

            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectInfoPerSity(string sity)
        {
            var selectRequest = from country in dataClasses.Country
                                join capitalCountry in dataClasses.CapitalsOfCountries on country.ID equals capitalCountry.CountryId
                                join partOfWorld in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals partOfWorld.ID
                                join sities in dataClasses.BigSities on country.ID equals sities.CountryId
                                where sities.Name == sity
                                select new { Name = country.Name, PartOfWorld = partOfWorld.Name, Sity = sities.Name, CountOfPersons = country.TotalCountOfPersons, Square = country.SquareOfCountry };

 
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i}");
            }
        }
        public static void SelectNameOfCountryByLetter(string letter)
        {
            var selectRequest = from country in dataClasses.Country
                                join capitalCountry in dataClasses.CapitalsOfCountries on country.ID equals capitalCountry.CountryId
                                join partOfWorld in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals partOfWorld.ID
                                join sities in dataClasses.BigSities on country.ID equals sities.CountryId
                                where country.Name.StartsWith(letter)
                                select new { Name = country.Name, PartOfWorld = partOfWorld.Name, Sity = sities.Name, CountOfPersons = country.TotalCountOfPersons, Square = country.SquareOfCountry };
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i.Name}");
            }
        }
        public static void SelectNamesOfCapitalsByLetter(string letter)
        {
            var selectRequest = from capital in dataClasses.CapitalsOfCountries
                                join sities in dataClasses.BigSities on capital.SityId equals sities.ID
                                where sities.Name.StartsWith(letter)
                                select new { Name = sities.Name };
            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i.Name}");
            }
        }
        public static void SelectTop3Capitals()
        {
            var selectRequest = from capital in dataClasses.CapitalsOfCountries
                                join sities in dataClasses.BigSities on capital.SityId equals sities.ID
                                orderby sities.CountOfPersons ascending
                                select new { Name = sities.Name };
            foreach (var i in selectRequest.Take(3))
            {
                Console.WriteLine($"{i.Name}");
            }
        }
        public static void SelectTop3Countries()
        {
            var selectRequest = from countries in dataClasses.Country
                                orderby countries.TotalCountOfPersons ascending
                                select countries;
            foreach (var i in selectRequest.Take(3))
            {
                Console.WriteLine($"{i.Name}");
            }
        }
        //Показать среднее количество жителей в столицах по каждой части света не знаю как сделать ибо не шарю за группировку в linq(((
        public static void SelectTop3WithMaxCountOfPersons(string partOfTheWorld)//Тут я выведу топ 3 страны по определенной части света с наименьшим количеством жителей
        {
            var selectRequest = from countries in dataClasses.Country
                                join partOfWorld in dataClasses.PartsOfTheWorld on countries.PartOfTheWorldId equals partOfWorld.ID
                                where partOfWorld.Name == partOfTheWorld
                                orderby countries.TotalCountOfPersons descending
                                select new { CountryName = countries.Name, PartName = partOfWorld.Name,CountfPersons = countries.TotalCountOfPersons};
            foreach (var i in selectRequest.Take(3))
            {
                Console.WriteLine($"{i.CountryName}");
            }
        }
        public static void SelectTop3WithMinCountOfPersons(string partOfTheWorld)
        {
            var selectRequest = from countries in dataClasses.Country
                                join partOfWorld in dataClasses.PartsOfTheWorld on countries.PartOfTheWorldId equals partOfWorld.ID
                                where partOfWorld.Name == partOfTheWorld
                                orderby countries.TotalCountOfPersons ascending
                                select new { CountryName = countries.Name, PartName = partOfWorld.Name, CountfPersons = countries.TotalCountOfPersons };
            foreach (var i in selectRequest.Take(3))
            {
                Console.WriteLine($"{i.CountryName}");
            }
        }
        public static void SelectAvgCountOfPersonsPerCountry(string country)
        {
            var selectRequest = from countries in dataClasses.Country
                                where countries.Name == country
                                select countries;

            foreach (var i in selectRequest)
            {
                Console.WriteLine($"{i.Name} {i.TotalCountOfPersons}");
            }
        }
        public static void SelectSityWhereMinCountOfPersonsPerContry(string country)
        {
            var selectRequest = from bigsities in dataClasses.BigSities
                                join capitals in dataClasses.CapitalsOfCountries on bigsities.ID equals capitals.SityId
                                join countries in dataClasses.Country on capitals.CountryId equals countries.ID
                                orderby countries.TotalCountOfPersons ascending
                                where countries.Name == country
                                select new { Name = bigsities.Name };

            foreach (var i in selectRequest.Take(1))
            {
                Console.WriteLine($"{i}");
            }
        }
        static void Main(string[] args)
        {
            dataClasses = new DataClasses1DataContext();
            //сделал домашку лениво, кривовато, но она практически такая же как и практика наша
            //методы идут по порядку как задания, исключая задания где требуется группировка ибо я так и не разобрался с ней(
            //ну а в общем я все что в заданиях было требуется исключая пару задачек)

        }
    }
}