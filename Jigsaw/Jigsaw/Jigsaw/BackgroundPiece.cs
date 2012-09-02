using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using EXS;

namespace Jigsaw
{
    class BackgroundPiece : GameObject
    {
        public BackgroundPiece(Vector2 dimensions, Vector2 offsetVector)
            : base()
        {
            _size = dimensions;
            _position = offsetVector;
        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D SetTexture(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            return content.Load<Texture2D>("BackgroundPiece");
        }
    }
}