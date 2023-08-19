using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice.Characters
{
    internal class Character
    {
        private string _name = string.Empty;
        private string _rule = string.Empty;
        private int[] _cards = Array.Empty<int>();
        private string _disqualificationCondition = string.Empty;
        private string _evolutionCondition = string.Empty;
        private string _additionalPointCondition = string.Empty;

        public string Name { get => _name; set => _name = value; }
        
        public string Rule { get => _rule; set => _rule = value; }
        
        public int[] Cards { get => _cards; set => _cards = value; }
        
        public string DisqualificationCondition { get => _disqualificationCondition; set => _disqualificationCondition = value; }
        
        public string EvolutionCondition { get => _evolutionCondition; set => _evolutionCondition = value; }
        
        public string AdditionalPointCondition { get => _additionalPointCondition; set => _additionalPointCondition = value; }
    }
}
