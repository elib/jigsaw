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
    public class Player : EmittingGameObject
    {
        private const float ACCEL_RATE = 180;

        private PuzzlePiece attachedPiece = null;

        private Puzzle _puzzle;

        private PlayerIndex _playerIndex;

        public Player(Puzzle puzzle, PlayerIndex playerIndex, ParticleType particleType)
            : base(200, 1, 100, particleType)
        {
            ScaleFactor = 2;
            _playerIndex = playerIndex;

            _maxVelocity.X = _maxVelocity.Y = 100;
            _drag.X = _drag.Y = 250;
            _size.X = _size.Y = 32;

            spawnLocation();

            _puzzle = puzzle;
        }

        protected override void initializeAnimation()
        {
            base.initializeAnimation();

            _animation.Add("idle", new int[] { 0 }, 1);
            _animation.Add("piecemove", new int[] { 1 }, 10);
            Play("idle");
        }

        private void spawnLocation()
        {
            _position.Y = Core.game.Height / 2 - this._size.Y / 2;

            if (_playerIndex == PlayerIndex.One)
            {
                _position.X = Core.game.Width / 4.0f - this._size.X / 2;
            }
            else
            {
                _position.X = 3.0f * Core.game.Width / 4.0f - this._size.X / 2.0f;
            }
        }

        public override Texture2D SetTexture(ContentManager content)
        {
            switch (_playerIndex)
            {
                case PlayerIndex.One:
                    return content.Load<Texture2D>("player1");
                case PlayerIndex.Two:
                    return content.Load<Texture2D>("player2");
            }

            return content.Load<Texture2D>("player");
        }

        public override void UpdateAnimation()
        {
            base.UpdateAnimation();

            if (_velocity.LengthSquared() > 0)
            {
                this.PulseEmitting(0.1);
            }

            if (attachedPiece != null)
            {
                this.Play("piecemove", true);
            }
            else
            {
                this.Play("idle");
            }
        }

        public override void Update()
        {
            base.Update();

            if (_puzzle == null)
            {
                return;
            }

            Vector2 dir = Vector2.Zero;

            

            if (Keyboard.GetState(_playerIndex).IsKeyDown(Keys.Right)
                || InputManager.Going(_playerIndex, Directions.Right))
            {
                dir.X = 1;
            }
            else if (Keyboard.GetState(_playerIndex).IsKeyDown(Keys.Left) || InputManager.Going(_playerIndex, Directions.Left))
            {
                dir.X = -1;
            }

            if (Keyboard.GetState(_playerIndex).IsKeyDown(Keys.Down) || InputManager.Going(_playerIndex, Directions.Down))
            {
                dir.Y = 1;
            }
            else if (Keyboard.GetState(_playerIndex).IsKeyDown(Keys.Up) || InputManager.Going(_playerIndex, Directions.Up))
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
                _acceleration.X = (float)(dir.X * ACCEL_RATE * Core.TotalTime);
            }

            //Y
            if (dir.Y == 0)
            {
                _acceleration.Y = 0;
            }
            else
            {
                _acceleration.Y = (float)(dir.Y * ACCEL_RATE * Core.TotalTime);
            }

            if (InputManager.justPressedKeys.Contains(Keys.X) || InputManager.justPressedButton[_playerIndex])
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

            if (_position.X + this.Size.X > Core.game.Width)
            {
                _position.X = Core.game.Width - this.Size.X;
            }

            if (_position.Y + this.Size.Y > Core.game.Height)
            {
                _position.Y = Core.game.Height - this.Size.Y;
            }
        }

        private void Attach()
        {
            GameObject puzzlePiece = _puzzle.GetFirstOverlappingMember(this);
            if (puzzlePiece != null)
            {
                this.attachedPiece = (PuzzlePiece)puzzlePiece;
                this.attachedPiece.Bounce(GameObject.MIN_BOUNCE_PERCENTAGE, true);
                _puzzle.PiecePicked((PuzzlePiece)puzzlePiece);
            }
        }

        private void Detach()
        {
            //add cooler detach logic
            if (attachedPiece.TrySnap())
            {
                //visual effects here
                attachedPiece.Bounce(GameObject.MEDIUM_BOUNCE_PERCENTAGE, true);
                _puzzle.PiecePlaced(attachedPiece);
            }
            else
            {
                _puzzle.PieceReplaced(attachedPiece);
            }

            attachedPiece = null;
        }
    }
}