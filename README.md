
# PokemonGoStats
C# library used to calculate Pokemon Stats in Pokemon Go game.

## This repository is C# version of [PokemonGoStats.js](https://github.com/earthchie/PokemonGoStats.js)

## How to use?
```csharp
using PokemonGoStat.Enums;
var vaporean = new PokemonGoStat.PokemonGoStat(Pokemon.Vaporeon);

//or

var vaporean = new PokemonGoStat.PokemonGoStat(dexId: 134);

var stat = vaporean.GetStats(
                    ivStamina: 15,
                    ivAttack: 15,
                    ivDefense: 15,
                    cp: 2595
                );
//stat = {Attack: 152, Defense: 138, HP: 208}

var level = vaporean.GetLevel(
                    ivStamina: 15,
                    ivAttack: 15,
                    ivDefense: 15,
                    cp: 2595
                );
//level = 34.5

var cp = vaporean.GetCP(
                    ivStamina: 15,
                    ivAttack: 15,
                    ivDefense: 15,
                    pokemonLevel: 34.5
                );
//cp = 2595

var maxCp = vaporean.GetMaxCP(
                    ivStamina: 15,
                    ivAttack: 15,
                    ivDefense: 15,
                    playerLevel: 40
                );
//maxCp = 2816    
```

## initialize constructor with ivStamina, ivAttack, ivDefense
```csharp
using PokemonGoStat.Enums;
var vaporean = new PokemonGoStat.PokemonGoStat(
                    pokemon: Pokemon.Vaporeon,
                    ivStamina: 15,
                    ivAttack: 15,
                    ivDefense: 15
                );

//or

var vaporean = new PokemonGoStat.PokemonGoStat(
                    dexId: 134,
                    ivStamina: 15,
                    ivAttack: 15,
                    ivDefense: 15
                );

var stat = vaporean.GetStats(cp: 2595);
//stat = {Attack: 152, Defense: 138, HP: 208}

var level = vaporean.GetLevel(cp: 2595);
//level = 34.5

var cp = vaporean.GetCP(pokemonLevel: 34.5);
//cp = 2595

var maxCp = vaporean.GetMaxCP(playerLevel: 40);
//maxCp = 2816    
```
## Limitation
- Both Player level and Pokemon level max at 40
- First Generation Pokemons only (Dex 001-151)

## License
WTFPL 2.0 http://www.wtfpl.net/

##Credits
earthchie - [PokemonGoStats.js](https://github.com/earthchie/PokemonGoStats.js)