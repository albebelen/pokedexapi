using Microsoft.AspNetCore.Mvc;

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
}
