using App05Mono.Menu;
using App05Mono.Sound;
using App05Mono.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace App05Mono
{
    public class DvDGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public const int ScreenHeight = 720;
        public const int ScreenWidth = 1280;
        public static Random random;
        private State _currentState;
        private State _nextState;

        /// <summary>
        /// 
        /// </summary>
        public DvDGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);

            SoundController.LoadContent(Content);
            //SoundController.PlaySong("Klee!");
        }

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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }
    }
}
