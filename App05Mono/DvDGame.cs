using App05Mono.Menu;
using App05Mono.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace App05Mono
{
    /// <summary>
    /// The Dragon vs Dino game is a shooting game, 
    /// where the dragon will be the player and 
    /// dino will be the enemy. Player can control the dragon
    /// and shoot the enemy with its fire bullet. 
    /// When the enemy collides with the dragon, it will
    /// decrease health. Dragon has only 3 health, lose 3 health 
    /// and the player lose. 
    /// </summary>
    /// <authors>
    /// Nerizza Flores ver 3. 05/01/21
    /// </authors>
    public class DvDGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public const int ScreenHeight = 720;
        public const int ScreenWidth = 1280;
        private State _currentState;
        private State _nextState;

        /// <summary>
        /// Constructor of the game
        /// </summary>
        public DvDGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Setup the game window size to 720P 1280 x 720 pixels
        /// </summary>
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        ///LoadContent will load the game's content incluidng
        ///sprite, menu and music.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);

            SoundController.LoadContent(Content);
            SoundController.PlaySong("Klee!");
        }

        /// <summary>
        /// Check whether the state of the game is
        /// on menu or within the game itself
        /// Load the menu state as current state
        /// then load game state as next state
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            if(_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();
                _nextState = null;
            }
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw all the sprites and other 
        /// drawable images here
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Method for changing the state
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(State state)
        {
            _nextState = state;
        }
    }
}
