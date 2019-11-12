using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.Device
{
    class Input
    {
        //移動量
        private static Vector2 velocity = Vector2.Zero;
        //キーボード
        private static KeyboardState currentKey;
        private static KeyboardState previouskey;
        //マウス
        private static MouseState currentMouse;
        private static MouseState previousMouse;

        public static void Update()

        {
            //キーボード
            previouskey = currentKey;
            currentKey = Keyboard.GetState();
            //マウス
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            UpdateVelocity();

        }
        //キーボード関連
        public static Vector2 Velocity()
        {
            return velocity;
        }
        private static void UpdateVelocity()
        {
            //毎ループ初期化
            velocity = Vector2.Zero;
            //右
            if (currentKey.IsKeyDown(Keys.Right))
            {
                velocity.X += 1.0f;
            }
            //左
            if (currentKey.IsKeyDown(Keys.Left))
            {
                velocity.X -= 1.0f;
            }
            //上
            if (currentKey.IsKeyDown(Keys.Up))
            {
                velocity.Y -= 1.0f;
            }
            //下
            if (currentKey.IsKeyDown(Keys.Down))
            {
                velocity.Y += 1.0f;
            }
            //正規化
            if (velocity.Length() != 0)
            {
                velocity.Normalize();
            }

        }
        public static bool IskeyDown(Keys key)
        {
            return currentKey.IsKeyDown(key) && !previouskey.IsKeyDown(key);
        }
        public static bool GetKeyTrigger(Keys key)
        {
            return IskeyDown(key);
        }
        public static bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }
        public static bool GetKeyRelease(Keys key)
        {
            return currentKey.IsKeyUp(key) && previouskey.IsKeyDown(key);
        }
        public static bool IsMouseLBottonDown()
        {
            return currentMouse.LeftButton == ButtonState.Pressed &&
                previousMouse.LeftButton == ButtonState.Released;
        }
        public static bool IsMouseLBottonUp()
        {
            return currentMouse.LeftButton == ButtonState.Released &&
                previousMouse.LeftButton == ButtonState.Pressed;
        }
        public static bool IsMouseLButton()
        {
            return currentMouse.LeftButton == ButtonState.Pressed;
        }
        public static bool IsMouseRButtonDown()
        {
            return currentMouse.RightButton == ButtonState.Pressed &&
                previousMouse.RightButton == ButtonState.Released;
        }
        public static bool IsMouseRBotton()
        {
            return currentMouse.RightButton == ButtonState.Pressed;
        }
        public static Vector2 MousePosition
        {
            get
            {
                return new Vector2(currentMouse.X, currentMouse.Y);
            }
        }
        public static int GetMouseWheel()
        {
            return previousMouse.ScrollWheelValue -
                currentMouse.ScrollWheelValue;
        }

    }
}
