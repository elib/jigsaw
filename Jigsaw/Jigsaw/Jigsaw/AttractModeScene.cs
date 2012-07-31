using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jigsaw
{
    class AttractModeScene : Scene
    {
        //public Puzzle puzzle;
        public Player player;
        public Canvas canvas;

        public AttractModeScene()
            : base()
        {
            canvas = new Canvas();
            canvas.Initialize(Core.game.Content);
            this.add(canvas);

            player = new Player(null);
            player.Initialize(Core.game.Content);
            this.add(player);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.justPressedKeys.Count > 0)
            {
                Core.game.SetScene(new PlayScene());
            }
        }
    }
}