using Microsoft.AspNetCore.Mvc;
using PokedexApi.Models;
using Newtonsoft.Json.Linq;
using System.Text;

namespace PokedexApi.Service;

public class PokemonService
{
    private readonly ILogger<PokemonService> _logger; 
    private readonly string _url = "https://pokeapi.co/api/v2/pokemon-species/";

    public PokemonService(ILogger<PokemonService> logger)
    {
        _logger = logger;
    }

    public async Task<Pokemon> GetPokemon(string name)
    {
        Pokemon pokemon;

        using (HttpClient client = new HttpClient())
        {
            string descr = "";

            try
            {
                HttpResponseMessage res = await client.GetAsync(_url + name);

                if ((int)res.StatusCode == 200)
                {
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
                        Habitat = jsonRes["habitat"].ToArray().Count() == 0 ? "" : jsonRes["habitat"]["name"].ToString(),
                        Description = descr,
                        IsLegendary = (bool)jsonRes["is_legendary"]
                    };
                }
                else
                {
                    pokemon = new Pokemon()
                    {
                        Id = null,
                        Name = String.Empty,
                        Habitat = String.Empty,
                        Description = String.Empty,
                        IsLegendary = null
                    };
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