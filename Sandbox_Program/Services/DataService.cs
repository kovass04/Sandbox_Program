using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace Sandbox_Program.Services
{

    public class DataService
    {
        private readonly HttpClient _httpClient;
        public DataService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.hackerearth.com/v4/partner/code-evaluation/submissions/");
        }
        public async Task<string> SubmitCodeAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = _httpClient.BaseAddress,
                Headers =
                {
                    { "client-secret", $"{ReadCodeFromFile()}" },
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "lang", "PYTHON" },
                    { "source", "print 'Hello World'" },
                    { "context", "2215" },
                    { "time_limit", "5" },
                    { "memory_limit", "262144" },
                    { "input", "" },
                    { "callback","https://client.com/callback/" }
                }),
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);

                return body;
            }
        }
        private string ReadCodeFromFile()
        {
            string code = null;

            try
            {
                string filePath = "key.txt";
                code = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }

            return code;
        }
    }
}
