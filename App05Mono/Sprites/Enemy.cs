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
        public Vector2 enemyPosition;


        public bool isVisible = true;

        Random random = new Random();
        int randX, randY;

        public Enemy(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;

            randY = random.Next(-4, 4);
            randX = random.Next(-4, -1);

            velocity = new Vector2(randX, randY);
        }

        public void Update()
        {
            position += velocity;
            if (position.Y <= 0 || position.Y >= DvDGame.ScreenHeight - texture.Height)
            {
                velocity.Y = -velocity.Y;
            }
            else if (position.X < 0 - texture.Width)
            {
                isVisible = false;
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

