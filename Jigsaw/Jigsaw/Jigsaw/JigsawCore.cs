using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EXS;

namespace Jigsaw
{
    public class JigsawCore
    {
        private JigsawCore() { }

        public static Game1 specificGame;

        private static Queue<string> recentChosen = new Queue<string>();
        public static Background GlobalBackground { get; set; }
        public static string GetNextImage()
        {
            if (specificGame.availablePuzzleImages.Count == 1)
            {
                //special case -- we can't avoid duplicates.
                return specificGame.availablePuzzleImages[0];
            }

            List<string> temporaryList = new List<string>(specificGame.availablePuzzleImages);

            foreach(var recent in recentChosen)
            {
                //remove last chosen from the temporary list
                temporaryList.Remove(recent);
            }

            int randIndex = (int) (Math.Floor(Core.rand.NextDouble() * temporaryList.Count));
            string randomChosen = temporaryList[randIndex];

            int maxRecents = (int)(Math.Ceiling(specificGame.availablePuzzleImages.Count / 20.0));
            if (recentChosen.Count >= maxRecents)
            {
                recentChosen.Dequeue();
            }
            recentChosen.Enqueue(randomChosen);

            return randomChosen;
        }
    }
}
