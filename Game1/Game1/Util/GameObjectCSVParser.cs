//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Game1.Actor;
//using Game1.Device;
//using Microsoft.Xna.Framework;
//using System.Diagnostics;

//namespace Action.Util
//{
//    /// <summary>
//    /// ゲームオブジェクトがまとめられたCSVでーたの解析
//    /// </summary>
//    class GameObjectCSVParser
//    {
//        private CSVReader csvReader;//CSV読み込み用オブジェクト
//        private List<GameObject> gameObjects;//ゲームオブジェクトのリスト

//        //デリゲート宣言（メソッドを変数に保存するための型宣言）
//        //戻り値の型がGameObject、引数はList<string>のメソッドを
//        //保存できるiFunction型を宣言
//        private delegate GameObject iFunction(List<string> data);

//        //文字列とiFunction型をディクショナリで保存
//        private Dictionary<string, iFunction> functionTable;

//        private IGameObjectMediator mediator;

//        public GameObjectCSVParser(IGameObjectMediator mediator)
//        {
//            //仲介者の設定
//            this.mediator = mediator;

//            csvReader = new CSVReader();

//            gameObjects = new List<GameObject>();

//            functionTable = new Dictionary<string, iFunction>();

//            //ディクショナリにデータを追加
//            //文字列はクラス名の文字列と実行用メソッド名
//            functionTable.Add("SlidingBlock", NewSlidingBlock);
//            functionTable.Add("Block", NewBlock);
//            functionTable.Add("ChaseEnemy", NewChaseEnemy);
//        }

//        /// <summary>
//        /// 解析
//        /// </summary>
//        /// <param name="filename"></param>
//        /// <param name="path"></param>
//        /// <returns></returns>
//        public List<GameObject> Parse(string filename, string path = "./")
//        {
//            gameObjects.Clear();

//            csvReader.Read(filename, path);
//            var data = csvReader.GetData();

//            foreach (var line in data)
//            {
//                if (line[0] == "#")
//                {
//                    continue;
//                }
//                if (line[0] == "")
//                {
//                    continue;
//                }

//                //空白文字削除処理
//                var temp = line.ToList();//配列からリストへ
//                temp.RemoveAll(s => s == "");//List内にある空文字を削除

//                //ゲームオブジェクトリストに解析後作られたゲームオブジェクトを追加
//                gameObjects.Add(functionTable[line[0]](temp));
//            }

//            return gameObjects;
//        }

//        /// <summary>
//        /// 移動ブロックの解析と生成
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        private SlidingBlock NewSlidingBlock(List<string> data)
//        {
//            Debug.Assert(
//                (data.Count >= 5) || (data.Count <= 7),
//                "CSVデータを確認してください。");

//            if (data.Count == 5)//移動量なし版
//            {
//                return new SlidingBlock(
//                    new Vector2(float.Parse(data[1]), float.Parse(data[2])) *
//                      32,
//                    new Vector2(float.Parse(data[3]), float.Parse(data[4])) *
//                      32,
//                    GameDevice.Instance());
//            }
//            else if (data.Count == 7)//移動量あり版
//            {
//                return new SlidingBlock(
//                    new Vector2(float.Parse(data[1]), float.Parse(data[2])) * 32,
//                    new Vector2(float.Parse(data[3]), float.Parse(data[4])) * 32,
//                    new Vector2(float.Parse(data[5]), float.Parse(data[6])),
//                    GameDevice.Instance());
//            }

//            //IDアリ
//            else if (data.Count == 6)
//            {
//                SlidingBlock tempObj = new SlidingBlock(
//                    new Vector2(float.Parse(data[1]), float.Parse(data[2])) * 32,
//                    new Vector2(float.Parse(data[3]), float.Parse(data[4])) * 32,
//                    GameDevice.Instance());
//                tempObj.SetID(stringToGameObjectID_IF(data[5]));

//                return tempObj;
//            }
//            else if (data.Count == 8)
//            {
//                SlidingBlock tempObj = new SlidingBlock(
//                    new Vector2(float.Parse(data[1]), float.Parse(data[2])) * 32,
//                    new Vector2(float.Parse(data[3]), float.Parse(data[4])) * 32,
//                    new Vector2(float.Parse(data[5]), float.Parse(data[6])),
//                    GameDevice.Instance());
//                tempObj.SetID(stringToGameObjectID_IF(data[7]));

//                return tempObj;
//            }


//            return null;
//        }

//        private Block NewBlock(List<string> data)
//        {
//            Debug.Assert(
//                (data.Count == 3),
//                "CSVデータを確認してください。");

//            return new Block(
//                new Vector2(float.Parse(data[1]), float.Parse(data[2])) *
//                32,
//                GameDevice.Instance());
//        }

//        private ChaseEnemy NewChaseEnemy(List<string> data)
//        {
//            Debug.Assert(
//                (data.Count == 3),
//                "CSVデータを確認してください。");

//            return new ChaseEnemy(
//                new Vector2(float.Parse(data[1]), float.Parse(data[2])) * 32,
//                GameDevice.Instance(),
//                mediator);
//        }


//        private GameObjectID stringToGameObjectID_IF(string stringID)
//        {
//            //Player,
//            //Enemy01,
//            //Enemy02,
//            //SlidingBlock_A,
//            //SlidingBlock_B,
//            //SlidingBlock_C,
//            //Button_A,
//            //Button_B,
//            //Door_A,
//            //Door_B,
//            //NONE,
//            if (stringID == "Player")
//            {
//                return GameObjectID.Player;
//            }
//            else if (stringID == "Enemy01")
//            {
//                return GameObjectID.Enemy01;
//            }
//            else if (stringID == "Enemy02")
//            {
//                return GameObjectID.Enemy02;
//            }
//            else if (stringID == "SlidingBlock_A")
//            {
//                return GameObjectID.SlidingBlock_A;
//            }
//            else if (stringID == "SlidingBlock_B")
//            {
//                return GameObjectID.SlidingBlock_B;
//            }
//            else if (stringID == "SlidingBlock_C")
//            {
//                return GameObjectID.SlidingBlock_C;
//            }
//            else if (stringID == "Button_A")
//            {
//                return GameObjectID.Button_A;
//            }
//            else if (stringID == "Button_B")
//            {
//                return GameObjectID.Button_B;
//            }
//            else if (stringID == "Door_A")
//            {
//                return GameObjectID.Door_A;
//            }
//            else if (stringID == "Door_B")
//            {
//                return GameObjectID.Door_B;
//            }
//            return GameObjectID.NONE;


//        }
//    }
//}
