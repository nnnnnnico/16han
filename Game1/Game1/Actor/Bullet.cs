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
        private GameDevice gameDevice;
        Vector2 velocity;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position"></param>
        /// <param name="gameDevice"></param>
        public Bullet(Vector2 position,GameDevice gameDevice)
            :base ("Bullet16",position,16,16)
        {
          
            //velocity = Vector2.Zero;
        }

      

        public override void Update(GameTime gameTime)
        {
            // float speed = 4.0f;

            //position = new Vector2(300, 300);
            

          //  velocity = Input.Velocity() * speed;

            //位置の計算
           // position = position + velocity;


           

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

       

      
    }
}
