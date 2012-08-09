using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    public class Puzzle : GameObjectGroup
    {
        private const double FLICKER_BOUNCE_TIME = 0.1;
        private Canvas _canvas;
        private TimeNotifier _flickerBounceNotifier = new TimeNotifier(FLICKER_BOUNCE_TIME);

        private GameObjectGroup _completedPieces;
        private GameObjectGroup _attachedPieces;

        private int _numberOfPieces = 0;

        public Puzzle(GameObjectGroup completedPieces, GameObjectGroup attachedPieces) : base()
        {
            _completedPieces = completedPieces;
            _attachedPieces = attachedPieces;
        }

        public void Create(string imageSource, int roughSize, Canvas canvas)
        {
            //load info
            Texture2D texture = Core.game.dynamicContentManager.Load<Texture2D>(imageSource);
            //if (roughSize > texture.Width || roughSize > texture.Height)
            //{
            //    throw new Exception("Rough size requested is too large for this image.");
            //}

            _canvas = canvas;
            _canvas.setSize((int)(texture.Width * PuzzlePiece.SCALE_FACTOR), (int) (texture.Height * PuzzlePiece.SCALE_FACTOR));

            chopUp(roughSize, texture);

            distributePieces();

            //initialize with number of pieces -- to detect win condition
            _numberOfPieces = Count;

            _flickerBounceNotifier.NotifyMe();
        }

        private void distributePieces()
        {
            //SHUFFLE ME
            this.shuffle();

            int i = 0;
            int count = this.Count;
            int target = count / 2;
            if (count % 2 != 0)
            {
                if (Core.rand.NextDouble() > 0.5)
                {
                    target++;
                }
            }

            foreach (var obj in this)
            {
                bool leftSide = i++ < target;
                PutPiece(obj, leftSide);
            }
        }

        private void PutPiece(Updatable obj, bool leftSide)
        {
            const int margin = 10;

            GameObject gobj = (GameObject)obj;

            int leftSpace = 0;// (int) ((Core.game.Width - _canvas.Size.X - margin*4 - gobj.Size.X*2) / 2);

            int left = margin;
            if(!leftSide)
            {
                left = (int) (_canvas._position.X + _canvas.Size.X + margin);
            }

            gobj._position.X = (int)(left + leftSpace * Core.rand.NextDouble());

            int top = margin + (int)(Core.rand.NextDouble() * (Core.game.Height - margin * 2 - (int)gobj.Size.Y));
            gobj._position.Y = top;
        }

        private void chopUp(int roughSize, Texture2D texture)
        {
            int timesX = (int)Math.Ceiling((double)(texture.Width / ((double)roughSize))); //rounded up!
            int timesY = (int)Math.Ceiling((double)(texture.Height / ((double)roughSize))); //rounded up!

            int actualX = texture.Width / timesX;
            int actualY = texture.Height / timesY;

            if (actualX % 2 == 1) actualX--;
            if (actualY % 2 == 1) actualY--;

            //now slice it up
            for (int x = 0; x < timesX; x++)
            {
                for (int y = 0; y < timesY; y++)
                {
                    Rectangle pieceRect = new Rectangle(x * actualX, y * actualY, actualX, actualY);
                    PuzzlePiece p = new PuzzlePiece(texture, pieceRect, this, _canvas._position + _canvas.offSet);
                    this.Add(p);
                }
            }
        }

        public bool IsComplete
        {
            get
            {
                return (_completedPieces.Count == _numberOfPieces);
            }
        }


        internal void PiecePlaced(PuzzlePiece puzzlePiece)
        {
            _attachedPieces.Remove(puzzlePiece);
            _completedPieces.Add(puzzlePiece);
        }

        internal void PiecePicked(PuzzlePiece puzzlePiece)
        {
            this.Remove(puzzlePiece);
            _attachedPieces.Add(puzzlePiece);
        }

        internal void PieceReplaced(PuzzlePiece puzzlePiece)
        {
            _attachedPieces.Remove(puzzlePiece);
            this.Add(puzzlePiece);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_flickerBounceNotifier.Notify)
            {
                _flickerBounceNotifier.NotifyMe();

                if (Core.rand.NextDouble() < 0.08)
                {
                    int which = (int)(Core.rand.NextDouble() * Count);
                    if (which < Count)
                    {
                        ((GameObject)this[which]).RandomBounce();
                    }
                }
            }
        }
    }
}