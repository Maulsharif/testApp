using Newtonsoft.Json.Linq;

namespace wallet.Services
{
    public class RateDeserilizer
    { 
        
        public RateDeserilizer(string json, string code1, string code2)
        {
            JObject jObject = JObject.Parse(json);
            JToken jCur = jObject["rates"];
            JToken jRate = jCur[$"{code1+code2}"];
            Rate = (decimal) jRate["rate"];
           
        }
        public decimal Rate { get; set; }
       
    }
}