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

        /// <summary>
        /// Submits provided code for evaluation to a specified API endpoint.
        /// </summary>
        /// <param name="code">The code to be submitted for evaluation.</param>
        /// <returns>A string containing the response body received from the API.</returns>
        public async Task<string> SubmitCodeAsync(string code)
        {
            try
            {
                // Create an HTTP request message for submitting the code
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = _httpClient.BaseAddress,
                    Headers =
                {
                    // Set the client-secret header using the key read from the file
                    { "client-secret", $"{ReadKeyFromFile()}" },
                },
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "lang", "PYTHON" }, // TODO Switching of programming language.
                    { "source", code },
                    { "context", "1" }, // TODO maybe I can delete this 
                    { "time_limit", "5" }, // TODO Setting a runtime limit.
                    { "memory_limit", "262144" }, // TODO Setting a memory limit
                    { "input", "" }, // TODO optional
                    { "callback","https://client.com/callback/" }
                }),
                };
                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    // Read and return the response body from the API
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);

                    return body;
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs during the request, catch the exception and display an error message
                Console.WriteLine("Error submitting code: " + ex.Message);

                // Return an empty string to indicate an error
                return string.Empty;
            }
        }

        public async Task<string> GetStatusAsync(string he_id) 
        {

            _httpClient.DefaultRequestHeaders.Add("client-secret", ReadKeyFromFile());

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri (_httpClient.BaseAddress + he_id),
                Headers =
                {
                    // Set the client-secret header using the key read from the file
                    { "client-secret", $"{ReadKeyFromFile()}" },
                }
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                // Read and return the response body from the API
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);

                return body;
            }
        }

        /// <summary>
        /// A utility method to read a key from a file named "key.txt".
        /// </summary>
        /// <returns>The key read from the file. Returns null if there was an error reading the key.</returns>
        private string ReadKeyFromFile()
        {
            string key = null;

            try
            {
                // Define the path to the file containing the key   
                string filePath = "key.txt";

                // Read the content of the file into a string
                key = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }

            // TODO rewrite the method or add error notification
            return key;
        }
    }
}
