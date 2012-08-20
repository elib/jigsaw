using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    public class Canvas : GameObject
    {
        public override Texture2D SetTexture(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            return content.Load<Texture2D>("canvas_frame");
        }

        //width of the surrounding border
        private const float _percentage = 9.0f / 64.0f;

        internal void setSize(int width, int height)
        {
            _size.X = width * (1.0f + _percentage * 2);
            _size.Y = height * (1.0f + _percentage * 2);

            offSet = new Vector2(_percentage * width, _percentage * height);

            //center in field
            _position.X = (float)Math.Floor((double)((Core.game.Width - _size.X) / 2));
            _position.Y = (float)Math.Floor((double)((Core.game.Height - _size.Y) / 2));
        }

        public Vector2 offSet
        {
            get; private set;
        }

        public Vector2 TotalOffset
        {
            get
            {
                return _position + offSet;
            }
        }
    }
}