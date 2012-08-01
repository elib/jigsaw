using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jigsaw
{
    class AttractModeScene : Scene
    {
        //public Puzzle puzzle;
        public Player player;
        public Canvas canvas;

        private TimeNotifier refreshHarmonics = new TimeNotifier();

        private List<TrigItem> trigSumX;
        private List<TrigItem> trigSumY;

        public AttractModeScene()
            : base()
        {
            canvas = new Canvas();
            canvas.Initialize(Core.game.Content);
            this.add(canvas);

            player = new Player(null);
            player.Initialize(Core.game.Content);
            this.add(player);

            refreshTrigSums();
        }

        private void refreshTrigSums()
        {
            float radius = Math.Min(Core.game.Width, Core.game.Height) / 3;
            double speed = Core.rand.NextDouble() * 3 + 1;

            trigSumX = MakeHarmonicSum(speed, radius, true);
            trigSumY = MakeHarmonicSum(speed, radius, false);

            refreshHarmonics.NotifyMe(5.0 * speed);
        }

        private List<TrigItem> MakeHarmonicSum(double baseSpeed, double baseRadius, bool startCos)
        {
            List<TrigItem> triglist = new List<TrigItem>();

            int length = 3;//(int)(Core.rand.NextDouble() * 5 + 3);
            for (int i = 0; i < length; i++)
            {
                TrigItem item = new TrigItem();
                if (i == 0)
                {
                    if (startCos)
                    {
                        item.TheMethod = Math.Cos;
                    }
                    else
                    {
                        item.TheMethod = Math.Sin;
                    }
                }
                else
                {
                    //not first one
                    if (Core.rand.NextDouble() > 0.5)
                    {
                        item.TheMethod = Math.Sin;
                    }
                    else
                    {
                        item.TheMethod = Math.Cos;
                    }
                }

                item.Period = 1 / baseSpeed;
                baseSpeed /= ((int)(Core.rand.NextDouble() * 3.0 + 2));
                item.Radius = baseRadius;
                baseRadius /= (Core.rand.NextDouble() * 3.0 + 2);
                //baseRadius *= 0.25;
                if (Core.rand.NextDouble() > 0.2)
                {
                    //flip some of them
                    baseRadius = -baseRadius;
                }

                triglist.Add(item);
            }

            return triglist;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.justPressedKeys.Count > 0)
            {
                Core.game.SetScene(new PlayScene());
            }

            updateAttract();

            if (refreshHarmonics.Notify)
            {
                refreshTrigSums();
            }
        }

        private void updateAttract()
        {
            //attract mode
            float radius = Math.Min(Core.game.Width, Core.game.Height) / 3;
            double speed = 1 / 3.0;

            double baseRadians = 2 * speed * Math.PI * Core.TotalTime;

            //do silly animation
            player._position.X = (Core.game.Width / 2) - (player._size.X / 2) + evaluateTrigSum(trigSumX, Core.TotalTime);
            player._position.Y = (Core.game.Height / 2) - (player._size.Y / 2) + evaluateTrigSum(trigSumY, Core.TotalTime);
        }

        private float evaluateTrigSum(List<TrigItem> trigSum, double totalTime)
        {
            double result = 0;
            foreach (var item in trigSum)
            {
                result += item.Radius * item.TheMethod(2 * Math.PI * item.Period * totalTime);
            }
            return (float)result;
        }
    }

    enum TrigType
    {
        COS, SIN
    }


    struct TrigItem
    {
        public delegate double TrigMethod(double val);
        public TrigMethod TheMethod;
        
        public double Radius;
        public double Period;
    }
}