using EmployeeDirectory.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OmsLearn.Model;
using System.Data;
using System.Data.SqlClient;
using Root = OmsLearn.Model.Root;

namespace EmployeeDirectory.DAL
{
    public class LearnRepository : BaseRepository
    {
        public async Task<Transtatus> InsertEmployee(EmployeeInsertModel model)
        {
            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                Transtatus transtatus = new Transtatus();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.InputEmployee";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@WorkDept", model.WorkDept);
                cmd.Parameters.AddWithValue("@PhoneNo", model.PhoneNo);
                cmd.Parameters.AddWithValue("@JoiningDate", model.JoiningDate);
                cmd.Parameters.AddWithValue("@Salary", model.Salary);
                cmd.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                cmd.Parameters.Add("@message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@code", SqlDbType.Int).Direction = ParameterDirection.Output;

                foreach (IDataParameter item in cmd.Parameters)
                {
                    if (item.Value == null) item.Value = DBNull.Value;
                }
                cmd.ExecuteNonQuery();
                transtatus.Message = (string)cmd.Parameters["@message"].Value;
                transtatus.Code = (int)cmd.Parameters["@code"].Value;

                return transtatus;
            }
        }
        public async Task<Transtatus> UpdateEmployee(UpdateEmployeeModel model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                Transtatus transtatus = new Transtatus();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.UpdateEmployee";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@EmpId", model.EmpId);
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@WorkDept", model.WorkDept);
                cmd.Parameters.AddWithValue("@PhoneNo", model.PhoneNo);
                cmd.Parameters.AddWithValue("@Salary", model.Salary);
                cmd.Parameters.AddWithValue("@ModifiedBy", model.ModifiedBy);
                cmd.Parameters.Add("@message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@code", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                transtatus.Message = (string)cmd.Parameters["@message"].Value;
                transtatus.Code = (int)cmd.Parameters["@code"].Value;

                return transtatus;
            }
        }
        public async Task<Transtatus> DeleteEmployee(DeleteEmployeeModel model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                Transtatus transtatus = new Transtatus();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.DeleteEmployee";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@empId", model.EmpId);
                cmd.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@code", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                transtatus.Message = (string)cmd.Parameters["@message"].Value;
                transtatus.Code = (int)cmd.Parameters["@code"].Value;
                return transtatus;
            }
        }
        public async Task<Transtatus> BulkInsert(List<InsertBulkData> dataTable)
        {
            Transtatus transtatus = new Transtatus();
            using (var connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.BulkInsert";
                await connection.OpenAsync();
                var paramater = cmd.CreateParameter();

                DataTable dataTable1 = CommomHelper.ToDataTable(dataTable);
                cmd.Parameters.AddWithValue("@Data", dataTable1);
                paramater.SqlDbType = SqlDbType.Structured;
                paramater.TypeName = "dbo.bulkData";

                cmd.Parameters.Add("@message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@code", SqlDbType.Int).Direction = ParameterDirection.Output;
                foreach (IDataParameter item in cmd.Parameters)
                {
                    if (item.Value == null) item.Value = DBNull.Value;
                }
                cmd.ExecuteNonQuery();
                transtatus.Message = (string)cmd.Parameters["@Message"].Value;
                transtatus.Code = (int)cmd.Parameters["@Code"].Value;
                return transtatus;
            }
        }
        public async Task<Tuple<List<GetBooksModel>, Transtatus, int>> GetBookData(Pagination model)
        {
            List<GetBooksModel> getbooks = new List<GetBooksModel>();
            Transtatus transtatus = new Transtatus();

            using (var connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetBookData";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@SearchText", model.SearchText);
                cmd.Parameters.AddWithValue("@PageNo", model.PageNo);
                cmd.Parameters.AddWithValue("@PageSize", model.PageSize);
                cmd.Parameters.Add("@RowCount", sqlDbType: SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@code", SqlDbType.Int).Direction = ParameterDirection.Output;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    getbooks = (reader.DataReaderMapToList<GetBooksModel>()).ToList();
                }

                model.RowCount = (int)cmd.Parameters["@RowCount"].Value;
                transtatus.Message = (string)cmd.Parameters["@message"].Value;
                transtatus.Code = (int)cmd.Parameters["@code"].Value;

            }
            return new Tuple<List<GetBooksModel>, Transtatus, int>(getbooks, transtatus, model.RowCount);
        }
        public async Task<Transtatus> AddFavorite(favorite model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                Transtatus transtatus = new Transtatus();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.AddFavorite";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@title", model.Title);
                cmd.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@code", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                transtatus.Message = (string)cmd.Parameters["@message"].Value;
                transtatus.Code = (int)cmd.Parameters["@code"].Value;
                return transtatus;
            }
        }
        public async Task<Transtatus> AddBooks(AddbooksModel model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                Transtatus transtatus = new Transtatus();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.AddBooks";
                await connection.OpenAsync();

                model.Password = CommomHelper.Encrypt(model.Password);

                cmd.Parameters.AddWithValue("@ID", model.ID);
                cmd.Parameters.AddWithValue("@password", model.Password);
                cmd.Parameters.AddWithValue("@title", model.Title);
                cmd.Parameters.AddWithValue("@Author", model.Author);
                cmd.Parameters.AddWithValue("@Publisher", model.Publisher);
                cmd.Parameters.AddWithValue("@publishedYear", model.PublishedYear);
                cmd.Parameters.AddWithValue("@price", model.Price);
                cmd.Parameters.AddWithValue("@desciption", model.Description);
                cmd.Parameters.AddWithValue("@Language", model.Language);
                cmd.Parameters.AddWithValue("@stockQuantity", model.StockQuantity);
                cmd.Parameters.AddWithValue("@Pages", model.Pages);
                cmd.Parameters.AddWithValue("@CreatedBy ", model.CreatedBy);
                cmd.Parameters.Add("@message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@code", SqlDbType.Int).Direction = ParameterDirection.Output;

                foreach (IDataParameter item in cmd.Parameters)
                {
                    if (item.Value == null) item.Value = DBNull.Value;
                }
                cmd.ExecuteNonQuery();
                transtatus.Message = (string)cmd.Parameters["@message"].Value;
                transtatus.Code = (int)cmd.Parameters["@code"].Value;

                return transtatus;
            }
        }
        public async Task<Tuple<List<GetBooksModel>, int>> GetFavorite(GetFavoriteModel model)
        {
            List<GetBooksModel> getbooks = new List<GetBooksModel>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetFavorite";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@PageNo", model.PageNo);
                cmd.Parameters.AddWithValue("@PageSize", model.PageSize);
                cmd.Parameters.Add("@RowCount", sqlDbType: SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    getbooks = (reader.DataReaderMapToList<GetBooksModel>()).ToList();
                }

                model.RowCount = (int)cmd.Parameters["@RowCount"].Value;

            }
            return new Tuple<List<GetBooksModel>, int>(getbooks, model.RowCount);
        }
        public async Task<EmployeeData> GetAllEmployeeData()
        {
            List<JObject> jsonObject = new List<JObject>();
            List<JObject> PlayerData = new List<JObject>();
            List<JObject> Markets = new List<JObject>();
            Transtatus transtatus = new Transtatus();
            List<Root> mongodbdata = new List<Root>();

            var filter = Builders<BsonDocument>.Filter.Empty;
            var orFilters = new List<FilterDefinition<BsonDocument>>();



            List<GetLastHourFraudBetsModel> GetOneHourBets = new List<GetLastHourFraudBetsModel>();
            List<GetBfIdModel> GetTime = new List<GetBfIdModel>();

            using (SqlConnection con = new SqlConnection(Connection.ConnectionString1))
            {
                try
                {
                    //MondoDB Connection
                    var client = new MongoClient(Connection.MongoConnectionString);
                    IMongoDatabase database = client.GetDatabase("Central247_log");
                    IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("fraud_detections");

                    //Sql Connection
                    SqlCommand command = con.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.GetOneHourFraudBets";
                    await con.OpenAsync();

                    //Get Data From the SatSportDB
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        GetOneHourBets = (reader.DataReaderMapToList<GetLastHourFraudBetsModel>()).ToList();
                        reader.NextResult();
                        GetTime = reader.DataReaderMapToList<GetBfIdModel>().ToList();
                    }

                    await con.CloseAsync();

                    foreach (var query in GetTime)
                    {
                        var beforeTimeIso = query.BeforeTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
                        var afterTimeIso = query.AfterTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");

                        var marketFilter = Builders<BsonDocument>.Filter.Eq("data.bmi", query.BfMarketID) &
                                          Builders<BsonDocument>.Filter.Gte("time", BsonDateTime.Create(beforeTimeIso)) &
                                          Builders<BsonDocument>.Filter.Lte("time", BsonDateTime.Create(afterTimeIso));
                        orFilters.Add(marketFilter);
                    }
                    //Get Data From MongoDB
                    if (orFilters.Count > 0)
                    {
                        filter = Builders<BsonDocument>.Filter.Or(orFilters);
                        var results = await collection.Find(filter).ToListAsync();

                        foreach (var item in results)
                        {
                            var jsonString = item.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { OutputMode = MongoDB.Bson.IO.JsonOutputMode.Strict });
                            var parsedObject = JObject.Parse(jsonString);
                            jsonObject.Add(parsedObject);
                        }

                        foreach (var market in GetTime)
                        {
                            foreach (var rates in jsonObject)
                            {
                                var bmiProperty = rates["data"]["bmi"].Value<string>();

                                if (market.BfMarketID == bmiProperty)
                                {
                                    Root data = JsonConvert.DeserializeObject<Root>(rates.ToString());
                                    mongodbdata.Add(data);
                                }
                            }
                        }

                        foreach (var item in mongodbdata)
                        {
                            long dateTime = long.Parse(item.time.date);
                            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(dateTime);
                            DateTime dateTime1 = dateTimeOffset.UtcDateTime;

                            // Now, 'dateTime' contains the converted DateTime from the Unix timestamp



                            var marketData = mongodbdata
                                                .Where(root => GetOneHourBets.Any(bet =>
                                                    bet.BfMarketID == root.data.bmi &&
                                                    root.data.rt.Any(rt => rt.id == bet.BfRunnerId) &&
                                                    DateTime.Compare(dateTime1, bet.BetMatchedDate) == 0
                                                ))
                                                .ToList();


                        }





                        //var marketdata = mongodbdata
                        //.Where(root => GetOneHourBets
                        //.Any(bet => bet.BfMarketID == root.data.bmi))
                        //.ToList();

                        //var runnerData = mongodbdata
                        //                .Where(root => GetOneHourBets
                        //                .Any(bet => root.data.rt.Any(rt => rt.id == bet.BfRunnerId)))
                        //                .ToList();



                    }
                }
                catch (Exception ex)
                {
                }
            }
            return default;
        }
        public async Task<Transtatus> InsertImage(string filePath)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                Transtatus transtatus = new Transtatus();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.InsertImage";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@imageURL", filePath);
                cmd.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@code", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                transtatus.Message = (string)cmd.Parameters["@message"].Value;
                transtatus.Code = (int)cmd.Parameters["@code"].Value;
                return transtatus;
            }
        }
        public async Task<Transtatus> LoginUser(Authentication model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                Transtatus transtatus = new Transtatus();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Authentication";
                await connection.OpenAsync();
                model.Password = CommomHelper.Encrypt(model.Password);

                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@code", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                transtatus.Message = (string)cmd.Parameters["@message"].Value;
                transtatus.Code = (int)cmd.Parameters["@code"].Value;

                return transtatus;
            }
        }
        public async Task<Transtatus> GetData()
        {
            Transtatus transtatus = new Transtatus();
            List<GetMarketRateModel> marketRate = new List<GetMarketRateModel>();
            List<GetBfIdModel> getbfmodel = new List<GetBfIdModel>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var client = new MongoClient(Connection.MongoConnectionString);
                IMongoDatabase database = client.GetDatabase("Central247_log");
                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("fraud_detections");

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetTime";
                await connection.OpenAsync();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    getbfmodel = (reader.DataReaderMapToList<GetBfIdModel>()).ToList();
                }

                var filter = Builders<BsonDocument>.Filter.Empty;
                var orFilters = new List<FilterDefinition<BsonDocument>>();

                foreach (var query in getbfmodel)
                {
                    var beforeTimeIso = query.BeforeTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    var afterTimeIso = query.AfterTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

                    var marketFilter = Builders<BsonDocument>.Filter.Eq("data.bmi", query.BfMarketID) &
                                      Builders<BsonDocument>.Filter.Gte("time", BsonDateTime.Create(beforeTimeIso)) &
                                      Builders<BsonDocument>.Filter.Lte("time", BsonDateTime.Create(afterTimeIso));
                    orFilters.Add(marketFilter);
                }

                filter = Builders<BsonDocument>.Filter.Or(orFilters);
                var results = await collection.Find(filter).ToListAsync();
                foreach (var item in results)
                {
                    var jsonString = item.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { OutputMode = MongoDB.Bson.IO.JsonOutputMode.Strict }); //OK
                    GetMarketRateModel Rates = new GetMarketRateModel
                    {
                        marketRates = jsonString
                    };
                    marketRate.Add(Rates);
                }
                try
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.AddmarketRates";

                    var paramater = command.CreateParameter();

                    DataTable dataTable1 = CommomHelper.ToDataTable(marketRate);
                    command.Parameters.AddWithValue("@Rates", dataTable1);
                    paramater.SqlDbType = SqlDbType.Structured;
                    paramater.TypeName = "dbo.AddMarketRates";

                    command.Parameters.Add("@message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@code", SqlDbType.Int).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();
                    transtatus.Message = (string)command.Parameters["@Message"].Value;
                    transtatus.Code = (int)command.Parameters["@Code"].Value;
                }
                catch (Exception ex)
                {
                    throw;
                }
                return transtatus;
            }
        }
        public async Task<Transtatus> GetMarketRates(GetMarketRatesReqModel model)
        {

            List<GetMarketRatesResModel> GetMarketRates = new List<GetMarketRatesResModel>();
            Transtatus transtatus = new Transtatus();

            using (var connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetMarketRates";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@BfmarketId", model.BfMarketId);
                cmd.Parameters.Add("@RowCount", sqlDbType: SqlDbType.Int).Direction = ParameterDirection.Output;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    GetMarketRates = (reader.DataReaderMapToList<GetMarketRatesResModel>()).ToList();
                }

                foreach (var item in GetMarketRates)
                {

                }

                model.RowCount = (int)cmd.Parameters["@RowCount"].Value;
                return transtatus;
            }
        }
        public async Task<Transtatus> AddMusic(string Base64)
        {

            Transtatus transtatus = new Transtatus();
            using (var connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.AddMusic";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@Base64", Base64);
                cmd.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@code", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                transtatus.Message = (string)cmd.Parameters["@message"].Value;
                transtatus.Code = (int)cmd.Parameters["@code"].Value;
                return transtatus;
            }
        }
    }
}