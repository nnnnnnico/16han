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
        private float _gravity;
        private float _plusGravity;
        private IGameMediator _mediator;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position"></param>
        /// <param name="gameDevice"></param>
        public BossBomb(Vector2 position,float speed, float plusGravity,int dir, GameDevice gameDevice,IGameMediator mediator)
            : base("EBomb", position, 16, 16, gameDevice)
        {
            _speed = speed;
            count = 0;
            _dir = dir;
            _gravity = 2.0f;
            _plusGravity = plusGravity;
            _mediator = mediator;
        }

        public BossBomb(BossBomb other)
            : this(other.position,other._speed,other._plusGravity, other._dir, other.gameDevice,other._mediator)
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
                _mediator.AddGameObject(new BombEffect(position, gameDevice));
            }
            if (other is Player)
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
            if (count / 60.0f * time == 1)
            {
                isDeadFlag = true;
            }
        }

        private void Shot()
        {
            position.X = position.X + _speed * _dir;
            _speed -= 0.2f;
            position.Y += _gravity;
            _gravity += _plusGravity;
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify(), new Vector2(2.0f, 2.0f));
        }
    }
}
