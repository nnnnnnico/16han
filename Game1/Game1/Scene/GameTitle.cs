using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Game1.Device;
using Microsoft.Xna.Framework.Input;
using Game1.Actor;

namespace Game1.Scene
{
    class GameTitle : IScene
    {
        private bool isEndFlag;
        public Player player;
        private GameDevice gameDevice;

        public GameTitle()
        {
            isEndFlag = false;
            gameDevice = GameDevice.Instance();
            player = new Player(new Vector2(100, 100), gameDevice);

        }


        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            player.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
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
            player.Update(gameTime);
        }
    }
}
