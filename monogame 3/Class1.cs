using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_3
{
    internal class trobble
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        private Rectangle _window;

        public trobble(Texture2D texture, Rectangle rect, Vector2 speed, Rectangle window)
        {
            _texture = texture;
            _rectangle = rect;
            _speed = speed;
            _window = window;   
        }

        public Texture2D Texture
        {
            get { return _texture; }
        }

        public Rectangle Bounds
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        public int Right
        {
            get { return _rectangle.Right; }
        }

        public bool Intersects(Rectangle rect)
        {
            return _rectangle.Intersects(rect);
        }

        public void Move()
        {
            _rectangle.Offset(_speed);

            if (_rectangle.Right > _window.Width || _rectangle.Left < 0)
                _speed.X *= -1;
            if (_rectangle.Bottom > _window.Height || _rectangle.Top < 0)
                _speed.Y *= -1;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);


        }

    }
}
