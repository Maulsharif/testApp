using System;
using System.Threading.Tasks;
using RestSharp;
namespace wallet.Services
{
    public static class CurrentRate
    {
        public static async Task<IRestResponse> GetRateAsync(string code1, string code2)
        {
            if (string.IsNullOrEmpty(code1))
            {
                throw new ArgumentNullException(nameof(code1));
            }
            if (string.IsNullOrEmpty(code2))
            {
                throw new ArgumentNullException(nameof(code2));
            }

            string apiAddress = $"https://www.freeforexapi.com/api/live?pairs={code1 + code2}";
            var client = new RestClient(apiAddress);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            var data = response;

            return data;
        }

    }
    
}