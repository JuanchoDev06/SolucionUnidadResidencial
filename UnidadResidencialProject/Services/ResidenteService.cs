using System.Net.Http.Headers;
using System.Net.Http.Json;
using UnidadResidencialProject.Models;

namespace UnidadResidencialProject.Services
{
    public class ResidenteService
    {
        private readonly HttpClient _http;
        private readonly AuthService _auth;

        public ResidenteService(HttpClient http, AuthService auth)
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

        // ── GET /api/Usuarios ──
        public async Task<List<UsuarioDto>> GetUsuariosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<UsuarioDto>>("api/Usuarios");
            return result ?? new List<UsuarioDto>();
        }

        // ── POST /api/Usuarios ──
        public async Task<(bool ok, string? error)> CrearUsuarioAsync(UsuarioCreateDto usuario)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/Usuarios", usuario);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        // ── PUT /api/Usuarios/{id} ──
        public async Task<(bool ok, string? error)> ActualizarUsuarioAsync(int id, UsuarioDto usuario)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PutAsJsonAsync($"api/Usuarios/{id}", usuario);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        // ── DELETE /api/Usuarios/{id} ──
        public async Task<(bool ok, string? error)> EliminarUsuarioAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _http.DeleteAsync($"api/Usuarios/{id}");

            if (response.IsSuccessStatusCode)
                return (true, null);

            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        // ── GET /api/Apartamentos ──

        public async Task<List<ApartamentoDto>> GetApartamentosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ApartamentoDto>>("api/Apartamentos");
            return result ?? new List<ApartamentoDto>();
        }

        // ── GET /api/Torres ──
        public async Task<List<TorreDto>> GetTorresAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<TorreDto>>("api/Torres");
            return result ?? new List<TorreDto>();
        }

        // ── POST /api/ResidentesUnidad ──
        public async Task<(bool ok, string? error)> AsignarApartamentoAsync(int usuarioId, int unidadId, bool esPropietario)
        {
            await SetAuthHeaderAsync();
            var body = new
            {
                UsuarioId = usuarioId,
                UnidadId = unidadId,
                EsPropietario = esPropietario
            };
            var response = await _http.PostAsJsonAsync("api/ResidentesUnidad", body);
            if (response.IsSuccessStatusCode) return (true, null);
            var error = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {error}");
        }
    }
}
