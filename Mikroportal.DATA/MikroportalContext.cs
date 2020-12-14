using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Mikroportal.MODEL.Entities
{
    public partial class MikroportalContext : DbContext
    {
        IConfiguration _configuration;

        public static readonly ILoggerFactory MyLoggerFactory
        = LoggerFactory.Create(
            builder =>
            {
                builder
                  .AddFilter((category, level) =>
                              category == DbLoggerCategory.Database.Command.Name
                              && level == LogLevel.Information)
                  .AddDebug();
            });

        internal MikroportalContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MikroportalContext(DbContextOptions<MikroportalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblUsers> TblUsers { get; set; }
        public virtual DbSet<TblRoles> TblRoles { get; set; }
        public virtual DbSet<TblUserRoles> TblUserRoles { get; set; }
        public virtual DbSet<TblMenuItems> TblMenuItems { get; set; }
        public virtual DbSet<TblUserMenuItem> TblUserMenuItem { get; set; }
        public virtual DbSet<TblSshMachineryServices> TblSshMachineryServices { get; set; }
        public virtual DbSet<TblSshMailExceptions> TblSshMailExceptions { get; set; }
        public virtual DbSet<TblSshMachines> TblSshMachines { get; set; }
        public virtual DbSet<TblSshMachineryServicesType> TblSshMachineryServicesType { get; set; }
        public virtual DbSet<TblSshReplacementPart> TblSshReplacementPart { get; set; }
        public virtual DbSet<TblSshReplacementPartPrice> TblSshReplacementPartPrice { get; set; }
        public virtual DbSet<TblSettings> TblSettings { get; set; }
        public virtual DbSet<MesEngineeringdrawing> MesEngineeringdrawing { get; set; }
        public virtual DbSet<MesEngineeringdrawingsubtype> MesEngineeringdrawingsubtype { get; set; }
        public virtual DbSet<TblFmOvertimeRequestStaff> TblFmOvertimeRequestStaff { get; set; }
        public virtual DbSet<TblMenus> TblMenus { get; set; }
        public virtual DbSet<TblFmOvertimeType> TblFmOvertimeType { get; set; }
        public virtual DbSet<MesGelirgideryeri> MesGelirgideryeri { get; set; }
        public virtual DbSet<TblFmShift> TblFmShift { get; set; }
        public virtual DbSet<TblFmOvertimeRequest> TblFmOvertimeRequest { get; set; }
        public virtual DbSet<Users> Users { get; set; }


        public virtual DbSet<MachineProductScrapDetail> MachineProductSacrapDetail { get; set; }
        public virtual DbSet<Machines> Machines { get; set; }
        public virtual DbSet<TblSapMudurlukler> TblSapMudurlukler { get; set; }

        public virtual DbSet<TblHatalar> TblHatalar { get; set; }

        public virtual DbSet<TblScrapNetting> TblScrapNetting { get; set; }

      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=192.168.2.16;Database=MIKROMES;User ID=sa;Password=oz1976*er;Trusted_Connection=True;Integrated Security=false;Persist Security Info=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TblHatalar>(entity =>
            {
                entity.HasKey(e => e.HataId);

                entity.ToTable("tbl_Hatalar");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");
            });



            modelBuilder.Entity<TblUsers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_tbl_CrossListUsers");

                entity.ToTable("tbl_Users");

                entity.Property(e => e.CanSeePriceForCrosslist).HasColumnName("canSeePriceForCrosslist");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsActiveForCrosslist).HasColumnName("isActiveForCrosslist");

                entity.Property(e => e.IsAdminForCrosslist).HasColumnName("isAdminForCrosslist");

                entity.Property(e => e.IsDefined).HasColumnName("isDefined");

                entity.Property(e => e.UserAccount).HasMaxLength(500);

                entity.Property(e => e.UserEmail).HasMaxLength(500);

                entity.Property(e => e.UserName).HasMaxLength(250);

                entity.Property(e => e.UserPassword).HasMaxLength(1500);


            });

            modelBuilder.Entity<TblRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("tbl_Roles");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.RoleDescription).HasMaxLength(500);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblMenuItems>(entity =>
            {
                entity.HasKey(e => e.MenuItemId);

                entity.ToTable("tbl_MenuItems");

                entity.Property(e => e.MenuItemClass).HasMaxLength(50);

                entity.Property(e => e.MenuItemDescription).HasMaxLength(500);

                entity.Property(e => e.MenuItemIcon).HasMaxLength(250);

                entity.Property(e => e.MenuItemName).HasMaxLength(50);

                entity.Property(e => e.MenuItemUrl).HasMaxLength(2000);
                entity.Property(e => e.MenuItemUrlLocalhost).HasMaxLength(2000);
            });

            modelBuilder.Entity<TblUserMenuItem>(entity =>
            {
                entity.HasKey(e => e.UserMenuItemId)
                    .HasName("PK_tbl_UserMenu");

                entity.ToTable("tbl_UserMenuItem");
            });

            modelBuilder.Entity<TblUserRoles>(entity =>
            {
                entity.HasKey(e => e.UserRoleId);

                entity.ToTable("tbl_UserRoles");
            });

            modelBuilder.Entity<TblSshMachineryServices>(entity =>
            {
                entity.HasKey(e => e.MachineryServiceId)
                    .HasName("PK_tbl_SSH_MachineryServices2");

                entity.ToTable("tbl_SSH_MachineryServices");

                entity.Property(e => e.CaringCompanyName).HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(150);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.GuaranteeBanDate).HasColumnType("datetime");

                entity.Property(e => e.GuaranteeEndDate).HasColumnType("datetime");

                entity.Property(e => e.GuaranteeStartDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RelatedPersonEmail).HasMaxLength(50);

                entity.Property(e => e.RelatedPersonLastName).HasMaxLength(50);

                entity.Property(e => e.RelatedPersonName).HasMaxLength(50);

                entity.Property(e => e.RelatedPersonPhone).HasMaxLength(50);

                entity.Property(e => e.RelatedPersonTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNo).HasMaxLength(50);

                entity.Property(e => e.SkuCode).HasMaxLength(50);

                entity.Property(e => e.StaffLastName).HasMaxLength(50);

                entity.Property(e => e.StaffName).HasMaxLength(50);

                entity.Property(e => e.TracingName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblSshMailExceptions>(entity =>
            {
                entity.HasKey(e => e.MailId)
                    .HasName("PK_tbl_SSHMailExceptions");

                entity.ToTable("tbl_SSH_MailExceptions");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Subject).HasMaxLength(50);
            });

            modelBuilder.Entity<TblSshMachines>(entity =>
            {
                entity.HasKey(e => e.MachineId);

                entity.ToTable("tbl_SSH_Machines");

                entity.Property(e => e.AirDryerType).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.MachineStartedDate).HasColumnType("datetime");

                entity.Property(e => e.ModelNo).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SerialNo).HasMaxLength(50);

                entity.Property(e => e.StaffLastName).HasMaxLength(50);

                entity.Property(e => e.StaffName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblSshMachineryServicesType>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK_tbl_SSH_MachineryServicesType");

                entity.ToTable("tbl_SSH_MachineryServicesType");

                entity.Property(e => e.ServiceName).HasMaxLength(50);
            });

            modelBuilder.Entity<sp_SSH_GetSkuBySerial>(entity =>
            {
                entity.HasKey(e => e.SkuId)
                    .HasName("sp_SSH_GetSkuBySerial");

                entity.ToTable("sp_SSH_GetSkuBySerial");

                entity.Property(e => e.Sku).HasMaxLength(50);
            });


            modelBuilder.Entity<sp_SSH_GetUserRoles>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("sp_SSH_GetUserRoles");

                entity.ToTable("sp_SSH_GetUserRoles");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<sp_SSH_GetTechnicalDocument>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("sp_SSH_GetTechnicalDocument");

                entity.ToTable("sp_SSH_GetTechnicalDocument");

                entity.Property(e => e.NAME).HasMaxLength(50);
            });

            modelBuilder.Entity<TblSshReplacementPart>(entity =>
            {
                entity.HasKey(e => e.ReplacementPartId);

                entity.ToTable("tbl_SSH_ReplacementPart");

                entity.Property(e => e.Sad).HasMaxLength(50);
            });
            modelBuilder.Entity<TblSshReplacementPartPrice>(entity =>
            {
                entity.HasKey(e => e.ReplacementPartPriceId);

                entity.ToTable("tbl_SSH_ReplacementPartPrice");

                entity.Property(e => e.Currency).HasMaxLength(50);

                entity.Property(e => e.DryerModel).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Voltage).HasMaxLength(50);
            });

            modelBuilder.Entity<TblSettings>(entity =>
            {
                entity.HasKey(e => e.SettingId);

                entity.ToTable("tbl_Settings");

                entity.Property(e => e.SettingDate).HasColumnType("datetime");

                entity.Property(e => e.SettingName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SettingValue).IsRequired();
            });

            modelBuilder.Entity<MesEngineeringdrawing>(entity =>
            {
                entity.ToTable("MES_ENGINEERINGDRAWING");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("CODE")
                    .HasMaxLength(50);

                entity.Property(e => e.Contenttype)
                    .HasColumnName("CONTENTTYPE")
                    .HasMaxLength(200);

                entity.Property(e => e.Createdby).HasColumnName("CREATEDBY");

                entity.Property(e => e.Createddate)
                    .HasColumnName("CREATEDDATE")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Engineeringdrawinghistoryfid).HasColumnName("ENGINEERINGDRAWINGHISTORYFID");

                entity.Property(e => e.Filecontent).HasColumnName("FILECONTENT");

                entity.Property(e => e.Filename)
                    .HasColumnName("FILENAME")
                    .HasMaxLength(200);

                entity.Property(e => e.Filepath)
                    .HasColumnName("FILEPATH")
                    .HasMaxLength(2000);

                entity.Property(e => e.Filesize).HasColumnName("FILESIZE");

                entity.Property(e => e.Groupfid).HasColumnName("GROUPFID");

                entity.Property(e => e.Issecret).HasColumnName("ISSECRET");

                entity.Property(e => e.Material)
                    .HasColumnName("MATERIAL")
                    .HasMaxLength(150);

                entity.Property(e => e.Modifiedby).HasColumnName("MODIFIEDBY");

                entity.Property(e => e.Modifieddate)
                    .HasColumnName("MODIFIEDDATE")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(150);

                entity.Property(e => e.Revisionno)
                    .IsRequired()
                    .HasColumnName("REVISIONNO")
                    .HasMaxLength(50);

                entity.Property(e => e.Subtypefid).HasColumnName("SUBTYPEFID");

                entity.Property(e => e.Typefid).HasColumnName("TYPEFID");
            });


            modelBuilder.Entity<MesEngineeringdrawingsubtype>(entity =>
            {
                entity.ToTable("MES_ENGINEERINGDRAWINGSUBTYPE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Createdby).HasColumnName("CREATEDBY");

                entity.Property(e => e.Createddate)
                    .HasColumnName("CREATEDDATE")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(2000);

                entity.Property(e => e.Modifiedby).HasColumnName("MODIFIEDBY");

                entity.Property(e => e.Modifieddate)
                    .HasColumnName("MODIFIEDDATE")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblMenus>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.ToTable("tbl_Menus");

                entity.Property(e => e.MenuName).HasMaxLength(50);

                entity.Property(e => e.MenuRoot).HasMaxLength(500);
            });

            modelBuilder.Entity<TblFmOvertimeRequestStaff>(entity =>
            {
                entity.HasKey(e => e.OvertimeRequestStaffId)
                    .HasName("PK_tbl_FM_OvertimeRequestStaff2");

                entity.ToTable("tbl_FM_OvertimeRequestStaff");

                entity.Property(e => e.ConfirmationModifiedBy)
                    .HasMaxLength(41)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(41)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy)
                    .HasMaxLength(41)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(41)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UsersEmployeeCode)
                    .HasMaxLength(41)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<TblFmOvertimeType>(entity =>
            {
                entity.HasKey(e => e.OvertimeTypeId);

                entity.ToTable("tbl_FM_OvertimeType");

                entity.Property(e => e.Name).HasMaxLength(50);
            });
            modelBuilder.Entity<MesGelirgideryeri>(entity =>
            {
                entity.ToTable("MES_GELIRGIDERYERI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GgyAdi)
                    .HasColumnName("GGY_ADI")
                    .HasMaxLength(1050);

                entity.Property(e => e.GgyKodu)
                    .HasColumnName("GGY_KODU")
                    .HasMaxLength(50);

                entity.Property(e => e.GustoId).HasColumnName("GUSTO_ID");

                entity.Property(e => e.UstId).HasColumnName("UST_ID");
            });
            modelBuilder.Entity<TblFmShift>(entity =>
            {
                entity.HasKey(e => e.ShiftId);

                entity.ToTable("tbl_FM_Shift");

                entity.Property(e => e.Name).HasMaxLength(50);
            });
            modelBuilder.Entity<TblFmOvertimeRequest>(entity =>
            {
                entity.HasKey(e => e.OvertimeRequestId)
                    .HasName("PK_tbl_FM_OvertimeRequest5");

                entity.ToTable("tbl_FM_OvertimeRequest");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(41)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy)
                    .HasMaxLength(41)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(41)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OvertimeDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cellphoneno)
                    .HasColumnName("CELLPHONENO")
                    .HasMaxLength(50);

                entity.Property(e => e.Createdby)
                    .HasColumnName("CREATEDBY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddate)
                    .HasColumnName("CREATEDDATE")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Departmentfid).HasColumnName("DEPARTMENTFID");

                entity.Property(e => e.EmployeeCode)
                    .IsRequired()
                    .HasColumnName("EMPLOYEE_CODE")
                    .HasMaxLength(41)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ErpDepartman).HasMaxLength(250);

                entity.Property(e => e.Fullname)
                    .HasColumnName("FULLNAME")
                    .HasMaxLength(200);

                entity.Property(e => e.GustoDepartman).HasMaxLength(250);

                entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");

                entity.Property(e => e.Islogin).HasColumnName("ISLOGIN");

                entity.Property(e => e.Issms).HasColumnName("ISSMS");

                entity.Property(e => e.Logindatetime)
                    .HasColumnName("LOGINDATETIME")
                    .HasColumnType("datetime");

                entity.Property(e => e.Logoutdatetime)
                    .HasColumnName("LOGOUTDATETIME")
                    .HasColumnType("datetime");

                entity.Property(e => e.Machinefid).HasColumnName("MACHINEFID");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("MAIL")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Missionfid).HasColumnName("MISSIONFID");

                entity.Property(e => e.Modifiedby)
                    .HasColumnName("MODIFIEDBY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Modifieddate)
                    .HasColumnName("MODIFIEDDATE")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(41)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PasswordHash).HasMaxLength(250);

                entity.Property(e => e.PersonelKoduRowid).HasColumnName("PersonelKodu_rowid");

                entity.Property(e => e.Pillarfid).HasColumnName("PILLARFID");

                entity.Property(e => e.Terminalfid).HasColumnName("TERMINALFID");

                entity.Property(e => e.UserService).HasMaxLength(250);

                entity.Property(e => e.Usergroupfid).HasColumnName("USERGROUPFID");

                entity.Property(e => e.Userstatus).HasColumnName("USERSTATUS");

                entity.Property(e => e.Vacationbalance)
                    .HasColumnName("VACATIONBALANCE")
                    .HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Machines>(entity =>
            {
                entity.ToTable("MACHINES");

                entity.HasIndex(e => new { e.Id, e.MachineName, e.MachineCode })
                    .HasName("INX_CODE");

                entity.HasIndex(e => new { e.Id, e.Factoryfid, e.Typefid, e.Terminalfid, e.Andonfid })
                    .HasName("INX_ID");

                entity.HasIndex(e => new { e.Id, e.Panono, e.Panoip, e.Inputno, e.SignalType })
                    .HasName("INX_PLC");

                entity.HasIndex(e => new { e.Id, e.MachineName, e.MachineCode, e.Factoryfid, e.Typefid, e.Terminalfid, e.Andonfid })
                    .HasName("INX_ALL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Andonfid).HasColumnName("ANDONFID");

                entity.Property(e => e.Createdby)
                    .HasColumnName("CREATEDBY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddate)
                    .HasColumnName("CREATEDDATE")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Enginnerfid).HasColumnName("ENGINNERFID");

                entity.Property(e => e.Erpfid).HasColumnName("ERPFID");

                entity.Property(e => e.Factoryfid).HasColumnName("FACTORYFID");

                entity.Property(e => e.Groupfid).HasColumnName("GROUPFID");

                entity.Property(e => e.Inputno).HasColumnName("INPUTNO");

                entity.Property(e => e.IsActiveForProductivityCalculations).HasColumnName("isActiveForProductivityCalculations");

                entity.Property(e => e.IsReadFromOpc).HasColumnName("isReadFromOpc");

                entity.Property(e => e.IsStockBeNegative).HasColumnName("isStockBeNegative");

                entity.Property(e => e.IsUseDepartmentForDowntime).HasColumnName("isUseDepartmentForDowntime");

                entity.Property(e => e.IsUseMaterialDialogBox).HasColumnName("isUseMaterialDialogBox");

                entity.Property(e => e.Ispassive).HasColumnName("ISPASSIVE");

                entity.Property(e => e.MachineCode)
                    .HasColumnName("MACHINE_CODE")
                    .HasMaxLength(50);

                entity.Property(e => e.MachineIp)
                    .HasColumnName("MachineIP")
                    .HasMaxLength(20);

                entity.Property(e => e.MachineName)
                    .HasColumnName("MACHINE_NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MachineType).HasMaxLength(50);

                entity.Property(e => e.Modifiedby)
                    .HasColumnName("MODIFIEDBY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Modifieddate)
                    .HasColumnName("MODIFIEDDATE")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Panoip)
                    .HasColumnName("PANOIP")
                    .HasMaxLength(15);

                entity.Property(e => e.Panono).HasColumnName("PANONO");

                entity.Property(e => e.ProductScreen).HasColumnName("PRODUCT_SCREEN");

                entity.Property(e => e.SignalType).HasColumnName("SIGNAL_TYPE");

                entity.Property(e => e.TerminalCode).HasMaxLength(50);

                entity.Property(e => e.Terminalfid).HasColumnName("TERMINALFID");

                entity.Property(e => e.Typefid).HasColumnName("TYPEFID");

                entity.Property(e => e.UsePalet).HasColumnName("USE_PALET");

                entity.Property(e => e.UsePalletBarcode).HasColumnName("usePalletBarcode");

                entity.Property(e => e.UseProductionMultiplier).HasColumnName("useProductionMultiplier");

                entity.Property(e => e.UseSerial).HasColumnName("USE_SERIAL");
            });

            modelBuilder.Entity<MachineProductScrapDetail>(entity =>
            {
                entity.ToTable("MACHINE_PRODUCT_SCRAP_DETAIL");

                entity.HasIndex(e => e.EmployeeCode)
                    .HasName("iEMPLOYEE_CODE");

                entity.HasIndex(e => e.MachineId)
                    .HasName("iMACHINE_ID");

                entity.HasIndex(e => e.MppdId)
                    .HasName("iMPPD_ID");

                entity.HasIndex(e => e.Mpsid)
                    .HasName("iMPSID");

                entity.HasIndex(e => e.ReasonId)
                    .HasName("iREASON_ID");

                entity.HasIndex(e => e.ScrapDate)
                    .HasName("iSCRAP_DATE");

                entity.HasIndex(e => e.ScrapTicketId)
                    .HasName("iSCRAP_TICKET_ID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount)
                    .HasColumnName("AMOUNT")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Amount2)
                    .HasColumnName("AMOUNT2")
                    .HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Cunit)
                    .HasColumnName("CUNIT")
                    .HasMaxLength(10);

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(100);

                entity.Property(e => e.EmployeeCode)
                    .HasColumnName("EMPLOYEE_CODE")
                    .HasMaxLength(41)
                    .IsUnicode(false);

                entity.Property(e => e.IsDone).HasColumnName("isDone");

                entity.Property(e => e.IsSeperateBomMaterial).HasColumnName("isSeperateBomMaterial");

                entity.Property(e => e.IsUseableForErp).HasColumnName("isUseableForErp");

                entity.Property(e => e.IsUsed).HasColumnName("isUsed");

                entity.Property(e => e.MachineId).HasColumnName("MACHINE_ID");

                entity.Property(e => e.MppdId).HasColumnName("MPPD_ID");

                entity.Property(e => e.Mpsid).HasColumnName("MPSID");

                entity.Property(e => e.Onerpid)
                    .HasColumnName("ONERPID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Paletid)
                    .HasColumnName("PALETID")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductType).HasColumnName("PRODUCT_TYPE");

                entity.Property(e => e.Productserial)
                    .HasColumnName("PRODUCTSERIAL")
                    .HasMaxLength(50);

                entity.Property(e => e.Reason)
                    .HasColumnName("REASON")
                    .HasMaxLength(100);

                entity.Property(e => e.ReasonId).HasColumnName("REASON_ID");

                entity.Property(e => e.Sapflag).HasColumnName("SAPFlag");

                entity.Property(e => e.Scrap)
                    .HasColumnName("SCRAP")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ScrapDate)
                    .HasColumnName("SCRAP_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.ScrapTicketId).HasColumnName("SCRAP_TICKET_ID");

                entity.Property(e => e.StockId).HasColumnName("STOCK_ID");

                entity.Property(e => e.StockName)
                    .HasColumnName("STOCK_NAME")
                    .HasMaxLength(100);

                entity.Property(e => e.TotalAmountFromOperator).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.TotalAmountFromOperatorUnit).HasMaxLength(10);

                entity.Property(e => e.TrackingId).HasColumnName("TRACKING_ID");

                entity.Property(e => e.TrackingName)
                    .HasColumnName("TRACKING_NAME")
                    .HasMaxLength(250);

                entity.Property(e => e.Twastage)
                    .HasColumnName("TWASTAGE")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(10);

                entity.Property(e => e.Unit2)
                    .HasColumnName("UNIT2")
                    .HasMaxLength(10);

                entity.Property(e => e.ValidatedAmount)
                    .HasColumnName("VALIDATED_AMOUNT")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ValidatedAmount2)
                    .HasColumnName("VALIDATED_AMOUNT2")
                    .HasColumnType("decimal(18, 6)");

                entity.Property(e => e.ValidationDate)
                    .HasColumnName("VALIDATION_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.ValidationEmployeeCode)
                    .HasColumnName("VALIDATION_EMPLOYEE_CODE")
                    .HasMaxLength(41)
                    .IsUnicode(false);

                entity.Property(e => e.ValidationUnit2).HasMaxLength(10);

                entity.Property(e => e.Wastage)
                    .HasColumnName("WASTAGE")
                    .HasColumnType("decimal(18, 8)");
            });


            modelBuilder.Entity<TblSapMudurlukler>(entity =>
            {
                entity.HasKey(e => e.MudurlukId)
                    .HasName("PK_tbl_SapMudurlukler");

                entity.ToTable("tbl_Sap_Mudurlukler");

                entity.Property(e => e.MudurlukAdi).HasMaxLength(250);

                entity.Property(e => e.MudurlukKodu).HasMaxLength(50);
            });

            modelBuilder.Entity<TblScrapNetting>(entity =>
            {
                entity.HasKey(e => e.ScrapNettingId);

                entity.ToTable("tbl_ScrapNetting");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FinalAmountAfterNetting).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.NettingNagetiveAmount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ProcessAmount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.StockName).HasMaxLength(500);

                entity.Property(e => e.TechnicalScrapAmount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.TrackingName).HasMaxLength(500);

                entity.Property(e => e.Unit).HasMaxLength(50);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
