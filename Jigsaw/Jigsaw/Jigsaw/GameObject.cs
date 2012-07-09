using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Jigsaw
{
    public class GameObject : Updatable
    {

        protected Texture2D _texture;
        private bool _initialized = false;
        protected Vector2 _position;

        protected Vector2 _size;

        public Vector2 _velocity, _acceleration, _drag, _maxVelocity;

        public GameObject()
        {
            _position = Vector2.Zero;
            _size = Vector2.Zero;
            _velocity = Vector2.Zero;
            _acceleration = Vector2.Zero;
            _drag = Vector2.Zero;
            _maxVelocity = Vector2.Zero;
            _texture = null;
        }

        public Rectangle DestinationRect
        {
            get
            {
                return new Rectangle((int) _position.X, (int) _position.Y, (int)_size.X, (int)_size.Y);
            }
        }

        public GameObject(Texture2D initialTexture) : this()
        {
            _texture = initialTexture;
            _initialized = true;
        }

        public void Initialize(ContentManager content)
        {
            SetTexture(content);
            _initialized = true;
        }

        /// <summary>
        /// Override me.
        /// </summary>
        /// <param name="content"></param>
        public virtual void SetTexture(ContentManager content)
        {
            _texture = content.Load<Texture2D>("blank");
        }

        /// <summary>
        /// Override only if necessary ....
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="gameTime"></param>
        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (!_initialized)
            {
                throw new Exception("Not yet initialized!");
            }
            //Rectangle r_dest = new Rectangle(10, 10, 64, 64);//_position.X, _position.Y, _size.X, _size.Y);

            batch.Draw(_texture, _position, Color.White);
        }

        /// <summary>
        /// Override me -- AND CALL ME AFTER
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (!_initialized)
            {
                throw new Exception("Not yet initialized!");
            }

            updateMotion(gameTime);
        }


       /**
        * STOLEN FROM FLIXEL!!
        * 
		 * Internal function for updating the position and speed of this object.
		 * Useful for cases when you need to update this but are buried down in too many supers.
		 * Does a slightly fancier-than-normal integration to help with higher fidelity framerate-independenct motion.
		 */
		protected void updateMotion(GameTime gameTime)
		{
			float delta, velocityDelta;

            ////wait with angular ...
            //velocityDelta = (computeVelocity(gameTime, angularVelocity, angularAcceleration, angularDrag, maxAngular) - angularVelocity)/2;
            //angularVelocity += velocityDelta; 
            //angle += angularVelocity*FlxG.elapsed;
            //angularVelocity += velocityDelta;

            velocityDelta = (computeVelocity(gameTime, _velocity.X, _acceleration.X, _drag.X, _maxVelocity.X) - _velocity.X) / 2;
			_velocity.X += velocityDelta;
			delta = (float)(_velocity.X*gameTime.ElapsedGameTime.TotalSeconds);
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
			if(Acceleration != 0)
				Velocity += (float) (Acceleration*gameTime.ElapsedGameTime.TotalSeconds);
			else if(Drag != 0)
			{
                float newDrag = (float)(Drag * gameTime.ElapsedGameTime.TotalSeconds);
                if (Velocity - newDrag > 0)
                    Velocity = Velocity - newDrag;
                else if (Velocity + newDrag < 0)
                    Velocity += newDrag;
				else
					Velocity = 0;
			}
			if((Velocity != 0) && (Max != 10000))
			{
				if(Velocity > Max)
					Velocity = Max;
				else if(Velocity < -Max)
					Velocity = -Max;
			}
			return Velocity;
		}
    }
}