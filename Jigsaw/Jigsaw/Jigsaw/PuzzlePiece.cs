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
        Vector2 _destinationOffset;

        protected override Rectangle? GetTextureCoords()
        {
            return _coords;
        }

        protected Rectangle OffsetDestinationRect
        {
            get
            {
                Rectangle rect = DestinationRect;
                rect.Offset(new Point((int) -_destinationOffset.X, (int) - _destinationOffset.Y));
                return rect;
            }
        }

        public PuzzlePiece(Texture2D texture, Rectangle pieceCoordinates, Puzzle parentPuzzle, Vector2 destinationOffset) : base(texture)
        {
            _coords = pieceCoordinates;
            _puzzle = parentPuzzle;
            _destinationOffset = destinationOffset;

            _size.X = _coords.Width;
            _size.Y = _coords.Height;
        }

        public bool TrySnap()
        {
            //if this piece is in a good spot, snap it properly and gogogogo
            if (_coords.Intersects(OffsetDestinationRect))
            {
                _position.X = _destinationOffset.X + _coords.X;
                _position.Y = _destinationOffset.Y + _coords.Y;
                return true;
            }

            return false;
        }
    }
}