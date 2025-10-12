namespace PokedexApi.Models;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsLegendary { get; set; }
    public string Description { get; set; }
    public string? Habitat {get; set; }
}
