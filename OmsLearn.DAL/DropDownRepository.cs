using EmployeeDirectory.DAL;
using EmployeeDirectory.Model;
using System.Data.SqlClient;
using System.Data;
using OmsLearn.Model;

namespace OmsLearn.DAL
{
    public class DropDownRepository : BaseRepository
    {
        public async Task<Tuple<List<DropdownCommonResModel>, int>> GetSportDropdown_List(GetSportDropdown_ListReqModel model)
        {
            int TotalRocords;
            List<DropdownCommonResModel> dropdownlist = new List<DropdownCommonResModel>();
            using (var connection = new SqlConnection(ConnectionString1))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetSport_DropdownListFraud";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@SearchText", model.SearchText);
                cmd.Parameters.Add("@TotalRecords", sqlDbType: SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dropdownlist = (reader.DataReaderMapToList<DropdownCommonResModel>()).ToList();
                }
                TotalRocords = (int)cmd.Parameters["@TotalRecords"].Value;
            }
            return new Tuple<List<DropdownCommonResModel>, int>(dropdownlist, TotalRocords);
        }
        public async Task<Tuple<List<DropdownCommonResModel>, int>> GetTournament_DropdownList(GetTournament_DropdownListReqModel model)
        {
            int TotalRocords;
            List<DropdownCommonResModel> dropdownlist = new List<DropdownCommonResModel>();
            using (var connection = new SqlConnection(ConnectionString1))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetTournament_DropdownListFraud";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@SportID", model.SportID);
                cmd.Parameters.AddWithValue("@EventDate", model.EventDate);
                cmd.Parameters.AddWithValue("@SearchText", model.SearchText);
                cmd.Parameters.Add("@TotalRecords", sqlDbType: SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dropdownlist = (reader.DataReaderMapToList<DropdownCommonResModel>()).ToList();
                }
                TotalRocords = (int)cmd.Parameters["@TotalRecords"].Value;
            }
            return new Tuple<List<DropdownCommonResModel>, int>(dropdownlist, TotalRocords);
        }
        public async Task<Tuple<List<DropdownCommonResModel>, int>> GetMatch_DropdownList(GetMatch_DropdownListReqModel model)
        {
            int TotalRocords;
            List<DropdownCommonResModel> dropdownlist = new List<DropdownCommonResModel>();
            using (var connection = new SqlConnection(ConnectionString1))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetMatch_DropdownListFruad";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@TournamentID", model.TournamentID);
                cmd.Parameters.AddWithValue("@EventDate", model.EventDate);
                cmd.Parameters.AddWithValue("@SearchText", model.SearchText);
                cmd.Parameters.Add("@TotalRecords", sqlDbType: SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dropdownlist = (reader.DataReaderMapToList<DropdownCommonResModel>()).ToList();
                }
                TotalRocords = (int)cmd.Parameters["@TotalRecords"].Value;
            }
            return new Tuple<List<DropdownCommonResModel>, int>(dropdownlist, TotalRocords);
        }
        public async Task<Tuple<List<DropdownCommonResModel>, int>> GetMarket_DropdownList(GetMarket_DropdownListReqModel model)
        {
            int TotalRocords;
            List<DropdownCommonResModel> dropdownlist = new List<DropdownCommonResModel>();
            using (var connection = new SqlConnection(ConnectionString1))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetMarket_DropdownListFraud";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@MatchID", model.MatchID);
                cmd.Parameters.AddWithValue("@EventDate", model.EventDate);
                cmd.Parameters.AddWithValue("@SearchText", model.SearchText);
                cmd.Parameters.Add("@TotalRecords", sqlDbType: SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dropdownlist = (reader.DataReaderMapToList<DropdownCommonResModel>()).ToList();
                }
                TotalRocords = (int)cmd.Parameters["@TotalRecords"].Value;
            }
            return new Tuple<List<DropdownCommonResModel>, int>(dropdownlist, TotalRocords);
        }
        public async Task<Tuple<List<GetPlayerListResModel>, int>> GetPlayerList(GetPlayerListReqModel model)
        {
            int TotalRocords;
            List<GetPlayerListResModel> getplayerList = new List<GetPlayerListResModel>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetPlayers";
                await connection.OpenAsync();

                cmd.Parameters.AddWithValue("@SportID", model.SportID);
                cmd.Parameters.AddWithValue("@TournamentID", model.TournamentID);
                cmd.Parameters.AddWithValue("@MatchID", model.MatchID);
                cmd.Parameters.AddWithValue("@MarketID", model.MarketID);   
                cmd.Parameters.AddWithValue("@status", model.status);   
                cmd.Parameters.AddWithValue("@MatchedWithIn", model.MatchedWithIn);   
                cmd.Parameters.AddWithValue("@PageNo", model.PageNo);   
                cmd.Parameters.AddWithValue("@PageSize", model.PageSize);   
                cmd.Parameters.Add("@TotalCount", sqlDbType: SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    getplayerList = (reader.DataReaderMapToList<GetPlayerListResModel>()).ToList();
                }
                TotalRocords = (int)cmd.Parameters["@TotalCount"].Value;
            }
            return new Tuple<List<GetPlayerListResModel>, int>(getplayerList, TotalRocords);
        }
    }
}
