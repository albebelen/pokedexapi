using Microsoft.AspNetCore.Mvc;
using PokedexApi.Models;
using Newtonsoft.Json.Linq;

namespace PokedexApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{
    private readonly ILogger<PokemonController> _logger;

    public PokemonController(ILogger<PokemonController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{name}")]
    public async Task<Pokemon> GetPokemon(string name) 
    {
        Pokemon pokemon;
        string url = $"https://pokeapi.co/api/v2/pokemon-species/{name}"; // global ? 

        using (HttpClient client = new HttpClient()) 
        {
            HttpResponseMessage res = await client.GetAsync(url);
            var jsonRes = JObject.Parse(await res.Content.ReadAsStringAsync());
            pokemon = new Pokemon()
            {
                Id = (int) jsonRes["id"],
                Name = jsonRes["name"].ToString(),
                Habitat = jsonRes["habitat"]["name"].ToString(),
                Description = jsonRes["flavor_text_entries"][0]["flavor_text"].ToString(),
                IsLegendary = (bool) jsonRes["is_legendary"]
            };  
        }

        return pokemon;
    }
}
