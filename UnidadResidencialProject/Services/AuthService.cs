using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace UnidadResidencialProject.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public AuthService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Auth/login", new
                {
                    Documento = email,
                    Password = password
                });

                if (!response.IsSuccessStatusCode) return false;

                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                var token = result.GetProperty("token").GetString();

                if (string.IsNullOrEmpty(token)) return false;

                // Guardar token en localStorage
                await _js.InvokeVoidAsync("sessionStorage.setItem", "jwt_token", token);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                return await _js.InvokeAsync<string>("sessionStorage.getItem", "jwt_token");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var token = await GetTokenAsync();
                return !string.IsNullOrEmpty(token);
            }
            catch
            {
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _js.InvokeVoidAsync("sessionStorage.removeItem", "jwt_token");
        }

        /// <summary>
        /// Decodifica el JWT almacenado y extrae el userId del payload.
        /// </summary>
        public async Task<int> GetUserIdAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token)) return 0;

            try
            {
                var parts = token.Split('.');
                if (parts.Length != 3) return 0;

                // Decodificar el payload (base64url)
                var payload = parts[1];
                payload = payload.Replace('-', '+').Replace('_', '/');
                switch (payload.Length % 4)
                {
                    case 2: payload += "=="; break;
                    case 3: payload += "="; break;
                }

                var bytes = Convert.FromBase64String(payload);
                var json = System.Text.Encoding.UTF8.GetString(bytes);
                var doc = JsonDocument.Parse(json);

                // Intentar nombres comunes de claims para el ID de usuario
                string[] claimNames = {
                    "sub", "nameid", "userId", "UsuarioId", "usuarioId",
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
                };

                foreach (var claim in claimNames)
                {
                    if (doc.RootElement.TryGetProperty(claim, out var value))
                    {
                        if (value.ValueKind == JsonValueKind.Number)
                            return value.GetInt32();
                        if (value.ValueKind == JsonValueKind.String &&
                            int.TryParse(value.GetString(), out int id))
                            return id;
                    }
                }
            }
            catch { }

            return 0;
        }

        public async Task<string> GetRolAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token)) return "";
            try
            {
                var parts = token.Split('.');
                if (parts.Length != 3) return "";
                var payload = parts[1];
                payload = payload.Replace('-', '+').Replace('_', '/');
                switch (payload.Length % 4)
                {
                    case 2: payload += "=="; break;
                    case 3: payload += "="; break;
                }
                var bytes = Convert.FromBase64String(payload);
                var json = System.Text.Encoding.UTF8.GetString(bytes);
                var doc = JsonDocument.Parse(json);
                string[] claimNames = {
            "role",
            "roles",
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };
                foreach (var claim in claimNames)
                {
                    if (doc.RootElement.TryGetProperty(claim, out var value))
                        return value.GetString() ?? "";
                }
            }
            catch { }
            return "";
        }
    }
}