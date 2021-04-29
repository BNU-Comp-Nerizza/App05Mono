using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace App05Mono.Sprites
{
    public class Enemy
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle enemyRectangle;

        public bool isVisible = true;

        Random random = new Random();
        int randX, randY;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        public bool IsAlive { get; private set; }

        public Enemy(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;

            randY = random.Next(-4, 4);
            randX = random.Next(-4, -1);

            velocity = new Vector2(randX, randY);
        }

        public void Update(List<Sprite> _sprites)
        {
            if (IsAlive)
            {
                position += velocity;
                if (position.Y <= 0 || position.Y >= DvDGame.ScreenHeight - texture.Height)
                {
                    velocity.Y = -velocity.Y;
                }
                else if (position.X < 0 - texture.Width)
                {
                    isVisible = false;
                    IsAlive = false;
                }

                OnColide(_sprites);
            }
        }



        public void OnColide(List<Sprite> _sprites)
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i] is Dragon && _sprites[i].Rectangle.Intersects(Rectangle))
                {
                    var dragon = _sprites[i] as Dragon;
                    dragon.Health--;
                    this.IsAlive = false;
                }
                if (_sprites[i] is Bullet && _sprites[i].Rectangle.Intersects(Rectangle))
                {
                    var bullet = _sprites[i] as Bullet;

                    bullet.IsActive = false;
                    bullet.IsRemoved = true;

                    this.IsAlive = false; // or add health and reduce health on impact.
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
        }
    }
}

