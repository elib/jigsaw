using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EXS
{
    public class Core
    {
        public static ExsGame game;

        private Core() { } //nope

        public static Vector2 PointToVector(Point p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static Random rand = new Random();

        public static double TotalTime { get; private set; }
        public static GameTime CurrentGameTime { get; private set; }

        public static void Update(GameTime gameTime)
        {
            TotalTime = gameTime.TotalGameTime.TotalSeconds;
            CurrentGameTime = gameTime;
        }
    }
}