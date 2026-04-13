using System.Net.Http.Headers;
using System.Net.Http.Json;
using UnidadResidencialProject.Models;

namespace UnidadResidencialProject.Services
{
    public class MensajeriaService
    {
        private readonly HttpClient _http;
        private readonly AuthService _auth;

        public MensajeriaService(HttpClient http, AuthService auth)
        {
            _http = http;
            _auth = auth;
        }

        private async Task SetAuthHeaderAsync()
        {
            var token = await _auth.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<MensajeriaDto>> GetMensajeriasAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<MensajeriaDto>>("api/Mensajerias");
            return result ?? new List<MensajeriaDto>();
        }

        public async Task<(bool ok, string? error)> CrearMensajeriaAsync(MensajeriaCreateDto dto)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/Mensajerias", dto);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        public async Task<(bool ok, string? error)> ActualizarMensajeriaAsync(int id, MensajeriaDto dto)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PutAsJsonAsync($"api/Mensajerias/{id}", dto);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        public async Task<List<ApartamentoDto>> GetApartamentosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ApartamentoDto>>("api/Apartamentos");
            return result ?? new List<ApartamentoDto>();
        }
    }
}
