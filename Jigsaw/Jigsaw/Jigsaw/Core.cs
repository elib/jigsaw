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

        public static Random rand = new Random();

        public static double TotalTime { get; private set; }

        public static void Update(GameTime gameTime)
        {
            TotalTime = gameTime.TotalGameTime.TotalSeconds;
        }

        private static string lastChosen = null;
        public static string GetNextImage()
        {
            if (game.availablePuzzleImages.Count == 1)
            {
                //special case -- we can't avoid duplicates.
                return game.availablePuzzleImages[0];
            }

            List<string> temporaryList = new List<string>(game.availablePuzzleImages);

            if (lastChosen != null)
            {
                //remove last chosen from the temporary list
                temporaryList.Remove(lastChosen);
            }

            int randIndex = (int) (Math.Floor(rand.NextDouble() * temporaryList.Count));
            lastChosen = temporaryList[randIndex];
            return lastChosen;
        }
    }
}