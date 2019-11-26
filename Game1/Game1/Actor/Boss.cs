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
        private float alpha;
        private int frameHalf;
        private Color[] color;
        private Color[] color2;
        private int colorNumber;

        private Sound sound;
        private int lastCount;

        public Boss(Vector2 position,GameDevice gameDevice,IGameMediator mediator)
            : base("BossLeft",position ,128*2, 128*2,gameDevice)
        {
            this.mediator = mediator;
            //bulletList = new List<Bullet>();
            rnd = new Random();
            hp = 2500;
            Charge = 0;
            Rectangle bound = new Rectangle(100, 100, 0, 50);
            gauge = new Gauge("gauge", "pixel", 150, 50, bound, hp, hp, 1100, Color.LightGreen);

            alpha = 1.0f;
            color = new Color[2] 
            {
                Color.White,
                Color.DarkGray,
            };
            color2 = new Color[2]
            {
                Color.Red,
                Color.DarkRed
            };
            colorNumber = 0;

            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
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
                hp -= 10;
            }
            if(other is PlayerFinalBullet)
            {
                hp -= 40;
            }
        }

        public override void Initialize()
        {
            //bulletList.Clear();
            right = true;
            vel = Vector2.Zero;
            isDeadFlag = false;
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            Dead();

            if (hp <= 0)
                return;

            gauge.ThisNum(hp);

            if (!PlayerInvisibleMode.isInvisibleMode)
            {
                alpha = 1.0f;
                colorNumber = 0;
                if(hp >= 0)
                    Move();

                position = position + vel;
            }
            else
            {
                alpha = 0.5f;
                colorNumber = 1;

                frameHalf++;
                if(frameHalf / 2.0f == 1 && hp >= 0)
                {
                    Move();
                    frameHalf = 0;
                }             

                position = position + (vel / 2);
            }

            position.X = MathHelper.Clamp(position.X, 0, mediator.MapSize().X - width/4);

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
            vel.X = 1.0f;
        }

        public void MoveLeft()
        {
            vel.X = -1.0f;
        }
        public void AttackMoveRight()
        {
            vel.X += 0.125f;
        }
        public void AttackMoveLeft()
        {
            vel.X -= 0.125f;
        }
        //右移動時に弾発射
        public void Attack1()
        {
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y + 64), gameDevice));
            if (dir == -1)
            {
                sound.PlaySE("short_bomb");
                mediator.AddGameObject(new Bullet(new Vector2(position.X, position.Y + 145), dir, gameDevice,mediator));
            }
            if (dir == 1)
            {
                sound.PlaySE("short_bomb");
                mediator.AddGameObject(new Bullet(new Vector2(position.X+128*2, position.Y + 145), dir, gameDevice,mediator));
            }
        }
        //左移動時に弾を発射
        public void Attack2()
        {
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y), gameDevice));
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y + 64), gameDevice));
            //bulletList.Add(new Bullet(new Vector2(position.X, position.Y + 128), gameDevice));
            if (dir == -1)
            {
                sound.PlaySE("battery1");
                mediator.AddGameObject(new BossBomb(new Vector2(position.X, position.Y+20), 12, 0.2f, dir, gameDevice,mediator));
                mediator.AddGameObject(new BossBomb(new Vector2(position.X, position.Y + 100), 10, 0.4f, dir, gameDevice,mediator));
                mediator.AddGameObject(new BossBomb(new Vector2(position.X, position.Y + 145), 8, 0.5f, dir, gameDevice,mediator));
            }
            if (dir == 1)
            {
                sound.PlaySE("battery1");
                mediator.AddGameObject(new BossBomb(new Vector2(position.X+128*2, position.Y+20), 12, 0.2f, dir, gameDevice,mediator));
                mediator.AddGameObject(new BossBomb(new Vector2(position.X+128*2, position.Y + 100), 10, 0.4f, dir, gameDevice,mediator));
                mediator.AddGameObject(new BossBomb(new Vector2(position.X+128*2, position.Y + 145), 8, 0.5f, dir, gameDevice,mediator));
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
                renderer.DrawTexture("BossLeft", position + gameDevice.GetDisplayModify(), new Vector2(2, 2),color[colorNumber]);
            }
            if (dir == -1 && Charge == 1)
            {
                renderer.DrawTexture("BossLeft", position + gameDevice.GetDisplayModify(), new Vector2(2, 2),color2[colorNumber]);
            }
            if (dir == 1 && Charge == 0)
            {
                renderer.DrawTexture("BossRight", position + gameDevice.GetDisplayModify(), new Vector2(2, 2), color[colorNumber]);
            }
            if (dir == 1 && Charge == 1)
            {
                renderer.DrawTexture("BossRight", position + gameDevice.GetDisplayModify(), new Vector2(2, 2), color2[colorNumber]);
            }
            gauge.Draw(renderer);
        }

        private void Move()
        {
            time += 1;
            int a = time % 30;
            int b = time % 60;
            int c = time % 90;
            if (c == 0)
            {
                count = rnd.Next(6);
            }
            //foreach (var bullet in bulletList)
            //{
            //    bullet.Update(gameTime);
            //}
            if (count == 0||count==2)
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
            if (count == 1 ||count==3)
            {
                Charge = 0;
                if (b == 1)
                {
                    Attack2();
                }
                if (position.X >= 0)
                {
                    MoveLeft();
                }
                else
                {
                    count = 0;
                }
            }
            if (count == 4)
            {
                if (position.X <= 1280 - 128 * 2)
                {
                    Charge = 1;
                    if (Charge == 1)
                    {
                        AttackMoveRight();
                    }
                }
                else
                {
                    count = 1;
                }
            }
            if (count == 5)
            {
                if (position.X <= 100)
                {
                    Charge = 1;
                    if (Charge == 1)
                    {
                        AttackMoveLeft();
                    }
                }
                else
                {
                    count = 0;
                }
            }
        }

        public void Dead()
        {
            if (hp >= 0)
                return;

            if (hp <= 0)
            {
                lastCount++;
                if (lastCount / 5.0f == 1)
                {
                    sound.PlaySE("bomb");
                    mediator.AddGameObject(new DeadEffect(position, 180, 100, gameDevice));
                }
                if (lastCount / 15.0f == 1)
                {
                    sound.PlaySE("bomb");
                    mediator.AddGameObject(new DeadEffect(position, -20, 50, gameDevice));
                }
                if (lastCount / 25.0f == 1)
                {
                    sound.PlaySE("bomb");
                    mediator.AddGameObject(new DeadEffect(position, 120, 80, gameDevice));
                }
                if (lastCount / 35.0f == 1)
                {
                    sound.PlaySE("bomb");
                    mediator.AddGameObject(new DeadEffect(position, 20, 30, gameDevice));
                }
                if (lastCount / 45.0f == 1)
                {
                    sound.PlaySE("bomb");
                    mediator.AddGameObject(new DeadEffect(position, 0, 50, gameDevice));
                }
                if (lastCount / 75.0f == 1)
                {
                    isDeadFlag = true;
                }
            }
        }
    }
}
