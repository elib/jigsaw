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

        public override void InitScene()
        {
            base.InitScene();
            Core.GlobalBackground.IsAnimated = false;
        }

        public PlayScene() : base(3, 3, 3)
        {
            //get next puzzle image
            string puzzleImage = Core.GetNextImage();

            canvas = new Canvas();
            canvas.Initialize(Core.game.Content);
            this.Add(canvas);

            completedPieces = new GameObjectGroup();
            carriedPieces = new GameObjectGroup();
            puzzle = new Puzzle(completedPieces, carriedPieces);
            puzzle.Create(puzzleImage, 250, canvas);

            
            this.Add(completedPieces);
            this.Add(puzzle);
            this.Add(carriedPieces);

            player1 = new Player(puzzle, PlayerIndex.One, ParticleType.Sparkles);
            player1.Initialize(Core.game.Content);
            this.Add(player1);

            player2 = new Player(puzzle, PlayerIndex.Two, ParticleType.Sparkles);
            player2.Initialize(Core.game.Content);
        //    this.add(player2);

            idleTimer.NotifyMe();
        }

        public override void Update()
        {
            base.Update();

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