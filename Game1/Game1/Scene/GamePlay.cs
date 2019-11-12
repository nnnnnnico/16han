using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Actor;
using Game1.Device;
using Microsoft.Xna.Framework;

namespace Game1.Scene
{
    class GamePlay : IScene
    {
        private bool isEndFlag;
        private Map map;
        private Player player;  

        public GamePlay()
        {
            isEndFlag = false;
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            map.Draw(renderer);
            player.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;

            map = new Map(GameDevice.Instance());
            //map.Load("map.csv","./csv/");

            player = new Player(new Vector2(200, 200), GameDevice.Instance());

        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public SceneName Next()
        {
            return SceneName.GameEnding;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {
            map.Update(gameTime);
        }
    }
}
