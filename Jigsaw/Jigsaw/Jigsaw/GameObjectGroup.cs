using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jigsaw
{
    //it's like a scene ... but has extra stuff to help us check locations and stuff?
    class GameObjectGroup : Updatable
    {

        private List<GameObject> _objects;
        protected Game1 _game;

        public GameObjectGroup(Game1 game)
        {
            _game = game;
            _objects = new List<GameObject>();
        }

        public void empty()
        {
            _objects.Clear();
        }

        public void add(GameObject newobj)
        {
            _objects.Add(newobj);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (var obj in _objects)
            {
                obj.Update(gameTime);
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
