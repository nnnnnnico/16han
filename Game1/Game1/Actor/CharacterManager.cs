using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Device;
using Game1.Scene;
using Microsoft.Xna.Framework;

namespace Game1.Actor
{
    class CharacterManager: IGameMediator
    {
        private List<Character> gameObjectList;//Playerグループ
        private List<Character> addGameObjects;//追加するキャラクターリスト

        private Map map;//マップ

        public CharacterManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            //リストの実体があればクリアし、なければ実体生成
            if (gameObjectList != null)
            {
                gameObjectList.Clear();
            }
            else
            {
                gameObjectList = new List<Character>();
            }

            if (addGameObjects != null)
            {
                addGameObjects.Clear();
            }
            else
            {
                addGameObjects = new List<Character>();
            }
        }

        public void Add(Character gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }

        /// <summary>
        /// マップの追加
        /// </summary>
        /// <param name="map"></param>
        public void Add(Map map)
        {
            if (map == null)
            {
                return;
            }
            this.map = map;
        }

        /// <summary>
        /// マップとの当たり判定
        /// </summary>
        private void hitToMap()
        {
            if (map == null)
            {
                return;
            }
            //全てのオブジェクトとマップとのヒット通知
            foreach (var obj in gameObjectList)
            {
                map.Hit(obj);
            }
        }

        private void hitToGameObject()
        {
            //ゲームオブジェクトリストを繰り返し
            foreach (var c1 in gameObjectList)
            {
                //同じゲームオブジェクトを繰り返し
                foreach (var c2 in gameObjectList)
                {
                    if (c1.Equals(c2) || c1.IsDead() || c2.IsDead())
                    {
                        //同じキャラか、キャラが死んでいたら次へ
                        continue;
                    }

                    //衝突判定
                    if (c1.IsCollision(c2))
                    {
                        //ヒット通知
                        c1.Hit(c2);
                        c2.Hit(c1);
                    }
                }
            }
        }

        /// <summary>
        /// 死亡キャラをリストから削除
        /// </summary>
        private void removeDeadCharacters()
        {
            gameObjectList.RemoveAll(c => c.IsDead());
        }

        public void Update(GameTime gameTime)
        {
            //全キャラ更新
            foreach (var c in gameObjectList)
            {
                c.Update(gameTime);
            }

            //キャラクタの追加
            foreach (var c in addGameObjects)
            {
                gameObjectList.Add(c);
            }


            //追加終了後、追加リストはクリア
            addGameObjects.Clear();

            //当たり判定
            hitToMap();
            hitToGameObject();

            //死亡フラグが立っているキャラを全て削除
            removeDeadCharacters();
        }

        public void Draw(Renderer renderer)
        {
            foreach (var c in gameObjectList)
            {
                c.Draw(renderer);
            }
        }

        public void AddGameObject(Character gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }

        public Character GetPlayer()
        {
            Character find = gameObjectList.Find(c => c is Player);
            if (find != null && !find.IsDead())
            {
                return find;
            }
            return null;//プレイヤーがいない
        }

        public bool IsPlayerDead()
        {
            Character find = gameObjectList.Find(C => C is Player);

            return (find == null || find.IsDead());
        }

        public bool IsBossDead()
        {
            Character find = gameObjectList.Find(C => C is Boss);

            return (find == null || find.IsDead());
        }

        //public Character GetGameObject(GameObjectID id)
        //{
        //    GameObject find = gameObjectList.Find(c => c.GetID() == id);

        //    if (find != null && !find.IsDead())
        //    {
        //        return find;
        //    }
        //    return null;
        //}

        //public List<GameObject> GetGameObjectList(GameObjectID id)
        //{
        //    List<GameObject> list = gameObjectList.FindAll(c => c.GetID() == id);
        //    List<GameObject> aliveList = new List<GameObject>();

        //    foreach (var c in list)
        //    {
        //        if (c.IsDead() == false)
        //        {
        //            aliveList.Add(c);
        //        }
        //    }
        //    return aliveList;
        //}

        public Vector2 MapSize()
        {
            return new Vector2(map.GetWidth(), map.GetHeight());
        }
    }
}