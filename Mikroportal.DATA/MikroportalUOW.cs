using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.DATA
{
    public class MikroportalUOW : IMikroportalUOW, IDisposable
    {
        IConfiguration _configuration;
        public MikroportalUOW(IConfiguration configuration)
        {
            _configuration = configuration;
            context = new MikroportalContext(_configuration);
        }

        private DbContext context;

        private GenericRepository<TblHatalar> tblHatalarRepository;
        public GenericRepository<TblHatalar> TblHatalarRepository
        {
            get
            {
                if (this.tblHatalarRepository == null)
                {
                    this.tblHatalarRepository = new GenericRepository<TblHatalar>(context);
                }
                return this.tblHatalarRepository;
            }
        }



        private GenericRepository<TblUsers> tblUsersRepository;
        public GenericRepository<TblUsers> TblUsersRepository
        {
            get
            {
                if (this.tblUsersRepository == null)
                {
                    this.tblUsersRepository = new GenericRepository<TblUsers>(context);
                }
                return this.tblUsersRepository;
            }
        }



        private GenericRepository<TblRoles> tblRolesRepository;
        public GenericRepository<TblRoles> TblRolesRepository
        {
            get
            {
                if (this.tblRolesRepository == null)
                {
                    this.tblRolesRepository = new GenericRepository<TblRoles>(context);
                }
                return this.tblRolesRepository;
            }
        }



        private GenericRepository<TblUserRoles> tblUserRolesRepository;
        public GenericRepository<TblUserRoles> TblUserRolesRepository
        {
            get
            {
                if (this.tblUserRolesRepository == null)
                {
                    this.tblUserRolesRepository = new GenericRepository<TblUserRoles>(context);
                }
                return this.tblUserRolesRepository;
            }
        }

        private GenericRepository<TblMenus> tblMenusRepository;
        public GenericRepository<TblMenus> TblMenusRepository
        {
            get
            {
                if (this.tblMenusRepository == null)
                {
                    this.tblMenusRepository = new GenericRepository<TblMenus>(context);
                }
                return this.tblMenusRepository;
            }
        }


        private GenericRepository<TblMenuItems> tblMenuItemsRepository;
        public GenericRepository<TblMenuItems> TblMenuItemsRepository
        {
            get
            {
                if (this.tblMenuItemsRepository == null)
                {
                    this.tblMenuItemsRepository = new GenericRepository<TblMenuItems>(context);
                }
                return this.tblMenuItemsRepository;
            }
        }


        private GenericRepository<TblUserMenuItem> tblUserMenuItemRepository;
        public GenericRepository<TblUserMenuItem> TblUserMenuItemRepository
        {
            get
            {
                if (this.tblUserMenuItemRepository == null)
                {
                    this.tblUserMenuItemRepository = new GenericRepository<TblUserMenuItem>(context);
                }
                return this.tblUserMenuItemRepository;
            }
        }
        private GenericRepository<TblFmOvertimeRequestStaff> tblFmOvertimeRequestStaffRepository;
        public GenericRepository<TblFmOvertimeRequestStaff> TblFmOvertimeRequestStaffRepository
        {
            get
            {
                if (this.tblFmOvertimeRequestStaffRepository == null)
                {
                    this.tblFmOvertimeRequestStaffRepository = new GenericRepository<TblFmOvertimeRequestStaff>(context);
                }
                return this.tblFmOvertimeRequestStaffRepository;
            }
        }


        //private GenericRepository<sp_SSH_GetSkuBySerial> spSSHGetSkuBySerialRepository;
        //public GenericRepository<sp_SSH_GetSkuBySerial> sp_SSH_GetSkuBySerialRepository
        //{
        //    get
        //    {
        //        if (this.spSSHGetSkuBySerialRepository == null)
        //        {
        //            this.spSSHGetSkuBySerialRepository = new GenericRepository<sp_SSH_GetSkuBySerial>(context);
        //        }
        //        return this.spSSHGetSkuBySerialRepository;
        //    }
        //}

        private GenericRepository<TblSshMachineryServices> tblSshMachineryServicesRepository;
        public GenericRepository<TblSshMachineryServices> TblSshMachineryServicesRepository
        {
            get
            {
                if (this.tblSshMachineryServicesRepository == null)
                {
                    this.tblSshMachineryServicesRepository = new GenericRepository<TblSshMachineryServices>(context);
                }
                return this.tblSshMachineryServicesRepository;
            }
        }

        private GenericRepository<vSSHMachineHistory> vssHMachineHistoryRepository;
        public GenericRepository<vSSHMachineHistory> vSSHMachineHistoryRepository
        {
            get
            {
                if (this.vssHMachineHistoryRepository == null)
                {
                    this.vssHMachineHistoryRepository = new GenericRepository<vSSHMachineHistory>(context);
                }
                return this.vssHMachineHistoryRepository;
            }
        }


        private GenericRepository<vFMList> vfmListRepository;
        public GenericRepository<vFMList> vFMListRepository
        {
            get
            {
                if (this.vfmListRepository == null)
                {
                    this.vfmListRepository = new GenericRepository<vFMList>(context);
                }
                return this.vfmListRepository;
            }
        }

        private GenericRepository<TblSshMachines> tblSshMachinesRepository;
        public GenericRepository<TblSshMachines> TblSshMachinesRepository
        {
            get
            {
                if (this.tblSshMachinesRepository == null)
                {
                    this.tblSshMachinesRepository = new GenericRepository<TblSshMachines>(context);
                }
                return this.tblSshMachinesRepository;
            }
        }


        private GenericRepository<TblSshMailExceptions> tblSshMailExceptionsRepository;
        public GenericRepository<TblSshMailExceptions> TblSshMailExceptionsRepository
        {
            get
            {
                if (this.tblSshMailExceptionsRepository == null)
                {
                    this.tblSshMailExceptionsRepository = new GenericRepository<TblSshMailExceptions>(context);
                }
                return this.tblSshMailExceptionsRepository;
            }
        }

        private GenericRepository<TblSshReplacementPartPrice> tblSshReplacementPartPriceRepository;
        public GenericRepository<TblSshReplacementPartPrice> TblSshReplacementPartPriceRepository
        {
            get
            {
                if (this.tblSshReplacementPartPriceRepository == null)
                {
                    this.tblSshReplacementPartPriceRepository = new GenericRepository<TblSshReplacementPartPrice>(context);
                }
                return this.tblSshReplacementPartPriceRepository;
            }
        }

        private GenericRepository<TblSshReplacementPart> tblSshReplacementPartRepository;
        public GenericRepository<TblSshReplacementPart> TblSshReplacementPartRepository
        {
            get
            {
                if (this.tblSshReplacementPartRepository == null)
                {
                    this.tblSshReplacementPartRepository = new GenericRepository<TblSshReplacementPart>(context);
                }
                return this.tblSshReplacementPartRepository;
            }
        }



        private GenericRepository<MesEngineeringdrawing> mesEngineeringdrawingRepository;
        public GenericRepository<MesEngineeringdrawing> MesEngineeringdrawingRepository
        {
            get
            {
                if (this.mesEngineeringdrawingRepository == null)
                {
                    this.mesEngineeringdrawingRepository = new GenericRepository<MesEngineeringdrawing>(context);
                }
                return this.mesEngineeringdrawingRepository;
            }
        }


        private GenericRepository<MesEngineeringdrawingsubtype> mesEngineeringdrawingsubtypeRepository;
        public GenericRepository<MesEngineeringdrawingsubtype> MesEngineeringdrawingsubtypeRepository
        {
            get
            {
                if (this.mesEngineeringdrawingsubtypeRepository == null)
                {
                    this.mesEngineeringdrawingsubtypeRepository = new GenericRepository<MesEngineeringdrawingsubtype>(context);
                }
                return this.mesEngineeringdrawingsubtypeRepository;
            }
        }


        private GenericRepository<TblFmOvertimeType> tblFmOvertimeTypeRepository;
        public GenericRepository<TblFmOvertimeType> TblFmOvertimeTypeRepository
        {
            get
            {
                if (this.tblFmOvertimeTypeRepository == null)
                {
                    this.tblFmOvertimeTypeRepository = new GenericRepository<TblFmOvertimeType>(context);
                }
                return this.tblFmOvertimeTypeRepository;
            }
        }

        private GenericRepository<MesGelirgideryeri> mesGelirgideryeriRepository;
        public GenericRepository<MesGelirgideryeri> MesGelirgideryeriRepository
        {
            get
            {
                if (this.mesGelirgideryeriRepository == null)
                {
                    this.mesGelirgideryeriRepository = new GenericRepository<MesGelirgideryeri>(context);
                }
                return this.mesGelirgideryeriRepository;
            }
        }


        private GenericRepository<TblFmShift> tblFmShiftRepository;
        public GenericRepository<TblFmShift> TblFmShiftRepository
        {
            get
            {
                if (this.tblFmShiftRepository == null)
                {
                    this.tblFmShiftRepository = new GenericRepository<TblFmShift>(context);
                }
                return this.tblFmShiftRepository;
            }
        }

        private GenericRepository<TblSettings> tblSettingsRepository;
        public GenericRepository<TblSettings> TblSettingsRepository
        {
            get
            {
                if (this.tblSettingsRepository == null)
                {
                    this.tblSettingsRepository = new GenericRepository<TblSettings>(context);
                }
                return this.tblSettingsRepository;
            }
        }
        private GenericRepository<TblFmOvertimeRequest> tblFmOvertimeRequestRepository;
        public GenericRepository<TblFmOvertimeRequest> TblFmOvertimeRequestRepository
        {
            get
            {
                if (this.tblFmOvertimeRequestRepository == null)
                {
                    this.tblFmOvertimeRequestRepository = new GenericRepository<TblFmOvertimeRequest>(context);
                }
                return this.tblFmOvertimeRequestRepository;
            }
        }

        private GenericRepository<Users> usersRepository;
        public GenericRepository<Users> UsersRepository
        {
            get
            {
                if (this.usersRepository == null)
                {
                    this.usersRepository = new GenericRepository<Users>(context);
                }
                return this.usersRepository;
            }
        }


        private GenericRepository<MachineProductScrapDetail> machineProductScrapDetailRepository;
        public GenericRepository<MachineProductScrapDetail> MachineProductScrapDetailRepository
        {
            get
            {
                if (this.machineProductScrapDetailRepository == null)
                {
                    this.machineProductScrapDetailRepository = new GenericRepository<MachineProductScrapDetail>(context);
                }
                return this.machineProductScrapDetailRepository;
            }
        }

        private GenericRepository<Machines> machinesRepository;
        public GenericRepository<Machines> MachinesRepository
        {
            get
            {
                if (this.machinesRepository == null)
                {
                    this.machinesRepository = new GenericRepository<Machines>(context);
                }
                return this.machinesRepository;
            }
        }

        private GenericRepository<TblSapMudurlukler> tblSapMudurluklerRepository;
        public GenericRepository<TblSapMudurlukler> TblSapMudurluklerRepository
        {
            get
            {
                if (this.tblSapMudurluklerRepository == null)
                {
                    this.tblSapMudurluklerRepository = new GenericRepository<TblSapMudurlukler>(context);
                }
                return this.tblSapMudurluklerRepository;
            }
        }


        private GenericRepository<TblScrapNetting> tblScrapNettingRepository;
        public GenericRepository<TblScrapNetting> TblScrapNettingRepository
        {
            get
            {
                if (this.tblScrapNettingRepository == null)
                {
                    this.tblScrapNettingRepository = new GenericRepository<TblScrapNetting>(context);
                }
                return this.tblScrapNettingRepository;
            }
        }
        


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
