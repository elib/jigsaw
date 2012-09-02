using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using EXS;

namespace Jigsaw
{
    public class Background : GameObjectGroup
    {
        public Background()
            : base()
        {
            makeTiledBackground();
        }

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

            int margin = 2;

            int tilesWidth = (tiles.Width + 2 * margin);
            int tilesHeight = (tiles.Height + 2 * margin);

            int numWid = (int)Math.Ceiling(Core.game.Width / (float)tilesWidth);
            int numHei = (int)Math.Ceiling(Core.game.Height / (float)tilesHeight);

            int widExcess = ((numWid * tilesWidth) - Core.game.Width) / 2;
            int heiExcess = ((numHei * tilesHeight) - Core.game.Height) / 2;

            for (int x = 0; x < numWid; x++)
            {
                for (int y = 0; y < numHei; y++)
                {
                    Tile tile = new Tile();
                    tile.Initialize(Core.game.Content);
                    tile._position = new Vector2(x * tilesWidth - widExcess, y * tilesHeight - heiExcess);
                    this.Add(tile);
                }
            }
        }

        public override void UpdateAnimation()
        {
            base.UpdateAnimation();

            if (_isAnimated)
            {
                const double omega = 2 * Math.PI / 2.0;
                const double omega_drift = 2 * Math.PI / 20;
                const double kX = 2 * Math.PI / 200;
                const double kY = 2 * Math.PI / 200;

                const double amplitude = 0.8;

                foreach (var item in this)
                {
                    GameObject obj = (GameObject)item;
                    obj.Alpha = (float)((1 - amplitude) + amplitude*(
                        Math.Cos(- omega * Core.TotalTime) * 
                        Math.Sin(kX * obj._position.X - omega_drift * Core.TotalTime) *
                        Math.Sin(kY * obj._position.Y - omega_drift * Core.TotalTime) + 1) / 2);
                }
            }
        }
    }
}