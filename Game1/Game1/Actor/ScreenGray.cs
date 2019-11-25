using Game1.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Actor
{
    class ScreenGray
    {
        private Vector2 _position;
        private float _scale;
        private float _alpha;
        private float _rotate;

        public ScreenGray(Vector2 position,float scale,float alpha,float rotate)
        {
            _position = position;
            _scale = scale;
            _alpha = alpha;
            _rotate = rotate;
        }

        public void Update()
        {

        }

        public void Initialize()
        {

        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("pixel", _position, Color.Gray * _alpha, _rotate, _position, _scale);
        }
    }
}
