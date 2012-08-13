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

        private int _numWid, _numHei;

        private bool _isAnimated = false;

        public bool IsAnimated
        {
            get { return _isAnimated; }
            set
            {
                if (_isAnimated != value)
                {
                    if (!value)
                    {
                        resetAnimation();
                    }
                }

                _isAnimated = value;
            }
        }

        private void resetAnimation()
        {
            foreach(var item in this)
            {
                ((GameObject)item).Alpha = 1;
            }
        }

        private void makeTiledBackground()
        {
            Texture2D tiles = Core.game.Content.Load<Texture2D>("tiles");

            _numWid = (int)Math.Ceiling(Core.game.Width / ((float)tiles.Width));
            _numHei = (int)Math.Ceiling(Core.game.Height / ((float)tiles.Height));

            int widExcess = ((_numWid * tiles.Width) - Core.game.Width) / 2;
            int heiExcess = ((_numHei * tiles.Height) - Core.game.Height) / 2;

            for (int x = 0; x < _numWid; x++)
            {
                for (int y = 0; y < _numHei; y++)
                {
                    Tile tile = new Tile();
                    tile.Initialize(Core.game.Content);
                    tile._position = new Vector2(x * tile.Size.X - widExcess, y * tile.Size.Y - heiExcess);
                    this.Add(tile);
                }
            }
        }

        public override void UpdateAnimation()
        {
            base.UpdateAnimation();

            if (_isAnimated)
            {
                const double omega = 2 * Math.PI / 3.0;
                const double kX = 2 * Math.PI / 200;
                const double kY = 2 * Math.PI / 200;
                foreach (var item in this)
                {
                    GameObject obj = (GameObject)item;
                    obj.Alpha = (float)((
                        Math.Cos(- omega * Core.TotalTime) * 
                        Math.Sin(kX * obj._position.X) *
                        Math.Sin(kY * obj._position.Y) + 1.01) / 2.01);
                }
            }
        }
    }
}