using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Jigsaw
{
    class PlayScene : Scene
    {
        public Puzzle puzzle;
        public Player player1;
        public Player player2;
        public Canvas canvas;

        public GameObjectGroup completedPieces;
        public GameObjectGroup carriedPieces;

        private TimeNotifier idleTimer = new TimeNotifier(3);

        public PlayScene() : base(3, 3, 3)
        {
            //get next puzzle image
            string puzzleImage = Core.GetNextImage();

            canvas = new Canvas();
            canvas.Initialize(Core.game.Content);
            this.add(canvas);

            completedPieces = new GameObjectGroup();
            carriedPieces = new GameObjectGroup();
            puzzle = new Puzzle(completedPieces, carriedPieces);
            puzzle.Create(puzzleImage, 120, canvas);

            
            this.add(completedPieces);
            this.add(puzzle);
            this.add(carriedPieces);

            player1 = new Player(puzzle, PlayerIndex.One);
            player1.Initialize(Core.game.Content);
            this.add(player1);

            player2 = new Player(puzzle, PlayerIndex.Two);
            player2.Initialize(Core.game.Content);
            this.add(player2);

            idleTimer.NotifyMe();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsTransitioning)
            {
                idleTimer.NotifyMe(true); //keep pushing idle timer ahead
                return;
            }

            if (InputManager.justPressedKeys.Count > 0 || Keyboard.GetState().GetPressedKeys().Count() > 0)
            {
                idleTimer.NotifyMe(true); //force reset if any key pressed
            }
            else
            {
                if (idleTimer.Notify)
                {
                    //this.GoToNextScene(new AttractModeScene());
                    Core.game.SetScene(new AttractModeScene());
                    return;
                }
            }

            if (puzzle.IsComplete)
            {
                //woo hoo, time to move on
                this.GoToNextScene(new PlayScene());
            }
        }
    }
}