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
            Player p = new Player();
            p.Initialize(_game.dynamicContentManager);
            this.add(p);
        }
    }
}