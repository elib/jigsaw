using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    public class Scene : Updatable
    {

        private List<Updatable> _objects;

        private bool _inputEnabled = true;

        private FadingLayer fadeInLayer;
        private FadingLayer fadeOutLayer;

        public double HangTime { get; set; }

        public Scene(double fadeInTime = 0, double fadeOutTime = 0, double hangTime = 0)
        {
            _objects = new List<Updatable>();
            HangTime = hangTime;

            fadeInLayer = new FadingLayer(true, fadeInTime);
            fadeInLayer.Initialize(Core.game.Content);
            fadeInLayer.Start();

            fadeOutLayer = new FadingLayer(false, fadeOutTime);
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
            if (!fadeInLayer.HasCompleted || (fadeOutLayer.HasStarted && !fadeOutLayer.HasCompleted))
            {
                _inputEnabled = false;
            }
            else
            {
                _inputEnabled = true;
            }

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

            fadeInLayer.UpdateAnimation(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (var obj in _objects)
            {
                obj.Draw(batch, gameTime);
            }

            fadeInLayer.Draw(batch, gameTime);
        }
    }
}