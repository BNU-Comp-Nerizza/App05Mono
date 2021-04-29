using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace App05Mono.Sprites
{
    public class Dragon : Sprite
    {
        public int Health { get; set; }

        public Bullet Bullet;
        public Rectangle dragonRectangle;

        public bool HasDied = false;

        public Dragon(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> _sprites)
        {
            MoveDragon(_sprites);

            foreach (var sprite in _sprites)
            {
                if (sprite is Dragon)
                    continue;
                if (sprite.Rectangle.Intersects(this.Rectangle))
                {
                    this.HasDied = true;
                }
            }

            Position += Velocity;
            Position.X = MathHelper.Clamp(Position.X, 0, DvDGame.ScreenWidth - Rectangle.Width);
            Velocity = Vector2.Zero;
        }

        private void MoveDragon(List<Sprite> _sprites)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _rotation -= MathHelper.ToRadians(RotationVelocity);
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                _rotation += MathHelper.ToRadians(RotationVelocity);

            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Position += Direction * LinearVelocity;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
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
