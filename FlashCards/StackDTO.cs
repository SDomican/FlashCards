using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards
{
    internal class StackDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public List<FlashCardDTO> CardList { get; set; } = new List<FlashCardDTO>();
    }

    internal class FlashCardDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public FlashCardDTO(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
