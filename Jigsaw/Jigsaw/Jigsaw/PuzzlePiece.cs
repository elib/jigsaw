using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    class PuzzlePiece : GameObject
    {
        Rectangle _coords;
        Puzzle _puzzle;

        public PuzzlePiece(Texture2D texture, Rectangle pieceCoordinates, Puzzle parentPuzzle) : base(texture)
        {
            _coords = pieceCoordinates;
            _puzzle = parentPuzzle;

            _size.X = _coords.Width;
            _size.Y = _coords.Height;
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Draw(_texture, DestinationRect, _coords, Color.White);
        }
    }
}