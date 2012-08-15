using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    public class HeartParticle : Particle
    {

         public HeartParticle()
            : base()
        {
            _size.X = _size.Y = 9;
            _maxVelocity = Vector2.One * 300;
            _drag = Vector2.One * 45;

        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D SetTexture(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            return content.Load<Texture2D>("heart");
        }

        protected override void initializeAnimation()
        {
            base.initializeAnimation();

            this._animation.Add("grow", new int[] { 0, 1, 2, 3 }, 15);
            this._animation.Add("undulate", new int[] { 1, 2, 3, 2 }, 15);
            this.Play("undulate");
            this._animation.RandomFrame();
        }
    }
}
