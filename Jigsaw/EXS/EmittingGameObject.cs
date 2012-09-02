using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace EXS
{
    public class EmittingGameObject : GameObject
    {
        private ParticleEmitter _emitter;

        private TimeNotifier _pulseTimer = new TimeNotifier();

        private void setParams(int maxParticles, double ttl, float spawnRate, Type particleType)
        {
            _emitter = new ParticleEmitter(particleType);
            _emitter.MaxParticles = maxParticles;
            _emitter.TTL = ttl;
            _emitter.SpawnRate = spawnRate;
        }

        public EmittingGameObject(int maxParticles, double ttl, float spawnRate, Type particleType)
            : base()
        {
            this.setParams(maxParticles, ttl, spawnRate, particleType);
        }

        public EmittingGameObject(Texture2D texture, int maxParticles, double ttl, float spawnRate, Type particleType)
            : base(texture)
        {
            this.setParams(maxParticles, ttl, spawnRate, particleType);
        }

        public void PulseEmitting(double time)
        {
            _emitter.StartEmitting();
            _pulseTimer.NotifyMe(time, true);
        }

        public void StartEmitting()
        {
            _emitter.StartEmitting();
        }

        public void StopEmitting()
        {
            _emitter.StopEmitting();
        }

        public override void UpdateAnimation()
        {
            base.UpdateAnimation();

            _emitter._position = _position + (Size / 2);
            _emitter._velocity = _velocity;

            if (_pulseTimer.Notify)
            {
                StopEmitting();
            }
            
            //I KNOW IT SUCKS
            _emitter.Update();

            _emitter.UpdateAnimation();
        }

        public override void Draw(SpriteBatch batch, bool drawParticles)
        {
            if (drawParticles)
            {
                _emitter.Draw(batch, drawParticles);
            }
            else
            {
                base.Draw(batch, drawParticles);
            }
        }
    }
}