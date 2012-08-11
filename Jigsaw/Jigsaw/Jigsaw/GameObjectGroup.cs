using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    //it's like a scene ... but has extra stuff to help us check locations and stuff?
    public class GameObjectGroup : Updatable, IEnumerable<Updatable>, ICollection<Updatable>
    {

        private List<Updatable> _objects;

        public GameObjectGroup()
        {
            _objects = new List<Updatable>();
        }

        public void shuffle()
        {
            _objects.Shuffle();
        }

        public override void Update()
        {
            foreach (var obj in _objects)
            {
                obj.Update();
            }
        }

        public override void UpdateAnimation()
        {
            base.UpdateAnimation();

            foreach (var obj in _objects)
            {
                obj.UpdateAnimation();
            }
        }

        public override void Draw(SpriteBatch batch, bool drawParticles)
        {
            foreach (var obj in _objects)
            {
                obj.Draw(batch, drawParticles);
            }
        }

        public GameObject GetFirstOverlappingMember(GameObject source)
        {
            Rectangle srcRect = source.DestinationRect;

            GameObject returnable = null;

            foreach (var item in this)
            {
                if (item is GameObjectGroup)
                {
                    var ret = ((GameObjectGroup)item).GetFirstOverlappingMember(source);
                    if (ret != null)
                    {
                        returnable = ret;
                    }
                }
                else if (item is GameObject)
                {
                    var itemGo = (GameObject)item;
                    if (itemGo.DestinationRect.Intersects(srcRect))
                    {
                        returnable = itemGo;
                    }
                }
            }

            return returnable;
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
            get { return false; }
        }

        public bool Remove(Updatable item)
        {
            return this._objects.Remove(item);
        }


        public int IndexOf(Updatable item)
        {
            return _objects.IndexOf(item);
        }

        public void Insert(int index, Updatable item)
        {
            _objects.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _objects.RemoveAt(index);
        }

        public Updatable this[int index]
        {
            get
            {
                return _objects[index];
            }
        }
    }
}
