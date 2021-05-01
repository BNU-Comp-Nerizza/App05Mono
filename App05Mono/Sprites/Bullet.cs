using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace App05Mono.Sprites
{
    /// <summary>
    /// This is the bullet sprite class where it
    /// inherits the sprite class methods.
    /// It builds the bullet that the dragon will be using
    /// </summary>
    public class Bullet : Sprite
    {
        private float _timer;
        public object Colour { get; internal set; }

        /// <summary>
        /// Constructor of the bullet
        /// </summary>
        /// <param name="texture"></param>
        public Bullet(Texture2D texture) : base(texture)
        {
        }

        /// <summary>
        /// Cast the bullet based on the 
        /// timer and lifespan of the bullet
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sprites"></param>
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > LifeSpan)
            {
                IsRemoved = true;
                IsVisible = false;
                IsActive = false;
                IsAlive = false;
            }

            Position += Direction * LinearVelocity;
        }
    }
}

