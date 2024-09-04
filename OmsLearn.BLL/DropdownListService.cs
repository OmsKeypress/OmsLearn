using EmployeeDirectory.BLL.Interface;
using EmployeeDirectory.DAL;
using EmployeeDirectory.Model;
using OmsLearn.DAL;
using OmsLearn.Model;

namespace EmployeeDirectory.BLL
{
    public class DropdownListService : IDropDown
    {
        DropDownRepository? dropdownrepository;
        public async Task<Tuple<List<DropdownCommonResModel>, int>> GetSportDropdown_List(GetSportDropdown_ListReqModel model)  
        {
            using (dropdownrepository = new DropDownRepository())
            {
                return await dropdownrepository.GetSportDropdown_List(model);
            }
        }
        public async Task<Tuple<List<DropdownCommonResModel>, int>> GetTournament_DropdownList(GetTournament_DropdownListReqModel model)
        {
            using (dropdownrepository = new DropDownRepository())
            {
                return await dropdownrepository.GetTournament_DropdownList(model);
            }
        }
        public async Task<Tuple<List<DropdownCommonResModel>, int>> GetMatch_DropdownList(GetMatch_DropdownListReqModel model)
        {
            using (dropdownrepository = new DropDownRepository())
            {
                return await dropdownrepository.GetMatch_DropdownList(model);
            }
        }
        public async Task<Tuple<List<DropdownCommonResModel>, int>> GetMarket_DropdownList(GetMarket_DropdownListReqModel model)
        {
            using (dropdownrepository = new DropDownRepository())
            {
                return await dropdownrepository.GetMarket_DropdownList(model);
            }
        }
        public async Task<Tuple<List<GetPlayerListResModel>, int>> GetPlayerList(GetPlayerListReqModel model)
        {
            using (dropdownrepository = new DropDownRepository())
            {
                return await dropdownrepository.GetPlayerList(model);
            }
        }
    }
}