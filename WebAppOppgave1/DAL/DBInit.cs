﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppOppgave1.Models
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {

                var context = serviceScope.ServiceProvider.GetService<DB>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var Passasjer = new Passasjer() { Fornavn="Nima", Etternavn="Abdollahi",Fodselsdato=new DateTime(1998,5,25)};                 
                var Utreise = new DateTime(2021, 10, 15, 14, 30, 0);
                var Ankomst = new DateTime(2021, 10, 17, 13, 30, 0);
               

                var Billett_1 = new Billett(){
                    Tur = new Rute()
                    {
                        Tur = "Oslo_Kiel",
                        Pris = 990.0
                    },
                    Retur = new Rute() { Tur="",Pris=0.0},
                    Type="Tur-retur",
                    Utreise=Utreise,
                    Ankomst=Ankomst,
                    AntallSykler=0,
                    Kjæledyr=0,
                    Passasjer=Passasjer
                };

                var Lugar = new Lugar() { LugarNummer = 6620, Type = "3-bed", Pris = 500.0 };
                var Meal = new Meal() { MealsNummer = 1, Maaltid = "Frokost", Pris = 299.0 };

                var Kunde = new Kunde() {
                    Fornavn = "Navn",
                    Etternavn = "Navnsen",
                    Tlfnummer ="12345678",
                    Epost = "navn@navnsen.com",
                    Adresse = "Osloveien 4",
                    Postnummer = new Postnummer()
                    {
                        Postnr = "2006",
                        Poststed = "Løvnestad"
                    }
                };
                var Billetter = new List<Billett>
                {
                    Billett_1
                };
                var Lugars = new List<Lugar>
                {
                    Lugar
                };
                var Meals = new List<Meal>
                {
                    Meal
                };

                var Bestilling1 = new Bestilling()
                {
                    Kunde = Kunde,
                };

                Bestilling1.Billetter = Billetter;
                Bestilling1.Lugars = Lugars;
                Bestilling1.Meals = Meals;
                
                Kunde.Bestillinger = new List<Bestilling>
                {
                    Bestilling1
                };

                //context.Kunde.Add(Kunde);
                context.Bestillinger.Add(Bestilling1);
                context.SaveChanges();
            }

        }
    }
}