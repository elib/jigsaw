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
        public Canvas canvas;

        public PlayScene(Game1 game ) : base(game) 
        {
            canvas = new Canvas();
            canvas.Initialize(game.Content);
            this.add(canvas);

            puzzle = new Puzzle(game);
            puzzle.Create("columbo", 100, canvas);
            this.add(puzzle);

            player = new Player();
            player.Initialize(_game.Content);
            this.add(player);
        }
    }
}