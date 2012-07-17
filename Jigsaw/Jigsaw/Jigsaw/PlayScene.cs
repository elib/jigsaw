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

        public GameObjectGroup completedPieces;

        public PlayScene() : base()
        {
            //get next puzzle image
            string puzzleImage = Core.GetNextImage();

            canvas = new Canvas();
            canvas.Initialize(Core.game.Content);
            this.add(canvas);

            completedPieces = new GameObjectGroup();
            this.add(completedPieces);

            puzzle = new Puzzle();
            puzzle.Create(puzzleImage, 80, canvas);
            this.add(puzzle);

            player = new Player();
            player.Initialize(Core.game.Content);
            this.add(player);
        }
    }
}