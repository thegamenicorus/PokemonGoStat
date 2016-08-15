using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonGoStat.DB;
using PokemonGoStat.Enums;

namespace PokemonGoStat
{
    public class Stat {
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
    }

    public class PokemonGoStat
    {
        private int dexId;
        private BaseStat BaseStat { get { return BaseStats.GetBaseStat(this.dexId); } }
        private int ivStamina;
        private int ivAttack;
        private int ivDefense;
        public PokemonGoStat(int dexId)
        {
            this.dexId = dexId;
        }

        public PokemonGoStat(int dexId, int ivStamina, int ivAttack, int ivDefense)
        {
            this.dexId = dexId;
            this.ivAttack = ivAttack;
            this.ivDefense = ivDefense;
            this.ivStamina = ivStamina;
        }

        public PokemonGoStat(Pokemon pokemon) : this((int)pokemon) { }

        public PokemonGoStat(Pokemon pokemon, int ivStamina, int ivAttack, int ivDefense) : this((int)pokemon, ivStamina, ivAttack, ivDefense) { }

        private bool isValidPromptStat()
        {
            return this.ivAttack != 0 && this.ivDefense != 0 && this.ivStamina != 0;
        }

        private double findPokemonLevelFromCPM(double cpm)
        {
            var index = Array.IndexOf(CPMs.All, CPMs.All.First(cpmItem => cpmItem >= cpm));
            return (+index + 2) / 2.0;
        }

        public double GetCP(double pokemonLevel)
        {
            if (!isValidPromptStat())
                return -1;
            return this.GetCP(this.ivStamina, this.ivAttack, this.ivDefense, pokemonLevel);
        }

        public double GetCP(int ivStamina, int ivAttack, int ivDefense, double pokemonLevel)
        {
            var CP = (BaseStat.BaseAttack + ivAttack) * Math.Sqrt(BaseStat.BaseDefense + ivDefense) * Math.Sqrt(BaseStat.BaseStamina + ivStamina) * Math.Pow(CPMs.GetCPM(pokemonLevel), 2) / 10.0;

            if (CP < Config.MinCP)
            {
                CP = Config.MinCP;
            }
            return Math.Floor(CP);
        }

        public double GetMaxCP(double playerLevel)
        {
            if (!isValidPromptStat())
                return -1;
            return this.GetMaxCP(this.ivStamina, this.ivAttack, this.ivDefense, playerLevel);
        }

        public double GetMaxCP(int ivStamina, int ivAttack, int ivDefense, double playerLevel)
        {
            var pokemonMaxLevel = playerLevel + Config.AdditionalLevel;
            if (pokemonMaxLevel > Config.MaxPokemonLevel)
            {
                pokemonMaxLevel = Config.MaxPokemonLevel;
            }
            return this.GetCP(ivStamina, ivAttack, ivDefense, pokemonMaxLevel);
        }

        public double GetCPM(double cp)
        {
            if (!isValidPromptStat())
                return -1;
            return GetCPM(this.ivStamina, this.ivAttack, this.ivDefense, cp);
        }

        public double GetCPM(int ivStamina, int ivAttack, int ivDefense, double cp)
        {
            return Math.Sqrt(10 * cp / (BaseStat.BaseAttack + ivAttack) / Math.Sqrt(BaseStat.BaseDefense + ivDefense) / Math.Sqrt(BaseStat.BaseStamina + ivStamina));
        }

        public double GetLevel(double cp)
        {
            if (!isValidPromptStat())
                return -1;
            return this.GetLevel(this.ivStamina, this.ivAttack, this.ivDefense, cp);
        }

        public double GetLevel(int ivStamina, int ivAttack, int ivDefense, double cp)
        {
            return this.findPokemonLevelFromCPM(this.GetCPM(ivStamina, ivAttack, ivDefense, cp));
        }

        public Stat GetStats(double cp)
        {
            if (!isValidPromptStat())
                return null;
            return this.GetStats(this.ivStamina, this.ivAttack, this.ivDefense, cp);
        }

        public Stat GetStats(int ivStamina, int ivAttack, int ivDefense, double cp)
        {
            var cpm = this.GetCPM(ivStamina, ivAttack, ivDefense, cp);

            return new Stat
            {
                HP = (int)Math.Floor((BaseStat.BaseStamina + ivStamina) * cpm),
                Attack = (int)Math.Floor((BaseStat.BaseAttack + ivAttack) * cpm),
                Defense = (int)Math.Floor((BaseStat.BaseDefense + ivDefense) * cpm)
            };
        }
    }
}
