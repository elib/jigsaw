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

        private PuzzlePiece attachedPiece = null;

        public Player()
            : base()
        {
            _maxVelocity.X = _maxVelocity.Y = 100;
            _drag.X = _drag.Y = 250;
            _size.X = _size.Y = 32;
        }

        public override Texture2D SetTexture(ContentManager content)
        {
            return content.Load<Texture2D>("player");
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


            if(InputManager.justPressedKeys.Contains(Keys.X))
            {
                //toggle grab a piece
                if (attachedPiece != null)
                {
                    Detach();
                }
                else
                {
                    Attach();
                }
            }

            ConstrainToScreen();

            UpdateAttachedPiece();
        }

        private void UpdateAttachedPiece()
        {
            if (attachedPiece != null)
            {
                attachedPiece._position.X = this._position.X;
                attachedPiece._position.Y = this._position.Y;
            }
        }

        private void ConstrainToScreen()
        {
            if (_position.X < 0)
            {
                _position.X = 0;
            }

            if (_position.Y < 0)
            {
                _position.Y = 0;
            }

            if (_position.X + this._size.X > Core.game.Width)
            {
                _position.X = Core.game.Width - this._size.X;
            }

            if (_position.Y + this._size.Y > Core.game.Height)
            {
                _position.Y = Core.game.Height - this._size.Y;
            }
        }

        private void Attach()
        {
            GameObject puzzlePiece = ((PlayScene)Core.game.currentScene).puzzle.GetFirstOverlappingMember(this);
            if (puzzlePiece != null)
            {
                this.attachedPiece = (PuzzlePiece)puzzlePiece;
            }
        }

        private void Detach()
        {
            //add cooler detach logic
            if (this.attachedPiece.TrySnap())
            {
                //I dunno, anything to do here?
            }
            this.attachedPiece = null;
        }
    }
}