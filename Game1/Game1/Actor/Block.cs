using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Device;
using Microsoft.Xna.Framework;
using Game1.Static;

namespace Game1.Actor
{
    class Block : Character
    {
        private float alpha;

        public Block(Vector2 positon,GameDevice gameDevice)
            : base("block1", positon, 64, 64,gameDevice)
        {
            alpha = 1.0f;
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
            if (PlayerInvisibleMode.isInvisibleMode)
                alpha = 0.5f;
            else
                alpha = 1.0f;
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify(), Color.White * alpha);
        }
    }
}
