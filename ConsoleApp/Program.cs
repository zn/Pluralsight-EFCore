using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        private static SamuraiContext context = new SamuraiContext();

        static void Main(string[] args)
        {
            ProjectSomeProperties();
        }

        private static void ProjectSomeProperties()
        {
            var someProps = context.Samurais.Select(s => new { s.Id, s.Name, s.Quotes.Count }).ToList();
        }

        private static void AddQuoteToExistingSamuraiNotTracked()
        {
            var quote = new Quote
            {
                Text = "Some interesting quote here ...",
                SamuraiId = 4
            };
            using (var contxt = new SamuraiContext())
            {
                contxt.Quotes.Add(quote);
                contxt.SaveChanges();
            }
        }

        private static void InsertNewSamuraiWithAQuote()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote{Text = "I've come to save you"},
                    new Quote{Text = "Hello world!"}
                }
            };
            context.Samurais.Add(samurai);
            context.SaveChanges();
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = context.Battles.AsNoTracking().First();
            battle.Name += '2';
            context.Update(battle);
            context.SaveChanges();
        }

        private static void InsertBattle()
        {
            context.Battles.Add(new Battle
            {
                Name = "Battle of Okehazama",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15)
            });
            context.SaveChanges();
        }

        private static void RetriveAndUpdateMultipleSamurais()
        {
            var samurais = context.Samurais.Skip(1).Take(3).ToList();
            samurais.ForEach(s => s.Name += "San");
            context.SaveChanges();
        }

        private static void FilterQuery()
        {
            var samurai = context.Samurais.LastOrDefault();
        }

        private static void AddMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Sampson" };
            var samurai2 = new Samurai { Name = "Tasha" };
            context.Samurais.AddRange(samurai, samurai2);
            context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Sampson" };
            context.Samurais.Add(samurai);
            context.SaveChanges();
        }
    }
}
