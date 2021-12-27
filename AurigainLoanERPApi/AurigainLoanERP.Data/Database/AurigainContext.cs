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

        public virtual DbSet<BalanceTransferLoanReturn> BalanceTransferLoanReturn { get; set; }
        public virtual DbSet<BalanceTransferReturnBankChequeDetail> BalanceTransferReturnBankChequeDetail { get; set; }
        public virtual DbSet<BankBranchMaster> BankBranchMaster { get; set; }
        public virtual DbSet<BankMaster> BankMaster { get; set; }
        public virtual DbSet<BtgoldLoanLead> BtgoldLoanLead { get; set; }
        public virtual DbSet<BtgoldLoanLeadActionHistory> BtgoldLoanLeadActionHistory { get; set; }
        public virtual DbSet<BtgoldLoanLeadAddressDetail> BtgoldLoanLeadAddressDetail { get; set; }
        public virtual DbSet<BtgoldLoanLeadAppointmentDetail> BtgoldLoanLeadAppointmentDetail { get; set; }
        public virtual DbSet<BtgoldLoanLeadApprovalActionHistory> BtgoldLoanLeadApprovalActionHistory { get; set; }
        public virtual DbSet<BtgoldLoanLeadDocumentDetail> BtgoldLoanLeadDocumentDetail { get; set; }
        public virtual DbSet<BtgoldLoanLeadExistingLoanDetail> BtgoldLoanLeadExistingLoanDetail { get; set; }
        public virtual DbSet<BtgoldLoanLeadJewelleryDetail> BtgoldLoanLeadJewelleryDetail { get; set; }
        public virtual DbSet<BtgoldLoanLeadKycdetail> BtgoldLoanLeadKycdetail { get; set; }
        public virtual DbSet<BtgoldLoanLeadStatusActionHistory> BtgoldLoanLeadStatusActionHistory { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<FreshLeadHlplcl> FreshLeadHlplcl { get; set; }
        public virtual DbSet<FreshLeadHlplclstatusActionHistory> FreshLeadHlplclstatusActionHistory { get; set; }
        public virtual DbSet<GoldLoanFreshLead> GoldLoanFreshLead { get; set; }
        public virtual DbSet<GoldLoanFreshLeadActionHistory> GoldLoanFreshLeadActionHistory { get; set; }
        public virtual DbSet<GoldLoanFreshLeadAppointmentDetail> GoldLoanFreshLeadAppointmentDetail { get; set; }
        public virtual DbSet<GoldLoanFreshLeadJewelleryDetail> GoldLoanFreshLeadJewelleryDetail { get; set; }
        public virtual DbSet<GoldLoanFreshLeadKycDocument> GoldLoanFreshLeadKycDocument { get; set; }
        public virtual DbSet<GoldLoanFreshLeadStatusActionHistory> GoldLoanFreshLeadStatusActionHistory { get; set; }
        public virtual DbSet<JewellaryType> JewellaryType { get; set; }
        public virtual DbSet<Managers> Managers { get; set; }
        public virtual DbSet<PaymentMode> PaymentMode { get; set; }
        public virtual DbSet<PincodeArea> PincodeArea { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<Purpose> Purpose { get; set; }
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
            modelBuilder.Entity<BalanceTransferLoanReturn>(entity =>
            {
                entity.Property(e => e.AmountPaidToExistingBank).HasDefaultValueSql("((0))");

                entity.Property(e => e.AmountReturn).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BankName).HasMaxLength(500);

                entity.Property(e => e.CustomerName).HasMaxLength(500);

                entity.Property(e => e.FinalPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.GoldReceived).HasDefaultValueSql("((0))");

                entity.Property(e => e.GoldSubmittedToBank).HasDefaultValueSql("((0))");

                entity.Property(e => e.LoanAccountNumber).HasMaxLength(50);

                entity.Property(e => e.LoanDisbursement).HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks).HasMaxLength(1000);

                entity.Property(e => e.UtrNumber).HasMaxLength(50);

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.BalanceTransferLoanReturn)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BalanceTr__LeadI__5555A4F4");
            });

            modelBuilder.Entity<BalanceTransferReturnBankChequeDetail>(entity =>
            {
                entity.Property(e => e.BtreturnId).HasColumnName("BTReturnId");

                entity.Property(e => e.ChequeNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Btreturn)
                    .WithMany(p => p.BalanceTransferReturnBankChequeDetail)
                    .HasForeignKey(d => d.BtreturnId)
                    .HasConstraintName("FK__BalanceTr__BTRet__573DED66");
            });

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

            modelBuilder.Entity<BtgoldLoanLead>(entity =>
            {
                entity.ToTable("BTGoldLoanLead");

                entity.Property(e => e.ApprovalStatus).HasMaxLength(100);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.EmailId).HasMaxLength(500);

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasMaxLength(3000);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(3000);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LeadSourceByuserId).HasColumnName("LeadSourceBYUserId");

                entity.Property(e => e.LeadStatus).HasMaxLength(100);

                entity.Property(e => e.LoanAccountNumber).HasMaxLength(100);

                entity.Property(e => e.LoanAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LoanCaseNumber).HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Profession)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Purpose).HasMaxLength(3000);

                entity.Property(e => e.SecondaryMobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.BtgoldLoanLeadCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__BTGoldLoa__Creat__47FBA9D6");

                entity.HasOne(d => d.CustomerUser)
                    .WithMany(p => p.BtgoldLoanLeadCustomerUser)
                    .HasForeignKey(d => d.CustomerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__Custo__4242D080");

                entity.HasOne(d => d.LeadSourceByuser)
                    .WithMany(p => p.BtgoldLoanLeadLeadSourceByuser)
                    .HasForeignKey(d => d.LeadSourceByuserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__LeadS__405A880E");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.BtgoldLoanLeadModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__BTGoldLoa__Modif__48EFCE0F");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BtgoldLoanLead)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__Produ__414EAC47");
            });

            modelBuilder.Entity<BtgoldLoanLeadActionHistory>(entity =>
            {
                entity.ToTable("BTGoldLoanLeadActionHistory");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ActionTakenByUser)
                    .WithMany(p => p.BtgoldLoanLeadActionHistoryActionTakenByUser)
                    .HasForeignKey(d => d.ActionTakenByUserId)
                    .HasConstraintName("FK__BTGoldLoa__Assig__00750D23");

                entity.HasOne(d => d.AssignToUser)
                    .WithMany(p => p.BtgoldLoanLeadActionHistoryAssignToUser)
                    .HasForeignKey(d => d.AssignToUserId)
                    .HasConstraintName("FK__BTGoldLoa__Assig__0169315C");
            });

            modelBuilder.Entity<BtgoldLoanLeadAddressDetail>(entity =>
            {
                entity.ToTable("BTGoldLoanLeadAddressDetail");

                entity.Property(e => e.Address).HasMaxLength(2000);

                entity.Property(e => e.AddressLine2).HasMaxLength(1000);

                entity.Property(e => e.CorrespondAddress).HasMaxLength(2000);

                entity.Property(e => e.CorrespondAddressLine2).HasMaxLength(1000);

                entity.HasOne(d => d.AeraPincode)
                    .WithMany(p => p.BtgoldLoanLeadAddressDetailAeraPincode)
                    .HasForeignKey(d => d.AeraPincodeId)
                    .HasConstraintName("FK__BTGoldLoa__AeraP__1446FBA6");

                entity.HasOne(d => d.CorrespondAeraPincode)
                    .WithMany(p => p.BtgoldLoanLeadAddressDetailCorrespondAeraPincode)
                    .HasForeignKey(d => d.CorrespondAeraPincodeId)
                    .HasConstraintName("FK__BTGoldLoa__Corre__153B1FDF");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.BtgoldLoanLeadAddressDetail)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__LeadI__162F4418");
            });

            modelBuilder.Entity<BtgoldLoanLeadAppointmentDetail>(entity =>
            {
                entity.ToTable("BTGoldLoanLeadAppointmentDetail");

                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.BtgoldLoanLeadAppointmentDetail)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__BTGoldLoa__Branc__1881A0DE");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.BtgoldLoanLeadAppointmentDetail)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__LeadI__39AD8A7F");
            });

            modelBuilder.Entity<BtgoldLoanLeadApprovalActionHistory>(entity =>
            {
                entity.ToTable("BTGoldLoanLeadApprovalActionHistory");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ActionTakenByUser)
                    .WithMany(p => p.BtgoldLoanLeadApprovalActionHistory)
                    .HasForeignKey(d => d.ActionTakenByUserId)
                    .HasConstraintName("FK__BTGoldLoa__Actio__2E3BD7D3");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.BtgoldLoanLeadApprovalActionHistory)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__LeadI__3E723F9C");
            });

            modelBuilder.Entity<BtgoldLoanLeadDocumentDetail>(entity =>
            {
                entity.ToTable("BTGoldLoanLeadDocumentDetail");

                entity.Property(e => e.AtmwithdrawalSlip).HasColumnName("ATMWithdrawalSlip");

                entity.Property(e => e.KycdocumentPoa).HasColumnName("KYCDocumentPOA");

                entity.Property(e => e.KycdocumentPoi).HasColumnName("KYCDocumentPOI");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.BtgoldLoanLeadDocumentDetail)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__LeadI__3AA1AEB8");
            });

            modelBuilder.Entity<BtgoldLoanLeadExistingLoanDetail>(entity =>
            {
                entity.ToTable("BTGoldLoanLeadExistingLoanDetail");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BalanceTransferAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BankName).HasMaxLength(1000);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.JewelleryValuation).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OutstandingAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RequiredAmount).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.BtgoldLoanLeadExistingLoanDetail)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__LeadI__3B95D2F1");
            });

            modelBuilder.Entity<BtgoldLoanLeadJewelleryDetail>(entity =>
            {
                entity.ToTable("BTGoldLoanLeadJewelleryDetail");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.JewelleryType)
                    .WithMany(p => p.BtgoldLoanLeadJewelleryDetail)
                    .HasForeignKey(d => d.JewelleryTypeId)
                    .HasConstraintName("FK__BTGoldLoa__Jewel__1C5231C2");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.BtgoldLoanLeadJewelleryDetail)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__LeadI__3C89F72A");
            });

            modelBuilder.Entity<BtgoldLoanLeadKycdetail>(entity =>
            {
                entity.ToTable("BTGoldLoanLeadKYCDetail");

                entity.Property(e => e.Pannumber)
                    .HasColumnName("PANNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PoadocumentNumber)
                    .IsRequired()
                    .HasColumnName("POADocumentNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PoadocumentTypeId).HasColumnName("POADocumentTypeId");

                entity.Property(e => e.PoidocumentNumber)
                    .IsRequired()
                    .HasColumnName("POIDocumentNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PoidocumentTypeId).HasColumnName("POIDocumentTypeId");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.BtgoldLoanLeadKycdetail)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__LeadI__3D7E1B63");

                entity.HasOne(d => d.PoadocumentType)
                    .WithMany(p => p.BtgoldLoanLeadKycdetailPoadocumentType)
                    .HasForeignKey(d => d.PoadocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__POADo__3D491139");

                entity.HasOne(d => d.PoidocumentType)
                    .WithMany(p => p.BtgoldLoanLeadKycdetailPoidocumentType)
                    .HasForeignKey(d => d.PoidocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__POIDo__3C54ED00");
            });

            modelBuilder.Entity<BtgoldLoanLeadStatusActionHistory>(entity =>
            {
                entity.ToTable("BTGoldLoanLeadStatusActionHistory");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ActionTakenByUser)
                    .WithMany(p => p.BtgoldLoanLeadStatusActionHistory)
                    .HasForeignKey(d => d.ActionTakenByUserId)
                    .HasConstraintName("FK__BTGoldLoa__Actio__30242045");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.BtgoldLoanLeadStatusActionHistory)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BTGoldLoa__LeadI__3F6663D5");
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

                entity.Property(e => e.IsBtleadKyc).HasColumnName("IsBTLeadKYC");

                entity.Property(e => e.IsFreshLeadKyc).HasColumnName("IsFreshLeadKYC");

                entity.Property(e => e.IsKyc).HasColumnName("IsKYC");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<FreshLeadHlplcl>(entity =>
            {
                entity.ToTable("FreshLeadHLPLCL");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.EmployeeType).HasMaxLength(200);

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LeadStatus).HasMaxLength(100);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NoOfItr).HasColumnName("NoOfITR");

                entity.Property(e => e.Pincode).HasMaxLength(20);

                entity.HasOne(d => d.AeraPincode)
                    .WithMany(p => p.FreshLeadHlplcl)
                    .HasForeignKey(d => d.AeraPincodeId)
                    .HasConstraintName("FK__FreshLead__AeraP__6C390A4C");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.FreshLeadHlplclCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__FreshLead__Creat__68687968");

                entity.HasOne(d => d.CustomerUser)
                    .WithMany(p => p.FreshLeadHlplclCustomerUser)
                    .HasForeignKey(d => d.CustomerUserId)
                    .HasConstraintName("FK__FreshLead__Custo__6D2D2E85");

                entity.HasOne(d => d.LeadSourceByUser)
                    .WithMany(p => p.FreshLeadHlplclLeadSourceByUser)
                    .HasForeignKey(d => d.LeadSourceByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FreshLead__LeadS__6A50C1DA");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.FreshLeadHlplclModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__FreshLead__Modif__695C9DA1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.FreshLeadHlplcl)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FreshLead__Produ__6B44E613");
            });

            modelBuilder.Entity<FreshLeadHlplclstatusActionHistory>(entity =>
            {
                entity.ToTable("FreshLeadHLPLCLStatusActionHistory");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ActionTakenByUser)
                    .WithMany(p => p.FreshLeadHlplclstatusActionHistory)
                    .HasForeignKey(d => d.ActionTakenByUserId)
                    .HasConstraintName("FK__FreshLead__Actio__4707859D");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.FreshLeadHlplclstatusActionHistory)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FreshLead__LeadI__6774552F");
            });

            modelBuilder.Entity<GoldLoanFreshLead>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

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

                entity.Property(e => e.LeadStatus).HasMaxLength(100);

                entity.Property(e => e.LoanCaseNumber).HasMaxLength(50);

                entity.Property(e => e.ModifedDate).HasColumnType("datetime");

                entity.Property(e => e.PrimaryMobileNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Purpose).HasMaxLength(1000);

                entity.Property(e => e.SecondaryMobileNumber).HasMaxLength(20);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.GoldLoanFreshLeadCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__GoldLoanF__Creat__43F60EC8");

                entity.HasOne(d => d.CustomerUser)
                    .WithMany(p => p.GoldLoanFreshLeadCustomerUser)
                    .HasForeignKey(d => d.CustomerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__Custo__47C69FAC");

                entity.HasOne(d => d.LeadSourceByUser)
                    .WithMany(p => p.GoldLoanFreshLeadLeadSourceByUser)
                    .HasForeignKey(d => d.LeadSourceByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__LeadS__45DE573A");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.GoldLoanFreshLeadModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__GoldLoanF__Modif__44EA3301");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.GoldLoanFreshLead)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__Produ__46D27B73");
            });

            modelBuilder.Entity<GoldLoanFreshLeadActionHistory>(entity =>
            {
                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AssignFromUser)
                    .WithMany(p => p.GoldLoanFreshLeadActionHistoryAssignFromUser)
                    .HasForeignKey(d => d.AssignFromUserId)
                    .HasConstraintName("FK__GoldLoanF__Assig__035179CE");

                entity.HasOne(d => d.AssignToUser)
                    .WithMany(p => p.GoldLoanFreshLeadActionHistoryAssignToUser)
                    .HasForeignKey(d => d.AssignToUserId)
                    .HasConstraintName("FK__GoldLoanF__Assig__04459E07");
            });

            modelBuilder.Entity<GoldLoanFreshLeadAppointmentDetail>(entity =>
            {
                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

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
                    .HasConstraintName("FK__GoldLoanF__BankI__57A801BA");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.GoldLoanFreshLeadAppointmentDetail)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__Branc__589C25F3");

                entity.HasOne(d => d.GlfreshLead)
                    .WithMany(p => p.GoldLoanFreshLeadAppointmentDetail)
                    .HasForeignKey(d => d.GlfreshLeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__GLFre__48BAC3E5");
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
                    .HasConstraintName("FK__GoldLoanF__GLFre__4119A21D");
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
                    .HasConstraintName("FK__GoldLoanF__GLFre__420DC656");

                entity.HasOne(d => d.KycDocumentType)
                    .WithMany(p => p.GoldLoanFreshLeadKycDocument)
                    .HasForeignKey(d => d.KycDocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__KycDo__3E3D3572");

                entity.HasOne(d => d.PincodeArea)
                    .WithMany(p => p.GoldLoanFreshLeadKycDocument)
                    .HasForeignKey(d => d.PincodeAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__Pinco__3552E9B6");
            });

            modelBuilder.Entity<GoldLoanFreshLeadStatusActionHistory>(entity =>
            {
                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ActionTakenByUser)
                    .WithMany(p => p.GoldLoanFreshLeadStatusActionHistory)
                    .HasForeignKey(d => d.ActionTakenByUserId)
                    .HasConstraintName("FK__GoldLoanF__Actio__320C68B7");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.GoldLoanFreshLeadStatusActionHistory)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GoldLoanF__LeadI__4301EA8F");
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

                entity.Property(e => e.AddressLine2).HasMaxLength(1000);

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

                entity.Property(e => e.UniqueId).HasMaxLength(500);

                entity.HasOne(d => d.AreaPincode)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.AreaPincodeId)
                    .HasConstraintName("FK__Managers__AreaPi__2942188C");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__Managers__Distri__2A363CC5");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK__Managers__StateI__2B2A60FE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Managers__UserId__2C1E8537");
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
                    .HasConstraintName("FK__Product__Product__5F492382");
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

            modelBuilder.Entity<Purpose>(entity =>
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
                    .HasMaxLength(1000);
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
                entity.Property(e => e.AddressLine2).HasMaxLength(1000);

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

                entity.HasOne(d => d.AreaPincode)
                    .WithMany(p => p.UserAgent)
                    .HasForeignKey(d => d.AreaPincodeId)
                    .HasConstraintName("FK__UserAgent__AreaP__1CDC41A7");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.UserAgent)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__UserAgent__Distr__1BE81D6E");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.UserAgent)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAgent__Quali__1AF3F935");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAgent)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAgent__UserI__1DD065E0");
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
                entity.Property(e => e.AddressLine2).HasMaxLength(1000);

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
                    .HasConstraintName("FK__UserCusto__Pinco__1FB8AE52");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCustomer)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserCusto__UserI__1EC48A19");
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
                    .HasConstraintName("FK__UserDocum__Docum__3F3159AB");

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
                entity.Property(e => e.AddressLine2).HasMaxLength(1000);

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

                entity.HasOne(d => d.AreaPincode)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.AreaPincodeId)
                    .HasConstraintName("FK__UserDoorS__AreaP__20ACD28B");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__UserDoorS__Distr__22951AFD");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDoorS__Quali__21A0F6C4");

                entity.HasOne(d => d.SecurityDeposit)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.SecurityDepositId)
                    .HasConstraintName("FK__UserDoorS__Secur__23893F36");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDoorStepAgent)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDoorS__UserI__247D636F");
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
                    .HasConstraintName("FK__UserKYC__KYCDocu__40257DE4");

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

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLoginLog)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserLogin__UserI__1387E197");
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

                entity.Property(e => e.Otp).HasColumnName("OTP");

                entity.Property(e => e.SessionStartOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
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
