using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Actor;
using Game1.Device;
using Game1.Static;
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
        private float alpha;
        private int pictureNum = 10;
        private int time;
        private int count;

        private Sound sound;

        public GamePlay()
        {
            isEndFlag = false;
            gameDevice = GameDevice.Instance();
            characterManager = new CharacterManager();
            alpha = 1.0f;
            count = 0;
            sound = gameDevice.GetSound();
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("bg2", new Vector2(), Color.White * alpha);
            renderer.DrawTexture("BossLeft", Vector2.Zero);
            map.Draw(renderer);
            characterManager.Draw(renderer);
            if (PlayerInvisibleMode.isInvisibleMode)
            {
                renderer.DrawTexture("frameeffects005s", new Vector2(0,0), new Rectangle(0, count * 240, 320, 240), new Vector2(4, 4));
            }
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            PlayerInvisibleMode.isInvisibleMode = false;

            characterManager.Initialize();

            map = new Map(GameDevice.Instance());
            map.Load("map.csv","./csv/");

            boss = new Boss(new Vector2(400, 513), GameDevice.Instance(),characterManager);
            player = new Player(new Vector2(100, 720), GameDevice.Instance(),characterManager);

            characterManager.Add(player);
            characterManager.Add(boss);
            characterManager.Add(map);

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
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            characterManager.Update(gameTime);
            map.Update(gameTime);

            FrameAnim();

            sound.PlayBGM("bgm_maoudamashii_neorock72");

            if (PlayerInvisibleMode.isInvisibleMode)
            {
                alpha = 0.5f;
                sound.PauseBGM();
            }
            else
            {
                alpha = 1.0f;
                sound.ResumeBGM();
            }

            if (characterManager.IsPlayerDead())
            {
                isEndFlag = true;
            }
            if (characterManager.IsBossDead())
            {
                isEndFlag = true;
            }
        }

        public void FrameAnim()
        {
            if (!PlayerInvisibleMode.isInvisibleMode)
                return;

            time++;

            //指定時間か?
            if (time / 5.0f == 1)
            {
                time = 0;
                count += 1;
                //初期化
                if (count >= pictureNum)
                {
                    count = 0;
                }
            }
        }
    }
}
