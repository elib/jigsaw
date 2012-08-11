using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    public class SparkleParticle : Particle
    {
        public SparkleParticle()
            : base()
        {
            _size.X = _size.Y = 5;
            _maxVelocity = Vector2.One * 300;
            _drag = Vector2.One * 45;

        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D SetTexture(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            return content.Load<Texture2D>("sparkles");
        }

        protected override void initializeAnimation()
        {
            base.initializeAnimation();

            this._animation.Add("sparkle", new int[] { 0, 1 }, 15);
            this.Play("sparkle");
            this._animation.RandomFrame();
        }
    }
}