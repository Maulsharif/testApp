using Newtonsoft.Json.Linq;

namespace wallet.Services
{
    public class Deserializer
    { 
        
        public Deserializer(string json, string code1, string code2)
        {
            JObject jObject = JObject.Parse(json);
            JToken jCur = jObject["rates"];
            JToken jRate = jCur[$"{code1.ToUpper()+code2.ToUpper()}"];
            Rate = (decimal) jRate["rate"];
           
        }
        public decimal Rate { get; set; }
       
    }
}