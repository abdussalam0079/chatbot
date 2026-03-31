using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace AIChatbot
{
    public partial class Form1 : Form
    {
        private const string FallbackModel = "gemini-flash-lite-latest";

        private readonly string _apiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY") ?? string.Empty;
        private readonly string _model = Environment.GetEnvironmentVariable("GOOGLE_MODEL") ?? FallbackModel;

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly List<object> _conversationHistory = new List<object>();

        public Form1()
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                btnSend.Enabled = false;
                lblStatus.Text = "Missing GOOGLE_API_KEY";
                lblStatus.ForeColor = Color.Red;
                AppendMessage("System", "Add your Google API key to the .env file as GOOGLE_API_KEY=your_key and restart the app.", Color.Red);
                return;
            }

            SetupHttpClient();
            AppendMessage("Assistant", "Hello! I am your AI assistant powered by Google Gemini. How can I help you today?", Color.FromArgb(0, 120, 215));
        }

        // ── HTTP Setup ──────────────────────────────
        private void SetupHttpClient()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "AIChatbot-WinForms");
        }

        // ── Send Button Click ───────────────────────
        private async void btnSend_Click(object sender, EventArgs e)
        {
            string userMessage = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(userMessage)) return;

            txtInput.Clear();
            AppendMessage("You", userMessage, Color.FromArgb(40, 167, 69));

            btnSend.Enabled = false;
            lblStatus.Text = "AI is thinking...";
            lblStatus.ForeColor = Color.Orange;

            try
            {
                string response = await SendMessageToGeminiAsync(userMessage);
                AppendMessage("Assistant", response, Color.FromArgb(0, 120, 215));
                lblStatus.Text = "Ready";
                lblStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                AppendMessage("Error", $"Failed to get response: {ex.Message}", Color.Red);
                lblStatus.Text = "Error occurred";
                lblStatus.ForeColor = Color.Red;
            }
            finally
            {
                btnSend.Enabled = !string.IsNullOrWhiteSpace(_apiKey);
            }
        }

        // ── Gemini API Call ─────────────────────────
        private async Task<string> SendMessageToGeminiAsync(string userMessage)
        {
            _conversationHistory.Add(new
            {
                role = "user",
                parts = new[]
                {
                    new { text = userMessage }
                }
            });

            var requestBody = new
            {
                contents = _conversationHistory,
                generationConfig = new
                {
                    temperature = 0.7,
                    maxOutputTokens = 512
                }
            };

            string json = JsonSerializer.Serialize(requestBody);
            string responseBody = await GenerateContentWithFallbackAsync(json);

            using JsonDocument doc = JsonDocument.Parse(responseBody);
            string assistantReply = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? "No response received.";

            _conversationHistory.Add(new
            {
                role = "model",
                parts = new[]
                {
                    new { text = assistantReply }
                }
            });

            return assistantReply;
        }

        private async Task<string> GenerateContentWithFallbackAsync(string json)
        {
            var (httpResponse, responseBody) = await PostGenerateContentAsync(_model, json);
            if (httpResponse.IsSuccessStatusCode)
            {
                return responseBody;
            }

            bool shouldTryFallback = (httpResponse.StatusCode == HttpStatusCode.TooManyRequests || httpResponse.StatusCode == HttpStatusCode.NotFound)
                && !string.Equals(_model, FallbackModel, StringComparison.OrdinalIgnoreCase);

            if (shouldTryFallback)
            {
                var (fallbackResponse, fallbackBody) = await PostGenerateContentAsync(FallbackModel, json);
                if (fallbackResponse.IsSuccessStatusCode)
                {
                    return fallbackBody;
                }

                throw new Exception(BuildFriendlyApiErrorMessage(fallbackResponse.StatusCode, fallbackBody, FallbackModel));
            }

            throw new Exception(BuildFriendlyApiErrorMessage(httpResponse.StatusCode, responseBody, _model));
        }

        private async Task<(HttpResponseMessage Response, string Body)> PostGenerateContentAsync(string model, string json)
        {
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync(CreateApiUrl(model), content);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            return (httpResponse, responseBody);
        }

        private string CreateApiUrl(string model)
        {
            return $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={_apiKey}";
        }

        private static string BuildFriendlyApiErrorMessage(HttpStatusCode statusCode, string responseBody, string model)
        {
            string apiMessage = "Request failed.";

            try
            {
                using JsonDocument doc = JsonDocument.Parse(responseBody);
                apiMessage = doc.RootElement.GetProperty("error").GetProperty("message").GetString() ?? apiMessage;
            }
            catch
            {
                apiMessage = responseBody;
            }

            return statusCode switch
            {
                HttpStatusCode.BadRequest => "Invalid Google API key or malformed request. Update `GOOGLE_API_KEY` in `.env` and try again.",
                HttpStatusCode.NotFound => $"Model '{model}' is unavailable. Set `GOOGLE_MODEL=gemini-flash-lite-latest` in `.env`.",
                HttpStatusCode.TooManyRequests => "Google quota is currently limited for this model. The app will try the lite model automatically; if it still fails, wait a minute or enable billing in Google AI Studio.",
                _ => $"API Error {(int)statusCode}: {apiMessage}"
            };
        }

        // ── Chat Display Helper ──────────────────────
        private void AppendMessage(string sender, string message, Color color)
        {
            rtbChat.SelectionStart  = rtbChat.TextLength;
            rtbChat.SelectionLength = 0;

            // Sender label (bold, colored)
            rtbChat.SelectionColor = color;
            rtbChat.SelectionFont  = new Font("Segoe UI", 9f, FontStyle.Bold);
            rtbChat.AppendText($"[{sender}]\n");

            // Message body
            rtbChat.SelectionColor = Color.FromArgb(30, 30, 30);
            rtbChat.SelectionFont  = new Font("Segoe UI", 9f, FontStyle.Regular);
            rtbChat.AppendText($"{message}\n\n");

            rtbChat.ScrollToCaret();
        }

        // ── Clear Chat ───────────────────────────────
        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbChat.Clear();
            _conversationHistory.Clear();
            AppendMessage("Assistant", "Chat cleared. How can I help you?", Color.FromArgb(0, 120, 215));
        }

        // ── Enter Key to Send ────────────────────────
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                btnSend_Click(sender, e);
            }
        }
    }
}
