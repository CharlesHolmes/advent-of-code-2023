using Day02Problem1.Draws;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02Problem1.Games
{
    public class GameFactory : IGameFactory
    {
        private readonly IDrawFactory _drawFactory;

        public GameFactory(IDrawFactory drawFactory)
        {
            _drawFactory = drawFactory;
        }

        public IGame Create(string input) => new Game(_drawFactory, input);
    }
}
