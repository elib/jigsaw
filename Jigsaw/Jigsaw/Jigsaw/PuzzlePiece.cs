using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    class PuzzlePiece : ScalableGameObject
    {
        Rectangle _coords;
        Puzzle _puzzle;

        protected override Rectangle? GetTextureCoords()
        {
            return _coords;
        }

        public PuzzlePiece(Texture2D texture, Rectangle pieceCoordinates, Puzzle parentPuzzle) : base(texture)
        {
            _coords = pieceCoordinates;
            _puzzle = parentPuzzle;

            _size.X = _coords.Width;
            _size.Y = _coords.Height;
        }
    }
}