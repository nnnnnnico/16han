using Game1.Device;
using Game1.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Actor
{
    class Map
    {
        private List<List<Character>> mapList;
        //ListのListで縦横の2次元配列を表現
        private GameDevice gameDevice;

        public Map(GameDevice gameDevice)
        {
            mapList = new List<List<Character>>();
            this.gameDevice = gameDevice;
        }

        private List<Character> addBlock(int lineCnt, string[] line)
        {
            //コピー元オブジェクト登録用でディクショナリ
            Dictionary<string, Character> objectDict = new Dictionary<string, Character>();
            objectDict.Add("0", new Space(Vector2.Zero, gameDevice));
            objectDict.Add("1", new Block(Vector2.Zero, gameDevice));

            //スペースは０


            //作業用リスト
            List<Character> workList = new List<Character>();

            int colCnt = 0;
            //渡された1行から１つずつ作業リストに登録
            foreach (var s in line)
            {
                try
                {
                    //ディクショナリから元データを取り出し、クローン機能で複製
                    Character work = (Character)objectDict[s].Clone();
                    work.SetPosition(new Vector2(colCnt * work.GetHeight(),
                        lineCnt * work.GetWidth()));
                    workList.Add(work);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                //列カウンターを増やす
                colCnt += 1;
            }
            return workList;
        }

        public void Load(string filename, string path = "./")
        {
            CSVReader csvReader = new CSVReader();
            csvReader.Read(filename, path);

            var data = csvReader.GetData();//List<string[]>型で取得

            //1行ごとmapListに追加していく
            for (int lineCnt = 0; lineCnt < data.Count(); lineCnt++)
            {
                mapList.Add(addBlock(lineCnt, data[lineCnt]));
            }
        }
        public void Unload()
        {
            mapList.Clear();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var list in mapList)
            {
                foreach (var obj in list)
                {
                    if (obj is Space)
                    {
                        continue;
                    }
                    obj.Update(gameTime);
                }
            }
        }

        public void Hit(Character gameObject)
        {
            Point work = gameObject.GetRectangle().Location;

            int x = work.X / 64;
            int y = work.Y / 64;

            if (x < 1)
            {
                x = 1;
            }
            if (y < 1)
            {
                y = 1;
            }

            Range yRange = new Range(0, mapList.Count() - 1);//行の範囲
            Range xRange = new Range(0, mapList[0].Count() - 1);//列の範囲

            for (int row = y - 1; row <= (y + 1); row++)
            {
                for (int col = x - 1; col <= (x + 1); col++)
                {
                    //配列外なら何もしない
                    if (xRange.IsOutOfRange(col) || yRange.IsOutOfRange(row))
                    {
                        continue;
                    }
                    //その場所のオブジェクトを取得
                    Character obj = mapList[row][col];

                    //objがSpaceクラスのオブジェクトなら次へ
                    if (obj is Space)
                    {
                        continue;
                    }

                    //衝突判定
                    if (obj.IsCollision(gameObject))
                    {
                        gameObject.Hit(obj);
                    }
                }
            }
        }

        public void Draw(Renderer renderer)
        {
            foreach (var List in mapList)
            {
                foreach (var obj in List)
                {
                    obj.Draw(renderer);
                }
            }
        }

        public int GetWidth()
        {
            int col = mapList[0].Count;
            int width = col * mapList[0][0].GetWidth();
            return width;
        }

        public int GetHeight()
        {
            int row = mapList.Count();
            int height = row * mapList[0][0].GetHeight();
            return height;
        }
    }
}
