using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame_3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Vector2 tribbleGspeed, tribbleOspeed, tribbleBspeed, tribbleCspeed;
        Texture2D tribbleGtexture, tribbleOtexture, tribbleCtexture, tribbleBtexture;
        Rectangle tribbleGrect, tribbleOrect, tribbleCrect, tribbleBrect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            tribbleGrect = new Rectangle(300, 10, 100, 100);
            tribbleGspeed = new Vector2(2, 1);

            tribbleCrect = new Rectangle(100, 50, 100, 100);
            tribbleCspeed = new Vector2(3, 0);

            tribbleBrect = new Rectangle(350, 75, 100, 100);
            tribbleBspeed = new Vector2(0, 4);



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            tribbleGtexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleBtexture = Content.Load<Texture2D>("tribbleBrown");
            tribbleCtexture = Content.Load<Texture2D>("tribbleCream");
            tribbleOtexture = Content.Load<Texture2D>("tribbleOrange");

            


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (tribbleGrect.Right > _graphics.PreferredBackBufferWidth || tribbleGrect.Left < 0)
            {
                tribbleGspeed.X *= -1;
            }
            tribbleGrect.X += (int)tribbleGspeed.X;

            if (tribbleGrect.Bottom > _graphics.PreferredBackBufferHeight || tribbleGrect.Top < 0)
            {
                tribbleGspeed.Y *= -1;
            }
            tribbleGrect.Y += (int)tribbleGspeed.Y;

            if(tribbleCrect.Right > _graphics.PreferredBackBufferWidth || tribbleCrect.Left < 0)
            {
                tribbleCspeed.X *= -1;
            }
            tribbleCrect.X += (int)tribbleCspeed.X;
            tribbleCrect.Y += (int)tribbleCspeed.Y;

            if(tribbleBrect.Top > _graphics.PreferredBackBufferHeight || tribbleBrect.Bottom < 0)
            {
                tribbleBspeed.Y *= -1;
            }
            tribbleBrect.Y += (int)tribbleBspeed.Y;
            tribbleBrect.X += (int)tribbleBspeed.X;







            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(tribbleGtexture, tribbleGrect, Color.White);
            _spriteBatch.Draw(tribbleBtexture, tribbleBrect, Color.White);
            _spriteBatch.Draw(tribbleCtexture, tribbleCrect, Color.White);
            _spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}