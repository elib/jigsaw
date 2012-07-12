using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    class Puzzle : GameObjectGroup
    {
        public Puzzle(Game1 game) : base(game) { }

        private Random _rnd = new Random();
        private Canvas _canvas;

        public void Create(string imageSource, int roughSize, Canvas canvas)
        {
            //load info
            Texture2D texture = _game.dynamicContentManager.Load<Texture2D>(imageSource);
            if (roughSize > texture.Width || roughSize > texture.Height)
            {
                throw new Exception("Rough size requested is too large for this image.");
            }

            _canvas = canvas;
            _canvas.setSize(texture.Width, texture.Height);

            chopUp(roughSize, texture);

            //SHUFFLE ME
            this.shuffle();

            int i = 0;
            int count = this.Count;
            int target = count / 2;
            if (count % 2 != 0)
            {
                if (_rnd.NextDouble() > 0.5)
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

            int left = margin;
            if(!leftSide)
            {
                left = _game.Width - margin - ((int)gobj._size.X);
            }

            gobj._position.X = left;

            int top = margin + (int)(_rnd.NextDouble() * (_game.Height - margin * 2 - (int)gobj._size.Y));
            gobj._position.Y = top;
        }

        private void chopUp(int roughSize, Texture2D texture)
        {
            int timesX = (int)Math.Ceiling((double)(texture.Width / ((double)roughSize))); //rounded up!
            int timesY = (int)Math.Ceiling((double)(texture.Height / ((double)roughSize))); //rounded up!

            int actualX = texture.Width / timesX;
            int actualY = texture.Height / timesY;


            //now slice it up
            for (int x = 0; x < timesX; x++)
            {
                for (int y = 0; y < timesY; y++)
                {
                    Rectangle pieceRect = new Rectangle(x * actualX, y * actualY, actualX, actualY);
                    PuzzlePiece p = new PuzzlePiece(texture, pieceRect, this);
                    this.Add(p);
                }
            }
        }

    }
}