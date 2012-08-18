using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Jigsaw
{
    public enum Directions
    {
        Right, Left, Up, Down
    }

    public class InputManager
    {
        public static Dictionary<PlayerIndex, bool> justPressedButton = new Dictionary<PlayerIndex, bool>();
        private static Dictionary<PlayerIndex, GamePadState> _lastGamePadStates = new Dictionary<PlayerIndex, GamePadState>();

        static InputManager()
        {
            _lastGamePadStates[PlayerIndex.One] = new GamePadState();
            _lastGamePadStates[PlayerIndex.Two] = new GamePadState();

            justPressedButton[PlayerIndex.One] = false;
            justPressedButton[PlayerIndex.Two] = false;
        }

        public static List<Keys> justPressedKeys = new List<Keys>();
        private static Keys[] lastKeys;

        public static void init()
        {
        }

        public static void update()
        {
            getLastKeyboardButtons();
            calculateLastGamePadButtons();
        }

        private static bool isSpecificButtonJustPressed(ButtonState oldButtonState, ButtonState newButtonState)
        {
            if (oldButtonState == newButtonState) return false;
            if (newButtonState == ButtonState.Pressed) return true;

            return false;
        }

        private static bool compareGamePadStates(GamePadState oldState, GamePadState newState)
        {
            if (isSpecificButtonJustPressed(oldState.Buttons.A, newState.Buttons.A)) return true;
            if (isSpecificButtonJustPressed(oldState.Buttons.B, newState.Buttons.B)) return true;
            if (isSpecificButtonJustPressed(oldState.Buttons.X, newState.Buttons.X)) return true;
            if (isSpecificButtonJustPressed(oldState.Buttons.Y, newState.Buttons.Y)) return true;

            return false;
        }

        private static void calculateSingleLastGamePadButtons(PlayerIndex playerIndex)
        {
            GamePadState oldState = _lastGamePadStates[playerIndex];
            GamePadState newState = GamePad.GetState(playerIndex);
            justPressedButton[playerIndex] = compareGamePadStates(oldState, newState);
            _lastGamePadStates[playerIndex] = newState;
        }

        private static void calculateLastGamePadButtons()
        {
            calculateSingleLastGamePadButtons(PlayerIndex.One);
            calculateSingleLastGamePadButtons(PlayerIndex.Two);
        }

        private static bool isAnyButtonPressed(PlayerIndex playerIndex)
        {
            var state = GamePad.GetState(playerIndex);
            return (state.Buttons.A == ButtonState.Pressed || state.Buttons.B == ButtonState.Pressed
                || state.Buttons.X == ButtonState.Pressed || state.Buttons.Y == ButtonState.Pressed);
        }

        private static void getLastKeyboardButtons()
        {
            KeyboardState ks = Keyboard.GetState(0);
            Keys[] newKeys = ks.GetPressedKeys();

            justPressedKeys.Clear();

            if (lastKeys != null)
            {
                foreach (var key in newKeys)
                {
                    if (!lastKeys.Contains(key))
                    {
                        //newly-pressed!
                        justPressedKeys.Add(key);
                    }
                }
            }

            lastKeys = newKeys;
        }

        internal static bool Going(PlayerIndex playerIndex, Directions direction)
        {
            var state = GamePad.GetState(playerIndex);
            switch (direction)
            {
                case Directions.Right:
                    return state.DPad.Right == ButtonState.Pressed || state.ThumbSticks.Left.X > 0 || state.ThumbSticks.Right.X > 0;
                case Directions.Left:
                    return state.DPad.Left == ButtonState.Pressed || state.ThumbSticks.Left.X < 0 || state.ThumbSticks.Right.X < 0;
                case Directions.Up:
                    return state.DPad.Up == ButtonState.Pressed || state.ThumbSticks.Left.Y > 0 || state.ThumbSticks.Right.Y > 0;
                case Directions.Down:
                    return state.DPad.Down == ButtonState.Pressed || state.ThumbSticks.Left.Y < 0 || state.ThumbSticks.Right.Y < 0;
            }

            return false;
        }

        private static bool isAnyGamePadPressed(PlayerIndex playerIndex)
        {
            var state = GamePad.GetState(playerIndex);
            if (state.DPad.Right == ButtonState.Pressed) return true;
            if (state.DPad.Left == ButtonState.Pressed) return true;
            if (state.DPad.Up == ButtonState.Pressed) return true;
            if (state.DPad.Down == ButtonState.Pressed) return true;

            if (state.Buttons.A == ButtonState.Pressed) return true;
            if (state.Buttons.B == ButtonState.Pressed) return true;
            if (state.Buttons.X == ButtonState.Pressed) return true;
            if (state.Buttons.Y == ButtonState.Pressed) return true;

            if (state.ThumbSticks.Left.LengthSquared() > 0) return true;
            if (state.ThumbSticks.Right.LengthSquared() > 0) return true;

            return false;
        }

        internal static bool IsIdle()
        {
            if (isAnyGamePadPressed(PlayerIndex.One) || isAnyGamePadPressed(PlayerIndex.One)) return false;

            return true;
        }

        public static bool IsFunctionButtonPressed
        {
            get
            {
                bool pressed = false;
                pressed |= GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed;
                pressed |= GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed;
                pressed |= GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed;
                pressed |= GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed;

                return pressed;
            }
        }
    }
}