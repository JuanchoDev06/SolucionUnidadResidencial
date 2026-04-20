using System.Net.Http.Headers;
using System.Net.Http.Json;
using UnidadResidencialProject.Models;

namespace UnidadResidencialProject.Services
{
    public class DashboardService
    {
        private readonly HttpClient _http;
        private readonly AuthService _auth;

        public DashboardService(HttpClient http, AuthService auth)
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

        public async Task<List<UsuarioDto>> GetUsuariosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<UsuarioDto>>("api/Usuarios");
            return result ?? new();
        }

        public async Task<List<IngresoDto>> GetIngresosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<IngresoDto>>("api/Ingresos");
            return result ?? new();
        }

        public async Task<List<ParqueaderoDto>> GetParqueaderosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ParqueaderoDto>>("api/Parqueaderos");
            return result ?? new();
        }

        public async Task<List<ParqueaderoVisitanteDto>> GetVisitantesAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ParqueaderoVisitanteDto>>("api/ParqueaderosVisitantes");
            return result ?? new();
        }

        public async Task<List<MensajeriaDto>> GetMensajeriaAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<MensajeriaDto>>("api/Mensajerias");
            return result ?? new();
        }

        public async Task<List<ReservaDto>> GetReservasAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<ReservaDto>>("api/Reservas");
            return result ?? new();
        }

        public async Task<List<MantenimientoDto>> GetMantenimientosAsync()
        {
            await SetAuthHeaderAsync();
            var result = await _http.GetFromJsonAsync<List<MantenimientoDto>>("api/Mantenimientos");
            return result ?? new();
        }
    }
}