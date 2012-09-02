using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using EXS;

namespace Jigsaw
{
    public class Tile : GameObject
    {
        public Tile()
            : base()
        {
            _size = Vector2.One * 32;
        }

        public override Texture2D SetTexture(ContentManager content)
        {
            return content.Load<Texture2D>("tiles");
        }

        public override void Draw(SpriteBatch batch, bool drawParticles)
        {
            base.Draw(batch, drawParticles);
        }
    }
}