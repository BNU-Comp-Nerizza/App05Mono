﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace App05Mono.Menu
{
    public abstract class Component
    {
        public abstract void Draw(SpriteBatch _spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
