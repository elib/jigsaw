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

        public PlayScene() : base()
        {
            canvas = new Canvas();
            canvas.Initialize(Core.game.Content);
            this.add(canvas);

            puzzle = new Puzzle();
            puzzle.Create("columbo", 80, canvas);
            this.add(puzzle);

            player = new Player();
            player.Initialize(Core.game.Content);
            this.add(player);
        }
    }
}