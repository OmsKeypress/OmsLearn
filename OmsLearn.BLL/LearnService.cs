using EmployeeDirectory.BLL.Interface;
using EmployeeDirectory.DAL;
using EmployeeDirectory.Model;

namespace EmployeeDirectory.BLL
{
    public class LearnService : ILearn
    {
        LearnRepository? learnRepository;
        public async Task<Transtatus> InsertEmployee(EmployeeInsertModel model)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.InsertEmployee(model);
            }
        }
        public async Task<Transtatus> GetMarketRates(GetMarketRatesReqModel model)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.GetMarketRates(model);
            }
        }
        public async Task<Transtatus> UpdateEmployee(UpdateEmployeeModel model)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.UpdateEmployee(model);
            }
        }
        public async Task<Transtatus> DeleteEmployee(DeleteEmployeeModel model)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.DeleteEmployee(model);
            }
        }
        public async Task<Transtatus> BulkInsert(List<InsertBulkData> dataTable)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.BulkInsert(dataTable);
            }
        }
        public async Task<Tuple<List<GetBooksModel>, Transtatus, int>> GetBookData(Pagination model)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.GetBookData(model);
            }
        }
        public async Task<Transtatus> AddFavorite(favorite model)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.AddFavorite(model);
            }
        }
        public async Task<Transtatus> AddBooks(AddbooksModel model)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.AddBooks(model);
            }
        }
        public async Task<Tuple<List<GetBooksModel>, int>> GetFavorite(GetFavoriteModel model)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.GetFavorite(model);
            }
        }
        public async Task<EmployeeData> GetAllEmployeeData()
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.GetAllEmployeeData();
            }
        }
        public async Task<Transtatus> InsertImage(string filePath)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.InsertImage(filePath);
            }
        }
        public async Task<Transtatus> LoginUser(Authentication model)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.LoginUser(model);
            }
        }
        public async Task<Transtatus> GetData()
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.GetData();
            }
        }
        public async Task<Transtatus> AddMusic(string Base64)
        {
            using (learnRepository = new LearnRepository())
            {
                return await learnRepository.AddMusic(Base64);
            }
        }
    }
}