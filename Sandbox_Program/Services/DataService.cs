using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace Sandbox_Program.Services
{
    /// <summary>
    /// A service class for interacting with the HackerEarth code evaluation API.
    /// </summary>
    public class DataService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the DataService class with the base API address.
        /// </summary>
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
        public async Task<string> PostCodeAsync(string code, string lang, int memory_limit, int time_limit)
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
                    { "lang", lang },
                    { "source", code },
                    { "time_limit", $"{time_limit}" },
                    { "memory_limit", $"{memory_limit}" },
                    { "input", "" }, // TODO optional
                    { "callback","https://client.com/callback/" }
                }),
                };
                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    // Read and return the response body from the API
                    var body = await response.Content.ReadAsStringAsync();

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

        /// <summary>
        /// Retrieves the status of a submitted code evaluation based on the provided `he_id`.
        /// </summary>
        /// <param name="he_id">The unique identifier (he_id) associated with the submitted code evaluation.</param>
        /// <returns>A string containing the response body received from the API, representing the evaluation status.</returns>
        public async Task<string> GetStatusAsync(string he_id) 
        {
            try
            {
                // Create an HTTP request message for retrieving the evaluation status
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_httpClient.BaseAddress + he_id),
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

                    return body;
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs during the request, catch the exception and display an error message
                Console.WriteLine("Error retrieving status: " + ex.Message);

                // Return an empty string to indicate an error
                return string.Empty;
            }
        }

        /// <summary>
        /// Retrieves the content from a specified URL using an HTTP GET request.
        /// </summary>
        /// <param name="url">The URL to send the GET request to.</param>
        /// <returns>A string containing the content received from the specified URL. Returns null if the request is not successful.</returns>
        public async Task<string> GetResultAsync(string url) 
        {
            try
            {
                string content = null;

                // Send an HTTP GET request to the specified URL
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Read the content of the response into a string
                    content = await response.Content.ReadAsStringAsync();
                }
                // Return the retrieved content or null if the request is not successful
                return content;
            }
            catch (Exception ex)
            {
                // If an exception occurs during the request, catch the exception and display an error message
                Console.WriteLine("Error retrieving content: " + ex.Message);

                // Return null to indicate an error
                return null;
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
