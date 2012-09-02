using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EXS;

namespace Jigsaw
{
    class JigsawScene : Scene
    {
        public JigsawScene(double fadeInTime = 0, double fadeOutTime = 0, double hangTime = 0)
            : base(fadeInTime, fadeOutTime, hangTime)
        {
            
        }

        public override void UpdateAnimation()
        {
            JigsawCore.GlobalBackground.UpdateAnimation();
            base.UpdateAnimation();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch, bool drawParticles)
        {
            JigsawCore.GlobalBackground.Draw(batch, false);

            base.Draw(batch, drawParticles);
        }
    }
}