using App05Mono.Sound;
using App05Mono.Sprites;
using App05Mono.Spritesa;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App05Mono.Menu
{
    /// <summary>
    /// This is the game state class which is 
    /// the game , dragon vs dino, itself.
    /// It adds the sprite for background,
    /// dragon, enemy and bullet
    /// that allows the player
    /// It also outputs the player's score and health.
    /// </summary>
    public class GameState : State
    {
        private List<Sprite> _sprites;
        private List<Enemy> enemy = new List<Enemy>();
        Random random = new Random();
        private float spawn = 0;
        private SpriteFont arialFont;
        public Bullet Bullet;
        public Dragon dragon;
        public bool IsOver = false;
        private Button reload;
        private Button quit;
        private SoundEffect explodeEffect;
        private bool DoesWin = false;
        Scrolling scrolling1;
        Scrolling scrolling2;

        /// <summary>
        /// Constructor of the gamestate class
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="content"></param>
        public GameState(DvDGame game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
        }

        /// <summary>
        /// Load the sprites of the game state class
        /// including the button, dragon, bullet and background.
        /// </summary>
        public override void LoadContent()
        {
            arialFont = _content.Load<SpriteFont>("arial");
            //Using the button design
            reload = new Button(_content.Load<Texture2D>("button"), arialFont);
            reload.Text = "Reload";
            //using the button images
            reload.Position = new Vector2(640, 420);
            reload.Click += Reload_Click;

            quit = new Button(_content.Load<Texture2D>("button"), arialFont);
            quit.Text = "Quit Game";
            quit.Position = new Vector2(640, 480);
            quit.Click += Quit_Click;

            explodeEffect = SoundController.GetSoundEffect("Explode");

            var dragonTexture = _content.Load<Texture2D>("GreenDragon");
            dragon = new Dragon(dragonTexture)
            {
                Position = new Vector2(100, 150),
                Bullet = new Bullet(_content.Load<Texture2D>("Bullet")),
            };

            var backgroundTexture1 = _content.Load<Texture2D>("RPG-background1");
            scrolling1 = new Scrolling(backgroundTexture1, new Rectangle(0, 0, DvDGame.ScreenWidth, DvDGame.ScreenHeight))
            {
                Layer = 0.0f,
            };

            var backgroundTexture2 = _content.Load<Texture2D>("RPG-background2");
            scrolling2 = new Scrolling(backgroundTexture2, new Rectangle(DvDGame.ScreenWidth, 0, DvDGame.ScreenWidth, DvDGame.ScreenHeight))
            {
                Layer = 0.0f,
            };

            _sprites = new List<Sprite>()
            {
                dragon
            };
        }

        /// <summary>
        /// Method when the reload button is clicked
        /// it will reload the game state again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reload_Click(object sender, EventArgs e)
        {
            if (IsOver)
            {
                LoadContent();
                IsOver = false;
            }
            else if (DoesWin)
            {
                LoadContent();
                DoesWin = false;
            }
        }

        /// <summary>
        /// Method when the quit button is clicked
        /// it will exit the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quit_Click(object sender, EventArgs e)
        {
            if (IsOver)
            {
                _game.Exit();
                IsOver = true;
            }
            else if (DoesWin)
            {
                _game.Exit();
                DoesWin = true;
            }
        }

        /// <summary>
        /// This is called when the sprite
        /// within the game state should update its state 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            reload.Update(gameTime);
            quit.Update(gameTime);

            if (!IsOver)
            {
                spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

                foreach (Enemy enemy in enemy)
                    enemy.Update(_sprites);
                LoadEnemy();

                foreach (var sprite in _sprites.ToArray())
                    sprite.Update(gameTime, _sprites);
                CheckScore();
            }
            if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0)
            {
                scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.texture.Width;
            }
            if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0)
            {
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.texture.Width;
            }
            scrolling1.Update();
            scrolling2.Update();
        }

        /// <summary>
        /// Check if the player has
        /// enough points to win
        /// </summary>
        public void CheckScore()
        {
            int maxScore = 50;
            if (dragon.Score == maxScore)
            {
                DoesWin = true;
                IsOver = true;
            }
        }

        /// <summary>
        /// Method for loading the enemy
        /// the enemy will be spawning in differnt times
        /// </summary>
        public void LoadEnemy()
        {

            int randY = random.Next(100, 400);
            if (spawn >= 1)
            {
                spawn = 0;
                if (enemy.Count() < 4)
                    enemy.Add(new Enemy(_content.Load<Texture2D>("Enemy"), new Vector2(1100, randY)));
            }

            for (int i = 0; i < enemy.Count; i++)
            {
                if (!enemy[i].IsAlive)
                {
                    explodeEffect.Play();
                    enemy.RemoveAt(i);
                    i--;
                }
                if (!dragon.IsAlive)
                    IsOver = true;
            }
        }

        /// <summary>
        /// Once the update is method called
        /// the post update will removed the sprites
        /// </summary>
        /// <param name="gameTime"></param>
        public override void PostUpdate(GameTime gameTime)
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// This is called when the sprite 
        /// within the game state should draw
        /// including the background, enemy and player and button
        /// it also outputs the score and health of the player
        /// </summary>
        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            scrolling1.Draw(_spriteBatch);
            scrolling2.Draw(_spriteBatch);

            foreach (var sprite in _sprites)
            sprite.Draw(gameTime, _spriteBatch);
            dragon.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            foreach (Enemy enemy in enemy)
                enemy.Draw(_spriteBatch);

            _spriteBatch.Begin();

            arialFont = _content.Load<SpriteFont>("arial");

            float x = 10f;
            foreach (var sprite in _sprites)
            {
                _spriteBatch.DrawString(arialFont, "App05: MonoGame by Nerizza Flores ", new Vector2(x, 05f), Color.DimGray);
                _spriteBatch.DrawString(arialFont, "Health: " + dragon.Health, new Vector2(x, 30f), Color.LightSlateGray);
                _spriteBatch.DrawString(arialFont, "Score: " + dragon.Score, new Vector2(x, 50f), Color.LightSlateGray);
            }

            x += 150;

            if (DoesWin)
            {
                //Middle of the screen - half of string size
                _spriteBatch.DrawString(arialFont, "You Win", new Vector2(640, 360) -
                    new Vector2(arialFont.MeasureString("You Win").X / 2, 0), Color.Black);
                reload.Draw(_spriteBatch);
                quit.Draw(_spriteBatch);
            }
            else if (IsOver)
            {
                //Middle of the screen - half of string size
                _spriteBatch.DrawString(arialFont, "Game Over", new Vector2(640, 360) -
                    new Vector2(arialFont.MeasureString("Game Over").X / 2, 0), Color.Black);
                reload.Draw(_spriteBatch);
                quit.Draw(_spriteBatch);
            }
            _spriteBatch.End();

        }
    }
}
