using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    class ParticleEmitter : GameObjectGroup
    {
        public int MaxParticles { get; set; }
        public float SpawnRate { get; set; }

        private TimeNotifier _spawnNextParticleTimer = new TimeNotifier();

        public Vector2 _position = Vector2.Zero;

        public override void UpdateAnimation(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.UpdateAnimation(gameTime);

            //emit stuff here I guess
        }
    }
}