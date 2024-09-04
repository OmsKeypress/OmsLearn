using EmployeeDirectory.Model;
using OmsLearn.Model;

namespace EmployeeDirectory.BLL.Interface
{
    public interface IDropDown
    {
        Task<Tuple<List<DropdownCommonResModel>, int>> GetSportDropdown_List(GetSportDropdown_ListReqModel model);
        Task<Tuple<List<DropdownCommonResModel>, int>> GetTournament_DropdownList(GetTournament_DropdownListReqModel model);
        Task<Tuple<List<DropdownCommonResModel>, int>> GetMatch_DropdownList(GetMatch_DropdownListReqModel model);
        Task<Tuple<List<DropdownCommonResModel>, int>> GetMarket_DropdownList(GetMarket_DropdownListReqModel model);
        Task<Tuple<List<GetPlayerListResModel>, int>> GetPlayerList(GetPlayerListReqModel model);
    }
}
