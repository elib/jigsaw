using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    public class EmittingGameObject<T> : GameObject
        where T : Particle, new()
    {
        private ParticleEmitter<T> _emitter = new ParticleEmitter<T>();

        public EmittingGameObject(Texture2D texture, int maxParticles, double ttl, float spawnRate) : base(texture)
        {
            _emitter.MaxParticles = maxParticles;
            _emitter.TTL = ttl;
            _emitter.SpawnRate = spawnRate;
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