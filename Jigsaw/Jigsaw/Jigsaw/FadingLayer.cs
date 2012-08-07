using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Jigsaw
{
    public class FadingLayer : GameObject
    {
        private TimeNotifier _fadeTimer = new TimeNotifier();
        private bool _isFadingIn;

        private double _fadeTime;

        public bool HasStarted
        {
            get;
            private set;
        }

        public bool HasCompleted
        {
            get;
            private set;
        }

        public FadingLayer(bool fadingIn, double fadeTime)
            : base()
        {
            HasCompleted = false;

            _fadeTime = fadeTime;

            _size.X = Core.game.Width;
            _size.Y = Core.game.Height;

            _position.X = _position.Y = 0;
            _isFadingIn = fadingIn;

            if (fadingIn)
            {
                Alpha = 1;
            }
            else
            {
                Alpha = 0;
            }
        }

        public override Texture2D SetTexture(ContentManager content)
        {
            return content.Load<Texture2D>("white_pixel");
        }

        public void Start()
        {
            if (_fadeTime > 0)
            {
                _fadeTimer.NotifyMe(_fadeTime, true);
            }

            HasStarted = true;
            HasCompleted = false;
        }

        public override void UpdateAnimation(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.UpdateAnimation(gameTime);

            if (HasCompleted)
            {
                return;
            }

            double newAlpha = _fadeTimer.TimerFraction; //calculate curve here
            if(_fadeTimer.Notify)
            {
                HasCompleted = true;
            }

            if (_isFadingIn)
            {
                //reverse if necessary
                newAlpha = 1 - newAlpha;
            }

            this.Alpha = (float)newAlpha;
        }
    }
}