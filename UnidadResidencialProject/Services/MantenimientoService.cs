using System.Net.Http.Headers;
using System.Net.Http.Json;
using UnidadResidencialProject.Models;

namespace UnidadResidencialProject.Services
{
    public class MantenimientoService
    {
        private readonly HttpClient _http;
        private readonly AuthService _auth;

        public MantenimientoService(HttpClient http, AuthService auth)
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

        public async Task<List<MantenimientoDto>> GetMantenimientosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<MantenimientoDto>>("api/Mantenimientos");
            return result ?? new List<MantenimientoDto>();
        }

        public async Task<(bool ok, string? error)> CrearMantenimientoAsync(MantenimientoCreateDto dto)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/Mantenimientos", dto);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        public async Task<(bool ok, string? error)> ActualizarMantenimientoAsync(int id, MantenimientoDto dto)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PutAsJsonAsync($"api/Mantenimientos/{id}", dto);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        public async Task<List<TipoMantenimientoDto>> GetTiposMantenimientoAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<TipoMantenimientoDto>>("api/TipoMantenimiento");
            return result ?? new List<TipoMantenimientoDto>();
        }

        public async Task<List<ZonaComunDto>> GetZonasComunesAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ZonaComunDto>>("api/ZonaComun");
            return result ?? new List<ZonaComunDto>();
        }
    }
}
