using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    class BackgroundScene : EXS.Scene
    {
        public override void UpdateAnimation()
        {
            base.UpdateAnimation();

            JigsawCore.GlobalBackground.UpdateAnimation();
        }


        public override void Draw(SpriteBatch batch, bool drawParticles)
        {
            base.Draw(batch, drawParticles);
            JigsawCore.GlobalBackground.Draw(batch, false);
        }
    }
}
