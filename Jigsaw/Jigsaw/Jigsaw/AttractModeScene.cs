using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jigsaw
{
    class AttractModeScene : Scene
    {
        //public Puzzle puzzle;
        public Player player1;
        public Player player2;
        //public Canvas canvas;

        private TimeNotifier refreshHarmonics = new TimeNotifier();

        private List<TrigItem> trigSumX1;
        private List<TrigItem> trigSumY1;

        private List<TrigItem> trigSumX2;
        private List<TrigItem> trigSumY2;

        public AttractModeScene()
            : base()
        {
            //canvas = new Canvas();
            //canvas.Initialize(Core.game.Content);
            //canvas.setSize(100, 100);
            //this.Add(canvas);

            player1 = new Player(null, Microsoft.Xna.Framework.PlayerIndex.One, ParticleType.Hearts);
            player1.Initialize(Core.game.Content);
            player1.StartEmitting();

            player2 = new Player(null, Microsoft.Xna.Framework.PlayerIndex.Two, ParticleType.Hearts);
            player2.Initialize(Core.game.Content);
            player2.StartEmitting();

            this.Add(player1);
            this.Add(player2);

            player1.StartEmitting();
            player2.StartEmitting();

            refreshTrigSums();
        }

        public override void InitScene()
        {
            base.InitScene();
            Core.GlobalBackground.IsAnimated = true;
        }

        private void refreshTrigSums()
        {
            float radius = Math.Min(Core.game.Width, Core.game.Height) / 3;
            double speed = Core.rand.NextDouble() * 3 + 1;

            trigSumX1 = MakeHarmonicSum(speed, radius, true);
            trigSumY1 = MakeHarmonicSum(speed, radius, false);

            trigSumX2 = MakeHarmonicSum(speed, radius, true);
            trigSumY2 = MakeHarmonicSum(speed, radius, false);

            refreshHarmonics.NotifyMe(5.0 * speed);
        }

        private List<TrigItem> MakeHarmonicSum(double baseSpeed, double baseRadius, bool startCos)
        {
            List<TrigItem> triglist = new List<TrigItem>();

            int length = 4;
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
                baseSpeed /= 2;
                item.Radius = baseRadius;
                baseRadius /= (Core.rand.NextDouble() * 3.0 + 2);
                if (Core.rand.NextDouble() > 0.2)
                {
                    //flip some of them
                    baseRadius = -baseRadius;
                }

                triglist.Add(item);
            }

            return triglist;
        }

        public override void Update()
        {
            base.Update();

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
            player1._position.X = (Core.game.Width / 2) - (player1.Size.X / 2) + evaluateTrigSum(trigSumX1, Core.TotalTime);
            player1._position.Y = (Core.game.Height / 2) - (player1.Size.Y / 2) + evaluateTrigSum(trigSumY1, Core.TotalTime);

            player2._position.X = (Core.game.Width / 2) - (player2.Size.X / 2) + evaluateTrigSum(trigSumX2, -Core.TotalTime);
            player2._position.Y = (Core.game.Height / 2) - (player2.Size.Y / 2) + evaluateTrigSum(trigSumY2, -Core.TotalTime);
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