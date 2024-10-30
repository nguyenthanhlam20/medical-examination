using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Models;

public partial class OmsContext : DbContext
{
    public OmsContext()
    {
    }

    public OmsContext(DbContextOptions<OmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Available> Availables { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Examination> Examinations { get; set; }

    public virtual DbSet<MedicalService> MedicalServices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DB"));
        }

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Available>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Availabl__3213E83FD7624C89");

            entity.ToTable("Available");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datetime)
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Availables)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Available__docto__4222D4EF");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctor__3213E83FD4B79F8C");

            entity.ToTable("Doctor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.YearOfExperience).HasColumnName("year_of_experience");

            entity.HasMany(d => d.Services).WithMany(p => p.Doctors)
                .UsingEntity<Dictionary<string, object>>(
                    "DoctorService",
                    r => r.HasOne<MedicalService>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Doctor_Se__servi__3F466844"),
                    l => l.HasOne<Doctor>().WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Doctor_Se__docto__3E52440B"),
                    j =>
                    {
                        j.HasKey("DoctorId", "ServiceId").HasName("PK__Doctor_S__1079EEEE8806A875");
                        j.ToTable("Doctor_Service");
                        j.IndexerProperty<int>("DoctorId").HasColumnName("doctor_id");
                        j.IndexerProperty<int>("ServiceId").HasColumnName("service_id");
                    });
        });

        modelBuilder.Entity<Examination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Examinat__3213E83F8F779AC9");

            entity.ToTable("Examination");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Assessments).HasColumnName("assessments");
            entity.Property(e => e.Diagnose).HasColumnName("diagnose");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.PatientName).HasColumnName("patient_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
            entity.Property(e => e.Reasons).HasColumnName("reasons");
            entity.Property(e => e.RegisterDate)
                .HasColumnType("datetime")
                .HasColumnName("register_date");
            entity.Property(e => e.Results).HasColumnName("results");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Symptoms).HasColumnName("symptoms");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Examinations)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_examination_doctor");

            entity.HasOne(d => d.Service).WithMany(p => p.Examinations)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Examinati__servi__398D8EEE");
        });

        modelBuilder.Entity<MedicalService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MedicalS__3213E83F19032E8F");

            entity.ToTable("MedicalService");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
