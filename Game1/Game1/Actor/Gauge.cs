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
        string bgTexture;   //アセット名
        string pixel;       //1ピクセルの画像名
        float width;
        float height;
        float maxHp;        //マックスHP
        public float currentHp;//現在のHP
        public float startHp;
        float gaugeWidth;        //ゲージの横幅
        Rectangle bound;    //
        Color color;        //色

        public Gauge(string BgTexture, string Pixel,float Width,float Height,Rectangle Bound,float StartHp,float MaxHp,float GaugeWidth,Color Color)
        {
            bgTexture = BgTexture;
            pixel = Pixel;
            width = Width;
            height = Height;
            bound = Bound;
            gaugeWidth = GaugeWidth;
            maxHp = MaxHp;
            currentHp = StartHp;
            color = Color;
        }
        public void Draw(Renderer renderer)
        {
            int gaugee = (int)((currentHp / maxHp) * gaugeWidth);
            renderer.DrawTexture(pixel,new Vector2(width,height), new Rectangle(bound.X, bound.Y, gaugee, bound.Height), color);
            renderer.DrawTexture(bgTexture,new Vector2(width,height), bound, Color.White);
        }

        public void Update()
        {
        }

        public void ThisNum(int num)
        {
            currentHp = num;
        }
    }
}
