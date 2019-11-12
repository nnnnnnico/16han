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
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position"></param>
        /// <param name="gameDevice"></param>
        /// <param name="loadScene"></param>
        public Bullet(Vector2 position,GameDevice gameDevice, LoadScene loadScene)
            :base ("Bullet16",position,64,64)
        {

        }
        public override void Update(GameTime gameTime)
        {

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
