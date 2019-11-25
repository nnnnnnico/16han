using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Device;
using Game1.Scene;
using Game1.Util;
using Microsoft.Xna.Framework;

namespace Game1.Actor
{
    class BombEffect : Character
    {
        private Motion motion;
        private Timer timer;
        private int count;
        private readonly int pictureNum = 8;
        private Vector2 _position;
        private int time;
        private Sound sound;

        public BombEffect(Vector2 position, GameDevice gameDevice)
            : base("pipo-btleffect030", position, 256, 16, gameDevice)
        {
            timer = new CountDownTimer(0.05f);
            _position = position;
            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            
        }

        public BombEffect(BombEffect other)
            : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new BombEffect(this);
        }

        public override void Hit(Character other)
        {
        }

        public override void Initialize()
        {
            count = 0;
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            sound.PlaySE("small_explosion2");

            time++;
            //タイマー更新
            //timer.Update(gameTime);

            //指定時間か?
            if (time / 5.0f == 1)
            {
                time = 0;
                //次の画像へ
                count += 1;
                //初期化
                //timer.Initialize();
                //アニメーション画像の最後までたどり着いてたら死亡へ
                if (count >= pictureNum)
                {
                    isDeadFlag = true;
                }
            }

        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, new Vector2(_position.X - 320, _position.Y - 210) + gameDevice.GetDisplayModify(), new Rectangle(0, count * 240, 640, 240));
        }
    }
}
