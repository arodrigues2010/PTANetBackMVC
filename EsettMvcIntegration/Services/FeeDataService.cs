using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
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

        public async Task<List<FeeAllDataModel>> GetFeesAsync()
        {
            try
            {
                var response = await _client.GetAsync("https://api.opendata.esett.com/EXP05/Fees");
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                // Verificar si el contenido no está vacío antes de deserializar
                if (!string.IsNullOrEmpty(jsonString))
                {
                    var feeDataList = JsonSerializer.Deserialize<List<FeeAllDataModel>>(jsonString);

                    // Verificar si la deserialización fue exitosa
                    if (feeDataList != null)
                    {
                        return feeDataList;
                    }
                }

                // Si el JSON está vacío o la deserialización falla, devolver una lista vacía o manejarlo según sea necesario
                return new List<FeeAllDataModel>();
            }
            catch (HttpRequestException ex)
            {
                // Manejo de errores de la solicitud HTTP
                Console.WriteLine($"Request error: {ex.Message}");
                // Podrías lanzar la excepción nuevamente, registrar el error, o manejarlo según las necesidades de tu aplicación.
                throw;
            }
            catch (JsonException ex)
            {
                // Manejo de errores en la deserialización del JSON
                Console.WriteLine($"JSON error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de error
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }

}