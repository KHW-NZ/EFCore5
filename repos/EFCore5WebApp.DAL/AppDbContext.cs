﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using EFCore5WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EFCore5WebApp.DAL
{
    public class AppDbContext: IdentityDbContext
    { 
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<LookUp> LookUps { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             * This is important without adding this base.OnModelCreating(modelBuilder)
             * Add-Migration InitialIdentity will not work properly with a following error
             * The entity type 'IdentityUserLogin<string>' requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'. For more information on keyless entity types
             * Found out the solution from the website : https://entityframework.net/knowledge-base/39798317/identityuserlogin-string---requires-a-primary-key-to-be-defined-error-while-adding-migration
             */
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LookUp>().HasData(new List<LookUp>() {
                new LookUp() { Code = "AK", Description = "Alaska", LookUpType = LookUpType.State},
                new LookUp() { Code = "AZ", Description = "Arizona",LookUpType = LookUpType.State},
                new LookUp() { Code = "AR", Description = "Arkansas",LookUpType = LookUpType.State},
                new LookUp() { Code = "CA", Description = "California",LookUpType = LookUpType.State},
                new LookUp() { Code = "CO", Description = "Colorado",LookUpType = LookUpType.State},
                new LookUp() { Code = "CT", Description = "Connecticut",LookUpType = LookUpType.State},
                new LookUp() { Code = "DE", Description = "Delaware",LookUpType = LookUpType.State},
                new LookUp() { Code = "DC", Description = "District of Columbia", LookUpType = LookUpType.State},
                new LookUp() { Code = "FL", Description = "Florida",LookUpType = LookUpType.State},
                new LookUp() { Code = "GA", Description = "Georgia",LookUpType = LookUpType.State},
                new LookUp() { Code = "ID", Description = "Hawaii",LookUpType = LookUpType.State},
                new LookUp() { Code = "IL", Description = "Idaho",LookUpType = LookUpType.State},
                new LookUp() { Code = "IN", Description = "Illinois",LookUpType = LookUpType.State},
                new LookUp() { Code = "IA", Description = "Indiana",LookUpType = LookUpType.State},
                new LookUp() { Code = "KS", Description = "Iowa",LookUpType = LookUpType.State},
                new LookUp() { Code = "KY", Description = "Kansas",LookUpType = LookUpType.State},
                new LookUp() { Code = "LA", Description = "Kentucky",LookUpType = LookUpType.State},
                new LookUp() { Code = "ME", Description = "Louisiana",LookUpType = LookUpType.State},
                new LookUp() { Code = "MD", Description = "Maine",LookUpType = LookUpType.State},
                new LookUp() { Code = "MA", Description = "Maryland",LookUpType = LookUpType.State},
                new LookUp() { Code = "MI", Description = "Massachusetts",LookUpType = LookUpType.State},
                new LookUp() { Code = "MN", Description = "Michigan",LookUpType = LookUpType.State},
                new LookUp() { Code = "MS", Description = "Minnesota",LookUpType = LookUpType.State},
                new LookUp() { Code = "MO", Description = "Mississippi",LookUpType = LookUpType.State},
                new LookUp() { Code = "MT", Description = "Missouri",LookUpType = LookUpType.State},
                new LookUp() { Code = "NE", Description = "Montana",LookUpType = LookUpType.State},
                new LookUp() { Code = "NV", Description = "Nevada",LookUpType = LookUpType.State},
                new LookUp() { Code = "NH", Description = "New Hampshire",LookUpType = LookUpType.State},
                new LookUp() { Code = "NJ", Description = "New Jersey",LookUpType = LookUpType.State},
                new LookUp() { Code = "NM", Description = "New Mexico",LookUpType = LookUpType.State},
                new LookUp() { Code = "NY", Description = "New York",LookUpType = LookUpType.State},
                new LookUp() { Code = "NC", Description = "New Carolina",LookUpType = LookUpType.State},
                new LookUp() { Code = "ND", Description = "North Dakota",LookUpType = LookUpType.State},
                new LookUp() { Code = "OH", Description = "Ohio",LookUpType = LookUpType.State},
                new LookUp() { Code = "OK", Description = "Oklahoma",LookUpType = LookUpType.State},
                new LookUp() { Code = "OR", Description = "Oregon",LookUpType = LookUpType.State},
                new LookUp() { Code = "PA", Description = "Pennsylvania",LookUpType = LookUpType.State},
                new LookUp() { Code = "RI", Description = "Rhode Island",LookUpType = LookUpType.State},
                new LookUp() { Code = "SC", Description = "South Carolina",LookUpType = LookUpType.State},
                new LookUp() { Code = "SD", Description = "South Dakota",LookUpType = LookUpType.State},
                new LookUp() { Code = "TN", Description = "Tennessee",LookUpType = LookUpType.State},
                new LookUp() { Code = "TX", Description = "Texas",LookUpType = LookUpType.State},
                new LookUp() { Code = "UT", Description = "Utah",LookUpType = LookUpType.State},
                new LookUp() { Code = "VT", Description = "Vermont",LookUpType = LookUpType.State},
                new LookUp() { Code = "VA", Description = "Virginia",LookUpType = LookUpType.State},
                new LookUp() { Code = "WA", Description = "Washington",LookUpType = LookUpType.State},
                new LookUp() { Code = "WV", Description = "West Virginia",LookUpType = LookUpType.State},
                new LookUp() { Code = "WI", Description = "Wisconsis",LookUpType = LookUpType.State},
                new LookUp() { Code = "WY", Description = "Wyoming",LookUpType = LookUpType.State},
                new LookUp() { Code = "PR", Description = "Puerto Rico",LookUpType = LookUpType.State},
                new LookUp() { Code = "USA", Description = "United States of America", LookUpType = LookUpType.Country}
            });

            modelBuilder.Entity<Person>().HasData(new List<Person>()
            {
                new Person() {Id=1, FirstName="John", LastName="Smith", EmailAddress="john@smith.com", Age=20},
                new Person() {Id=2, FirstName="Suan", LastName="Jones", EmailAddress="john@smith.com", Age=30}
            });

            modelBuilder.Entity<Address>().HasData(new List<Address>()
            {
                new Address(){Id=1, AddressLine1="123 Test St", AddressLine2="", City="Beverly Hills", State="CA", ZipCode="90210", PersonId=1, Country="USA"},
                new Address(){Id=2, AddressLine1="123 Michigan Ave", AddressLine2="", City="Chicago", State="IL", ZipCode="60612", PersonId=2, Country="USA"},
                new Address(){Id=3, AddressLine1="100 1St St", AddressLine2="", City="Chicago", State="IL", ZipCode="60612", PersonId=2, Country="USA"},
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasMany(x => x.Addresses).WithOne(p => p.Person).HasForeignKey(f => f.PersonId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}