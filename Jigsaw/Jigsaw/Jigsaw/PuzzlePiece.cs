using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    public class PuzzlePiece : EmittingGameObject
    {
        Rectangle _coords;
        Puzzle _puzzle;
        Vector2 _destinationOffset;

        private const int MARGIN_FACTOR = 3;
        public const float SCALE_FACTOR = 1 / 2.0f;

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

        public PuzzlePiece(Texture2D texture, Rectangle pieceCoordinates, Puzzle parentPuzzle, Vector2 destinationOffset) 
            : base(texture, 100, 2, 20, ParticleType.Hearts)
        {
            ScaleFactor = SCALE_FACTOR;

            _coords = pieceCoordinates;
            _puzzle = parentPuzzle;
            _destinationOffset = destinationOffset;

            _size.X = _coords.Width;
            _size.Y = _coords.Height;


        }

        public bool TrySnap()
        {
            Rectangle insetCoords = _coords;
            insetCoords.Width = (int)(_coords.Width * ScaleFactor);
            insetCoords.Height = (int)(_coords.Height * ScaleFactor);
            insetCoords.Offset(new Point(-_coords.X / 2, -_coords.Y / 2));
            insetCoords.Inflate(-insetCoords.Width / (MARGIN_FACTOR), -insetCoords.Height / (MARGIN_FACTOR));


            //if this piece is in a good spot, snap it properly and gogogogo
            if (insetCoords.Intersects(OffsetDestinationRect))
            {
                _position.X = _destinationOffset.X + _coords.X * ScaleFactor;
                _position.Y = _destinationOffset.Y + _coords.Y * ScaleFactor;

                this.PulseEmitting(2);

                return true;
            }

            return false;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch batch, bool drawParticles)
        {
            if (drawParticles)
            {
                base.Draw(batch, drawParticles);
            }
            else
            {
                batch.Draw(_texture, DestinationRect, GetTextureCoords(), Color.White);
            }
        }
    }
}