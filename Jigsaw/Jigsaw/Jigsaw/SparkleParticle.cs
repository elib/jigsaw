using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    public class SparkleParticle : Particle
    {
        public SparkleParticle()
            : base()
        {
            _size.X = _size.Y = 5;

        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D SetTexture(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            return content.Load<Texture2D>("sparkles");
        }

        protected override void initializeAnimation()
        {
            base.initializeAnimation();

            this._animation.Add("sparkle", new int[] { 0, 1 }, 1);
            this.Play("sparkle");
        }
    }
}