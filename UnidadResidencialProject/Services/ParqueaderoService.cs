using System.Net.Http.Headers;
using System.Net.Http.Json;
using UnidadResidencialProject.Models;

namespace UnidadResidencialProject.Services
{
    public class ParqueaderoService
    {
        private readonly HttpClient _http;
        private readonly AuthService _auth;

        public ParqueaderoService(HttpClient http, AuthService auth)
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
        public async Task<List<ApartamentoDto>> GetApartamentosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ApartamentoDto>>("api/Apartamentos");
            return result ?? new List<ApartamentoDto>();
        }

        // ── GET /api/Parqueaderos ──
        public async Task<List<ParqueaderoDto>> GetParqueaderosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ParqueaderoDto>>("api/Parqueaderos");
            return result ?? new List<ParqueaderoDto>();
        }

        // ── PUT /api/Parqueaderos/{id} ──
        public async Task<(bool ok, string? error)> ActualizarParqueaderoAsync(int id, ParqueaderoDto parqueadero)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PutAsJsonAsync($"api/Parqueaderos/{id}", parqueadero);
            if (response.IsSuccessStatusCode) return (true, null);
            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        // ── GET /api/ParqueaderosVisitantes ──
        public async Task<List<ParqueaderoVisitanteDto>> GetVisitantesAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ParqueaderoVisitanteDto>>("api/ParqueaderosVisitantes");
            return result ?? new List<ParqueaderoVisitanteDto>();
        }

        // ── POST /api/ParqueaderosVisitantes ──
        public async Task<(bool ok, string? error)> RegistrarVisitanteAsync(ParqueaderoVisitanteCreateDto visitante)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/ParqueaderosVisitantes", visitante);
            if (response.IsSuccessStatusCode) return (true, null);
            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        // ── PUT /api/ParqueaderosVisitantes/{id} ──
        public async Task<(bool ok, string? error)> RegistrarSalidaVisitanteAsync(int id, ParqueaderoVisitanteDto visitante)
        {
            await SetAuthHeaderAsync();
            visitante.FechaHoraSalida = DateTime.Now;
            var response = await _http.PutAsJsonAsync($"api/ParqueaderosVisitantes/{id}", visitante);
            if (response.IsSuccessStatusCode) return (true, null);
            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }
    }
}