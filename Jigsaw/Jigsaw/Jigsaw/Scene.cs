using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    public class Scene : Updatable
    {

        private List<Updatable> _objects;

        private bool _inputEnabled = true;

        public Scene()
        {
            _objects = new List<Updatable>();
        }

        public void empty()
        {
            _objects.Clear();
        }

        public void add(Updatable newobj)
        {
            _objects.Add(newobj);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_inputEnabled)
            {
                foreach (var obj in _objects)
                {
                    obj.Update(gameTime);
                }
            }

            UpdateAnimation(gameTime);
        }

        public override void UpdateAnimation(GameTime gameTime)
        {
            base.UpdateAnimation(gameTime);

            foreach (var obj in _objects)
            {
                obj.UpdateAnimation(gameTime);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (var obj in _objects)
            {
                obj.Draw(batch, gameTime);
            }
        }
    }
}