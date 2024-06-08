using Microsoft.AspNetCore.Mvc;
using PokemonApp.Models;
using System.Text.Json;

namespace PokemonApp.Controllers
{
    public class PokemonController : Controller
    {
        private readonly HttpClient _httpClient;

        public PokemonController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/?limit=20&offset={(page - 1) * 20}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var PokemonResponse = JsonSerializer.Deserialize<PokemonResponse>(responseBody);

            if (PokemonResponse != null && PokemonResponse.Results !=null) 
            { 
            ViewBag.NextPage = PokemonResponse.Results.Count == 20 ? page + 1 : (int?)null;
            ViewBag.PreviousPage = page > 1 ? page - 1 : (int?)null;
            }

            return View(PokemonResponse.Results);
        }

        public async Task<IActionResult> Details(string name)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var pokemon = JsonSerializer.Deserialize<Pokemon>(responseBody);

            // Map abilities and moves
            pokemon.Abilities = new List<Ability>();
            foreach (var ability in JsonSerializer.Deserialize<JsonElement>(responseBody).GetProperty("abilities").EnumerateArray())
            {
                pokemon.Abilities.Add(new Ability { Abilities = ability.GetProperty("ability").GetProperty("name").GetString() });
            }

            pokemon.Moves = new List<Move>();
            foreach (var move in JsonSerializer.Deserialize<JsonElement>(responseBody).GetProperty("moves").EnumerateArray())
            {
                pokemon.Moves.Add(new Move { Moves = move.GetProperty("move").GetProperty("name").GetString() });
            }

            return View(pokemon);
        }
    }
}
