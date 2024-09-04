using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace EmployeeDirectory.Model
{
    public class EmployeeInsertModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkDept { get; set; }
        public long PhoneNo { get; set; }
        public string JoiningDate { get; set; }
        public int Salary { get; set; }
        public int CreatedBy { get; set; }
    }
    public class GetMarketRatesResModel
    {

        public string BfmarketId { get;set; }
        public string MarketRates { get; set;}

        //public int EmpId { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string WorkDept { get; set; }
        //public string PhoneNo { get; set; }
        //public DateTime JoiningDate { get; set; }
        //public int Salary { get; set; }
    }
    public class GetMarketRatesReqModel
    {
        public string BfMarketId { get; set; }
        public int RowCount { get; set; } = 0;
        //public int PageNo { get; set; }
        //public int PageSize { get; set; }
        //public string SearchText { get; set; }
    }
    public class UpdateEmployeeModel
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkDept { get; set; }
        public long PhoneNo { get; set; }
        public int Salary { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class DeleteEmployeeModel
    {
        public int EmpId { get; set; }
    }
    public class Dept
    {
        public string Department { get; set; }
        public List<GetAllEmployee> GetEmployee { get; set; }
        //public int CountDept { get; set; }
        //public int TotalSalary { get; set; }
    }
    public class EmployeeData
    {
        public List<Dept> Data { get; set; }
        public int Count { get; set; }
    }
    public class GetAllEmployee
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkDept { get; set; }
        public string PhoneNo { get; set; }
        public DateTime JoiningDate { get; set; }
        public int Salary { get; set; }
    }
    public class Filter
    {
        public int MinSalary { get; set; }
    }
    public class InsertBulkData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkDept { get; set; }
        public long PhoneNo { get; set; }
        public string JoiningDate { get; set; }
        public int Salary { get; set; }
        public int CreatedBy { get; set; }
    }
    public class GetBooksModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublishedYear { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int StockQuantity { get; set; }
        public int Pages { get; set; }
        public bool IsFavorite { get; set; }
    }
    public class Pagination
    {
        public int RowCount { get; set; } = 0;
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }
    }
    public class favorite
    {
        public string Title { get; set; }
    }
    public class AddbooksModel
    {
        public int ID { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublishedYear { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int StockQuantity { get; set; }
        public int Pages { get; set; }
        public int CreatedBy { get; set; }
    }
    public class GetFavoriteModel
    {
        public int RowCount { get; set; } = 0;
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    public class ImageInsert
    {
        public IFormFile File { get; set; }
    }
    public class Authentication
    {
        public int Id { get; set; }
        public string Password { get; set; }

    }
    public class studentreqModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }
    }
    public class GetStudentReqModel
    {
        public int StudentId { get; set; }
        public string Student_Name { get; set; }
        public int ClassId { get; set; }
        public int TeacherId { get; set; }

    }
    public class GetBfIdModel
    {   public int FraudPlayerId { get;set; }
        public string BfMarketID { get; set; }
        public DateTime BeforeTime { get; set; }
        public DateTime AfterTime { get; set; }
    }

    public class GetMarketRateModel
    {
        public string marketRates { get; set; }
    }
    //public class MarketData
    //{
    //    public string MarketId { get; set; }
    //    public string MatchId { get; set; }
    //    public DateTime RecordTime { get; set; }
    //    public int RunnerId { get; set; }
    //    public string RunnerStatus { get; set; }
    //    public List<Rates> Rate { get; set; }
    //}

    //public class Rates
    //{
    //    public int BetType { get; set; }
    //    public int Priority { get; set; }
    //    public decimal Rate { get; set; }
    //    public decimal Volume { get; set; }
    //}

    //public class Root
    //{
    //    public string _id { get; set; }
    //    public string sourceId { get; set; }
    //    public Data data { get; set; }
    //    public Time time { get; set; }
    //}
    //public class Data
    //{
    //    public string bmi { get; set; }
    //    public int ip { get; set; }
    //    public int mi { get; set; }
    //    public int ms { get; set; }
    //    public string eti { get; set; }
    //    public string eid { get; set; }
    //    public Grt grt { get; set; }
    //    public double? tv { get; set; }
    //    public List<Rt> rt { get; set; }
    //}



    //public class Rt
    //{
    //    public double? tv { get; set; }
    //    public int id { get; set; }
    //    public object adjustmentFactor { get; set; }
    //    public string status { get; set; }
    //    public int sortPriority { get; set; }
    //    public List<List<double>> bdatb { get; set; }
    //    public List<List<double>> bdatl { get; set; }
    //}

    //public class Grt
    //{
    //    [JsonProperty("$date")]
    //    public long Date { get; set; }
    //}
    //public class Id
    //{
    //    [JsonProperty("$oid")]
    //    public string oid { get; set; }
    //}
    //public class Time
    //{
    //    [JsonProperty("$date")]
    //    public long date { get; set; }
    //}

    public class MarketData
    {
        public string bmi { get; set; }
        public string eid { get; set; }
        public Grt grt { get; set; }
        public List<Rates> rt { get; set; }
    }

    public class Rates
    {
        public int Id { get; set; }
        public string Status { get; set; }


        public List<List<double>> bdatb { get; set; }
        public List<List<double>> bdatl { get; set; }
    }

    //public class Root
    //{
    //    public MarketData data { get; set; }
    //}






    //public class Data
    //{
    //    public string bmi { get; set; }
    //    public int ip { get; set; }
    //    public int mi { get; set; }
    //    public int ms { get; set; }
    //    public string eti { get; set; }
    //    public string eid { get; set; }
    //    public Grt grt { get; set; }
    //    public double? tv { get; set; }
    //    public List<Rt> rt { get; set; }
    //}



    //public class Rt
    //{
    //    public double? tv { get; set; }
    //    public int id { get; set; }
    //    public object adjustmentFactor { get; set; }
    //    public string status { get; set; }
    //    public int sortPriority { get; set; }
    //    public List<List<double>> bdatb { get; set; }
    //    public List<List<double>> bdatl { get; set; }
    //}

    public class Grt
    {
        [JsonProperty("$date")]
        public long Date { get; set; }
    }
    //public class Id
    //{
    //    [JsonProperty("$oid")]
    //    public string oid { get; set; }
    //}
    //public class Time
    //{
    //    [JsonProperty("$date")]
    //    public long date { get; set; }
    //}


    public class JsonDataTestingModel
    {
        public int DataNum { get; set; }
        public DateTime Date { get; set; }
        public double OpeningBlc { get; set; }
        public int ProductType { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string? Remark { get; set; }
        public decimal? Balance { get; set; }
        public int MarketId { get; set; }

    }


}