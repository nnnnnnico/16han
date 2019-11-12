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
        private int count;
        private List<Bullet> bulletList;
        private int Hp;
        public Boss(Vector2 position,GameDevice gameDevice)
            : base("Boss()",position ,128, 128,gameDevice)
        {
            bulletList = new List<Bullet>();
        }

        public Boss(Boss other)
            : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new Boss(this);
        }

        public override void Hit(Character other)
        {
            Hp = Hp - 2;
        }

        public override void Initialize()
        {
            bulletList.Clear();
            Hp = 100;
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var bullet in bulletList)
            {
                bullet.Update(gameTime);
            }
            if (count == 0)
            {
                position.X++;
                Attack1();
            }
            if (count == 1)
            {
                position.X--;
                Attack2();
            }
            if (count == 2)
            {
                Attack3();
                count = 0;
            }
            if (Input.GetKeyTrigger(Keys.X) || Input.GetKeyTrigger(Keys.Z))
            {
                count++;
            }
            //bulletList.RemoveAll(a => a.IsDead() == true);
        }
        //右に弾発射
        public void Attack1()
        {
            bulletList.Add(new Bullet(new Vector2(position.X+128,position.Y+64), gameDevice));
        }
        //左に弾を発射
        public void Attack2()
        {
            bulletList.Add(new Bullet(new Vector2(position.X, position.Y + 64), gameDevice));
        }

        public void Attack3()
        {

        }

        public override void Draw(Renderer renderer)
        {
            foreach(var bullet in bulletList)
            {
                bullet.Draw(renderer);
            }
            renderer.DrawTexture("Boss()", position);
        }

    }
}
