using System.Collections.Generic;

namespace PokemonApp.Models
{
    public class Pokemon
    {
        public string Name { get; set; }
        public List<PokemonAbility> Abilities { get; set; }
        public List<PokemonMove> Moves { get; set; }

        public class PokemonAbility
        {
            public AbilityDetail Ability { get; set; }

            public class AbilityDetail
            {
                public string Name { get; set; }
            }
        }

        public class PokemonMove
        {
            public MoveDetail Move { get; set; }

            public class MoveDetail
            {
                public string Name { get; set; }
            }
        }
    }
}