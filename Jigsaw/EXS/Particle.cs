﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EXS
{
    public class Particle : GameObject
    {
        public bool IsAlive
        {
            get
            {
                return ((_createdTime + TTL) > Core.TotalTime);
            }
        }
        private double _totalLifeTime = 0;
        private double _TTL = 0;

        public double TTL { get { return _TTL; } set { _totalLifeTime = value; _TTL = value; } }

        private double _createdTime = 0;

        protected override void initializeAnimation()
        {
            base.initializeAnimation();

            _createdTime = Core.TotalTime;
        }

        public override bool ShouldDrawForParticles
        {
            get
            {
                //HACK HACK HACK HACK
                return true;
            }
        }

        public override void UpdateAnimation()
        {
            base.UpdateAnimation();
            if(IsAlive)
            {
                double aliveFraction = (_createdTime + TTL - Core.TotalTime) / _totalLifeTime;
                if (aliveFraction < 0.2)
                {
                    this.Alpha = (float) (aliveFraction / 0.2);
                }
            }
        }

        internal static Particle Create(Type particleType)
        {
            var constructor = particleType.GetConstructor(System.Type.EmptyTypes);
            Particle theParticle = (Particle)constructor.Invoke(null);
            return theParticle;

            //switch (particleType)
            //{
            //    case ParticleType.Sparkles:
            //        return new SparkleParticle();
            //    case ParticleType.Hearts:
            //        return new HeartParticle();
            //    default:
            //        return new SparkleParticle();
            //}
        }
    }
}