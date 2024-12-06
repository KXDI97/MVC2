using MVC2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC2.Context
{
    public class WebMVC2Context : DbContext
    {
        public WebMVC2Context() : base("name=MVC")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) //modificacion de metodos//
        {
            // modelBuilder.Ignore<LoginViewModel>();

            modelBuilder.Entity<User>()
             .ToTable("User")
             .HasKey(p => p.User_ID);

            modelBuilder.Entity<User>()  //llamamos tabla y ponesmos atributos//
                .Property(p => p.User_ID)
                .IsRequired()
                .HasColumnName("User_ID");

            modelBuilder.Entity<User>()
                .Property(p => p.Username)
                .IsRequired()
                .HasColumnName("Username")
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(p => p.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(p => p.Creation_Date) // Acceso correcto a la propiedad
                .HasColumnName("Creation_Date");

            modelBuilder.Entity<User>()
                .Property(p => p.HashKey)
                .IsRequired()
                .HasColumnName("HashKey");

            modelBuilder.Entity<User>()
                .Property(p => p.HashIV)
                .IsRequired()
                .HasColumnName("HashIV");

            modelBuilder.Entity<Person>()
               .ToTable("Person")
               .HasKey(p => p.Person_ID);

            modelBuilder.Entity<Person>()
                .Property(p => p.Person_ID)
                .IsRequired()
                .HasColumnName("Person_ID");

            modelBuilder.Entity<Person>()
                .Property(p => p.Person_Name)
                .IsRequired()
                .HasColumnName("Person_Name")
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(p => p.Person_Last_Name)
                .IsRequired()
                .HasColumnName("Person_Last_Name")
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(p => p.ID_Number)
                .IsRequired()
                .HasColumnName("ID_Number")
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(p => p.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(p => p.ID_Type)
                .IsRequired()
                .HasColumnName("ID_Type")
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(p => p.Creation_Date)
                .IsRequired()
                .HasColumnName("Creation_Date");

            base.OnModelCreating(modelBuilder);
        }
    }

   
}