using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Game1.Def;
using Game1.Device;
using Game1.Scene;
using Game1.Util;
using Game1.Static;


namespace Game1.Actor
{
    class Player : Character
    {
        Vector2 velocity;
        

        int hp;

        public Player(Vector2 position,GameDevice gameDevice)
            :base("TankRight",position,64,64,gameDevice)
        {
            position = new Vector2(100, 100);
            velocity = Vector2.Zero;
            hp = 10;
        }

        public Player(Player other)
            : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new Player(this);
        }

        public override void Hit(Character other)
        {
            if (other is Block)
            {
                hitBlock(other);
            }

        }

        public override void Initialize()
        {

        }

        public override void Shutdown()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            float speed = 4.0f;

            velocity = Input.Velocity() * speed;

            //位置の計算
            position = position + velocity;
        }

        private void hitBlock(Character gameObject)
        {
            //当たった方向の取得
            Direction dir = this.CheckDirection(gameObject);

            //ブロックの上面と衝突
            if (dir == Direction.Top)
            {
                //プレイヤーがブロックの上にのった
                if (position.Y > 0.0f)//降下中の時、ジャンプ状態終了
                {
                    position.Y = gameObject.GetRectangle().Top - this.height;
                    velocity.Y = 0.0f;
                }
            }
            else if (dir == Direction.Right)//右
            {
                position.X = gameObject.GetRectangle().Right;
            }
            else if (dir == Direction.Left)//左
            {
                position.X = gameObject.GetRectangle().Left - this.width;
            }
            else if (dir == Direction.Bottom)//下
            {
                position.Y = gameObject.GetRectangle().Bottom;
            }
            //SetDisplayModify();
        }

        //private void SetDisplayModify()
        //{
        //    //中心で描画するよう補正値を設定
        //    gameDevice.SetDisplayModify(new Vector2(-position.X + (Screen.Width / 2 -
        //        width / 2), 0.0f));

        //    //プレイヤーのX座標が画面の中心より左なら見切れているので、Vector2.Zeroで設定しなおす
        //    if (position.X < (Screen.Width / 2) - width / 2)
        //    {
        //        gameDevice.SetDisplayModify(Vector2.Zero);
        //    }
        //    if (position.X > (mediator.MapSize().X) - (Screen.Width / 2 + width / 2))
        //    {
        //        gameDevice.SetDisplayModify(new Vector2(-mediator.MapSize().X + Screen.Width, 0));
        //    }
        //}
    }
}
