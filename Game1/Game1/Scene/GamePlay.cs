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
        public Player player;
        public Boss boss;
        private CharacterManager characterManager;
        private GameDevice gameDevice;

        public GamePlay()
        {
            isEndFlag = false;
            gameDevice = GameDevice.Instance();
            characterManager = new CharacterManager(); 
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            map.Draw(renderer);
            characterManager.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;

            characterManager.Initialize();

            map = new Map(GameDevice.Instance());
            map.Load("map.csv","./csv/");

            boss = new Boss(new Vector2(400, 480), GameDevice.Instance(),characterManager);
            player = new Player(new Vector2(200, 200), GameDevice.Instance(),characterManager);

            characterManager.Add(map);
            characterManager.Add(player);
            characterManager.Add(boss);

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
            characterManager.Update(gameTime);
            map.Update(gameTime);
        }
    }
}
