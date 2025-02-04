using System;
using System.Collections.Generic;
using Blagodat.Models;
using Microsoft.EntityFrameworkCore;

namespace Blagodat.Context;

public partial class User9Context : DbContext
{
    public User9Context()
    {
    }

    public User9Context(DbContextOptions<User9Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Connection> Connections { get; set; }

    public virtual DbSet<Connectionstatus> Connectionstatuses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Listservice> Listservices { get; set; }

    public virtual DbSet<Passport> Passports { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Serviceandorder> Serviceandorders { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=45.67.56.214; Port=5454; Username=user9; Database=user9; Password=X8C8NTnD");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("adress_pkey");

            entity.ToTable("adress", "grace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Codeemail).HasColumnName("codeemail");
            entity.Property(e => e.Numberstreet).HasColumnName("numberstreet");
            entity.Property(e => e.Roomnum).HasColumnName("roomnum");
            entity.Property(e => e.Street).HasColumnName("street");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client", "grace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Datahb).HasColumnName("datahb");
            entity.Property(e => e.Firstname).HasColumnName("firstname");
            entity.Property(e => e.Idadress).HasColumnName("idadress");
            entity.Property(e => e.Idpasport).HasColumnName("idpasport");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
            entity.Property(e => e.Mail).HasColumnName("mail");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");

            entity.HasOne(d => d.IdadressNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.Idadress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_adress");

            entity.HasOne(d => d.IdpasportNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.Idpasport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_passport");
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("connection_pkey");

            entity.ToTable("connection", "grace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Dataconnection)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dataconnection");
            entity.Property(e => e.Idemployees).HasColumnName("idemployees");
            entity.Property(e => e.Idstatus).HasColumnName("idstatus");

            entity.HasOne(d => d.IdemployeesNavigation).WithMany(p => p.Connections)
                .HasForeignKey(d => d.Idemployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_employees");

            entity.HasOne(d => d.IdstatusNavigation).WithMany(p => p.Connections)
                .HasForeignKey(d => d.Idstatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_connectionstatus");
        });

        modelBuilder.Entity<Connectionstatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("connectionstatus_pkey");

            entity.ToTable("connectionstatus", "grace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employees_pkey");

            entity.ToTable("employees", "grace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Firstname).HasColumnName("firstname");
            entity.Property(e => e.Idjob).HasColumnName("idjob");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
            entity.Property(e => e.Mail).HasColumnName("mail");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");

            entity.HasOne(d => d.IdjobNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Idjob)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_job");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("job_pkey");

            entity.ToTable("job", "grace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Listservice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("listservice_pkey");

            entity.ToTable("listservice", "grace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Idorder).HasColumnName("idorder");
            entity.Property(e => e.Idservice).HasColumnName("idservice");

            entity.HasOne(d => d.IdorderNavigation).WithMany(p => p.Listservices)
                .HasForeignKey(d => d.Idorder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_serviceandorders");

            entity.HasOne(d => d.IdserviceNavigation).WithMany(p => p.Listservices)
                .HasForeignKey(d => d.Idservice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_service");
        });

        modelBuilder.Entity<Passport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("passport_pkey");

            entity.ToTable("passport", "grace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Series).HasColumnName("series");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_pkey");

            entity.ToTable("service", "grace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Codservice).HasColumnName("codservice");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<Serviceandorder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("serviceandorders_pkey");

            entity.ToTable("serviceandorders", "grace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Datacloseorder).HasColumnName("datacloseorder");
            entity.Property(e => e.Datacreate).HasColumnName("datacreate");
            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Idstatus).HasColumnName("idstatus");
            entity.Property(e => e.Listserv).HasColumnName("listserv");
            entity.Property(e => e.Rentaltime).HasColumnName("rentaltime");
            entity.Property(e => e.Timeorders).HasColumnName("timeorders");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Serviceandorders)
                .HasForeignKey(d => d.Idclient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_client");

            entity.HasOne(d => d.IdstatusNavigation).WithMany(p => p.Serviceandorders)
                .HasForeignKey(d => d.Idstatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_status");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("status", "grace");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
