using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 書いてない
using Microsoft.Xna.Framework;

using Game1.Device;

namespace Game1.Scene
{
    class LoadScene : IScene
    {
        // フィールド
        // レンダラー
        private Renderer renderer;
        // 読み込み用オブジェクト
        private TextureLoader textureLoader;
        private BGMLoader bgmLoader;
        private SELoader seLoader;

        // 全リソース数
        private int totalResourceNum;
        // 終了フラグ
        private bool isEndFlag;
        //// タイマー
        //private Timer timer;

        //使いたい画像あったらここでロードして使ってくれ
        #region テクスチャ用
        /// <summary>
        /// テクスチャ読み込み用2次元配列の取得
        /// </summary>
        /// <returns>アセット名の配列</returns>
        private string[,] textureMatrix()
        {
            // テクスチャディレクトリのデフォルトパス
            string path = "./Texture/";
            string pathAnimation = "./Animation/";

            // 読み込み対象データ
            string[,] data = new string[,]
            {
                {"Boss()",path },
                {"TankRight",path },
                {"TankLeft",path },
                {"Bullet16",path },
                {"gauge",path },
                {"pixel",path },
                {"Block",path },
                {"block1",path },
                {"BossLeft",path },
                {"BossRight",path },
                {"bg2",path },
                {"Title",path },
                {"Ending",path },
                {"PBullet",path },
                {"EBullet",path },
                {"EBomb",path },
                {"pipo-btleffect022",pathAnimation },
                //{"pipo-btleffect015",pathAnimation},
                //{"pipo-btleffect001",pathAnimation },
                //{"pipo-btleffect002",pathAnimation },
                {"pipo-btleffect003",pathAnimation },
                //{"pipo-btleffect004",pathAnimation },
                //{"pipo-btleffect007",pathAnimation },
                {"pipo-btleffect008",pathAnimation },
                //{"pipo-btleffect009",pathAnimation },
                //{"pipo-btleffect012",pathAnimation},
                //{"pipo-btleffect013",pathAnimation },
                //{"pipo-btleffect014",pathAnimation },
                //{"pipo-btleffect016",pathAnimation },
                //{"pipo-btleffect017",pathAnimation},
                //{"pipo-btleffect018",pathAnimation},
                //{"pipo-btleffect019",pathAnimation },
                //{"pipo-btleffect020",pathAnimation },
                //{"pipo-btleffect021",pathAnimation },
                //{"pipo-btleffect023",pathAnimation },
                //{"pipo-btleffect024",pathAnimation},
                //{"pipo-btleffect025",pathAnimation },
                //{"pipo-btleffect026",pathAnimation},
                //{"pipo-btleffect027",pathAnimation },
                //{"pipo-btleffect028",pathAnimation },
                //{"pipo-btleffect029",pathAnimation },
                {"pipo-btleffect030",pathAnimation },
                //{"pipo-btleffect031",pathAnimation},
                //{"pipo-btleffect032",pathAnimation },
                //{"pipo-btleffect033",pathAnimation },
                //{"pipo-btleffect034",pathAnimation },
                //{"pipo-btleffect035",pathAnimation},
                {"frameeffects005s",pathAnimation},
                {"pipo-mapeffect005",pathAnimation},
                
            };

            return data;
        }
        #endregion テクスチャ用

        #region BGM用
        /// <summary>
        /// BGM読み込み用2次元配列の取得
        /// </summary>
        /// <returns>アセット名の配列</returns>
        private string[,] BGMMatrix()
        {
            // サウンドディレクトリのデフォルトパス
            string path = "./Sound/";
            // BGM読み込み対象データ
            string[,] data = new string[,]
            {
                //ここから下追加
                {"bgm_maoudamashii_cyber44",path },
                {"bgm_maoudamashii_cyber45" ,path},
                {"bgm_maoudamashii_neorock70",path },
                {"bgm_maoudamashii_neorock72",path },
                {"bgm_maoudamashii_neorock81",path },
                {"bgm_maoudamashii_healing17",path },
                //ここから上追加
            };

            return data;
        }
        #endregion BGM用

        #region SE用
        /// <summary>
        /// SE読み込み用2次元配列の取得
        /// </summary>
        /// <returns>アセット名の配列</returns>
        private string[,] SEMatrix()
        {
            // SEディレクトリのデフォルトパス
            string path = "./Sound/";

            // SE読み込み対象データ
            string[,] data = new string[,]
            {
                {"battery1" ,path},
                {"beamgun1",path },
                {"bomb",path },
                {"cannon2",path },
                {"launcher1",path },
                {"launcher3",path },
                {"launcher6",path },
                {"ricochets1",path },
                {"running-tank-1",path },
                {"short_bomb",path },
                {"shot1",path },
                {"small_explosion2",path},
                {"kick1",path},
            };

            return data;
        }
        #endregion SE用

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LoadScene()
        {
            // レンダラー取得
            renderer = GameDevice.Instance().GetRenderer();
            // 読み込む対象を取得し、実体生成
            textureLoader = new TextureLoader(textureMatrix());
            bgmLoader = new BGMLoader(BGMMatrix());
            seLoader = new SELoader(SEMatrix());
            isEndFlag = false;

            //// タイマー生成
            //timer = new CountDownTimer(0.1f);
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        public void Draw(Renderer renderer)
        {
            // Begin
            renderer.Begin();

            //renderer.DrawTexture("load", new Vector2(20, 20));

            // 現在読み込んでいる数を取得
            int currentCount =
            textureLoader.CurrentCount() +
            bgmLoader.CurrentCount() +
            seLoader.CurrentCount();

            // 読み込むものがあれば描画
            //if (totalResourceNum != 0)
            //{
            //    // 読みこんだ割合
            //    float rate = (float)currentCount / totalResourceNum;
            //    // 数字で描画
            //    renderer.DrawNumber(
            //        "number",
            //        new Vector2(20, 100),
            //        (int)(rate * 100.0f));

            //    // バーで描画
            //    renderer.DrawTexture(
            //        "fade",
            //        new Vector2(0, 500),
            //        null,
            //        0.0f,
            //        Vector2.Zero,
            //        new Vector2(rate * Screen.Width, 20));
            //}

            // 終了
            // すべてのデータを読み込んだか？
            if (textureLoader.IsEnd() &&
            bgmLoader.IsEnd() &&
            seLoader.IsEnd())
            {
                isEndFlag = true;
            }

            // End
            renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            // 終了フラグを継続に設定
            isEndFlag = false;
            // テクスチャ読み込みオブジェクトを初期化
            textureLoader.Initialize();
            // BGM読み込みオブジェクトを初期化
            bgmLoader.Initialize();
            // SE読み込みオブジェクトを初期化
            seLoader.Initialize();
            // 全リソース数を計算
            totalResourceNum =
            textureLoader.RegistMAXNum() +
            bgmLoader.RegistMAXNum() +
            seLoader.RegistMAXNum();
        }

        /// <summary>
        /// シーン終了かどうか
        /// </summary>
        /// <returns>シーン終了ならtrue</returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }

        /// <summary>
        /// 次のシーン
        /// </summary>
        /// <returns>次のシーン</returns>

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Shutdown()
        {

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            // 演出確認用
            // 一定時間ごとに読み込み
            //timer.Update(gameTime);
            // 時間になっていない時
            //if (timer.IsTime() == false)
            //{
            //    // 終了
            //    return;
            //}
            // 時間になっている時、初期化
            //timer.Initialize();

            // テクスチャから順々に読み込みを行う
            if (textureLoader.IsEnd() == false)
            {
                textureLoader.Update(gameTime);
            }
            else if (bgmLoader.IsEnd() == false)
            {
                bgmLoader.Update(gameTime);
            }
            else if (seLoader.IsEnd() == false)
            {
                seLoader.Update(gameTime);
            }
        }

        public SceneName Next()
        {
            return SceneName.GameTitle;
        }
    }
}
