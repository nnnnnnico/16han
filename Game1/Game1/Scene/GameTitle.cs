using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Game1.Device;
using Microsoft.Xna.Framework.Input;
using Game1.Actor;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Scene
{
    class GameTitle : IScene
    {
        private bool isEndFlag;
        //public Player player;
        //public Boss boss;
        //private GameDevice gameDevice;
        //public Gauge Gauge;
        //private int width;

        public GameTitle()
        {
            isEndFlag = false;
            //gameDevice = GameDevice.Instance();
            //player = new Player(new Vector2(100, 100), gameDevice);
            //boss = new Boss(new Vector2(400, 400), gameDevice);
            //Rectangle bound = new Rectangle(100, 100, width, 50);
            //width = 350;
            //Gauge = new Gauge("gauge", "pixel", bound, 100, 100, width, Color.LightGreen);
        }


        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            //boss.Draw(renderer);
            //player.Draw(renderer);
            //Gauge.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            //boss.Initialize();
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public SceneName Next()
        {
            return SceneName.GamePlay;
        }

        public void Shutdown()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
            }
            //player.Update(gameTime);
            //boss.Update(gameTime);
            //Gauge.Update();
        }
    }
}
