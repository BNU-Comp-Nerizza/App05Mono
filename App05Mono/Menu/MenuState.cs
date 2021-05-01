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
    /// <summary>
    /// This is the menu state class which is 
    /// the front screen of the game 
    /// where it build the menu of the game 
    /// it adds the button that allows the player
    /// to select whether to quit or start the game
    /// </summary>
    public class MenuState : State
    {
        private List<Component> _components;
        private object backgroundMenu;

        /// <summary>
        /// constructor of the menustate class
        /// it builds the button of the menu
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="content"></param>
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

        /// <summary>
        /// constructor of the menustate class
        /// </summary>
        /// <param name="game"></param>
        /// <param name="content"></param>
        public MenuState(DvDGame game, ContentManager content) : base(game, content)
        {
        }

        /// <summary>
        /// This is called when the sprite 
        /// within the menu state should draw
        /// including the background and button
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="_spriteBatch"></param>
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

        /// <summary>
        /// Loading the content of the menu state class
        /// </summary>
        public override void LoadContent()
        {
        }

        /// <summary>
        /// This is classed after the update class
        /// </summary>
        /// <param name="gameTime"></param>
        public override void PostUpdate(GameTime gameTime)
        {
        }

        /// <summary>
        /// This is called when the sprite
        /// within the menu state should update its state 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        /// <summary>
        /// Method when the quit button is clicked
        /// it will exit the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        /// <summary>
        /// Method when the new game is clicked
        /// It will start the game and go to the gamestate class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }
    }
}
