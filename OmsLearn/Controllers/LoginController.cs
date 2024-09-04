using EmployeeDirectory.BLL.Interface;
using EmployeeDirectory.Model;
using Ipify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;


namespace EmployeeDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILearn Ilearn;
        IConfiguration iConfiguration;
        private readonly IHttpContextAccessor ihttpContextAccessor;
        private static readonly HttpClient client = new HttpClient();
        //IHttpContextAccessor ihttpContextAccessor;
        public LoginController(ILearn employee, IConfiguration iConfiguration, IHttpContextAccessor ihttpContextAccessor)
        {
            this.Ilearn = employee;
            this.iConfiguration = iConfiguration;
            this.ihttpContextAccessor = ihttpContextAccessor;
        }

        [HttpPost]
        [Route("LoginPage")]
        public async Task<IActionResult> LoginUser([FromBody] Authentication model)
        {
            Transtatus transtatus = new Transtatus();
            Dictionary<string, object> dctData = new Dictionary<string, object>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string systemError = "NO SYSTEM ERROR";
            try
            {
                transtatus = await Ilearn.LoginUser(model);
                if (transtatus.Code == 1)
                {
                    transtatus.Message = "Invalid Credentials";
                    transtatus.Code = 1;
                    return BadRequest("Unauthorized");
                }
                else
                {
                    transtatus.Message = "Login Success";

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
                string Token = GetToken(model);
                dctData.Add("status", transtatus);
                dctData.Add("Token", Token);
            }
            return this.StatusCode(Convert.ToInt32(statusCode), dctData);
        }
        public string GetToken(Authentication authentication)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,iConfiguration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("Id",authentication.Id.ToString()),
                new Claim("Password",authentication.Password)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iConfiguration["Jwt:Key"]));
            var singin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                iConfiguration["Jwt:Issuer"],
                iConfiguration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: singin);

            string Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }

        [HttpGet]
        [Route("CallAPI")]
        public async Task<IActionResult> CallAPI()
        {
            string method = "Hello World";
            HttpStatusCode statusCode = HttpStatusCode.OK;

            try
            {
                ExecuteSp();
            }
            catch
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            return this.StatusCode(Convert.ToInt32(statusCode), method);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetIPAddress")]
        public async Task<IActionResult> GetIPAddress()
        {
            string IPAddress = string.Empty;
            string Data = string.Empty;

            //string ip4 = await GetPublicIPv4Async();
            //string ip6 = await GetPublicIPv6Async();


            //var ip4 = IpifyIp.GetPublicIp();
            //var ip6 = IpifyIpv6.GetPublicIpV6();

            string ipv = GetIPAddress4();
            string data = GetIp();
            return Ok(new { CreatedIpv4 = ipv, });
            //var ipAddress = ihttpContextAccessor.HttpContext.Connection.RemoteIpAddress;

            ////var nginxHelper = new NginxHelper();

            //// Check for X-Real-IP header first (assuming Nginx is configured to forward it)


            //if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
            //{
            //    // IPv4 address
            //    string ipv4Address = ipAddress.ToString();
            //}
            //else if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
            //{
            //    // IPv6 address
            //    string ipv6Address = ipAddress.ToString();
            //}
            ////string CreatedIpv4 = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            ////string CreatedIpv1 = JsonConvert.SerializeObject(ihttpContextAccessor.HttpContext.Connection);
            //try
            //{

            //    IPAddress = JsonConvert.SerializeObject(ihttpContextAccessor.HttpContext.Connection.RemoteIpAddress);
            //    string CreatedIpv6 = ihttpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString();
            //} 
            //catch (Exception ex)
            //{

            //    throw;
            //}

        }

        public string GetIp()
        {
            try
            {
                //var context = System.Web.HttpContext.Current;
                var context = ihttpContextAccessor.HttpContext;
                if (context == null)
                {
                    throw new InvalidOperationException("No HTTP context available.");
                }

                var request =  context.Request;
                if (request == null)
                {
                    throw new InvalidOperationException("No HTTP request available.");
                }

                //string ip = request.ServerVariables ["HTTP_X_FORWARDED_FOR"];
                //if (string.IsNullOrEmpty(ip))
                //{
                //    //ip = request.ServerVariables["REMOTE_ADDR"];
                //}
                //return ip ?? "IP not found";
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return $"Error: {ex.Message}";
            }
            return default;
        }




        public static string GetLocalIPAddress()
        {

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public string GetIPAddress4()
        {
            string IPAddress = string.Empty;

            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
                return IPAddress;
        } 

        //public static async Task<string> GetPublicIPv4Async()
        //{
        //    string ipv4Url = "https://api.ipify.org?format=text";
        //    HttpResponseMessage response = await client.GetAsync(ipv4Url);
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadAsStringAsync();
        //}

        //public static async Task<string> GetPublicIPv6Async()
        //{
        //    string ipv6Url = "https://api6.ipify.org?format=text";
        //    HttpResponseMessage response = await client.GetAsync(ipv6Url);
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadAsStringAsync();
        //}

        public void ExecuteSp()
        {
            Task.Run(async () =>
           {
               while (true)
               {
                   await Task.Delay(TimeSpan.FromSeconds(5));
                   using (var connection = new SqlConnection(Connection.ConnectionString))
                   {
                       SqlCommand cmd = connection.CreateCommand();
                       cmd.CommandType = CommandType.StoredProcedure;
                       cmd.CommandText = "dbo.InsertLogs";
                       await connection.OpenAsync();
                       await cmd.ExecuteNonQueryAsync();

                   }
               }
           });
        }
    }
}
