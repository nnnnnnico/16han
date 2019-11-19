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
    class Boss : Character
    {
        private List<Bullet> bulletList;
        private int Hp;
        private bool right;
        Vector2 vel;
        private int time;
        private Random rnd;
        private int count;
        private IGameMediator mediator;
        private int dir;

        public Boss(Vector2 position,GameDevice gameDevice,IGameMediator mediator)
            : base("Boss()",position ,128, 128,gameDevice)
        {
            this.mediator = mediator;
            //bulletList = new List<Bullet>();
            rnd = new Random();
        }

        public Boss(Boss other)
            : this(other.position, other.gameDevice,other.mediator)
        {

        }

        public override object Clone()
        {
            return new Boss(this);
        }

        public override void Hit(Character other)
        {
            if (other is Player)
            {
                Hp = Hp - 1;
            }
        }

        public override void Initialize()
        {
            //bulletList.Clear();
            Hp = 100;
            right = true;
            vel = Vector2.Zero;
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            time += 1;
            int a = time % 15;
            int b = time % 30;
            int c = time % 90;
            if (c == 0)
            {
                count = rnd.Next(6);
            }
            //foreach (var bullet in bulletList)
            //{
            //    bullet.Update(gameTime);
            //}
            if (count == 0 || count == 4)
            {
                if (a == 0)
                {
                    Attack1();
                }
                if (position.X <= 1280 - 128)
                {
                    MoveRight();
                }
                else
                {
                    count = 1;
                }
            }
            if (count == 1 || count == 5)
            {
                if (b == 0)
                {
                    Attack2();
                }
                if (position.X >= 300)
                {
                    MoveLeft();
                }
                else
                {
                    count = 0;
                }
            }
            if (count == 2)
            {
                if (position.X <= 1280 - 128)
                {
                    AttackMoveRight();
                }
                else
                {
                    count = 3;
                }
            }
            if (count == 3)
            {
                if (position.X >= 300)
                {
                    AttackMoveLeft();
                }
                else
                {
                    count = 2;
                }
            }
            position = position + vel;
            //bulletList.RemoveAll(bullets => bullets.IsDead());
            if(vel.X >= 1)
            {
                dir = 1; //右
            }
            else if(vel.X <= 1)
            {
                dir = -1; //左
            }

        }
        public void MoveRight()
        {
            vel.X = 1.5f;
        }

        public void MoveLeft()
        {
            vel.X = -1.5f;
        }
        public void AttackMoveRight()
        {
            vel.X = 3;
        }
        public void AttackMoveLeft()
        {
            vel.X = -3;
        }
        //右移動時に弾発射
        public void Attack1()
        {
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y + 64), gameDevice));
            mediator.AddGameObject(new Bullet(new Vector2(position.X, position.Y + 64),dir, gameDevice));
        }
        //左移動時に弾を発射
        public void Attack2()
        {
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y), gameDevice));
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y + 64), gameDevice));
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y + 128), gameDevice));
            mediator.AddGameObject(new Bullet(new Vector2(position.X, position.Y),dir, gameDevice));
            mediator.AddGameObject(new Bullet(new Vector2(position.X, position.Y + 64),dir,gameDevice));
            mediator.AddGameObject(new Bullet(new Vector2(position.X, position.Y + 128),dir, gameDevice));
        }

        public override void Draw(Renderer renderer)
        {
            //foreach(var bullet in bulletList)
            //{
                //bullet.Draw(renderer);
            //}
            renderer.DrawTexture("Boss()", position + gameDevice.GetDisplayModify());
        }
    }
}
