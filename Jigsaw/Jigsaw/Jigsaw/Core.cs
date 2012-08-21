using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    public class Core
    {
        public static Game1 game;

        private Core() { } //nope

        public static Vector2 PointToVector(Point p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static Random rand = new Random();

        public static double TotalTime { get; private set; }
        public static GameTime CurrentGameTime { get; private set; }

        public static Background GlobalBackground { get; set; }

        public static void Update(GameTime gameTime)
        {
            TotalTime = gameTime.TotalGameTime.TotalSeconds;
            CurrentGameTime = gameTime;
        }

        private static Queue<string> recentChosen = new Queue<string>();

        public static string GetNextImage()
        {
            if (game.availablePuzzleImages.Count == 1)
            {
                //special case -- we can't avoid duplicates.
                return game.availablePuzzleImages[0];
            }

            List<string> temporaryList = new List<string>(game.availablePuzzleImages);

            foreach(var recent in recentChosen)
            {
                //remove last chosen from the temporary list
                temporaryList.Remove(recent);
            }

            int randIndex = (int) (Math.Floor(rand.NextDouble() * temporaryList.Count));
            string randomChosen = temporaryList[randIndex];

            int maxRecents = (int)(Math.Ceiling(game.availablePuzzleImages.Count / 20.0));
            if (recentChosen.Count >= maxRecents)
            {
                recentChosen.Dequeue();
            }
            recentChosen.Enqueue(randomChosen);

            return randomChosen;
        }
    }
}