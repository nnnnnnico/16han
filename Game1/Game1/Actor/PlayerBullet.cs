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
    class PlayerBullet : Character
    {
        private Vector2 vector2;
        Vector2 velocity;
        private float speed;
        private int _dir;
        private int count;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position"></param>
        /// <param name="gameDevice"></param>
        public PlayerBullet(Vector2 position, int dir, GameDevice gameDevice)
            : base("Bullet16", position, 16, 16, gameDevice)
        {
            speed = 20.0f;
            count = 0;
            _dir = dir;
        }

        public PlayerBullet(PlayerBullet other)
            : this(other.position, other._dir, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new PlayerBullet(this);
        }

        public override void Update(GameTime gameTime)
        {
            //位置の計算
            position.X = position.X + speed * _dir;

            Destroy(1.0f);
        }

        public override void Hit(Character other)
        {
            if (other is Block)
            {
                isDeadFlag = true;
            }
            if(other is Boss)
            {
                isDeadFlag = true;
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
    }
}
