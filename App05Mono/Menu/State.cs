﻿using App05Mono.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace App05Mono.Menu
{
    public abstract class State
    {
        protected DvDGame _game;
        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;

        public State(DvDGame game, ContentManager content)
        {
            _game = game;
            _content = content;
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void PostUpdate(GameTime gameTime);

        public State(DvDGame game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;
        }
        public abstract void Draw(GameTime gameTime, SpriteBatch _spriteBatch);
    }
}
