using System.Net.Http.Headers;

namespace UnidadResidencialProject.Services
{
    public class ReporteService
    {
        private readonly HttpClient _http;
        private readonly AuthService _auth;

        public ReporteService(HttpClient http, AuthService auth)
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

        // ── PDF ───────────────────────────────────────────────
        public async Task<byte[]> GetReporteIngresosPdfAsync(
            DateTime fechaInicio, DateTime fechaFin, string? tipo)
        {
            await SetAuthHeaderAsync();
            var url = $"api/Reportes/ingresos-pdf?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}";
            if (!string.IsNullOrEmpty(tipo)) url += $"&tipo={tipo}";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GetReporteResidentesPdfAsync(
            int? torreId, string? tipoResidente)
        {
            await SetAuthHeaderAsync();
            var url = "api/Reportes/residentes-pdf?";
            if (torreId.HasValue) url += $"torreId={torreId}&";
            if (!string.IsNullOrEmpty(tipoResidente)) url += $"tipoResidente={tipoResidente}";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GetReporteMantenimientosPdfAsync(
            string? estado, int? tipoMantenimientoId)
        {
            await SetAuthHeaderAsync();
            var url = "api/Reportes/mantenimientos-pdf?";
            if (!string.IsNullOrEmpty(estado)) url += $"estado={estado}&";
            if (tipoMantenimientoId.HasValue) url += $"tipoMantenimientoId={tipoMantenimientoId}";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GetReporteReservasPdfAsync(
            int? zonaComunId, DateTime fechaInicio, DateTime fechaFin)
        {
            await SetAuthHeaderAsync();
            var url = $"api/Reportes/reservas-pdf?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}";
            if (zonaComunId.HasValue) url += $"&zonaComunId={zonaComunId}";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GetReporteVisitantesParqueaderoPdfAsync(
            DateTime fechaInicio, DateTime fechaFin)
        {
            await SetAuthHeaderAsync();
            var url = $"api/Reportes/visitantes-parqueadero-pdf?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        // ── Excel ─────────────────────────────────────────────
        public async Task<byte[]> GetReporteOcupacionParqueaderoExcelAsync()
        {
            await SetAuthHeaderAsync();
            var response = await _http.GetAsync("api/Reportes/ocupacion-parqueadero-excel");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GetReporteMensajeriaExcelAsync(
            int? apartamentId, DateTime fechaInicio, DateTime fechaFin)
        {
            await SetAuthHeaderAsync();
            var url = $"api/Reportes/mensajeria-excel?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}";
            if (apartamentId.HasValue) url += $"&apartamentId={apartamentId}";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GetReporteIngresosPorTipoExcelAsync()
        {
            await SetAuthHeaderAsync();
            var response = await _http.GetAsync("api/Reportes/ingresos-por-tipo-excel");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GetReporteZonasComunesExcelAsync(
            int? zonaComunId, int? mes, int? año)
        {
            await SetAuthHeaderAsync();
            var url = "api/Reportes/zonas-comunes-excel?";
            if (zonaComunId.HasValue) url += $"zonaComunId={zonaComunId}&";
            if (mes.HasValue) url += $"mes={mes}&";
            if (año.HasValue) url += $"año={año}";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GetReporteUsuariosPorRolExcelAsync()
        {
            await SetAuthHeaderAsync();
            var response = await _http.GetAsync("api/Reportes/usuarios-por-rol-excel");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}