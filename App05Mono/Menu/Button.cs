using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace App05Mono.Menu
{
    /// <summary>
    /// The button class is the class used for buttons
    /// that will be used in the menu state and game stae
    /// Methods within the button class will 
    /// allow the button to be clickable.
    /// It inherits from the component class
    /// </summary>
    public class Button : Component
    {
        #region Fields

        private MouseState _currentMouse;

        private SpriteFont _font;

        private bool _isHovering;

        private MouseState _previousMouse;

        private Texture2D _texture;

        #endregion

        #region Properties

        public EventHandler Click;

        public bool Clicked { get; private set; }

        public float Layer { get; set; }

        public Vector2 Origin
        {
            get
            {
                return new Vector2(_texture.Width / 2, _texture.Height / 2);
            }
        }

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X - ((int)Origin.X), (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);
            }
        }

        public string Text;

        #endregion

        #region Methods

        /// <summary>
        /// Constructor for the button class 
        /// set up the texture and colour of the button
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="font"></param>
        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            PenColour = Color.AntiqueWhite;
        }

        /// <summary>
        /// It draws the button sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (_isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(_texture, Position, null, colour, 0f, Origin, 1f, SpriteEffects.None, Layer);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, Layer + 0.01f);
            }
        }

        /// <summary>
        /// Updates and check the button state 
        /// whether the mouse intersects with the 
        /// button or not
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
