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
    class PBulletEffect : Character
    {
        private Motion motion;
        private Timer timer;
        private int count;
        private readonly int pictureNum = 8;
        private Vector2 _position;
        private float _widthD, _heightD;

        public PBulletEffect(Vector2 position,float widthD, float heightD, GameDevice gameDevice)
            : base("pipo-btleffect008", position, 16, 16, gameDevice)
        {
            timer = new CountDownTimer(0.05f);
            _position = position;
            _widthD = widthD;
            _heightD = heightD;
        }

        public PBulletEffect(PBulletEffect other)
            : this(other.position,other._widthD,other._heightD, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new PBulletEffect(this);
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
            //タイマー更新
            timer.Update(gameTime);

            //指定時間か?
            if (timer.IsTime())
            {
                //次の画像へ
                count += 1;
                //初期化
                timer.Initialize();
                //アニメーション画像の最後までたどり着いてたら死亡へ
                if (count >= pictureNum)
                {
                    isDeadFlag = true;
                }
            }

        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, new Vector2(_position.X - _widthD,_position.Y - _heightD) + gameDevice.GetDisplayModify(), new Rectangle(count * 240,0,240,240));
        }
    }
}
