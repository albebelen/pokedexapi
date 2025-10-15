using Microsoft.AspNetCore.Mvc;
using PokedexApi.Models;
using Newtonsoft.Json.Linq;
using System.Net;
using PokedexApi.Service;

namespace PokedexApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{
    private readonly ILogger<PokemonController> _logger;
    private readonly PokemonService _pokemonService;
    private readonly string _urlTranslator = "https://api.funtranslations.com/translate/"; 

    public PokemonController(ILogger<PokemonController> logger, PokemonService pokemonService)
    {
        _logger = logger;
        _pokemonService = pokemonService;
    }

    [HttpGet("{name}")]
    public async Task<Pokemon> GetPokemon(string name) 
    {
        Pokemon pokemon = await _pokemonService.GetPokemon(name);
        return pokemon;
    }
    
    [HttpGet("translated/{name}")]
    public async Task<Pokemon> GetTranslatedPokemon(string name)
    {
        Pokemon pokemon = await _pokemonService.GetPokemon(name);

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string param = $"text={WebUtility.UrlEncode(pokemon.Description).Replace("%0C", "%20").Replace("+", "%20").Replace("%0A", "%20")}"; // todo: this could be done with regex
                HttpResponseMessage res;
                                          
                if (pokemon.Habitat.ToLower() == "cave" || pokemon.IsLegendary == true)          
                        res = await client.GetAsync(_urlTranslator + "yoda.json?" + param);
                    else

                        res = await client.GetAsync(_urlTranslator + "shakespeare.json?" + param);

                if ((int)res.StatusCode == 200)
                {
                    var jsonRes = JObject.Parse(await res.Content.ReadAsStringAsync());
                    pokemon.Description = jsonRes["contents"]["translated"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pokemon;
        }
    }
}