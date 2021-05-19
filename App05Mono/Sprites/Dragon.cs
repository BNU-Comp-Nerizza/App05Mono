using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace App05Mono.Sprites
{
    /// <summary>
    /// This is the dragon sprite class where it
    /// inherits the sprite class methods.
    /// It builds the dragon that the player will be using
    /// </summary>
    public class Dragon : Sprite
    {
        public int Health { get; set; }

        public int Score { get; set; }

        public Bullet Bullet;

        /// <summary>
        /// Constructor of the dragon
        /// it sets the player's health and score
        /// </summary>
        /// <param name="texture"></param>
        public Dragon(Texture2D texture) : base(texture)
        {
            Health = 5;
            Score = 0;
        }

        /// <summary>
        /// Check for the player's collision and position
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="_sprites"></param>
        public override void Update(GameTime gameTime, List<Sprite> _sprites)
        {
            MoveDragon(_sprites);
            if (IsAlive)
            {
                foreach (var sprite in _sprites)
                {
                    if (sprite is Dragon)
                        continue;
                    if (sprite.HasDied == true)
                        continue;
                    if (sprite is Bullet && sprite.Parent == this)
                        continue;
                    if (sprite.Rectangle.Intersects(this.Rectangle))
                    {
                        this.Health--;
                        sprite.HasDied = true;
                    }
                }

                Position += Velocity;
                Position.X = MathHelper.Clamp(Position.X, 0, DvDGame.ScreenWidth - Rectangle.Width);
                Velocity = Vector2.Zero;
            }
            if (Health <= 0)
                IsAlive = false;
        }

        /// <summary>
        /// Set up the controls and movement of the dragon
        /// </summary>
        /// <param name="_sprites"></param>
        private void MoveDragon(List<Sprite> _sprites)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Position.Y -= 1 * LinearVelocity;
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                Position.Y += 1 * LinearVelocity;

            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Position += Direction * LinearVelocity;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Position -= Direction * LinearVelocity;

            if (_currentKey.IsKeyDown(Keys.Space) &&
                _previousKey.IsKeyUp(Keys.Space))
            {
                AddBullet(_sprites);
            }
        }

        /// <summary>
        /// Add bullet to the dragon
        /// </summary>
        /// <param name="_sprites"></param>
        private void AddBullet(List<Sprite> _sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.LinearVelocity = this.LinearVelocity * 2;
            bullet.LifeSpan = 2f;
            bullet.Parent = this;

            _sprites.Add(bullet);
        }
    }
}
