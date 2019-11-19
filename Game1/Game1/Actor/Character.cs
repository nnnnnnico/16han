using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Game1.Device;
using Game1.Def;
using Game1.Scene;

namespace Game1.Actor
{

    enum Direction
    {
        //上、下、　左、　右
        Top, Bottom, Left, Right
    };

    abstract class Character
    {
        protected Vector2 position;
        protected string name;
        protected bool isDeadFlag;
        //protected IGameMediator mediator;
        protected int width;//幅
        protected int height;//高さ
        protected GameDevice gameDevice;

        public Character(string name,Vector2 position, int width, int height,GameDevice gameDevice)
        {
            this.name = name;
            this.position = position;
            this.gameDevice = gameDevice;
            isDeadFlag = false;
            //this.mediator = mediator;
            this.width = width;
            this.height = height;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public abstract object Clone();
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Shutdown();
        public abstract void Hit(Character other);

        public bool IsDead()
        {
            return isDeadFlag;
        }

        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify());
        }
        //public bool IsCollision(Character other)
        //{
        //    float length = (position - other.position).Length();

        //    float radiusSum = 64f;
        //    if (length <= radiusSum)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public Direction CheckDirection(Character otherObj)
        {
            Point thisCenter = this.GetRectangle().Center;
            Point otherCenter = otherObj.GetRectangle().Center;

            Vector2 dir = new Vector2(thisCenter.X, thisCenter.Y) - new Vector2(otherCenter.X, otherCenter.Y);

            if (Math.Abs(dir.X) > Math.Abs(dir.Y))
            {
                if (dir.X > 0)
                {
                    return Direction.Right;
                }
                return Direction.Left;
            }

            if (dir.Y > 0)
            {
                return Direction.Bottom;
            }
            return Direction.Top;
        }

        public void SetPosition(ref Vector2 other)
        {
            other = position;
        }

        public Rectangle GetRectangle()
        {
            //矩形の生成
            Rectangle area = new Rectangle();

            //位置と幅、高さを設定
            area.X = (int)position.X;
            area.Y = (int)position.Y;
            area.Height = height;
            area.Width = width;

            return area;
        }

        public bool IsCollision(Character other)
        {
            //RectangleクラスのIntersectsメソッドで重なり判定
            return this.GetRectangle().Intersects(other.GetRectangle());
        }
    }

}

