using EmployeeDirectory.BLL.Interface;
using EmployeeDirectory.DAL;
using EmployeeDirectory.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver.Core.Configuration;
using Newtonsoft.Json;
using OmsLearn.BLL;
using System.Data.SqlClient;
using System.Data;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Media;
using System.Net.NetworkInformation;

namespace EmployeeDirectory.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController : Controller
    {
        private readonly ApiService _apiService;

        DateTime reqDateTime = DateTime.UtcNow;
        private readonly ILearn Ilearn;
        private readonly IHttpContextAccessor ihttpContextAccessor;
        IConfiguration iConfiguration;
        IDistributedCache idistributedCache;
        public EmployeeController(ILearn employee, IConfiguration iConfiguration, IDistributedCache distributedCache, ApiService apiService, IHttpContextAccessor ihttpContextAccessor)
        {
            this.Ilearn = employee;
            this.iConfiguration = iConfiguration;
            _apiService = apiService;
            this.idistributedCache = distributedCache;
            this.ihttpContextAccessor = ihttpContextAccessor;
        }

        [HttpPost]
        [Route("InsertEmployee")]
        public async Task<IActionResult> InsertEmployee([FromBody] EmployeeInsertModel model)
        {
            string method = "InsertEmployee";

            Transtatus transtatus = new Transtatus();
            Dictionary<string, object> dctdata = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";

            try
            {
                if (string.IsNullOrEmpty(model.FirstName) && string.IsNullOrEmpty(model.LastName))
                {
                    transtatus.Message = "Please enter a valid Employee name.";
                    transtatus.Code = 1;
                }
                else if (model.PhoneNo.ToString().Length != 10)
                {
                    transtatus.Message = "You enter invalid Phone number.";
                }
                else
                {
                    transtatus = await Ilearn.InsertEmployee(model);
                }
            }
            catch (Exception ex)
            {
                transtatus.Message = ex.Message;
                transtatus.Code = 1;
                statusCode = HttpStatusCode.BadRequest;
            }
            finally
            {
                dctdata.Add("Status", transtatus);
                string path = "";
                string message = "*******************************************************************" + Environment.NewLine
                                           + "Method :" + method + Environment.NewLine
                                           + "Request UTC :" + reqDateTime.ToString() + Environment.NewLine
                                           + "Response UTC :" + DateTime.UtcNow.ToString() + Environment.NewLine
                                           + "Request:" + JsonConvert.SerializeObject(model) + Environment.NewLine
                                           + "Response:" + JsonConvert.SerializeObject(dctdata) + Environment.NewLine
                                           + "Exception:" + systemError + Environment.NewLine;

                path = string.Format("\\{0}\\{1}", path, DateTime.UtcNow.ToString("yyyyMMdd"));
                CommomHelper.WriteToFile(message, path, DateTime.UtcNow.ToString("hh_00 tt"), false);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), dctdata);
        }

        [HttpGet]
        [Route("GetEmployee")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEmployeeData([FromBody] GetMarketRatesReqModel model)
        {
            string method = "GetEmployeeData";

            Transtatus transtatus = new Transtatus();
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";

            try
            {
                var result = await Ilearn.GetMarketRates(model);

                //foreach (var entry in result.Item1)
                //{
                //    entry.JoiningDate = new DateTime(entry.JoiningDate.Year, entry.JoiningDate.Month, entry.JoiningDate.Day);
                //}

                //if (result.Item2.Code == 1)
                //{
                //    dctData.Add("Status", result.Item2);

                //}
                //dctData.Add("Data", result.Item1);
                //dctData.Add("RowCount", result.Item3);

            }
            catch (Exception)
            {
                transtatus.Message = "Something went wrong.";
                transtatus.Code = 1;
                statusCode = HttpStatusCode.BadRequest;

            }
            finally
            {
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

        [HttpPost]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeModel model)
        {
            string method = "UpdateEmployee";

            Transtatus transtatus = new Transtatus();
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";

            try
            {
                if (string.IsNullOrEmpty(model.FirstName) && string.IsNullOrEmpty(model.LastName))
                {
                    transtatus.Message = "Please enter a valid Data";
                    transtatus.Code = 1;
                }
                else
                {
                    transtatus = await Ilearn.UpdateEmployee(model);
                }
            }
            catch (Exception)
            {
                transtatus.Message = "Something Went wrong.";
                transtatus.Code = 1;
                statusCode = HttpStatusCode.BadRequest;
            }
            finally
            {
                dctData.Add("status", transtatus);
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

        [HttpPost]
        [Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromBody] DeleteEmployeeModel model)
        {
            string method = "DeleteEmployee";

            Transtatus transtatus = new Transtatus();
            Dictionary<string, object> DctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";

            try
            {
                transtatus = await Ilearn.DeleteEmployee(model);
            }
            catch (Exception)
            {
                transtatus.Message = "Something went Wrong";
                transtatus.Code = 1;
                statusCode = HttpStatusCode.BadRequest;
            }
            finally
            {
                DctData.Add("Status", transtatus);
                string path = "";
                string message = "*******************************************************************" + Environment.NewLine
                                           + "Method :" + method + Environment.NewLine
                                           + "Request UTC :" + reqDateTime.ToString() + Environment.NewLine
                                           + "Response UTC :" + DateTime.UtcNow.ToString() + Environment.NewLine
                                           + "Request:" + JsonConvert.SerializeObject(model) + Environment.NewLine
                                           + "Response:" + JsonConvert.SerializeObject(DctData) + Environment.NewLine
                                           + "Exception:" + systemError + Environment.NewLine;

                path = string.Format("\\{0}\\{1}", path, DateTime.UtcNow.ToString("yyyyMMdd"));
                CommomHelper.WriteToFile(message, path, DateTime.UtcNow.ToString("hh_00 tt"), false);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), DctData);
        }

        [HttpPost]
        [Route("BulkInsert")]
        public async Task<IActionResult> BulkInsert([FromBody] List<InsertBulkData> dataTable)
        {
            string method = "BulkInsert";

            Transtatus transtatus = new Transtatus();
            Dictionary<string, object> DctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";

            try
            {
                transtatus = await Ilearn.BulkInsert(dataTable);
            }
            catch (Exception)
            {
                transtatus.Message = "Something went Wrong";
                transtatus.Code = 1;
                statusCode = HttpStatusCode.BadRequest;
            }
            finally
            {
                DctData.Add("status", transtatus);
                string path = "";
                string message = "*******************************************************************" + Environment.NewLine
                                           + "Method :" + method + Environment.NewLine
                                           + "Request UTC :" + reqDateTime.ToString() + Environment.NewLine
                                           + "Response UTC :" + DateTime.UtcNow.ToString() + Environment.NewLine
                                           + "Request:" + JsonConvert.SerializeObject(dataTable) + Environment.NewLine
                                           + "Response:" + JsonConvert.SerializeObject(DctData) + Environment.NewLine
                                           + "Exception:" + systemError + Environment.NewLine;

                path = string.Format("\\{0}\\{1}", path, DateTime.UtcNow.ToString("yyyyMMdd"));
                CommomHelper.WriteToFile(message, path, DateTime.UtcNow.ToString("hh_00 tt"), false);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), DctData);
        }

        [HttpGet]
        [Route("GetBooks")]
        public async Task<IActionResult> GetBookData([FromBody] Pagination model)
        {
            string method = "GetBookData";
            string dataKey = GetBookKey.Datakey;
            string serializedList;
            Tuple<List<GetBooksModel>, Transtatus, int> BookData = new Tuple<List<GetBooksModel>, Transtatus, int>(new List<GetBooksModel>(), new Transtatus(), 0);

            Transtatus transtatus = new Transtatus();
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";

            try
            {
                var redisDb = await idistributedCache.GetAsync(dataKey);
                if (redisDb != null)
                {
                    try
                    {
                        serializedList = Encoding.UTF8.GetString(redisDb);
                        BookData = JsonConvert.DeserializeObject<Tuple<List<GetBooksModel>, Transtatus, int>>(serializedList) ?? new Tuple<List<GetBooksModel>, Transtatus, int>(new List<GetBooksModel>(), new Transtatus(), 0);
                    }
                    catch (Exception)
                    {
                    }
                }
                if (BookData.Item1?.Count == 0)
                {
                    BookData = await Ilearn.GetBookData(model);
                    serializedList = JsonConvert.SerializeObject(BookData);
                    redisDb = Encoding.UTF8.GetBytes(serializedList);

                    var time = new DistributedCacheEntryOptions()
                                    .SetAbsoluteExpiration(DateTime.Now.AddHours(GetBookKey.ExpiryTime));
                    await idistributedCache.SetAsync(dataKey, redisDb, time);
                }

                if (BookData?.Item2.Code == 1)
                {
                    dctData.Add("Status", BookData.Item2);

                }
                dctData.Add("Data", BookData.Item1);
                dctData.Add("RowCount", BookData.Item3);

            }
            catch (Exception ex)
            {
                transtatus.Message = "Something went wrong.";
                transtatus.Code = 1;
                statusCode = HttpStatusCode.BadRequest;
                systemError = JsonConvert.SerializeObject(ex);

            }
            finally
            {
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

        [HttpPost]
        [Route("AddFavorite")]
        public async Task<IActionResult> AddFavorite([FromBody] favorite model)
        {
            string method = "AddFavorite";


            Transtatus transtatus = new Transtatus();
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";


            try
            {
                transtatus = await Ilearn.AddFavorite(model);

            }
            catch (Exception)
            {
                transtatus.Message = "Something Went wrong.";
                transtatus.Code = 1;
                statusCode = HttpStatusCode.BadRequest;
            }
            finally
            {

                dctData.Add("status", transtatus);
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

        [HttpPost]
        [Route("AddBooks")]
        public async Task<IActionResult> AddBooks([FromBody] AddbooksModel model)
        {
            string method = "AddBooks";

            Transtatus transtatus = new Transtatus();
            Dictionary<string, object> dctdata = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";

            try
            {
                if (string.IsNullOrEmpty(model.Title))
                {
                    transtatus.Message = "Please enter a valid Book name.";
                    transtatus.Code = 1;
                }
                else
                {
                    transtatus = await Ilearn.AddBooks(model);
                }
            }
            catch (Exception ex)
            {
                transtatus.Message = "Something went Wrong.";
                transtatus.Code = 1;
                statusCode = HttpStatusCode.BadRequest;
                systemError = JsonConvert.SerializeObject(ex);

            }
            finally
            {
                dctdata.Add("Status", transtatus);
                string path = "";
                string message = "*******************************************************************" + Environment.NewLine
                                           + "Method :" + method + Environment.NewLine
                                           + "Request UTC :" + reqDateTime.ToString() + Environment.NewLine
                                           + "Response UTC :" + DateTime.UtcNow.ToString() + Environment.NewLine
                                           + "Request:" + JsonConvert.SerializeObject(model) + Environment.NewLine
                                           + "Response:" + JsonConvert.SerializeObject(dctdata) + Environment.NewLine
                                           + "Exception:" + systemError + Environment.NewLine;

                path = string.Format("\\{0}\\{1}", path, DateTime.UtcNow.ToString("yyyyMMdd"));
                CommomHelper.WriteToFile(message, path, DateTime.UtcNow.ToString("hh_00 tt"), false);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), dctdata);

        }

        [HttpGet]
        [Route("GetFavorite")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFavorite([FromBody] GetFavoriteModel model)
        {
            Transtatus transtatus = new Transtatus();
            string method = "GetFavorite";

            string dataKey = GetFavoriteKey.Datakey;
            string serializedfavoriteList;
            Dictionary<string, object> dctData = new Dictionary<string, object>();

            Tuple<List<GetBooksModel>, int> favoriteTask = new Tuple<List<GetBooksModel>, int>(new List<GetBooksModel>(), 0);
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";

            try
            {
                var redisDb = await idistributedCache.GetAsync(dataKey);
                if (redisDb != null)
                {
                    try
                    {
                        serializedfavoriteList = Encoding.UTF8.GetString(redisDb);
                        favoriteTask = JsonConvert.DeserializeObject<Tuple<List<GetBooksModel>, int>>(serializedfavoriteList) ?? new Tuple<List<GetBooksModel>, int>(new List<GetBooksModel>(), 0);
                    }
                    catch (Exception)
                    {
                    }
                }
                if (favoriteTask?.Item1?.Count == 0)
                {
                    favoriteTask = await Ilearn.GetFavorite(model);
                    serializedfavoriteList = JsonConvert.SerializeObject(favoriteTask);
                    redisDb = Encoding.UTF8.GetBytes(serializedfavoriteList);

                    var time = new DistributedCacheEntryOptions()
                                    .SetAbsoluteExpiration(DateTime.Now.AddHours(GetFavoriteKey.ExpiryTime));
                    await idistributedCache.SetAsync(dataKey, redisDb, time);
                }
                //dctData.Add("Data", favoriteTask.Item1);
                //dctData.Add("RowCount", favoriteTask.Item2);

            }
            catch (Exception ex)
            {
                favoriteTask = await Ilearn.GetFavorite(model);
                transtatus.Message = "Something went wrong please try again";
                transtatus.Code = 1;
                systemError = JsonConvert.SerializeObject(ex);
            }
            finally
            {
                dctData.Add("Data", favoriteTask.Item1);
                dctData.Add("RowCount", favoriteTask.Item2);
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

        [HttpGet]
        [Route("GetAllEmployee")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEmployeeData()
        {
            string method = "GetAllEmployeeData";

            EmployeeData employeeData = new EmployeeData();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";
            try
            {

                employeeData = await Ilearn.GetAllEmployeeData();
            }
            catch
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            finally
            {
                string path = "";
                string message = "*******************************************************************" + Environment.NewLine
                                           + "Method :" + method + Environment.NewLine
                                           + "Request UTC :" + reqDateTime.ToString() + Environment.NewLine
                                           + "Response UTC :" + DateTime.UtcNow.ToString() + Environment.NewLine
                                           + "Request:" + JsonConvert.SerializeObject("") + Environment.NewLine
                                           + "Response:" + JsonConvert.SerializeObject(employeeData) + Environment.NewLine
                                           + "Exception:" + systemError + Environment.NewLine;



                path = string.Format("\\{0}\\{1}", path, DateTime.UtcNow.ToString("yyyyMMdd"));
                CommomHelper.WriteToFile(message, path, DateTime.UtcNow.ToString("hh_00 tt"), false);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), employeeData);
        }

        [HttpPost]
        [Route("AddImage")]
        public async Task<IActionResult> InsertImage([FromForm] ImageInsert model)
        {
            string method = "InsertImage";
            Transtatus transtatus = new Transtatus();
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";

            try
            {
                if (model.File != null && model.File.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Upload");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.File.CopyTo(stream);
                    }
                    transtatus = await Ilearn.InsertImage(filePath);
                }
            }
            catch (Exception ex)
            {
                transtatus.Message = "Something Went wrong.";
                transtatus.Code = 1;
                statusCode = HttpStatusCode.BadRequest;
                systemError = JsonConvert.SerializeObject(ex);
            }
            finally
            {
                dctData.Add("status", transtatus);
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

        [HttpGet("call")]
        [AllowAnonymous]
        public async Task<IActionResult> CallApi([FromBody] studentreqModel model)
        {
            string apiUrl = "https://localhost:44394/api/StudentInsert/calling"; // Replace with the URL of the first project's API
            Tuple<List<GetStudentReqModel>, int> answer = new Tuple<List<GetStudentReqModel>, int>(new List<GetStudentReqModel>(), 0);
            Dictionary<string, object> dctData = new Dictionary<string, object>();

            try
            {
                string response = await _apiService.CallThirdPartyApiAsync(apiUrl, model);
                answer = JsonConvert.DeserializeObject<Tuple<List<GetStudentReqModel>, int>>(response) ?? new Tuple<List<GetStudentReqModel>, int>(new List<GetStudentReqModel>(), 0);

                dctData.Add("Data", answer.Item1);
                dctData.Add("Count", answer.Item2);
                return Ok(dctData);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while calling the API from student insert.");
            }
        }

        [HttpGet]
        [Route("GetData")]
        [AllowAnonymous]
        public async Task<IActionResult> GetData()
        {
            string method = "GetData";

            Transtatus transtatus = new Transtatus();
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";

            try
            {
                var result = await Ilearn.GetData();

                dctData.Add("Data", result);

            }
            catch (Exception)
            {
                transtatus.Message = "Something went wrong.";
                transtatus.Code = 1;
                statusCode = HttpStatusCode.BadRequest;
            }
            finally
            {
                string path = "";
                string message = "*******************************************************************" + Environment.NewLine
                                           + "Method :" + method + Environment.NewLine
                                           + "Request UTC :" + reqDateTime.ToString() + Environment.NewLine
                                           + "Response UTC :" + DateTime.UtcNow.ToString() + Environment.NewLine
                                           + "Request:" + JsonConvert.SerializeObject("") + Environment.NewLine
                                           + "Response:" + JsonConvert.SerializeObject(dctData) + Environment.NewLine
                                           + "Exception:" + systemError + Environment.NewLine;

                path = string.Format("\\{0}\\{1}", path, DateTime.UtcNow.ToString("yyyyMMdd"));
                CommomHelper.WriteToFile(message, path, DateTime.UtcNow.ToString("hh_00 tt"), false);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), dctData);
        }


        [HttpPost]
        [Route("JsonData")]
        [AllowAnonymous]
        public async Task<IActionResult> JsonData([FromBody] List<JsonDataTestingModel> model)
        {
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;

            var dataNum = model.Select(x => x.DataNum).Distinct().ToList();

            foreach (var item in dataNum)
            {
                double Openbalace = model.Where(y => y.DataNum == item).Select(x => x.OpeningBlc).FirstOrDefault();
                foreach (var item1 in model)
                {
                    if (item1.DataNum == item)
                    {
                        var currentBalance = Openbalace + item1.Credit;
                        item1.Balance = (decimal)(currentBalance - item1.Debit);
                        Openbalace = (double)item1.Balance;
                    }
                }
            }

            dctData.Add("Result", model);
            return this.StatusCode(Convert.ToInt32(statusCode), dctData);
        }

        [HttpPost]
        [Route("audio")]
        [AllowAnonymous]
        public async Task<IActionResult> ProcessCommandAsync([FromForm] IFormFile command)
        {
            byte[] audioBytes;

            // Read the audio file content into a byte array
            using (var memoryStream = new MemoryStream())
            {
                command.CopyToAsync(memoryStream).Wait();
                audioBytes = memoryStream.ToArray();
            }

            // Convert byte array to Base64 string
            string data = Convert.ToBase64String(audioBytes);

            var compressedString = CompressBase64String(data);

            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                Transtatus transtatus = new Transtatus();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.AddAudio";
                await connection.OpenAsync();
                try
                {

                    cmd.Parameters.AddWithValue("@Audio", compressedString);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return Ok();
        }

        //[HttpGet]
        //[Route("GetAudio")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetAudio()
        //{
        //    Stack<int> num = new Stack<int>();
        //    string AudioString = string.Empty;
        //    using (var connection = new SqlConnection(Connection.ConnectionString))
        //    {
        //        Transtatus transtatus = new Transtatus();
        //        SqlCommand cmd = connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "dbo.GetAudiotoPlay";
        //        await connection.OpenAsync();
        //        cmd.Parameters.Add("@AudioString", SqlDbType.NVarChar, int.MaxValue).Direction = ParameterDirection.Output;
        //        await cmd.ExecuteNonQueryAsync();
        //        AudioString = (string)cmd.Parameters["@AudioString"].Value;
        //    }
        //    byte[] byteArray = Convert.FromBase64String(AudioString);

        //    using (MemoryStream memoryStream = new MemoryStream(byteArray))
        //    {
        //        // Create a SoundPlayer object to play the audio
        //        using (SoundPlayer player = new SoundPlayer(memoryStream))
        //        {
        //            // Play the audio directly from the memory stream
        //            player.Play();
        //        }
        //    }

        //    return Ok();
        //}


        [HttpGet]
        [AllowAnonymous]
        [Route("GetIPAddress")]
        public async Task<IActionResult> GetIPAddress()
        {
            string IPAddress = string.Empty;

            //string CreatedIpv4 = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            string CreatedIpv1 = JsonConvert.SerializeObject(ihttpContextAccessor.HttpContext.Connection);
            string CreatedIpv2 = JsonConvert.SerializeObject(HttpContext.Connection);
            //string CreatedIpv3 = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(); ;

            return Ok(new { CreatedIpv1 = CreatedIpv1, CreatedIpv2 = CreatedIpv2 });

        }

        [HttpGet]
        [Route("GetMacAddress")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMacAddress()
        {
            string MacAddress = "There is no value";

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterface networkInterface = networkInterfaces[0];
            PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
            byte[] addressBytes = physicalAddress.GetAddressBytes();
            MacAddress = BitConverter.ToString(addressBytes).Replace("-", ":");

            return Ok(new { MacAdress = MacAddress });
        }
        public static string CompressBase64String(string base64String, CompressionMethod compressionMethod)
        {
            byte[] compressedBytes;

            // Convert Base64 string to byte array
            byte[] base64Bytes = Convert.FromBase64String(base64String);

            // Compress the byte array using chosen method
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Stream compressionStream = null;

                if (compressionMethod == CompressionMethod.Deflate)
                {
                    compressionStream = new DeflateStream(memoryStream, CompressionLevel.Optimal);
                }
                else if (compressionMethod == CompressionMethod.GZip)
                {
                    compressionStream = new GZipStream(memoryStream, CompressionLevel.Optimal);
                }

                compressionStream.Write(base64Bytes, 0, base64Bytes.Length);
                compressionStream.Close();

                compressedBytes = memoryStream.ToArray();
            }

            // Convert compressed byte array back to Base64 string
            return Convert.ToBase64String(compressedBytes);
        }

        public static string DecompressBase64String(string base64String, CompressionMethod compressionMethod)
        {
            byte[] decompressedBytes;

            // Convert Base64 string to byte array
            byte[] compressedBytes = Convert.FromBase64String(base64String);

            // Decompress the byte array using chosen method
            using (MemoryStream compressedStream = new MemoryStream(compressedBytes))
            {
                using (MemoryStream decompressedStream = new MemoryStream())
                {
                    Stream decompressionStream = null;

                    if (compressionMethod == CompressionMethod.Deflate)
                    {
                        decompressionStream = new DeflateStream(compressedStream, CompressionMode.Decompress);
                    }
                    else if (compressionMethod == CompressionMethod.GZip)
                    {
                        decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress);
                    }

                    decompressionStream.CopyTo(decompressedStream);
                    decompressionStream.Close();

                    decompressedBytes = decompressedStream.ToArray();
                }
            }

            // Convert decompressed byte array to Base64 string
            return Convert.ToBase64String(decompressedBytes);
        }
        #region Compression
        public static string ConvertToByteArrayContent(IFormFile audofile)
        {
            using (var memoryStream = new MemoryStream())
            {
                audofile.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }
        public static string CompressBase64String(string base64String)
        {
            byte[] decodedBytes = Convert.FromBase64String(base64String);

            using (var outputStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    gzipStream.Write(decodedBytes, 0, decodedBytes.Length);
                }

                return Convert.ToBase64String(outputStream.ToArray());
            }
        }
        public static string DecompressBase64String(string compressedBase64String)
        {
            byte[] compressedBytes = Convert.FromBase64String(compressedBase64String);

            using (var inputStream = new MemoryStream(compressedBytes))
            using (var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var outputStream = new MemoryStream())
            {
                gzipStream.CopyTo(outputStream);
                return Convert.ToBase64String(outputStream.ToArray());
            }
        }
        #endregion
    }
}
public enum CompressionMethod
{
    Deflate,
    GZip
}

 
