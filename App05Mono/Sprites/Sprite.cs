using App05Mono.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App05Mono.Sprites
{
    /// <summary>
    /// This is a basic sprite which has a single image which
    /// can be scaled and rotated around an origin.  The Bounding
    /// Box is the Rectangle the image occupies, and the Bounday
    /// if it exists is the area inside outside which the Sprite can
    /// not move.  Direction is a Vector such as (0, 1) which indicate
    /// the down direction, and Speed is the rate of movement.  A
    /// Speed of 60 is one pixel per second.  The Sprite can only
    /// move if it is Active and Alive.
    /// </summary>
    public class Sprite : ICloneable
    {
        protected Texture2D _texture;
        protected float _rotation;
        protected KeyboardState _currentKey;
        protected KeyboardState _previousKey;

        public Vector2 Position;
        public Vector2 Origin;
        public float Speed;
        public Vector2 Velocity;

        public Vector2 Direction;
        public float RotationVelocity = 3f;
        public float LinearVelocity = 4f;

        public float Layer { get; internal set; }

        public Sprite Parent;
        public float LifeSpan = 0f;
        public bool IsRemoved = false;
        public bool IsActive = false;
        public bool IsAlive = true;
        public bool IsVisible = false;
        public bool HasDied = false;
        internal int score;
        public readonly Color[] TextureData;

        public List<Sprite> Children { get; set; }

        /// <summary>
        /// The area of the sprite that could be collided with
        /// </summary>
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        /// <summary>
        /// Constructor for the sprite class
        /// </summary>
        /// <param name="texture"></param>
        public Sprite(Texture2D texture)
        {
            _texture = texture;

            // The default origin in the centre of the sprite
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        /// <summary>
        /// This is called when the sprite should update its state 
        /// </summary>
        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
        }

        /// <summary>
        /// This is called when the sprite should draw 
        /// </summary>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Allows the sprite to be cloned
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
