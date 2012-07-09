using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jigsaw
{
    //it's like a scene ... but has extra stuff to help us check locations and stuff?
    class GameObjectGroup : Updatable, IEnumerable<Updatable>, ICollection<Updatable>
    {

        private List<Updatable> _objects;
        protected Game1 _game;

        public GameObjectGroup(Game1 game)
        {
            _game = game;
            _objects = new List<Updatable>();
        }

        public void shuffle()
        {
            _objects.Shuffle();
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

        public IEnumerator<Updatable> GetEnumerator()
        {
            return this._objects.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._objects.GetEnumerator();
        }



        public void Add(Updatable item)
        {
            this._objects.Add(item);
        }

        public void Clear()
        {
            this._objects.Clear();
        }

        public bool Contains(Updatable item)
        {
            return _objects.Contains(item);
        }

        public void CopyTo(Updatable[] array, int arrayIndex)
        {
            _objects.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _objects.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(Updatable item)
        {
            throw new NotImplementedException();
        }
    }
}
