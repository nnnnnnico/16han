using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Device;
using Microsoft.Xna.Framework;

namespace Game1.Actor
{
    class Block : Character
    {

        public Block(Vector2 positon,GameDevice gameDevice)
            : base("block1", positon, 64, 64,gameDevice)
        {
        }

        public Block(Block other)
            :this(other.position,other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new Block(this);
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
        }
    }
}
