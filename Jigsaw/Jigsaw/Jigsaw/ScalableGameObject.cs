using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    class ScalableGameObject : GameObject
    {
        public ScalableGameObject() : base() {}
        public ScalableGameObject(Texture2D texture) : base(texture) { }


        protected virtual Rectangle? GetTextureCoords()
        {
            return null;
        }
        
        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Draw(_texture, DestinationRect, GetTextureCoords(), Color.White);
        }
    }
}