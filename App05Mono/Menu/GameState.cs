using App05Mono.Sprites;
using App05Mono.Spritesa;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App05Mono.Menu
{
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

        public GameState(DvDGame game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
        }

        public override void LoadContent()
        {
            arialFont = _content.Load<SpriteFont>("arial");
            //Using your button design
            reload = new Button(_content.Load<Texture2D>("button"), arialFont);
            reload.Text = "Reload";
            //using the button images
            //reload = new Button(_content.Load<Texture2D>("reload"), arialFont);
            reload.Position = new Vector2(640, 420);
            reload.Click += Reload_Click;

            quit = new Button(_content.Load<Texture2D>("button"), arialFont);
            quit.Text = "Quit Game";
            quit.Position = new Vector2(640, 480);
            quit.Click += Quit_Click;

            var dragonTexture = _content.Load<Texture2D>("GreenDragon");
            dragon = new Dragon(dragonTexture)
            {
                Position = new Vector2(100, 150),
                Bullet = new Bullet(_content.Load<Texture2D>("Bullet")),
            };

            _sprites = new List<Sprite>()
            {
                new Sprite(_content.Load<Texture2D>("RPG-background"))
                {
                    Layer = 0.0f,
                    Position = new Vector2(DvDGame.ScreenWidth/2, DvDGame.ScreenHeight /2 ),
                },
                dragon
            };
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            if (IsOver)
            {
                LoadContent();
                IsOver = false;
            }
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            if (IsOver)
            {
                _game.Exit();
                IsOver = true;
            }
        }

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
            }

        }

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
                    dragon.Score++;
                    enemy.RemoveAt(i);
                    i--;
                }
                if (!dragon.IsAlive)
                    IsOver = true;
            }
        }


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

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

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

            if (IsOver)
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
