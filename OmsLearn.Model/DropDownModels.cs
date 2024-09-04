namespace OmsLearn.Model
{
    public class GetSportDropdown_ListReqModel
    {
        public string? SearchText { get; set; }
    }
    public class GetTournament_DropdownListReqModel
    {
        public int SportID { get; set; }
        public DateTime? EventDate { get; set; }
        public string? SearchText { get; set; }
    }
    public class GetMatch_DropdownListReqModel
    {
        public int TournamentID { get; set; }
        public DateTime? EventDate { get; set; }
        public string? SearchText { get; set; }
    }
    public class GetMarket_DropdownListReqModel
    {
        public int MatchID { get; set; }
        public DateTime? EventDate { get; set; }
        public string? SearchText { get; set; }
    }

    public class DropdownCommonResModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetPlayerListResModel
    {

        public int FruadId { get; set; }
        public int PlayerId { get; set; }
        public string PUN { get; set; }
        public DateTime PlacedDate { get; set; }
        public string Runner { get; set; }
        public bool Ishandicap { get; set; }
        public bool IsBack { get; set; }
        public decimal Rate{ get; set; }
        public int Stake { get; set; }
        public decimal? Bf_Odds{ get; set; }
        public bool IsbetWon{ get; set; }
        public int FraudStatus{ get; set; }
    }
    public class GetPlayerListReqModel
    {
        public int SportID { get; set; }
        public int TournamentID { get; set; }
        public int MatchID { get; set; }
        public int MarketID { get; set; }
        public int status { get; set; }
        public int MatchedWithIn { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
