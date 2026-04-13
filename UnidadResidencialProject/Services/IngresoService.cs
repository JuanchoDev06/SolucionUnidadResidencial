using System.Net.Http.Headers;
using System.Net.Http.Json;
using UnidadResidencialProject.Models;

namespace UnidadResidencialProject.Services
{
    public class IngresoService
    {
        private readonly HttpClient _http;
        private readonly AuthService _auth;

        public IngresoService(HttpClient http, AuthService auth)
        {
            _http = http;
            _auth = auth;
        }

        /// <summary>
        /// Configura el header Authorization con el token JWT almacenado.
        /// </summary>
        private async Task SetAuthHeaderAsync()
        {
            var token = await _auth.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        /// <summary>
        /// GET /api/Ingresos — Obtiene todos los registros de ingreso.
        /// </summary>
        public async Task<List<IngresoDto>> GetIngresosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<IngresoDto>>("api/Ingresos");
            return result ?? new List<IngresoDto>();
        }

        /// <summary>
        /// POST /api/Ingresos — Crea un nuevo registro de ingreso.
        /// Retorna (true, null) si fue exitoso, o (false, mensajeError) si falló.
        /// </summary>
        public async Task<(bool ok, string? error)> CrearIngresoAsync(IngresoCreateDto ingreso)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/Ingresos", ingreso);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var body = await response.Content.ReadAsStringAsync();
            return (false, $"HTTP {(int)response.StatusCode}: {body}");
        }

        /// <summary>
        /// PUT /api/Ingresos/{id} — Actualiza un registro (ej: registrar salida).
        /// Retorna (true, null) si fue exitoso, o (false, mensajeError) si falló.
        /// </summary>
        public async Task<(bool ok, string? error)> ActualizarIngresoAsync(int id, IngresoDto ingreso)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PutAsJsonAsync($"api/Ingresos/{id}", ingreso);

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
    }
}
