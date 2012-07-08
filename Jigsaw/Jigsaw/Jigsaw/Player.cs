using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Jigsaw
{
    public class Player : GameObject
    {
        private const float ACCEL_RATE = 150;

        public Player()
            : base()
        {
            _maxVelocity.X = _maxVelocity.Y = 100;
            _drag.X = _drag.Y = 250;
        }

        public override void SetTexture(ContentManager content)
        {
            _texture = content.Load<Texture2D>("columbo");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 dir = Vector2.Zero;

            if(Keyboard.GetState(0).IsKeyDown(Keys.Right))
            {
                dir.X = 1;
            }
            else if (Keyboard.GetState(0).IsKeyDown(Keys.Left))
            {
                dir.X = -1;
            }

            if (Keyboard.GetState(0).IsKeyDown(Keys.Down))
            {
                dir.Y = 1;
            }
            else if (Keyboard.GetState(0).IsKeyDown(Keys.Up))
            {
                dir.Y = -1;
            }

            //no double-chording!!!
            if (dir.LengthSquared() > 0)
            {
                dir.Normalize();
            }


            //X
            if (dir.X == 0)
            {
                _acceleration.X = 0;
            }
            else
            {
                _acceleration.X = (float)(dir.X * ACCEL_RATE * gameTime.TotalGameTime.TotalSeconds);
            }

            //Y
            if (dir.Y == 0)
            {
                _acceleration.Y = 0;
            }
            else
            {
                _acceleration.Y = (float)(dir.Y * ACCEL_RATE * gameTime.TotalGameTime.TotalSeconds);
            }

        }
    }
}