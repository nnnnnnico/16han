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
        private int hp;
        private bool right;
        Vector2 vel;
        private int time;
        private Random rnd;
        private int count;
        private IGameMediator mediator;
        private int dir;
        public Gauge gauge;
        private int Charge;

        public Boss(Vector2 position,GameDevice gameDevice,IGameMediator mediator)
            : base("Boss()",position ,128*2, 128*2,gameDevice)
        {
            this.mediator = mediator;
            //bulletList = new List<Bullet>();
            rnd = new Random();
            hp = 100;
            Charge = 0;
            Rectangle bound = new Rectangle(100, 100, 0, 50);
            gauge = new Gauge("gauge", "pixel", 100, 100, bound, hp, hp, 1100, Color.LightGreen);
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
            if (other is PlayerBullet)
            {
                hp -= 1;
            }
        }

        public override void Initialize()
        {
            //bulletList.Clear();
            right = true;
            vel = Vector2.Zero;
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            gauge.ThisHp(hp);
            if (hp <= 0)
            {
                isDeadFlag = true;
            }
            time += 1;
            int a = time % 30;
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
                Charge = 0;
                if (a == 0)
                {
                    Attack1();
                }
                if (position.X <= 1280 - 128 * 2)
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
                Charge = 0;
                if (b == 0)
                {
                    Attack2();
                }
                if (position.X >= 0+128*2)
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
                if (position.X <= 1280 - 128 * 2)
                {
                    Charge = 1;
                    AttackMoveRight();
                }
                else
                {
                    count = 1;
                }
            }
            if (count == 3)
            {
                if (position.X >= 0+128*2)
                {
                    Charge = 1;
                    AttackMoveLeft();
                }
                else
                {
                    count = 0;
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
            vel.X += 0.2f;
        }
        public void AttackMoveLeft()
        {
            vel.X -= 0.2f;
        }
        //右移動時に弾発射
        public void Attack1()
        {
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y + 64), gameDevice));
            if (dir == -1)
            {
                mediator.AddGameObject(new Bullet(new Vector2(position.X, position.Y + 100), dir, gameDevice));
            }
            if (dir == 1)
            {
                mediator.AddGameObject(new Bullet(new Vector2(position.X+128*2, position.Y + 100), dir, gameDevice));
            }
        }
        public void Stop()
        {
            vel.X = 0;
        }
        //左移動時に弾を発射
        public void Attack2()
        {
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y), gameDevice));
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y + 64), gameDevice));
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y + 128), gameDevice));
            if (dir == -1)
            {
                mediator.AddGameObject(new Bullet(new Vector2(position.X, position.Y+20), dir, gameDevice));
                mediator.AddGameObject(new Bullet(new Vector2(position.X, position.Y + 100), dir, gameDevice));
                mediator.AddGameObject(new Bullet(new Vector2(position.X, position.Y + 145), dir, gameDevice));
            }
            if (dir == 1)
            {
                mediator.AddGameObject(new Bullet(new Vector2(position.X+128*2, position.Y+20), dir, gameDevice));
                mediator.AddGameObject(new Bullet(new Vector2(position.X+128*2, position.Y + 100), dir, gameDevice));
                mediator.AddGameObject(new Bullet(new Vector2(position.X+128*2, position.Y + 145), dir, gameDevice));
            }
        }

        public override void Draw(Renderer renderer)
        {
            //foreach(var bullet in bulletList)
            //{
            //bullet.Draw(renderer);
            //}
            if (dir == -1 && Charge == 0)
            {
                renderer.DrawTexture("BossLeft", position + gameDevice.GetDisplayModify(), new Vector2(2, 2));
            }
            if (dir == -1 && Charge == 1)
            {
                renderer.DrawTexture("BossLeft", position + gameDevice.GetDisplayModify(), new Vector2(2, 2),Color.Red);
            }
            if (dir == 1 && Charge == 0)
            {
                renderer.DrawTexture("BossRight", position + gameDevice.GetDisplayModify(), new Vector2(2, 2));
            }
            if (dir == 1 && Charge == 1)
            {
                renderer.DrawTexture("BossRight", position + gameDevice.GetDisplayModify(), new Vector2(2, 2), Color.Red);
            }
            gauge.Draw(renderer);
        }
    }
}
