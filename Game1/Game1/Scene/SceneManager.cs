using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game1.Def;
//using Volon.Actor;
using Game1.Device;
using Game1.Scene;
using Game1.Static;

using Microsoft.Xna.Framework;

namespace Game1.Scene
{
    class SceneManager
    {
        // フィールド
        // シーン管理用ディクショナリ
        private Dictionary<SceneName, IScene> scenes =
            new Dictionary<SceneName, IScene>();
        // 現在のシーン
        //public static IScene currentScene = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SceneManager()
        {

        }

        /// <summary>
        /// シーンの追加
        /// </summary>
        /// <param name="name">シーン名</param>
        /// <param name="scene">追加するシーン</param>
        public void Add(SceneName name, IScene scene)
        {
            // 既にシーン名が登録されていたら
            if (scenes.ContainsKey(name))
            {
                // 何もしない
                return;
            }

            // シーンの追加
            scenes.Add(name, scene);
        }

        public void Update(GameTime gameTime)
        {
            // 今のシーンが空だったら
            if (NowCurrentScene.currentScene == null)
            {
                // 何もせず終了
                return;
            }

            // 今のシーンの更新処理
            NowCurrentScene.currentScene.Update(gameTime);

            // 今のシーンが終了していたら
            if (NowCurrentScene.currentScene.IsEnd())
            {
                // 次のシーンに変更
                Change(NowCurrentScene.currentScene.Next());
            }
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        public void Draw(Renderer renderer)
        {
            // 今のシーンが空だったら
            if (NowCurrentScene.currentScene == null)
            {
                // 何もせず終了
                return;
            }

            // 今のシーンの描画処理
            NowCurrentScene.currentScene.Draw(renderer);
            //Console.WriteLine("NowCurrentScene.currentScene = " + NowCurrentScene.currentScene);
        }

        public void Change(SceneName name)
        {
            //何かシーンが登録されていたら
            if (NowCurrentScene.currentScene != null)
            {
                //現在のシーンの終了処理
                NowCurrentScene.currentScene.Shutdown();
            }

            //ディクショナリから次のシーンを取り出し、
            //現在のシーンに設定
            NowCurrentScene.currentScene = scenes[name];

            //シーンの初期化
            NowCurrentScene.currentScene.Initialize();
        }

        //public SceneName GetCurrentScene()
        //{
        //    return SceneName.GamePlay;
        //}

        //public IScene GetCurrentScene()
        //{
        //    if (currentScene == null)
        //    {
        //        return null;
        //    }
        //    return currentScene;
        //}


        public void Initialize()
        {

        }

        public void Shutdown()
        {
        }

        public bool IsEnd()
        {
            throw new NotImplementedException();
        }

        public SceneName Next()
        {
            return SceneName.GamePlay;
        }
    }
}
