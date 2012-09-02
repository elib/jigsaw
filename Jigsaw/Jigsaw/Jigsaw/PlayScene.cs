using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using EXS;

namespace Jigsaw
{
    class PlayScene : JigsawScene
    {
        public Puzzle puzzle;
        public Player player1;
        public Player player2;
        public Canvas canvas;

        public GameObjectGroup completedPieces;
        public GameObjectGroup carriedPieces;

        private TimeNotifier idleTimer = new TimeNotifier(15);

        private Dictionary<PlayerIndex, Vector2> _cheeringPositions = new Dictionary<PlayerIndex, Vector2>();
        private double cheerStartTime = 0;

        public override void InitScene()
        {
            base.InitScene();
            JigsawCore.GlobalBackground.IsAnimated = false;
        }

        public PlayScene() : base(2, 2, 1)
        {
            //get next puzzle image
            string puzzleImage = JigsawCore.GetNextImage();

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

            player1 = new Player(puzzle, PlayerIndex.One, typeof(SparkleParticle));
            player1.Initialize(Core.game.Content);
            
            player2 = new Player(puzzle, PlayerIndex.Two, typeof(SparkleParticle));
            player2.Initialize(Core.game.Content);

            this.Add(player2);
            this.Add(player1);

            idleTimer.NotifyMe();
        }

        public override void Update()
        {
            base.Update();

            if (IsTransitioning)
            {
                cheer();
                idleTimer.NotifyMe(true); //keep pushing idle timer ahead
                return;
            }

            if (InputManager.justPressedKeys.Count > 0 || Keyboard.GetState().GetPressedKeys().Count() > 0
                || !InputManager.IsIdle())
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

                player1.StartEmitting();
                player2.StartEmitting();

                _cheeringPositions[PlayerIndex.One] = player1._position;
                _cheeringPositions[PlayerIndex.Two] = player2._position;
                cheerStartTime = Core.TotalTime;
                
                this.GoToNextScene(new PlayScene());
            }

            if (InputManager.IsFunctionButtonPressed || InputManager.justPressedKeys.Contains(Keys.N))
            {
                Core.game.SetScene(new PlayScene());
            }
        }

        private void cheer()
        {
            player1.Play("cheer");
            player2.Play("cheer");
            const double omega = 1;
            const float amplitude = 50;
            if (cheerStartTime > 0)
            {
                player1._position.Y = _cheeringPositions[PlayerIndex.One].Y + (float)Math.Sin(2 * Math.PI * omega * (Core.TotalTime - cheerStartTime)) * amplitude;
                player2._position.Y = _cheeringPositions[PlayerIndex.Two].Y + (float)Math.Sin(- 2 * Math.PI * omega * (Core.TotalTime - cheerStartTime)) * amplitude;
            }
        }
    }
}