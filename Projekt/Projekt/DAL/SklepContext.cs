﻿using Microsoft.AspNet.Identity.EntityFramework;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Projekt.DAL
{
    public class SklepContext : IdentityDbContext<ApplicationUser>
    {
        public SklepContext() : base("SklepContext")
        {

        }

        public static SklepContext Create()
        {
            return new SklepContext();
        }

        static SklepContext() => Database.SetInitializer(new SklepInitializer());

        public DbSet<Sklep> Sklep { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Zamowienia> Zamowienie { get; set; }
        public DbSet <PozycjaZamowienia> PozycjaZamowienia { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBilder)
        {
            base.OnModelCreating(modelBilder);
            modelBilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    } 
}