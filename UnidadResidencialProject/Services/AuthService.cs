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
                    username = email,
                    password = password
                });

                if (!response.IsSuccessStatusCode) return false;

                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                var token = result.GetProperty("token").GetString();

                if (string.IsNullOrEmpty(token)) return false;

                // Guardar token en localStorage
                await _js.InvokeVoidAsync("localStorage.setItem", "jwt_token", token);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> GetTokenAsync()
            => await _js.InvokeAsync<string>("localStorage.getItem", "jwt_token");

        public async Task LogoutAsync()
            => await _js.InvokeVoidAsync("localStorage.removeItem", "jwt_token");

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }
    }
}