using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    class PlayScene : Scene
    {
        public PlayScene(Game1 game ) : base(game) 
        {
            Puzzle puzzle = new Puzzle(game);
            puzzle.Create("columbo", 100);
            this.add(puzzle);

            Player p = new Player();
            p.Initialize(_game.Content);
            this.add(p);
        }
    }
}