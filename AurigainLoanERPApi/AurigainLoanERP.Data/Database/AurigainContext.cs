﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class AurigainContext : DbContext
    {
        public AurigainContext()
        {
        }

        public AurigainContext(DbContextOptions<AurigainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<PaymentMode> PaymentMode { get; set; }
        public virtual DbSet<QualificationMaster> QualificationMaster { get; set; }
        public virtual DbSet<SecurityDepositDetails> SecurityDepositDetails { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<UserAgent> UserAgent { get; set; }
        public virtual DbSet<UserBank> UserBank { get; set; }
        public virtual DbSet<UserDocument> UserDocument { get; set; }
        public virtual DbSet<UserDocumentFiles> UserDocumentFiles { get; set; }
        public virtual DbSet<UserDoorStepAgent> UserDoorStepAgent { get; set; }
        public virtual DbSet<UserKyc> UserKyc { get; set; }
        public virtual DbSet<UserLoginLog> UserLoginLog { get; set; }
        public virtual DbSet<UserMaster> UserMaster { get; set; }
        public virtual DbSet<UserNominee> UserNominee { get; set; }
        public virtual DbSet<UserOtp> UserOtp { get; set; }
        public virtual DbSet<UserReportingPerson> UserReportingPerson { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=208.91.198.174;Database=aurigain;Trusted_Connection=True;password=Aurigain@123#;User ID=admin_aurigain;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Pincode)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.StateId).HasColumnName("State_Id");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.District)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_State");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DocumentName).IsRequired();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<PaymentMode>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Mode)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<QualificationMaster>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<SecurityDepositDetails>(entity =>
            {
                entity.Property(e => e.AccountNumber).HasMaxLength(25);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BankName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreditDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ReferanceNumber).HasMaxLength(30);

                entity.HasOne(d => d.PaymentMode)
                    .WithMany(p => p.SecurityDepositDetails)
                    .HasForeignKey(d => d.PaymentModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SecurityD__Payme__6166761E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SecurityDepositDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SecurityD__UserI__607251E5");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("ModifiedON")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<UserAgent>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FatherName).HasMaxLength(2000);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PinCode).HasMaxLength(50);

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.UserAgent)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__UserAgent__Distr__40058253");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.UserAgent)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAgent__Quali__151B244E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAgent)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAgent__UserI__40F9A68C");
            });

            modelBuilder.Entity<UserBank>(entity =>
            {
                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ifsccode)
                    .IsRequired()
                    .HasColumnName("IFSCCode")
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBank)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserBank__UserId__41EDCAC5");
            });

            modelBuilder.Entity<UserDocument>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.UserDocument)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDocum__Docum__17F790F9");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDocument)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDocum__UserI__42E1EEFE");
            });

            modelBuilder.Entity<UserDocumentFiles>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName).IsRequired();

                entity.Property(e => e.FileType)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Path).IsRequired();

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.UserDocumentFiles)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDocum__Docum__6AEFE058");
            });

            modelBuilder.Entity<UserDoorStepAgent>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PinCode).HasMaxLength(50);

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__UserDoorS__Distr__6442E2C9");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDoorS__Quali__634EBE90");

                entity.HasOne(d => d.SecurityDeposit)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.SecurityDepositId)
                    .HasConstraintName("FK__UserDoorS__Secur__65370702");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDoorS__UserI__625A9A57");
            });

            modelBuilder.Entity<UserKyc>(entity =>
            {
                entity.ToTable("UserKYC");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.KycdocumentTypeId).HasColumnName("KYCDocumentTypeId");

                entity.Property(e => e.Kycnumber)
                    .IsRequired()
                    .HasColumnName("KYCNumber")
                    .HasMaxLength(200);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.KycdocumentType)
                    .WithMany(p => p.UserKyc)
                    .HasForeignKey(d => d.KycdocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserKYC__KYCDocu__19DFD96B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserKyc)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserKYC__UserId__43D61337");
            });

            modelBuilder.Entity<UserLoginLog>(entity =>
            {
                entity.Property(e => e.LoggedInTime)
                    .HasColumnName("LoggedInTIme")
                    .HasColumnType("datetime");

                entity.Property(e => e.LoggedOutTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLoginLog)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserLogin__UserI__51300E55");
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(300);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Mpin)
                    .IsRequired()
                    .HasColumnName("MPin")
                    .HasMaxLength(500);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.UserMaster)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserMaste__UserR__44CA3770");
            });

            modelBuilder.Entity<UserNominee>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NamineeName)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.RelationshipWithNominee)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserNominee)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserNomin__UserI__45BE5BA9");
            });

            modelBuilder.Entity<UserOtp>(entity =>
            {
                entity.ToTable("UserOTP");

                entity.Property(e => e.ExpireOn).HasColumnType("datetime");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Otp)
                    .HasColumnName("OTP")
                    .HasMaxLength(10);

                entity.Property(e => e.SessionStartOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOtp)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserOTP__UserId__46B27FE2");
            });

            modelBuilder.Entity<UserReportingPerson>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.ReportingUser)
                    .WithMany(p => p.UserReportingPersonReportingUser)
                    .HasForeignKey(d => d.ReportingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRepor__Repor__489AC854");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserReportingPersonUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRepor__UserI__47A6A41B");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
