using EmployeeDirectory.BLL.Interface;
using EmployeeDirectory.DAL;
using EmployeeDirectory.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OmsLearn.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;


namespace EmployeeDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropdownController : ControllerBase
    {
        private readonly IDropDown idropdown;
        DateTime reqDateTime = DateTime.UtcNow;
        public DropdownController(IDropDown dropdown)
        {
            this.idropdown = dropdown;
        }

        [HttpPost]
        [Route("GetSportDropdown_List")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSportDropdown_List(GetSportDropdown_ListReqModel model)
        {
            int TotalRecords = 0;
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            List<DropdownCommonResModel> dropdownList = new List<DropdownCommonResModel>();
            try
            {
                (dropdownList, TotalRecords) = await idropdown.GetSportDropdown_List(model);
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            finally
            {
                dctData.Add("Data", dropdownList);
                dctData.Add("TotalRecords", TotalRecords);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), dctData);
        }
        [HttpPost]
        [Route("GetTournament_DropdownList")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTournament_DropdownList(GetTournament_DropdownListReqModel model)
        {
            int TotalRecords = 0;
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            List<DropdownCommonResModel> dropdownList = new List<DropdownCommonResModel>();
            try
            {
                (dropdownList, TotalRecords) = await idropdown.GetTournament_DropdownList(model);
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            finally
            {
                dctData.Add("Data", dropdownList);
                dctData.Add("TotalRecords", TotalRecords);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), dctData);
        }
        [HttpPost]
        [Route("GetMatch_DropdownList")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMatch_DropdownList(GetMatch_DropdownListReqModel model)
        {
            int TotalRecords = 0;
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            List<DropdownCommonResModel> dropdownList = new List<DropdownCommonResModel>();
            try
            {
                (dropdownList, TotalRecords) = await idropdown.GetMatch_DropdownList(model);
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            finally
            {
                dctData.Add("Data", dropdownList);
                dctData.Add("TotalRecords", TotalRecords);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), dctData);
        }
        [HttpPost]
        [Route("GetMarket_DropdownList")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMarket_DropdownList(GetMarket_DropdownListReqModel model)
        {
            int TotalRecords = 0;
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            List<DropdownCommonResModel> dropdownList = new List<DropdownCommonResModel>();
            try
            {
                (dropdownList, TotalRecords) = await idropdown.GetMarket_DropdownList(model);
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            finally
            {
                dctData.Add("Data", dropdownList);
                dctData.Add("TotalRecords", TotalRecords);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), dctData);
        }
        [HttpPost]
        [Route("GetPlayerList")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlayerList(GetPlayerListReqModel model)
        {
            string method = "GetPlayerList";
            int TotalRecords = 0;
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            List<GetPlayerListResModel> GetPlayer = new List<GetPlayerListResModel>();
            string systemError = "NO SYSTEM ERROR";
            try
            {
                (GetPlayer, TotalRecords) = await idropdown.GetPlayerList(model);
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.BadRequest; 
                systemError = JsonConvert.SerializeObject(ex);
            }
            finally
            {
                dctData.Add("Data", GetPlayer); 
                dctData.Add("TotalRecords", TotalRecords);

                string path = "";
                string message = "*******************************************************************" + Environment.NewLine
                                           + "Method :" + method + Environment.NewLine
                                           + "Request UTC :" + reqDateTime.ToString() + Environment.NewLine
                                           + "Response UTC :" + DateTime.UtcNow.ToString() + Environment.NewLine
                                           + "Request:" + JsonConvert.SerializeObject(model) + Environment.NewLine
                                           + "Response:" + JsonConvert.SerializeObject(dctData) + Environment.NewLine
                                           + "Exception:" + systemError + Environment.NewLine;

                path = string.Format("\\{0}\\{1}", path, DateTime.UtcNow.ToString("yyyyMMdd"));
                CommomHelper.WriteToFile(message, path, DateTime.UtcNow.ToString("hh_00 tt"), false);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), dctData);
        }
    }
}
