using Game1.Device;
using Game1.Scene;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Actor
{
    class BossBomb : Character
    {
        private Vector2 vector2;
        Vector2 velocity;
        private float _speed;
        private int _dir;
        private int count;
        private float gravity;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position"></param>
        /// <param name="gameDevice"></param>
        public BossBomb(Vector2 position,float speed ,int dir, GameDevice gameDevice)
            : base("Bullet16", position, 16, 16, gameDevice)
        {
            _speed = speed;
            count = 0;
            _dir = dir;
            gravity = 2.0f;
        }

        public BossBomb(BossBomb other)
            : this(other.position,other._speed, other._dir, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new BossBomb(this);
        }

        public override void Update(GameTime gameTime)
        {
            //位置の計算
            //position.X = position.X + speed * _dir;

            Shot();
        }

        public override void Hit(Character other)
        {
            if (other is Block)
            {
                isDeadFlag = true;
            }
            if (other is Player)
            {
                //isDeadFlag = true;
            }
        }

        public override void Initialize()
        {

        }

        public override void Shutdown()
        {

        }

        private void Destroy(float time)
        {
            count++;
            if (count / 60.0f * time == 1)
            {
                isDeadFlag = true;
            }
        }

        private void Shot()
        {
            position.X = position.X + _speed * _dir;

            position.Y += gravity;
            gravity += 0.2f;
        }
    }
}
