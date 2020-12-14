using Microsoft.EntityFrameworkCore;
using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.DATA
{
    public class SpDataContext : DbContext
    {
        public SpDataContext(DbContextOptions<SpDataContext> options) : base(options)
        {

        }
        public DbSet<sp_SSH_GetSkuBySerial> sp_SSH_GetSkuBySerial { get; set; }
        public DbSet<sp_SSH_GetTechnicalDocument> sp_SSH_GetTechnicalDocument { get; set; }

        public DbSet<vSSHMachineHistory> vSSHMachineHistory { get; set; }
        public DbSet<vFMList> vFMList { get; set; }
        public DbSet<sp_SSH_GetTrackingNameByModelNo> sp_SSH_GetTrackingNameByModelNo { get; set; }
        public DbSet<sp_SSH_GetUserRoles> sp_SSH_GetUserRoles { get; set; }
        public DbSet<sp_FM_GetPersonelList> sp_FM_GetPersonelList { get; set; }
        public DbSet<MSP_ANDON_GETPRODUCTIONLINE_SERIAL> MSP_ANDON_GETPRODUCTIONLINE_SERIAL { get; set; }



        public DbSet<sp_Mes_V2_HurdaMaliyetRaporuGetir> sp_Mes_V2_HurdaMaliyetRaporuGetir { get; set; }
        public DbSet<sp_Mes_V2_HurdaNegatifeDusenler> sp_Mes_V2_HurdaNegatifeDusenler { get; set; }
        public DbSet<sp_Mes_V2_HurdaNegatifDetayGetir> sp_Mes_V2_HurdaNegatifDetayGetir { get; set; }
        public DbSet<sp_Mes_V2_HurdaNegatiftekiIsEmirleri> sp_Mes_V2_HurdaNegatiftekiIsEmirleri { get; set; }

        

        public DbSet<sp_mkrprtl_Mes_MesCogiList> sp_mkrprtl_Mes_MesCogiList { get; set; }
        public DbSet<sp_mkrprtl_Mes_MesCogiTamamlandi> sp_mkrprtl_Mes_MesCogiTamamlandi { get; set; }
        public DbSet<sp_mkrprtl_Mes_MesCogiSil> sp_mkrprtl_Mes_MesCogiSil { get; set; }


    }


}
