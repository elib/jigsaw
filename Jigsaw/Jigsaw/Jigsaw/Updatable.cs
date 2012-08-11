using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    public abstract class Updatable
    {
        public abstract void Update();
        public virtual void UpdateAnimation() { }
        public abstract void Draw(SpriteBatch batch, bool drawParticles);
    }
}