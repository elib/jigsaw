using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EXS
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

            HasStarted = true;

            if (_fadeTime > 0)
            {
                _fadeTimer.NotifyMe(_fadeTime, true);
                HasCompleted = false;
            }
            else
            {
                HasCompleted = true;
                Alpha = 0;
            }
        }

        public override void UpdateAnimation()
        {
            base.UpdateAnimation();

            if (HasCompleted)
            {
                return;
            }

            double newAlpha = easeCurve(_fadeTimer.TimerFraction); //calculate curve here
            //Console.WriteLine("In: {0}, out: {1}", _fadeTimer.TimerFraction, newAlpha);
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

        private static double easeCurve(double t)
        {
            if (t < 0) return 0;
            if (t > 1) return 1;

            t *= 2;
            if (t < 1) return t * t * t / 2;

            t -= 2;
            return (t * t * t + 2) / 2;
        }
    }
}