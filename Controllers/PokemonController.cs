using Microsoft.AspNetCore.Mvc;
using PokedexApi.Models;
using Newtonsoft.Json.Linq;

namespace PokedexApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{
    private readonly ILogger<PokemonController> _logger;
    private readonly string _url = "https://pokeapi.co/api/v2/pokemon-species/";

    public PokemonController(ILogger<PokemonController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{name}")]
    public async Task<Pokemon> GetPokemon(string name) 
    {
        Pokemon pokemon;

        using (HttpClient client = new HttpClient())
        {
            string descr = ""; 

            HttpResponseMessage res = await client.GetAsync(_url + name);
            var jsonRes = JObject.Parse(await res.Content.ReadAsStringAsync());
            var list = jsonRes["flavor_text_entries"].ToList();
            
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i]["language"]["name"].ToString() == "en") // get the first english description
                {
                    descr = list[i]["flavor_text"].ToString();
                    break;
                }
            }

            pokemon = new Pokemon()
            {
                Id = (int)jsonRes["id"],
                Name = jsonRes["name"].ToString(),
                Habitat = jsonRes["habitat"]["name"].ToString(),
                Description = descr, 
                IsLegendary = (bool)jsonRes["is_legendary"]
            };
        }

        return pokemon;
    }
}
