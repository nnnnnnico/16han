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
        private int invisibleCount;

        public Gauge gauge;
        public Gauge invisibleGauge;

        public ScreenGray gray;
        private float grayScale;
        private float grayRotate;
        private float grayAlpha;

        int hp;
        float alpha;

        private Sound sound;

        public Player(Vector2 position,GameDevice gameDevice,IGameMediator mediator)
            :base("TankRight",position,64,64,gameDevice)
        {
            position = new Vector2(650, 128);
            this.mediator = mediator;
            velocity = Vector2.Zero;
            hp = 1000;
            gravity = 10;
            isGround = false;
            escape = false;
            alpha = 1.0f;
            flashPlayer = false;
            right = 1;
            isShot = false;
            invisibleCount = 540;

            Rectangle bound = new Rectangle(100, 100, 0, 40);
            Rectangle invBound = new Rectangle(100, 100, 0, 20);
            gauge = new Gauge("gauge", "pixel",0,660,bound, hp,hp, 350, Color.LightGreen);
            invisibleGauge = new Gauge("gauge", "pixel", 0, 700, invBound, invisibleCount, invisibleCount,350, Color.LightBlue);


            grayScale = 1.0f;
            grayAlpha = 0.5f;
            grayRotate = 0.0f;
            gray = new ScreenGray(position, grayScale, grayAlpha, grayRotate);

            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
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
            if (!PlayerInvisibleMode.isInvisibleMode)
            {
                if (other is Boss)
                {
                    if (!escape)
                    {
                        if (!hitDirect)
                        {
                            sound.PlaySE("kick1");
                            hitDirect = true;
                            hp -= 50;
                        }
                    }
                }
                if (other is Bullet)
                {
                    sound.PlaySE("kick1");
                    hp -= 30;
                }
                if (other is BossBomb)
                {
                    hp -= 80;
                }
                if (other is BombEffect)
                {
                    hp -= 1;
                }
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
            gauge.ThisNum(hp);
            invisibleGauge.ThisNum(invisibleCount);
            velocity = Input.Velocity() * speed;

            //位置の計算
            position.X = position.X + velocity.X;
            position.X = MathHelper.Clamp(position.X,0, mediator.MapSize().X - width);

            Invisible();
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
            if (!PlayerInvisibleMode.isInvisibleMode)
            {
                if(invisibleCount <= 540)
                {
                    invisibleCount++;
                }
                return;
            }

            invisibleCount -= 3;
            if(invisibleCount <= 0)
            {
                PlayerInvisibleMode.isInvisibleMode = false;
            }
        }

        private void Invisible()
        {
            if (Input.GetKeyTrigger(Keys.Z)&&invisibleCount >= 540)
            {
                PlayerInvisibleMode.isInvisibleMode = true;
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
            invisibleGauge.Draw(renderer);
            gray.Draw(renderer);
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
                        sound.PlaySE("short_bomb");
                        mediator.AddGameObject(new PlayerBullet(new Vector2(position.X - 10, position.Y + 5), right, gameDevice, mediator));
                    }
                    else if(shotCount == 3)
                    {
                        sound.PlaySE("bomb");
                        mediator.AddGameObject(new PlayerFinalBullet(new Vector2(position.X - 10, position.Y + 5),right, gameDevice, mediator));
                    }
                }

                else if(right == 1)
                {
                    if (shotCount == 1 || shotCount == 2)
                    {
                        sound.PlaySE("short_bomb");
                        mediator.AddGameObject(new PlayerBullet(new Vector2(position.X + width, position.Y + 6), right, gameDevice, mediator));
                    }
                    else if (shotCount == 3)
                    {
                        sound.PlaySE("bomb");
                        mediator.AddGameObject(new PlayerFinalBullet(new Vector2(position.X +width, position.Y + 6),right, gameDevice, mediator));
                    }
                }
            }

            if (shotCount == 3)
            {
                shotCount = 0;
                isShot = false;
            }
        }

        private void Gray()
        {
            grayScale += 1.0f;

        }
    }
}
