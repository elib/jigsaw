﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EXS
{
    public enum Facing
    {
        Right, Left
    }

    public class GameObject : Updatable
    {

        protected Texture2D _texture;
        private bool _initialized = false;
        public Vector2 _position;

        public Vector2? BoundingBoxDim = null;
        public Vector2? BoundingBoxOffset = null;

        public Facing CurrentFacing { get; set; }

        public float ScaleFactor
        {
            get;
            protected set;
        }

        protected Vector2 _size;
        public Vector2 Size
        {
            get { return _size * ScaleFactor; }

        }

        public Vector2 _velocity, _acceleration, _drag, _maxVelocity;

        protected AnimationInfo _animation;

        public const double MIN_BOUNCE_PERCENTAGE = 0.01;
        public const double MEDIUM_BOUNCE_PERCENTAGE = 0.05;
        public const double MAX_BOUNCE_PERCENTAGE = 0.1;

        private const double BOUNCE_TIME = 0.2;
        private TimeNotifier _bounceTimer = new TimeNotifier(BOUNCE_TIME);
        private double _bounceAmount = MIN_BOUNCE_PERCENTAGE;
        private Vector2 _bounceScale = Vector2.One;

        public float Alpha { get; set; }
        public byte AlphaByte
        {
            get
            {
                byte b = (byte)(255 * Alpha);
                return b;
            }
        }

        public GameObject()
        {
            _position = Vector2.Zero;
            _size = Vector2.Zero;
            _velocity = Vector2.Zero;
            _acceleration = Vector2.Zero;
            _drag = Vector2.Zero;
            _maxVelocity = Vector2.Zero;
            _texture = null;

            CurrentFacing = Facing.Right;

            ScaleFactor = 1;

            Alpha = 1;

            initializeAnimation();
        }

        protected virtual void initializeAnimation()
        {
            _animation = new AnimationInfo();
        }

        public Rectangle DestinationRect
        {
            get
            {
                return new Rectangle(
                                (int)(_position.X - (_size.X * ScaleFactor * (1.0 - _bounceScale.X) / 2)),
                                (int)(_position.Y - (_size.Y * ScaleFactor * (1.0 - _bounceScale.Y) / 2)),
                                (int)(_size.X * _bounceScale.X * ScaleFactor),
                                (int)(_size.Y * _bounceScale.Y * ScaleFactor));
            }
        }

        public GameObject(Texture2D initialTexture)
            : this()
        {
            _texture = initialTexture;
            _initialized = true;
        }

        public void Initialize(ContentManager content)
        {
            _texture = SetTexture(content);
            _initialized = true;
        }

        /// <summary>
        /// Override me.
        /// </summary>
        /// <param name="content"></param>
        public virtual Texture2D SetTexture(ContentManager content)
        {
            return content.Load<Texture2D>("blank");
        }

        /// <summary>
        /// Override only if necessary ....
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="gameTime"></param>
        public override void Draw(SpriteBatch batch, bool drawParticles)
        {
            if (drawParticles == ShouldDrawForParticles)
            {

                if (!_initialized)
                {
                    throw new Exception("Not yet initialized!");
                }
                //Rectangle r_dest = new Rectangle(10, 10, 64, 64);//_position.X, _position.Y, _size.X, _size.Y);

                Color tint = Color.White;
                tint.A = this.AlphaByte;
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (CurrentFacing == Facing.Left)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                batch.Draw(_texture, DestinationRect, AnimationFrame, tint, 0, Vector2.Zero, spriteEffects, 0);
            }
        }

        private Rectangle? AnimationFrame
        {
            get
            {
                //if animated ... get current animation
                if (_animation.CurrentAnimation == null)
                {
                    return null;
                }

                int number = _animation.CurrentAnimation.CurrentFrameNumber;
                Rectangle animatedSourceRect = new Rectangle((int)(number * _size.X), 0, (int)_size.X, (int)_size.Y);
                return animatedSourceRect;
            }
        }

        public void Play(string animationName, bool force = false)
        {
            _animation.SetCurrentAnimation(animationName, force);
        }

        /// <summary>
        /// Override me -- AND CALL ME AFTER
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update()
        {
            if (!_initialized)
            {
                throw new Exception("Not yet initialized!");
            }

            updateMotion();
        }

        public override void UpdateAnimation()
        {
            base.UpdateAnimation();

            if (_bounceTimer.StillGoing)
            {
                updateBounce();
            }
            else
            {
                _bounceScale = Vector2.One;
            }

            _animation.Update();
        }

        private void setBounce(double percentage)
        {
            _bounceAmount = percentage;
        }

        public void Bounce(double percentage)
        {
            Bounce(percentage, false);
        }

        public void Bounce(double percentage, bool force)
        {
            if (_bounceTimer.NotifyMe(force))
            {
                setBounce(percentage);
            }
        }

        public void RandomBounce()
        {
            double percentage = Core.rand.NextDouble() * (MAX_BOUNCE_PERCENTAGE - MIN_BOUNCE_PERCENTAGE) + MIN_BOUNCE_PERCENTAGE;
            Bounce(percentage);
        }

        private void updateBounce()
        {
            //get amount of bounce 0 to 1
            double timeValue = _bounceTimer.TimerFraction;
            if (timeValue <= 0 || timeValue >= 1.0)
            {
                _bounceScale = Vector2.One;
            }
            else
            {
                //y1 = 1 + amplitude1*(sin(pi * (x)) .^ 2);
                double xBounce = 1.0 + _bounceAmount * Math.Sin(2 * Math.PI * timeValue);
                double yBounce = 1.0 + _bounceAmount * Math.Sin(2 * Math.PI * (timeValue + 0.5));
                _bounceScale.X = (float)xBounce;
                _bounceScale.Y = (float)yBounce;
            }
        }


        /**
         * STOLEN FROM FLIXEL!!
         * 
          * Internal function for updating the position and speed of this object.
          * Useful for cases when you need to update this but are buried down in too many supers.
          * Does a slightly fancier-than-normal integration to help with higher fidelity framerate-independenct motion.
          */
        protected void updateMotion()
        {
            GameTime gameTime = Core.CurrentGameTime;
            float delta, velocityDelta;

            ////wait with angular ...
            //velocityDelta = (computeVelocity(gameTime, angularVelocity, angularAcceleration, angularDrag, maxAngular) - angularVelocity)/2;
            //angularVelocity += velocityDelta; 
            //angle += angularVelocity*FlxG.elapsed;
            //angularVelocity += velocityDelta;

            velocityDelta = (computeVelocity(gameTime, _velocity.X, _acceleration.X, _drag.X, _maxVelocity.X) - _velocity.X) / 2;
            _velocity.X += velocityDelta;
            delta = (float)(_velocity.X * gameTime.ElapsedGameTime.TotalSeconds);
            _velocity.X += velocityDelta;
            _position.X += delta;

            velocityDelta = (computeVelocity(gameTime, _velocity.Y, _acceleration.Y, _drag.Y, _maxVelocity.Y) - _velocity.Y) / 2;
            _velocity.Y += velocityDelta;
            delta = (float)(_velocity.Y * gameTime.ElapsedGameTime.TotalSeconds);
            _velocity.Y += velocityDelta;
            _position.Y += delta;
        }

        static public float computeVelocity(GameTime gameTime, float Velocity)
        {
            return computeVelocity(gameTime, Velocity, 0, 0, 10000);
        }

        static public float computeVelocity(GameTime gameTime, float Velocity, float Acceleration)
        {
            return computeVelocity(gameTime, Velocity, Acceleration, 0, 10000);
        }

        static public float computeVelocity(GameTime gameTime, float Velocity, float Acceleration, float Drag)
        {
            return computeVelocity(gameTime, Velocity, Acceleration, Drag, 10000);
        }

        static public float computeVelocity(GameTime gameTime, float Velocity, float Acceleration, float Drag, float Max)
        {
            if (Acceleration != 0)
                Velocity += (float)(Acceleration * gameTime.ElapsedGameTime.TotalSeconds);
            else if (Drag != 0)
            {
                float newDrag = (float)(Drag * gameTime.ElapsedGameTime.TotalSeconds);
                if (Velocity - newDrag > 0)
                    Velocity = Velocity - newDrag;
                else if (Velocity + newDrag < 0)
                    Velocity += newDrag;
                else
                    Velocity = 0;
            }
            if ((Velocity != 0) && (Max != 10000))
            {
                if (Velocity > Max)
                    Velocity = Max;
                else if (Velocity < -Max)
                    Velocity = -Max;
            }
            return Velocity;
        }
    }
}