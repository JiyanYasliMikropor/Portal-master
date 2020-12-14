using Microsoft.EntityFrameworkCore;
using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.DATA
{
    public interface IMikroportalUOW
    {

        public GenericRepository<TblUsers> TblUsersRepository { get; }

        public GenericRepository<TblRoles> TblRolesRepository { get; }
        public GenericRepository<TblUserRoles> TblUserRolesRepository { get; }
        public GenericRepository<TblMenuItems> TblMenuItemsRepository { get; }
        public GenericRepository<TblUserMenuItem> TblUserMenuItemRepository { get; }
        public GenericRepository<TblMenus> TblMenusRepository { get; }

        public GenericRepository<TblSshMachineryServices> TblSshMachineryServicesRepository { get; }

        public GenericRepository<TblSshMachines> TblSshMachinesRepository { get; }
        public GenericRepository<TblSshMailExceptions> TblSshMailExceptionsRepository { get; }
        public GenericRepository<TblSshReplacementPart> TblSshReplacementPartRepository { get; }
        public GenericRepository<TblSshReplacementPartPrice> TblSshReplacementPartPriceRepository { get; }
        public GenericRepository<TblFmOvertimeRequestStaff> TblFmOvertimeRequestStaffRepository { get; }
        public GenericRepository<MesEngineeringdrawing> MesEngineeringdrawingRepository { get; }
        public GenericRepository<MesEngineeringdrawingsubtype> MesEngineeringdrawingsubtypeRepository { get; }
        public GenericRepository<TblFmOvertimeType> TblFmOvertimeTypeRepository { get; }
        public GenericRepository<MesGelirgideryeri> MesGelirgideryeriRepository { get; }
        public GenericRepository<TblFmShift> TblFmShiftRepository { get; }
        public GenericRepository<TblFmOvertimeRequest> TblFmOvertimeRequestRepository { get; }

        public GenericRepository<TblSettings> TblSettingsRepository { get; }

        //public GenericRepository<sp_SSH_GetSkuBySerial> sp_SSH_GetSkuBySerialRepository { get; }
        //public GenericRepository<sp_SSH_GetTechnicalDocument> sp_SSH_GetTechnicalDocumentRepository { get; }
        public GenericRepository<vSSHMachineHistory> vSSHMachineHistoryRepository { get; }
        public GenericRepository<vFMList> vFMListRepository { get; }
        public GenericRepository<Users> UsersRepository { get; }

        public GenericRepository<MachineProductScrapDetail> MachineProductScrapDetailRepository { get; }

        public GenericRepository<Machines> MachinesRepository { get; }
        public GenericRepository<TblSapMudurlukler> TblSapMudurluklerRepository { get; }
        public GenericRepository<TblScrapNetting> TblScrapNettingRepository { get; }
        public GenericRepository<TblHatalar> TblHatalarRepository { get; }

      

        public void Save();
    }
}
