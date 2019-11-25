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
    class Player : Character
    {
        Vector2 velocity;
        private IGameMediator mediator;
        private float gravity;
        private bool isGround;
        private bool hitDirect;
        private bool escape;
        private int escapeTime;
        private const float speed = 4.0f;
        private bool flashPlayer;
        private int right;
        private bool isShot;
        private int shotInterval;
        private int shotCount;

        public Gauge gauge;


        int hp;
        float alpha;

        public Player(Vector2 position,GameDevice gameDevice,IGameMediator mediator)
            :base("TankRight",position,64,64,gameDevice)
        {
            position = new Vector2(650, 128);
            this.mediator = mediator;
            velocity = Vector2.Zero;
            hp = 100;
            gravity = 10;
            isGround = false;
            escape = false;
            alpha = 1.0f;
            flashPlayer = false;
            right = 1;
            isShot = false;
            

            Rectangle bound = new Rectangle(100, 100, 0, 50);
            gauge = new Gauge("gauge", "pixel",0,700,bound, hp,hp, 350, Color.LightGreen);
        }

        public Player(Player other)
            : this(other.position, other.gameDevice,other.mediator)
        {

        }

        public override object Clone()
        {
            return new Player(this);
        }

        public override void Hit(Character other)
        {
            if (other is Block)
            {
                hitBlock(other);
            }
            if(other is Boss)
            {
                if (!hitDirect)
                {
                    hitDirect = true;
                    hp -= 5;
                }
            }
            if(other is Bullet)
            {
                hp -= 3;
            }

        }

        public override void Initialize()
        {
            
        }

        public override void Shutdown()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            gauge.ThisHp(hp);
            velocity = Input.Velocity() * speed;

            //位置の計算
            position = position + velocity;
            position.X = MathHelper.Clamp(position.X,0, mediator.MapSize().X - width);

            EscapeMove();

            SetDisplayModify();

            if(hp <= 0)
            {
                isDeadFlag = true;
            }

            if (!isGround)
            {
                position.Y += gravity;
            }

            if (hitDirect)
            {
                escapeTime++;
                if(escapeTime % 10.0f == 0)
                {
                    flashPlayer = !flashPlayer;
                }
                if (escapeTime / 60.0f == 1)
                {
                    hitDirect = false;
                    escapeTime = 0;
                }
            }

            Flash();
            Shot();
            NowShot();

            if (!isShot)
            {
                if (Input.GetKeyTrigger(Keys.Left))
                {
                    right = -1;
                    name = "TankLeft";
                }
                else if (Input.GetKeyTrigger(Keys.Right))
                {
                    right = 1;
                    name = "TankRight";
                }
            }
        }

        private void EscapeMove()
        {
            if(velocity.X >= 0&&Input.GetKeyTrigger(Keys.Z))
            {
                position.X += 150;
            }
            if (velocity.X <= 0 && Input.GetKeyTrigger(Keys.Z))
            {
                position.X -= 150;
            }

        }

        private void hitBlock(Character gameObject)
        {
            //当たった方向の取得
            Direction dir = this.CheckDirection(gameObject);

            //ブロックの上面と衝突
            if (dir == Direction.Top)
            {
                isGround = true;
                //プレイヤーがブロックの上にのった
                if (position.Y > 0.0f)//降下中の時、ジャンプ状態終了
                {
                    position.Y = gameObject.GetRectangle().Top - this.height;
                    velocity.Y = 0.0f;
                }
            }
            if (dir == Direction.Right)//右
            {
                position.X = gameObject.GetRectangle().Right;
            }
            if (dir == Direction.Left)//左
            {
                position.X = gameObject.GetRectangle().Left - this.width;
            }
            if (dir == Direction.Bottom)//下
            {
                position.Y = gameObject.GetRectangle().Bottom;
            }
            SetDisplayModify();
        }

        private void SetDisplayModify()
        {
            //中心で描画するよう補正値を設定
            gameDevice.SetDisplayModify(new Vector2(-position.X + (Screen.Width / 2 -
                width / 2), -position.Y + (Screen.Height - height * 2)));

            //プレイヤーのX座標が画面の中心より左なら見切れているので、Vector2.Zeroで設定しなおす
            if (position.X < (Screen.Width / 2) - width / 2)
            {
                gameDevice.SetDisplayModify(new Vector2(0, -position.Y + (Screen.Height - height * 2)));
            }
            if (position.X > (mediator.MapSize().X) - (Screen.Width / 2 + width / 2))
            {
                gameDevice.SetDisplayModify(new Vector2(-mediator.MapSize().X + Screen.Width, -position.Y + (Screen.Height - height * 2)));
            }
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify(),Color.White * alpha);
            gauge.Draw(renderer);
        }

        public void Flash()
        {
            if (flashPlayer)
            {
                alpha = 0.5f;
            }
            else
            {
                alpha = 1.0f;
            }
        }

        private void Shot()
        {
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isShot = true;
            }
        }

        private void NowShot()
        {
            if (!isShot)
                return;

            shotInterval++;

            if(shotInterval / 20.0f == 1)
            {
                shotCount++;
                shotInterval = 0;
                if (right == -1)
                {
                    if (shotCount == 1 || shotCount == 2)
                    {
                        mediator.AddGameObject(new PlayerBullet(new Vector2(position.X - 10, position.Y + 5),360,240, right, gameDevice, mediator));
                        //mediator.AddGameObject(new BossBomb(new Vector2(position.X - 10, position.Y + 5),speed, right, gameDevice));
                    }
                    else if(shotCount == 3)
                    {
                        mediator.AddGameObject(new PlayerFinalBullet(new Vector2(position.X - 10, position.Y + 5),360,240, right, gameDevice, mediator));
                    }
                }

                else if(right == 1)
                {
                    //mediator.AddGameObject(new PlayerBullet(new Vector2(position.X + width, position.Y + 6), right, gameDevice, mediator));
                    //mediator.AddGameObject(new BossBomb(new Vector2(position.X + width, position.Y + 6),speed * 2, right, gameDevice));

                    if (shotCount == 1 || shotCount == 2)
                    {
                        mediator.AddGameObject(new PlayerBullet(new Vector2(position.X + width, position.Y + 6),120,240, right, gameDevice, mediator));
                        //mediator.AddGameObject(new BossBomb(new Vector2(position.X + width, position.Y + 6),speed , right, gameDevice));
                    }
                    else if (shotCount == 3)
                    {
                        mediator.AddGameObject(new PlayerFinalBullet(new Vector2(position.X +width, position.Y + 6),120,240, right, gameDevice, mediator));
                    }
                }
            }

            if (shotCount == 3)
            {
                shotCount = 0;
                isShot = false;
            }
        }
    }
}
