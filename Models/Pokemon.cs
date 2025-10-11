namespace PokedexApi.Models;

public class Pokemon
{
    public int id { get; set; };
    public string name { get; set; };
    public bool isLegendary { get; set; };
    public string description { get; set; };
    public string? habitat {get; set; }
}
