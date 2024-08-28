using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EsettMvcIntegration.Models;

namespace EsettMvcIntegration.Services
{
    public class FeeDataService
    {
        private readonly HttpClient _client;

        public FeeDataService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<FeeDataModel>> GetFeesAsync()
        {
            // Implement your logic to call the API and process the data
            return new List<FeeDataModel>();
        }
    }
}
