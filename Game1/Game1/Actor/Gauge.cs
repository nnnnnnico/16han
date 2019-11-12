using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Device;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game1.Actor
{
    class Gauge
    {
        string bgTexture;
        string pixel;
        float maxHp;
        public float currentHp;
        float width;
        Rectangle bound;
        Color color;

        public Gauge(string BgTexture, string Pixel,Rectangle Bound,float StartHp,float MaxHp,float Width,Color Color)
        {
            bgTexture = BgTexture;
            pixel = Pixel;
            bound = Bound;
            width = Width;
            maxHp = MaxHp;
            currentHp = StartHp;
            color = Color;
        }
        public void Draw(Renderer renderer)
        {
            int gaugee = (int)((currentHp / maxHp) * width);
            renderer.DrawTexture(pixel, new Rectangle(bound.X, bound.Y, gaugee, bound.Height), color);
            renderer.DrawTexture(bgTexture, bound, Color.White);
        }

        public void Update()
        {
            currentHp--;
        }
    }
}
