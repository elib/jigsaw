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
        Vector2 _destinationOffset;

        private const int MARGIN_FACTOR = 3;

        protected Rectangle? GetTextureCoords()
        {
            return _coords;
        }

        protected Rectangle OffsetDestinationRect
        {
            get
            {
                Rectangle rect = DestinationRect;
                rect.Offset(new Point((int) -_destinationOffset.X, (int) - _destinationOffset.Y));
                rect.Inflate(-rect.Width / MARGIN_FACTOR, -rect.Height / MARGIN_FACTOR);
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
            Rectangle insetCoords = _coords;
            insetCoords.Inflate(-_coords.Width / MARGIN_FACTOR, -_coords.Height / MARGIN_FACTOR);

            //if this piece is in a good spot, snap it properly and gogogogo
            if (insetCoords.Intersects(OffsetDestinationRect))
            {
                _position.X = _destinationOffset.X + _coords.X;
                _position.Y = _destinationOffset.Y + _coords.Y;

                _puzzle.PiecePlaced(this);
                return true;
            }

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Draw(_texture, DestinationRect, GetTextureCoords(), Color.White);
        }
    }
}