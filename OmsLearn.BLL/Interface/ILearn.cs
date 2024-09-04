using EmployeeDirectory.Model;

namespace EmployeeDirectory.BLL.Interface
{
    public interface ILearn
    {
        Task<Transtatus> InsertEmployee(EmployeeInsertModel model);
        Task<Transtatus> GetMarketRates(GetMarketRatesReqModel model);
        Task<Transtatus> UpdateEmployee(UpdateEmployeeModel model);
        Task<Transtatus> DeleteEmployee(DeleteEmployeeModel model);
        Task<Transtatus> BulkInsert(List<InsertBulkData> dataTable);
        Task<Tuple<List<GetBooksModel>, Transtatus, int>> GetBookData(Pagination model);
        Task<Transtatus> AddFavorite(favorite model);
        Task<Transtatus> AddBooks(AddbooksModel model);
        Task<Tuple<List<GetBooksModel>, int>> GetFavorite(GetFavoriteModel model);
        Task<Transtatus> InsertImage(string filePath);
        Task<Transtatus> LoginUser(Authentication model);
        Task<Transtatus> GetData();
        Task<EmployeeData> GetAllEmployeeData();
        Task<Transtatus> AddMusic(string Base64);
    }
}
