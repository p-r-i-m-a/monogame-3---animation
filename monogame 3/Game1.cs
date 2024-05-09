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

        trobble tribble1, tribble2, tribble3, tribble4;

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


           
            bgColor = Color.CornflowerBlue;

            x = generator.Next(1, 700);
            y = generator.Next(1, 400);

            base.Initialize();
            MediaPlayer.Play(screenTunes);
            tribble1 = new trobble(tribbleGtexture, new Rectangle(10, 10, 100, 100), new Vector2(2, 2), window);
            x = generator.Next(1, 700);
            y = generator.Next(1, 400);
            tribble2 = new trobble(tribbleCtexture, new Rectangle(x, y, 100, 100), new Vector2(2, 2), window); ;
            x = generator.Next(1, 700);
            y = generator.Next(1, 400);
            tribble3 = new trobble(tribbleBtexture, new Rectangle(x, y, 100, 100), new Vector2(2, 2), window);
            x = generator.Next(1, 700);
            y = generator.Next(1, 400);
            tribble4 = new trobble(tribbleOtexture, new Rectangle(x, y, 100, 100), new Vector2(2, 2), window); 
            


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


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();


            

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
                tribble1.Move();
                tribble2.Move();
                tribble3.Move();
                tribble4.Move();

                if (tribble1.Right >= window.Width || tribble1.Bounds.X <= 0)
                {
                    bgColor = Color.CornflowerBlue;
                }

                if (tribble1.Bounds.Bottom >= window.Height || tribble1.Bounds.Top <= 0)
                {
                    bgColor = Color.Green;
                }


                if (tribble2.Right >= window.Width || tribble2.Bounds.X <= 0)
                {
                    bgColor = Color.Red;
                }

                if (tribble2.Right >= window.Height || tribble2.Bounds.Top <= 0)
                {
                    bgColor = Color.Yellow;
                }

                if (tribble3.Right >= window.Width || tribble3.Bounds.X <= 0)
                {
                    bgColor = Color.Purple;
                }

                if (tribble4.Right >= window.Width || tribble4.Bounds.X <= 0)
                {
                    bgColor = Color.Magenta;
                }



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
                tribble2.Draw(_spriteBatch);
                tribble3.Draw(_spriteBatch);
                tribble4.Draw(_spriteBatch);
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