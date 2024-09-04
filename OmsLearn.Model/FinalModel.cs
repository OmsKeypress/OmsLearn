using Newtonsoft.Json;

namespace OmsLearn.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public string bmi { get; set; }
        public int ip { get; set; }
        public int mi { get; set; }
        public int ms { get; set; }
        public string eti { get; set; }
        public string eid { get; set; }
        public Grt grt { get; set; }
        public object tv { get; set; }
        public List<Rt> rt { get; set; }
    }

    public class Grt
    {
        [JsonProperty("$date")]
        public string date { get; set; }
    }

    public class Id
    {
        [JsonProperty("$oid")]
        public string oid { get; set; }
    }

    public class Root
    {
        public Id _id { get; set; }
        public string sourceId { get; set; }
        public Data data { get; set; }
        public Time time { get; set; }
    }

    public class Rt
    {
        public double tv { get; set; }
        public int id { get; set; }
        public object adjustmentFactor { get; set; }
        public string status { get; set; }
        public int sortPriority { get; set; }
        public List<List<double>> bdatb { get; set; }
        public List<List<double>> bdatl { get; set; }
    }

    public class Time
    {
        [JsonProperty("$date")]
        public string date { get; set; }
    }


}
