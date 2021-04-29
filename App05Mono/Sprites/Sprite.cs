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

        public Sprite Parent;
        public float LifeSpan = 0f;
        public bool IsRemoved = false;
        public bool IsActive = false;
        public bool IsAlive = false;
        public bool IsVisible = false;

        public readonly Color[] TextureData;

        public List<Sprite> Children { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public float Layer { get; internal set; }

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
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sprites"></param>
        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
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
