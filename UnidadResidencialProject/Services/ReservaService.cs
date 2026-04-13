using System.Net.Http.Headers;
using System.Net.Http.Json;
using UnidadResidencialProject.Models;

namespace UnidadResidencialProject.Services
{
    public class ReservaService
    {
        private readonly HttpClient _http;
        private readonly AuthService _auth;

        public ReservaService(HttpClient http, AuthService auth)
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

        public async Task<List<ReservaDto>> GetReservasAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ReservaDto>>("api/Reservas");
            return result ?? new List<ReservaDto>();
        }

        public async Task<(bool ok, string? error)> CrearReservaAsync(ReservaCreateDto dto)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/Reservas", dto);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        public async Task<(bool ok, string? error)> ActualizarReservaAsync(int id, ReservaDto dto)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PutAsJsonAsync($"api/Reservas/{id}", dto);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        public async Task<List<ZonaComunDto>> GetZonasComunesAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ZonaComunDto>>("api/ZonaComun");
            return result ?? new List<ZonaComunDto>();
        }
    }
}
