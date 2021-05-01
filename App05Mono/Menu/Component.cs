using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace App05Mono.Menu
{
    /// <summary>
    /// Component class is an abstract class
    /// that is used by the button class to 
    /// inherits its method.
    /// </summary>
    public abstract class Component
    {
        public abstract void Draw(SpriteBatch _spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
