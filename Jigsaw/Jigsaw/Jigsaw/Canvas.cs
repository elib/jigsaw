using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    class Canvas : ScalableGameObject
    {
        public override Texture2D SetTexture(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            return content.Load<Texture2D>("canvas");
        }

        internal void setSize(int width, int height)
        {
            _size.X = width;
            _size.Y = height;

            //center in field
            _position.X = (float) Math.Floor((double)((Core.game.Width - width) / 2));
            _position.Y = (float) Math.Floor((double)((Core.game.Height - height) / 2));


        }
    }
}