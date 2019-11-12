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



        public Player(Vector2 position,GameDevice gameDevice)
            :base("TankRight",position,64,64)
        {
            position = new Vector2(100, 100);
            velocity = Vector2.Zero;

        }

        public override void Hit(Character other)
        {
            
        }

        public override void Initialize()
        {

        }

        public override void Shutdown()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            float speed = 4.0f;

            velocity = Input.Velocity() * speed;

            //位置の計算
            position = position + velocity;
        }
    }
}
