using System.Net.Http.Headers;
using System.Net.Http.Json;
using UnidadResidencialProject.Models;

namespace UnidadResidencialProject.Services
{
    public class AdminService
    {
        private readonly HttpClient _http;
        private readonly AuthService _auth;

        public AdminService(HttpClient http, AuthService auth)
        {
            _http = http;
            _auth = auth;
        }

        private async Task SetAuthHeaderAsync()
        {
            var token = await _auth.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
        }

        // ── Roles ──────────────────────────────────────────
        public async Task<List<RolDto>> GetRolesAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<RolDto>>("api/Roles");
            return result ?? new();
        }

        public async Task<(bool ok, string? error)> CrearRolAsync(string nombre)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/Roles", new { Nombre = nombre });
            if (response.IsSuccessStatusCode) return (true, null);
            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        public async Task<(bool ok, string? error)> EliminarRolAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _http.DeleteAsync($"api/Roles/{id}");
            if (response.IsSuccessStatusCode) return (true, null);
            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        // ── Zonas Comunes ──────────────────────────────────
        public async Task<List<ZonaComunDto>> GetZonasComunesAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ZonaComunDto>>("api/ZonasComunes");
            return result ?? new();
        }

        public async Task<(bool ok, string? error)> ActualizarZonaComunAsync(int id, ZonaComunDto dto)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PutAsJsonAsync($"api/ZonasComunes/{id}", dto);
            if (response.IsSuccessStatusCode) return (true, null);
            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        public async Task<(bool ok, string? error)> CrearZonaComunAsync(ZonaComunDto dto)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/ZonasComunes", dto);
            if (response.IsSuccessStatusCode) return (true, null);
            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        // ── Torres ─────────────────────────────────────────
        public async Task<List<TorreDto>> GetTorresAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<TorreDto>>("api/Torres");
            return result ?? new();
        }

        public async Task<(bool ok, string? error)> CrearTorreAsync(string nombre, int conjuntoId)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/Torres",
                new { Nombre = nombre, ConjuntoId = conjuntoId });
            if (response.IsSuccessStatusCode) return (true, null);
            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        // ── Apartamentos ───────────────────────────────────
        public async Task<List<ApartamentoDto>> GetApartamentosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ApartamentoDto>>("api/Apartamentos");
            return result ?? new();
        }

        public async Task<(bool ok, string? error)> CrearApartamentoAsync(ApartamentoDto dto)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/Apartamentos", dto);
            if (response.IsSuccessStatusCode) return (true, null);
            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        // ── Tipos de Mantenimiento ─────────────────────────
        public async Task<List<TipoMantenimientoDto>> GetTiposMantenimientoAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<TipoMantenimientoDto>>("api/TiposMantenimiento");
            return result ?? new();
        }

        public async Task<(bool ok, string? error)> CrearTipoMantenimientoAsync(string nombre)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/TiposMantenimiento",
                new { Nombre = nombre });
            if (response.IsSuccessStatusCode) return (true, null);
            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        public async Task<(bool ok, string? error)> EliminarTipoMantenimientoAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _http.DeleteAsync($"api/TiposMantenimiento/{id}");
            if (response.IsSuccessStatusCode) return (true, null);
            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }
    }
}