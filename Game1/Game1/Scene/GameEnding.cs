using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Device;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.Scene
{
    class GameEnding : IScene
    {
        private bool isEndFlag;
        private Sound sound;
        private GameDevice gameDevice;

        public GameEnding()
        {
            isEndFlag = false;
            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("Ending", new Vector2());
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
            return SceneName.GameTitle;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("bgm_maoudamashii_healing17");
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
            }
        }
    }
}
