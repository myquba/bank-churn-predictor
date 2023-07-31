using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BankChurnPredictor.Models;
using System.Collections.Generic;  // Added this line

namespace BankChurnPredictor.Controllers
{
    [Route("predict")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerData data)
        {
            var scoreRequest = new
            {
                Inputs = new Dictionary<string, List<CustomerData>>()
                {
                    { 
                        "data", 
                        new List<CustomerData> { data }
                    },
                },
                GlobalParameters = new Dictionary<string, string>()
            };

            const string apiKey = "Your_Azure_ML_API_Key"; // Replace this with the API key for the web service
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            client.BaseAddress = new Uri("Your_Azure_ML_Endpoint");

            // WARNING: The 'await' statement below can result in a deadlock
            // if you are calling this code from the UI thread of an ASP.Net application.
            // One way to address this would be to call ConfigureAwait(false)
            // so that the execution does not attempt to resume on the original context.
            // For instance, replace code such as:
            //      result = await DoSomeTask()
            // with the following:
            //      result = await DoSomeTask().ConfigureAwait(false)

            HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            else
            {
                return BadRequest(string.Format("The request failed with status code: {0}", response.StatusCode));
            }
        }
    }
}
