using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Game1.Def;

namespace Game1.Device
{
    public static class Camera
    {
        // カメラの左上座標
        private static Vector2 position;
        // 最小値
        private static Vector2 min;
        // 最大値
        private static Vector2 max;

        /// <summary>
        /// 位置設定
        /// </summary>
        /// <param name="pos">位置</param>
        public static void SetPosition(Vector2 pos)
        {
            position = pos;
        }



        /// <summary>
        /// 移動メソッド
        /// </summary>
        /// <param name="velocity"></param>
        public static void Move(Vector2 velocity)
        {
            // 移動
            position += velocity;
            position = Vector2.Clamp(position, min, max);
        }

        /// <summary>
        /// 動ける範囲設定
        /// </summary>
        /// <param name="minimum">最小値</param>
        /// <param name="maximum">最大値</param>
        public static void SetMoveArea(Vector2 minimum, Vector2 maximum)
        {
            min = minimum;
            max = maximum;
        }

        /// <summary>
        /// 位置取得
        /// </summary>
        /// <returns>位置</returns>
        public static Vector2 GetPosition()
        {
            return position;
        }

        /// <summary>
        /// ウィンドウ上での位置取得
        /// </summary>
        /// <param name="pos">ウィンドウ上での位置</param>
        /// <returns></returns>
        public static Vector2 GetScreenPos(Vector2 pos)
        {
            return pos - position;
        }

        /// <summary>
        /// 最小値取得
        /// </summary>
        /// <returns>最小値</returns>
        public static Vector2 GetMin()
        {
            return min;
        }

        /// <summary>
        /// 最大値取得
        /// </summary>
        /// <returns>最大値</returns>
        public static Vector2 GetMax()
        {
            return max;
        }
    }
}
