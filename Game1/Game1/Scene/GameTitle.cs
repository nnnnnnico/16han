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
        private Sound sound;
        private GameDevice gameDevice;

        public GameTitle()
        {
            isEndFlag = false;
            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }


        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("Title", new Vector2());
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
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
            sound.PlayBGM("bgm_maoudamashii_neorock81");
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
            }
        }
    }
}
