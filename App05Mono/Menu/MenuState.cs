using App05Mono.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App05Mono.Menu
{
    public class MenuState : State
    {
        private List<Component> _components;
        private object backgroundMenu;

        public MenuState(DvDGame game, GraphicsDevice graphicsDevice, ContentManager content): base(game, content)
        {
            var buttonTexture = _content.Load<Texture2D>("button");
            var buttonFont = _content.Load<SpriteFont>("arial");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(650, 400),
                Text = "New Game",
            };
            newGameButton.Click += newGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(650, 450),
                Text = "Quit Game",
            };
            quitGameButton.Click += quitGameButton_Click;

            _components = new List<Component>()
            {
                newGameButton,
                quitGameButton,
            };
        }

        public MenuState(DvDGame game, ContentManager content) : base(game, content)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();

            backgroundMenu = _content.Load<Texture2D>(
                "SpalshBG");
            Vector2 position = new Vector2(0, 0);
            _spriteBatch.Draw((Texture2D)backgroundMenu, position, Color.White);
            foreach (var components in _components)
                components.Draw(_spriteBatch);

            _spriteBatch.End();
        }

        public override void LoadContent()
        {

        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void quitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }
    }
}
