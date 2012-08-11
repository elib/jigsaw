using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jigsaw
{
    public class Particle : GameObject
    {
        public bool IsAlive
        {
            get
            {
                return _createdTime + TTL > Core.TotalTime;
            }
        }

        public double TTL { get; set; }

        private double _createdTime = 0;

        protected override void initializeAnimation()
        {
            base.initializeAnimation();

            _createdTime = Core.TotalTime;
        }
    }
}
