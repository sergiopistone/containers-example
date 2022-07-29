using System.Text.Json;
using LoremIpsum.UI.Settings;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace LoremIpsum.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<LoremIpsumApiSettings> _loremIpsumApiSettings;

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory, IOptions<LoremIpsumApiSettings> loremIpsumApiSettings)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _loremIpsumApiSettings = loremIpsumApiSettings;
        }

        public IEnumerable<string> GeneratedParagraphs { get; set; } = Array.Empty<string>();

        public void OnGet()
        {
        }

        public async Task OnPost(int paragraphs)
        {
            this.GeneratedParagraphs = await FetchLorenIpsumParagraphs(paragraphs);
        }

        private async Task<IEnumerable<string>> FetchLorenIpsumParagraphs(int paragraphs)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{_loremIpsumApiSettings.Value.BaseUrl}/api/paragraphs/{paragraphs}")
            {
                Headers = {
                    { HeaderNames.Accept, "application/json" },
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                this.GeneratedParagraphs = new string[] { "There was an error invoking the API, respose status is " + httpResponseMessage.StatusCode };
            }

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            return (await JsonSerializer.DeserializeAsync<IEnumerable<string>>(contentStream)) ?? Array.Empty<string>();
        }
    }
}
