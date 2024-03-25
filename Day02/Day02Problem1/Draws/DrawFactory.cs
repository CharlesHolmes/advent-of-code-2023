using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day02Problem1.DrawnColors;

namespace Day02Problem1.Draws
{
    public class DrawFactory : IDrawFactory
    {
        private readonly IDrawnColorFactory _drawnColorFactory;

        public DrawFactory(IDrawnColorFactory drawnColorFactory)
        {
            _drawnColorFactory = drawnColorFactory;
        }

        public IDraw Create(string input) => new Draw(_drawnColorFactory, input);
    }
}
