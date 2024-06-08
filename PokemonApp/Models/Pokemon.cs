using Microsoft.AspNetCore.Mvc;

namespace PokemonApp.Models
{
    public class Pokemon
    {
        public string name { get; set; }
        public List<Ability> Abilities { get; set; }
        public List<Move> Moves { get; set; }
    }

    public class Ability
    {
        public string Abilities { get; set; }
    }

    public class Move
    {
        public string Moves { get; set; }
    }

    public class PokemonResponse
    {
        public List<PokemonListItem> Results { get; set; }
    }

    public class PokemonListItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}