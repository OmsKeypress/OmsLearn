using System.Collections.Generic;

namespace OmsLearn.Model
{
    public class MarketDataD
    {
        public string MarketId { get; set; }
        public string MatchId { get; set; }
        public DateTime RecordTime { get; set; }
        public int RunnerId { get; set; }
        public string RunnerStatus { get; set; }
    }

    public class RatesD
    {
        public int Id { get; set; }
        public int BetType { get; set; }
        public int Priority { get; set; }
        public double Rate { get; set; }
        public double Volume { get; set; }
    }



    public class GetLastHourFraudBetsModel
    {
        public int BetId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerUniqueName { get; set; }
        public decimal Stake { get; set; }
        public bool IsBack { get; set; }
        public decimal Rate { get; set; }
        public decimal BetPlacedRate { get; set; }
        public string BfMarketID { get; set; }
        public int MarketID { get; set; }
        public int MatchId { get; set; }
        public int BfmatchId { get; set; }
        public int RunnerId { get; set; }
        public int BfRunnerId { get; set; }
        public string Runner { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsBetWon { get; set; }
        public int FraudStatus { get; set; }
        public int IsHandicap { get; set; }
        public int SportId { get; set; }
        public int TournamentID { get; set; }
        public DateTime BetPlacedDate { get; set; }
        public DateTime BetMatchedDate { get; set; }
        public string SportName { get; set; }
        public string TournamentName { get; set; }
        public string MatchName { get; set; }
        public string MarketName { get; set; }
        public DateTime? OpenDate { get; set; }
        public string MarketType { get; set; }
    }
}
