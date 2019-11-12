using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Device
{
    class CSVReader
    {
        private List<string[]> stringData;
        public CSVReader()
        {
            stringData = new List<string[]>();
        }
        public void Clear()
        {
            //リストのクリア
            stringData.Clear();
        }
        /// <summary>
        /// CSVファイルの読み込み
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="path"></param>
        public void Read(string filename, string path = "./")
        {
            //リストのクリア
            stringData.Clear();
            //例外処理
            try
            {
                //CSVファイルを開く
                using (var sr = new System.IO.StreamReader(@"Content/" + path + filename))
                {
                    //  ストリームの末尾まで繰り返す
                    while (!sr.EndOfStream)
                    {
                        //１行読み込む
                        var line = sr.ReadLine();
                        //カンマごとに分けて配列に格納する
                        var values = line.Split(',');//文字のカンマ

                        //リストに読み込んだ１行を追加
                        stringData.Add(values);

#if DEBUG
                        //出力する
                        foreach (var v in values)
                        {
                            System.Console.Write("{0}", v);
                        }
                        System.Console.WriteLine();
#endif
                    }
                }
            }
            catch (System.Exception e)
            {
                //ファイルオープンが失敗したとき
                System.Console.WriteLine(e.Message);
            }
        }
        public List<string[]> GetData()
        {
            return stringData;
        }

        public string[][] GetArrayData()
        {
            return stringData.ToArray();
        }

        public int[][] GetIntData()
        {
            //リストの文字列の配列を取得
            var data = GetData();
            //行の取得
            int row = data.Count();
            //int型のジャグ配列で行の生成
            int[][] intData = new int[row][];

            //列方向の要素数を取得しながら、実体生成
            for (int y = 0; y < row; y++)
            {
                //列の要素数の取得
                int col = data[y].Count();
                intData[y] = new int[col];
            }

            //２重ループで整数に変換しながらコピー
            //行の繰り返し
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < intData[y].Count(); x++)
                {
                    intData[y][x] = int.Parse(data[y][x]);
                }
            }
            return intData;
        }
        public string[,] GetStringMatrix()
        {
            var data = GetData();//作業用データの取得
            int row = data.Count();//行の要素数の取得
            int col = data[0].Count();//列の要素数の取得

            //stringの多次元配列の生成
            string[,] result = new string[row, col];

            //データのコピー
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    result[y, x] = data[y][x];
                }
            }
            return result;
        }

        public int[,] GetIntMatrix()
        {
            var data = GetData();
            int row = data.Count();
            int col = data[0].Count();

            int[,] result = new int[row, col];

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    result[y, x] = int.Parse(data[y][x]);
                }
            }
            return result;
        }
    }
}
