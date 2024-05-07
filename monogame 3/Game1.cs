using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace monogame_3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public int color = 1;
        Vector2 tribbleGspeed, tribbleOspeed, tribbleBspeed, tribbleCspeed;
        Texture2D tribbleGtexture, tribbleOtexture, tribbleCtexture, tribbleBtexture, tribbleIntroTexture, endTexture;
        Rectangle tribbleGrect, tribbleOrect, tribbleCrect, tribbleBrect;
        MouseState mouseState, prevMouseState;
        public int clicks;
        SpriteFont fontText;
        Song screenTunes;
        float seconds;

        trobble tribble1;

        Rectangle window;

        Color bgColor;

        enum Screen
        {
            Intro,
            TribbleYard,
            end
        }

        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            seconds = 0f;
            Random generator = new Random();
            screen = Screen.Intro;
            endTexture = Content.Load<Texture2D>("endscreen");
            tribbleIntroTexture = Content.Load<Texture2D>("tribble_intro");
            
            int x, y;

            window = new Rectangle(0, 0, 800, 500);  
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            x = generator.Next(2, 700);
            y = generator.Next(2, 400);
           /* tribbleGrect = new Rectangle(x, y, 100, 100);
            tribbleGspeed = new Vector2(4, 3);
            x = generator.Next(2, 700);
            y = generator.Next(2, 400);*/

            tribbleCrect = new Rectangle(x, y, 100, 100);
            tribbleCspeed = new Vector2(3, 0);
            x = generator.Next(2, 700);
            y = generator.Next(2, 400);

            tribbleBrect = new Rectangle(x, y, 100, 100);
            tribbleBspeed = new Vector2(0, 4);
            

            tribbleOrect = new Rectangle(0, 0, 100, 100);
           
            bgColor = Color.CornflowerBlue;

            base.Initialize();
            MediaPlayer.Play(screenTunes);
            tribble1 = new trobble(tribbleGtexture, new Rectangle(10, 10, 100, 100), new Vector2(2, 2), window);
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            fontText = Content.Load<SpriteFont>("spriteFont");
            screenTunes = Content.Load<Song>("Life could be a dream");

            tribbleGtexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleBtexture = Content.Load<Texture2D>("tribbleBrown");
            tribbleCtexture = Content.Load<Texture2D>("tribbleCream");
            tribbleOtexture = Content.Load<Texture2D>("tribbleOrange");

            


            // TODO: use this.Content to load your game content here
        }

        int clickCooldown = 500;
        int timeSinceLastClick = 0;

        protected override void Update(GameTime gameTime)
        {
            //seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            //timeSinceLastClick += gameTime.ElapsedGameTime.Milliseconds;

            

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    screen = Screen.TribbleYard;
                    MediaPlayer.Stop();
                }
                

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.TribbleYard;
                }
            }
            else if (screen == Screen.TribbleYard)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    screen = Screen.end;
                   /* if (tribbleGrect.Right > _graphics.PreferredBackBufferWidth || tribbleGrect.Left < 0)
                {
                    color = 2;
                    tribbleGspeed.X *= -1;
                }
                tribbleGrect.X += (int)tribbleGspeed.X;

                 if (tribbleGrect.Bottom > _graphics.PreferredBackBufferHeight || tribbleGrect.Top < 0)
                 {
                     tribbleGspeed.Y *= -1;
                     color = 1;
                 }
                 tribbleGrect.Y += (int)tribbleGspeed.Y;*/
                tribble1.Move();
                if (tribble1.Right >= window.Width || tribble1.Bounds.X <= 0)
                {
                    bgColor = Color.CornflowerBlue;
                }

                if (tribble1.Bounds.Bottom >= window.Height || tribble1.Bounds.Top <= 0)
                {
                    bgColor = Color.Green;
                }

                


                if (tribbleCrect.Right > _graphics.PreferredBackBufferWidth || tribbleCrect.Left < 0)
                {
                    tribbleCspeed.X *= -1;
                    bgColor = Color.Red;
                }
                tribbleCrect.X += (int)tribbleCspeed.X;
                tribbleCrect.Y += (int)tribbleCspeed.Y;

                if (tribbleBrect.Top > (_graphics.PreferredBackBufferHeight - 100) || tribbleBrect.Bottom < 100)
                {
                    // tribbleBspeed.Y *= -1;
                    tribbleBrect.Y = 0;
                    bgColor = Color.Yellow;

                }
                tribbleBrect.Y += (int)tribbleBspeed.Y;
                tribbleBrect.X += (int)tribbleBspeed.X;

                tribbleOrect.X = Mouse.GetState().X;
                tribbleOrect.Y = Mouse.GetState().Y;

                if (clicks == 2)
                {
                    screen = Screen.end;
                }
            }
            else if (screen == Screen.end)
            {
                if (clicks == 3)
                {
                    System.Environment.Exit(0);
                }
            }

            base.Update(gameTime);


            
        }

        protected override void Draw(GameTime gameTime)
        {



            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(tribbleIntroTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.DrawString(fontText, "Click To Continue!", new Vector2(270, 200), Color.Black);

            }
            else if (screen == Screen.TribbleYard)
            {

                GraphicsDevice.Clear(bgColor);


                tribble1.Draw(_spriteBatch);

                _spriteBatch.Draw(tribbleBtexture, tribbleBrect, Color.White);
                _spriteBatch.Draw(tribbleCtexture, tribbleCrect, Color.White);
                _spriteBatch.Draw(tribbleOtexture, tribbleOrect, Color.White);
            }
            else if (screen == Screen.end)
            {
                _spriteBatch.Draw(endTexture, new Rectangle(0, 0, 800, 500), Color.White);
            }

            _spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}