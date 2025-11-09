using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameCore.GameFlow
{
    public class GamePhase
    {
        public string Name { get; private set; }
        public GamePhase(string name)
        {
            Name = name;
        }
    }
}
