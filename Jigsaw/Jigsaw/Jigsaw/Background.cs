using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    public class Background : GameObjectGroup
    {
        public Background()
            : base()
        {
            makeTiledBackground();
        }

        private void makeTiledBackground()
        {
            Texture2D tiles = Core.game.Content.Load<Texture2D>("tiles");

            int numWid = (int)Math.Ceiling(Core.game.Width / ((float)tiles.Width));
            int numHei = (int)Math.Ceiling(Core.game.Height / ((float)tiles.Height));

            int widExcess = ((numWid * tiles.Width) - Core.game.Width) / 2;
            int heiExcess = ((numHei * tiles.Height) - Core.game.Height) / 2;

            for (int x = 0; x < numWid; x++)
            {
                for (int y = 0; y < numHei; y++)
                {
                    Tile tile = new Tile();
                    tile.Initialize(Core.game.Content);
                    tile._position = new Vector2(x * tile.Size.X - widExcess, y * tile.Size.Y - heiExcess);
                    this.Add(tile);
                }
            }
        }
    }
}