using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    class PlayScene : Scene
    {
        public Puzzle puzzle;
        public Player player;

        public PlayScene(Game1 game ) : base(game) 
        {
            puzzle = new Puzzle(game);
            puzzle.Create("columbo", 100);
            this.add(puzzle);

            player = new Player();
            player.Initialize(_game.Content);
            this.add(player);
        }
    }
}