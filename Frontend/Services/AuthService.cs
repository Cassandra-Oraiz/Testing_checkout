using System.Text;
using System.Text.Json;
using Frontend.Models;

namespace Frontend.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _http;
        private const string BackendBase = "http://localhost:5096";

        public AuthService(IHttpClientFactory http)
        {
            _http = http;
        }

        public async Task<(bool Success, string Role, string Name, string UserId, string UserGroupId, string ErrorMessage)> AuthenticateAsync(LoginViewModel model)
        {
            try
            {
                var client = _http.CreateClient();
                var payload = JsonSerializer.Serialize(new { email = model.Email, password = model.Password });
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{BackendBase}/LogIn", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    string role = model.Role?.ToLower() ?? "admin";
                    string name = model.Email;
                    string userId = "";
                    string userGroupId = "";

                    try
                    {
                        using var doc = JsonDocument.Parse(json);
                        var root = doc.RootElement;
                        if (root.TryGetProperty("user_ID", out var uid)) userId = uid.GetRawText().Trim('"');
                        if (root.TryGetProperty("full_Name", out var fn)) name = fn.GetString() ?? model.Email;
                        if (root.TryGetProperty("userGroup_ID", out var ug)) userGroupId = ug.GetRawText().Trim('"');
                        if (root.TryGetProperty("role", out var rp)) role = rp.GetString()?.ToLower() ?? role;
                    }
                    catch { }

                    return (true, role, name, userId, userGroupId, "");
                }
                else
                {
                    string errMsg = "Invalid email or password.";
                    try
                    {
                        var errJson = await response.Content.ReadAsStringAsync();
                        using var doc = JsonDocument.Parse(errJson);
                        if (doc.RootElement.TryGetProperty("message", out var mp))
                            errMsg = mp.GetString() ?? errMsg;
                        else if (doc.RootElement.TryGetProperty("title", out var tp))
                            errMsg = tp.GetString() ?? errMsg;
                    }
                    catch { }

                    return (false, "", "", "", "", errMsg);
                }
            }
            catch (Exception ex)
            {
                return (false, "", "", "", "", "Cannot connect to backend: " + ex.Message);
            }
        }
    }
}