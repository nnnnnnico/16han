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
    class Bullet : Character
    {
        private Vector2 vector2;
        Vector2 velocity;
        private float speed;
        private int _dir;
        private int count;
        private float gravity;
        private IGameMediator _mediator;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position"></param>
        /// <param name="gameDevice"></param>
        public Bullet(Vector2 position,int dir, GameDevice gameDevice,IGameMediator mediator)
            : base("EBullet", position, 16, 16, gameDevice)
        {
            speed = 20.0f;
            count = 0;
            _dir = dir;
            _mediator = mediator;
        }

        public Bullet(Bullet other)
            : this(other.position,other._dir, other.gameDevice,other._mediator)
        {

        }

        public override object Clone()
        {
            return new Bullet(this);
        }

        public override void Update(GameTime gameTime)
        {
            gravity += 0.1f;
            //位置の計算
            position.X = position.X + speed * _dir;

            position.Y += gravity;
            Destroy(1.0f);
        }

        public override void Hit(Character other)
        {
            if(other is Block)
            {
                isDeadFlag = true;
            }
            if(other is Player)
            {
                _mediator.AddGameObject(new BombDirect(position, gameDevice));
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
            if(count/60.0f * time == 1)
            {
                isDeadFlag = true;
            }
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify(), Color.Cyan);
        }
    }
}
