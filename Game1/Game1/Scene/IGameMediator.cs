using Game1.Actor;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Scene
{
    interface IGameMediator
    {
        void AddGameObject(Character gameObject);
        Vector2 MapSize();
    }
}
