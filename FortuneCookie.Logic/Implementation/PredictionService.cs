using System.Text.Json.Serialization;
using FortuneCookie.Logic.Abstraction;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FortuneCookie.Logic.Implementation;

public class PredictionService : IPredictionService
{
    private readonly HttpClient _client;

    public PredictionService(HttpClient client)
    {
        _client = client;
    }
    public async Task<string> GetPrediction()
    {
        var response = await _client.GetAsync($"?method=getQuote&format=json&lang=en");
        return await ProcessPrediction(response);
    }

    private async Task<string> ProcessPrediction(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode) return String.Empty;
        try
        {
            var content = await response.Content.ReadAsStringAsync();
            dynamic deserializedContent = JObject.Parse(content);
            return deserializedContent?.quoteText?.ToString() ?? string.Empty;
        }
        catch (Exception e)
        {
            return string.Empty;
        }
        
    }
}