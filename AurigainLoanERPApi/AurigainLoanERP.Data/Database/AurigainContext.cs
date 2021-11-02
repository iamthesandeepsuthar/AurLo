using System;
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

        public virtual DbSet<BankBranchMaster> BankBranchMaster { get; set; }
        public virtual DbSet<BankMaster> BankMaster { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<GoldLoanFreshLead> GoldLoanFreshLead { get; set; }
        public virtual DbSet<GoldLoanFreshLeadAppointmentDetail> GoldLoanFreshLeadAppointmentDetail { get; set; }
        public virtual DbSet<GoldLoanFreshLeadJewelleryDetail> GoldLoanFreshLeadJewelleryDetail { get; set; }
        public virtual DbSet<GoldLoanFreshLeadKycDocument> GoldLoanFreshLeadKycDocument { get; set; }
        public virtual DbSet<JewellaryType> JewellaryType { get; set; }
        public virtual DbSet<Managers> Managers { get; set; }
        public virtual DbSet<PaymentMode> PaymentMode { get; set; }
        public virtual DbSet<PincodeArea> PincodeArea { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<QualificationMaster> QualificationMaster { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<UserAgent> UserAgent { get; set; }
        public virtual DbSet<UserAvailability> UserAvailability { get; set; }
        public virtual DbSet<UserBank> UserBank { get; set; }
        public virtual DbSet<UserCustomer> UserCustomer { get; set; }
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
        public virtual DbSet<UserSecurityDepositDetails> UserSecurityDepositDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress01;Database=Aurigain;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankBranchMaster>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(2000);

                entity.Property(e => e.BranchCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.BranchEmailId).HasMaxLength(200);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.ConfigurationSettingJson).HasColumnName("ConfigurationSettingJSON");

                entity.Property(e => e.ContactNumber).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ifsc)
                    .IsRequired()
                    .HasColumnName("IFSC")
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Pincode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.BankBranchMaster)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BankBranc__BankI__2BC97F7C");
            });

            modelBuilder.Entity<BankMaster>(entity =>
            {
                entity.Property(e => e.BankLogoUrl).HasMaxLength(1000);

                entity.Property(e => e.ContactNumber).HasMaxLength(20);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FaxNumber).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.WebsiteUrl)
                    .HasColumnName("Website_Url")
                    .HasMaxLength(200);
            });

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

            modelBuilder.Entity<GoldLoanFreshLead>(entity =>
            {
                entity.Property(e => e.CreatedDate)
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

                entity.Property(e => e.ModifedDate).HasColumnType("datetime");

                entity.Property(e => e.PrimaryMobileNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Purpose).HasMaxLength(1000);

                entity.Property(e => e.SecondaryMobileNumber).HasMaxLength(20);

                entity.HasOne(d => d.LeadSourceByUser)
                    .WithMany(p => p.GoldLoanFreshLead)
                    .HasForeignKey(d => d.LeadSourceByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__LeadS__2F9A1060");
            });

            modelBuilder.Entity<GoldLoanFreshLeadAppointmentDetail>(entity =>
            {
                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.Property(e => e.AppointmentTime)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GlfreshLeadId).HasColumnName("GLFreshLeadId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.GoldLoanFreshLeadAppointmentDetail)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__BankI__308E3499");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.GoldLoanFreshLeadAppointmentDetail)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__Branc__318258D2");

                entity.HasOne(d => d.GlfreshLead)
                    .WithMany(p => p.GoldLoanFreshLeadAppointmentDetail)
                    .HasForeignKey(d => d.GlfreshLeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__GLFre__32767D0B");
            });

            modelBuilder.Entity<GoldLoanFreshLeadJewelleryDetail>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GlfreshLeadId).HasColumnName("GLFreshLeadId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.GlfreshLead)
                    .WithMany(p => p.GoldLoanFreshLeadJewelleryDetail)
                    .HasForeignKey(d => d.GlfreshLeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__GLFre__336AA144");
            });

            modelBuilder.Entity<GoldLoanFreshLeadKycDocument>(entity =>
            {
                entity.Property(e => e.AddressLine1).HasMaxLength(2500);

                entity.Property(e => e.AddressLine2).HasMaxLength(2000);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DocumentNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GlfreshLeadId).HasColumnName("GLFreshLeadId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.PanNumber).HasMaxLength(20);

                entity.HasOne(d => d.GlfreshLead)
                    .WithMany(p => p.GoldLoanFreshLeadKycDocument)
                    .HasForeignKey(d => d.GlfreshLeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__GLFre__36470DEF");

                entity.HasOne(d => d.KycDocumentType)
                    .WithMany(p => p.GoldLoanFreshLeadKycDocument)
                    .HasForeignKey(d => d.KycDocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__KycDo__345EC57D");

                entity.HasOne(d => d.PincodeArea)
                    .WithMany(p => p.GoldLoanFreshLeadKycDocument)
                    .HasForeignKey(d => d.PincodeAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__Pinco__3552E9B6");
            });

            modelBuilder.Entity<JewellaryType>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Managers>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(2000);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FatherName).HasMaxLength(500);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Pincode).HasMaxLength(200);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Managers__Distri__3CF40B7E");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Managers__StateI__3DE82FB7");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Managers__UserId__3BFFE745");
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

            modelBuilder.Entity<PincodeArea>(entity =>
            {
                entity.Property(e => e.AreaName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Modifieddate).HasColumnType("datetime");

                entity.Property(e => e.Pincode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.PincodeArea)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PincodeAr__Distr__5BAD9CC8");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__BankId__477199F1");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Product__467D75B8");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
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
                    .HasConstraintName("FK__UserAgent__Distr__123EB7A3");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.UserAgent)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAgent__Quali__114A936A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAgent)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAgent__UserI__5D95E53A");
            });

            modelBuilder.Entity<UserAvailability>(entity =>
            {
                entity.Property(e => e.Capacity).HasColumnName("Capacity ");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FridayEt).HasColumnName("FridayET");

                entity.Property(e => e.FridaySt).HasColumnName("FridayST");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.MondayEt).HasColumnName("MondayET");

                entity.Property(e => e.MondaySt).HasColumnName("MondayST");

                entity.Property(e => e.SaturdayEt).HasColumnName("SaturdayET");

                entity.Property(e => e.SaturdaySt).HasColumnName("SaturdayST");

                entity.Property(e => e.SundayEt).HasColumnName("SundayET");

                entity.Property(e => e.SundaySt).HasColumnName("SundayST");

                entity.Property(e => e.ThursdayEt).HasColumnName("ThursdayET");

                entity.Property(e => e.ThursdaySt).HasColumnName("ThursdayST");

                entity.Property(e => e.TuesdayEt).HasColumnName("TuesdayET");

                entity.Property(e => e.TuesdaySt).HasColumnName("TuesdayST");

                entity.Property(e => e.WednesdayEt).HasColumnName("WednesdayET");

                entity.Property(e => e.WednesdaySt).HasColumnName("WednesdayST");

                entity.HasOne(d => d.PincodeArea)
                    .WithMany(p => p.UserAvailability)
                    .HasForeignKey(d => d.PincodeAreaId)
                    .HasConstraintName("FK_UserAvailability_PincodeArea");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAvailability)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAvailability_UserMaster");
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
                    .HasConstraintName("FK__UserBank__UserId__5F7E2DAC");
            });

            modelBuilder.Entity<UserCustomer>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FatherName).HasMaxLength(2000);

                entity.Property(e => e.FullName).HasMaxLength(2000);

                entity.Property(e => e.Gender).HasMaxLength(30);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.PincodeArea)
                    .WithMany(p => p.UserCustomer)
                    .HasForeignKey(d => d.PincodeAreaId)
                    .HasConstraintName("FK__UserCusto__Pinco__4959E263");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCustomer)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserCusto__UserI__4865BE2A");
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
                    .HasConstraintName("FK__UserDocum__Docum__151B244E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDocument)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDocum__UserI__607251E5");
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
                    .HasConstraintName("FK__UserDocum__Docum__17036CC0");
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
                    .HasConstraintName("FK__UserDoorS__Distr__19DFD96B");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDoorS__Quali__18EBB532");

                entity.HasOne(d => d.SecurityDeposit)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.SecurityDepositId)
                    .HasConstraintName("FK__UserDoorS__Secur__1AD3FDA4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDoorS__UserI__6166761E");
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
                    .HasConstraintName("FK__UserKYC__KYCDocu__1BC821DD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserKyc)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserKYC__UserId__634EBE90");
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
                    .HasConstraintName("FK__UserLogin__UserI__6442E2C9");
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
                    .HasConstraintName("FK__UserMaste__UserR__625A9A57");
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
                    .HasConstraintName("FK__UserNomin__UserI__65370702");
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
                    .HasConstraintName("FK__UserOTP__UserId__662B2B3B");
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
                    .HasConstraintName("FK__UserRepor__Repor__681373AD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserReportingPersonUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRepor__UserI__671F4F74");
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

            modelBuilder.Entity<UserSecurityDepositDetails>(entity =>
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
                    .WithMany(p => p.UserSecurityDepositDetails)
                    .HasForeignKey(d => d.PaymentModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserSecur__Payme__245D67DE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSecurityDepositDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserSecur__UserI__690797E6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
