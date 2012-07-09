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

        public void Create(string imageSource, int roughSize)
        {
            //load info
            Texture2D texture = _game.dynamicContentManager.Load<Texture2D>(imageSource);
            if (roughSize > texture.Width || roughSize > texture.Height)
            {
                throw new Exception("Rough size requested is too large for this image.");
            }

            int timesX = (int) Math.Ceiling((double) (texture.Width / ((double)roughSize))); //rounded up!
            int timesY = (int) Math.Ceiling((double) (texture.Height / ((double)roughSize))); //rounded up!

            int actualX = texture.Width / timesX;
            int actualY = texture.Height / timesY;


            //now slice it up
            for (int x = 0; x < timesX; x++)
            {
                for (int y = 0; y < timesY; y++)
                {
                    Rectangle pieceRect = new Rectangle(x*actualX, y*actualY, actualX, actualY);
                    PuzzlePiece p = new PuzzlePiece(texture, pieceRect, this);
                    this.add(p);
                }
            }
        }

    }
}