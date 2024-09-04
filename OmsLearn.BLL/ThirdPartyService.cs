using EmployeeDirectory.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsLearn.BLL
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> CallThirdPartyApiAsync(string apiUrl, studentreqModel model)
        {
            string requestJson = JsonConvert.SerializeObject(model);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode(); // Throw exception if not successful

            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
