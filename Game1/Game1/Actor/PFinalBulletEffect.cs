﻿using System;
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
    class PFinalBulletEffect : Character
    {
        private Motion motion;
        private Timer timer;
        private int count;
        private readonly int pictureNum = 8;
        private Vector2 _position;
        private float _widthD, _heightD;

        public PFinalBulletEffect(Vector2 position,float widthD,float heightD, GameDevice gameDevice)
            : base("pipo-btleffect022", position, 16, 16, gameDevice)
        {
            timer = new CountDownTimer(0.05f);
            _position = position;
            _widthD = widthD;
            _heightD = heightD;
        }

        public PFinalBulletEffect(PFinalBulletEffect other)
            : this(other.position,other._widthD,other._heightD, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new PFinalBulletEffect(this);
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
            renderer.DrawTexture(name, 
                new Vector2(_position.X - (_widthD * 1.5f), _position.Y - (_heightD * 1.5f)) + gameDevice.GetDisplayModify(), 
                new Rectangle(count * 240, 0, 240, 240),
                new Vector2(1.5f,1.5f));
        }
    }
}
