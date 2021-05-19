using App05Mono.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace App05Mono.Spritesa
{
    /// <summary>
    /// This is the enemy sprite class where
    /// it builds the dino which will the player's enemy
    /// it build the texture, position and collision of the enemy
    /// </summary>
    public class Enemy
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;

        public bool isVisible = true;
        public bool IsAlive { get; private set; } = true;

        Random random = new Random();
        int randX, randY;

        /// <summary>
        /// initialize the instance of rectangle
        /// </summary>
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        /// <summary>
        /// Constructor of the enemy where it sets the position 
        /// of the spawning of the enemy
        /// </summary>
        public Enemy(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;

            randY = random.Next(-4, 4);
            randX = random.Next(-4, -1);

            velocity = new Vector2(randX, randY);
        }

        /// <summary>
        ///Check for the enemy's collision and position
        /// </summary>
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

        /// <summary>
        /// Check the which sprite 
        /// is collided with the enemy
        /// </summary>
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

        /// <summary>
        /// Draw the enemy sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
        }
    }
}

