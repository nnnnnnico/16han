using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Device;
using Microsoft.Xna.Framework;

namespace Game1.Actor
{
    class Space : Character
    {

        public Space(Vector2 position,GameDevice gameDevice)
            : base("", position, 64, 64,gameDevice)
        {

        }

        public Space(Space other)
            : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new Space(this);
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

        public override void Draw(Renderer renderer)
        {
           
        }
    }
}
