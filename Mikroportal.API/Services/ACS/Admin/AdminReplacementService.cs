using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Mikroportal.DATA;
using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.ACS.Admin
{
    public class AdminReplacementService : IAdminReplacementService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;

        private readonly SpDataContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AdminReplacementService(IConfiguration configuration, IMikroportalUOW mikroportalContext, IHostingEnvironment hostingEnvironment, SpDataContext dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            context = mikroportalContext;
            _hostingEnvironment = hostingEnvironment;
        }


        public GetReplacementListResponse GetReplacementList(SearchPartNo searchPartNo)
        {

            GetReplacementListResponse response = new GetReplacementListResponse();

            List<AdminReplacementPartView> replacementPartData = new List<AdminReplacementPartView>();

            var replacementList = from replacementPart in context.TblSshReplacementPartRepository.Get()
                                  join replacementPartPrice in context.TblSshReplacementPartPriceRepository.Get()
                                  on replacementPart.PartNo equals replacementPartPrice.PartNo into replacementPartAndReplacementPartPrice
                                  from replacementPartPrice in replacementPartAndReplacementPartPrice.DefaultIfEmpty()
                                  select new
                                  {
                                      replacementPart.ReplacementPartId,
                                      replacementPart.Sad,
                                      replacementPart.PartNo,
                                      replacementPartPrice.ReplacementPartPriceId,
                                      replacementPartPrice.DryerModel,
                                      replacementPartPrice.EdDrawingItemNo,
                                      replacementPartPrice.Currency,
                                      replacementPartPrice.Price,
                                      replacementPartPrice.Qty,
                                      replacementPartPrice.Voltage
                                  } into selection
                                  select selection;


            //if (searchPartNo.partNo == null)
            //{
            //   var replacementList = from replacementPart in context.TblSshReplacementPartRepository.Get()
            //                      join replacementPartPrice in context.TblSshReplacementPartPriceRepository.Get()
            //                      on replacementPart.PartNo equals replacementPartPrice.PartNo into replacementPartAndReplacementPartPrice
            //                      from replacementPartPrice in replacementPartAndReplacementPartPrice.DefaultIfEmpty()
            //                      select new
            //                      {
            //                          replacementPart.ReplacementPartId,
            //                          replacementPart.Sad,
            //                          replacementPart.PartNo,
            //                          replacementPartPrice.ReplacementPartPriceId,
            //                          replacementPartPrice.DryerModel,
            //                          replacementPartPrice.EdDrawingItemNo,
            //                          replacementPartPrice.Currency,
            //                          replacementPartPrice.Price,
            //                          replacementPartPrice.Qty,
            //                          replacementPartPrice.Voltage
            //                      } into selection
            //                      select selection;

            //}
            //else
            //{
            //  var  replacementList = from replacementPart in context.TblSshReplacementPartRepository.Get()
            //                      join replacementPartPrice in context.TblSshReplacementPartPriceRepository.Get()
            //                      on replacementPart.PartNo equals replacementPartPrice.PartNo into replacementPartAndReplacementPartPrice
            //                      from replacementPartPrice in replacementPartAndReplacementPartPrice.DefaultIfEmpty()
            //                      where replacementPart.PartNo == Convert.ToInt32(searchPartNo.partNo)
            //                      select new
            //                      {
            //                          replacementPart.ReplacementPartId,
            //                          replacementPart.Sad,
            //                          replacementPart.PartNo,
            //                          replacementPartPrice.ReplacementPartPriceId,
            //                          replacementPartPrice.DryerModel,
            //                          replacementPartPrice.EdDrawingItemNo,
            //                          replacementPartPrice.Currency,
            //                          replacementPartPrice.Price,
            //                          replacementPartPrice.Qty,
            //                          replacementPartPrice.Voltage
            //                      } into selection
            //                      select selection;

            //}



            if (replacementList != null)
            {
                foreach (var item in replacementList)
                {
                    AdminReplacementPartView data = new AdminReplacementPartView();
                    data.ReplacementPartId = item.ReplacementPartId;
                    data.Sad = item.Sad;
                    data.PartNo = item.PartNo;
                    data.ReplacementPartPriceId = item.ReplacementPartPriceId;
                    data.DryerModel = item.DryerModel;
                    data.EdDrawingItemNo = item.EdDrawingItemNo;
                    data.Currency = item.Currency;
                    data.Price = item.Price;
                    data.Qty = item.Qty;
                    data.Voltage = item.Voltage;
                    replacementPartData.Add(data);
                }
                response.isSuccess = true;
                response.ReplacementList = replacementPartData;
            }
            else
                response.isSuccess = false;

            return response;
        }


        public GetInboxCountResponse GetInboxCount()
        {
            GetInboxCountResponse response = new GetInboxCountResponse();

            var ReadFlagsNothing = context.TblSshMachineryServicesRepository.Get().Where(q => q.IsApproved == false && q.IsRejected == false);
            var ReadFlagsNothingCount = ReadFlagsNothing.Count();

            response.ReadFlagsNothingCount = ReadFlagsNothingCount;
            response.isSuccess = true;

            return response;

        }


        public GetReplacementListResponse SaveReplacementPrice(UpdateReplacementPrice UpdateReplacementPrice)
        {
            GetReplacementListResponse response = new GetReplacementListResponse();
            TblSshReplacementPartPrice ReplacementPartPrice = new TblSshReplacementPartPrice();

            ReplacementPartPrice.Price = UpdateReplacementPrice.Price;
            ReplacementPartPrice.PartNo = UpdateReplacementPrice.PartNo;
            ReplacementPartPrice.EdDrawingItemNo = UpdateReplacementPrice.EdDrawingItemNo;
            ReplacementPartPrice.DryerModel = UpdateReplacementPrice.DryerModel;
            ReplacementPartPrice.Currency = UpdateReplacementPrice.Currency;
            ReplacementPartPrice.Qty = UpdateReplacementPrice.Qty;
            ReplacementPartPrice.Voltage = UpdateReplacementPrice.Voltage;
            //var dataKontrol = context.TblSshReplacementPartPriceRepository.Get().Where(q => q.PartNo == Convert.ToInt32(UpdateReplacementPrice.PartNo));
            bool kaydedildiMi;
            //if (dataKontrol == null)
            //{
            //     kaydedildiMi = context.TblSshReplacementPartPriceRepository.Insert(ReplacementPartPrice);
            //    context.Save();
            //}
            //else
            //{
            //     kaydedildiMi = context.TblSshReplacementPartPriceRepository.Update(ReplacementPartPrice);
            //    context.Save();
            //}

            kaydedildiMi = context.TblSshReplacementPartPriceRepository.Insert(ReplacementPartPrice);
                context.Save();
            if (kaydedildiMi == false)
            {
                response.isSuccess = false;
                response.ErrorMessage = "Hata oluştu!!";
                return response;
            }

            return response;


        }

        public GetDashboardMenuResponse GetACSAdminMenu(int UserId)
        {
            GetDashboardMenuResponse response = new GetDashboardMenuResponse();
            List<GetDashboardMenu> homeMenu = new List<GetDashboardMenu>();

            var userMenu = from menuItem in context.TblMenuItemsRepository.Get()
                           join userMenuItems in context.TblUserMenuItemRepository.Get()
                           on menuItem.MenuItemId equals userMenuItems.MenuItemId
                           where menuItem.MenuId == 3 && userMenuItems.UserId == UserId
                           select new
                           {
                               menuItem.MenuItemName,
                               menuItem.MenuItemOrderNo,
                               menuItem.MenuItemUrl,
                               menuItem.MenuItemIcon
                           } into selection
                           orderby selection.MenuItemOrderNo ascending
                           select selection;
            if (userMenu != null)
            {
                foreach (var item in userMenu)
                {
                    GetDashboardMenu menu = new GetDashboardMenu();
                    menu.MenuItemName = item.MenuItemName;
                    menu.MenuItemUrl = item.MenuItemUrl;
                    menu.MenuItemIcon = item.MenuItemIcon;
                    homeMenu.Add(menu);
                }
                response.isSuccess = true;
                response.GetDashboardMenuList = homeMenu;
            }
            else
                response.isSuccess = false;

            return response;
        }
    }
}
