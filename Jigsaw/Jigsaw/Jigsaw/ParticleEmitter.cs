using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    public class ParticleEmitter : GameObjectGroup
    {
        public int MaxParticles { get; set; }

        private ParticleType _particleType;
        private float _spawnRate = 1;

        public ParticleEmitter(ParticleType particleType)
        {
            _particleType = particleType;
        }

        /// <summary>
        /// Particles per second
        /// </summary>
        public float SpawnRate
        {
            get
            {
                return _spawnRate;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Spawn Rate must not be negative or zero.");
                }
                _spawnRate = value;
            }
        }
        public double TTL { get; set; }

        private TimeNotifier _spawnNextParticleTimer = new TimeNotifier();

        public Vector2 _position = Vector2.Zero;
        private bool _isEmitting;
        public Vector2 _velocity = Vector2.Zero;

        public void StartEmitting()
        {
            _spawnNextParticleTimer.NotifyMe(1 / SpawnRate, true);
            _isEmitting = true;
        }

        public void StopEmitting()
        {
            _isEmitting = false;
        }

        public override void UpdateAnimation()
        {
            base.UpdateAnimation();

            if (Count > 0)
            {
                for (int j = Count - 1; j >= 0; j--)
                {
                    if (!(this[j] as Particle).IsAlive)
                    {
                        this.RemoveAt(j);
                    }
                }
            }

            if (_isEmitting)
            {
                if (_spawnNextParticleTimer.Notify)
                {
                    if (_velocity.LengthSquared() > 0)
                    {
                        _velocity.Normalize();
                    }

                    if (Count < MaxParticles)
                    {
                        Particle p = Particle.Create(_particleType);

                        p.Initialize(Core.game.Content);
                        double ang = Core.rand.NextDouble() * 2 * Math.PI;
                        Vector2 randDir = new Vector2((float)Math.Sin(ang), (float)Math.Cos(ang));
                        randDir -= _velocity;
                        p._position = randDir * 30 + _position;
                        p._velocity = randDir * 100;
                        p.TTL = this.TTL;
                        this.Add(p);
                    }
                    _spawnNextParticleTimer.NotifyMe(1 / SpawnRate, true);
                }
            }
        }
    }
}