using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    class SparkleParticle : Particle
    {

        public override Microsoft.Xna.Framework.Graphics.Texture2D SetTexture(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            return content.Load<Texture2D>("sparkle");
        }

        protected override void initializeAnimation()
        {
            base.initializeAnimation();

            this._animation.Add("sparkle", new int[] { 0, 1 }, 0.5f);
        }
    }
}
