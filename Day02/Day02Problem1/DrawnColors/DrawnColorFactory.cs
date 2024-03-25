using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02Problem1.DrawnColors
{
    public class DrawnColorFactory : IDrawnColorFactory
    {
        public IDrawnColor Create(string drawnColorString) => new DrawnColor(drawnColorString);
    }
}
