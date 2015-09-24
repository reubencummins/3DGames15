using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Verteces
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Matrix World, View, Projection;
        public Matrix colorTriangleWorld = Matrix.Identity;
        BasicEffect colorEffect;
        VertexPositionColor[] Verteces;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            UpdateView();

            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90), GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.AspectRatio, 1, 1000);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            CreateVerteces();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateView();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);
            DrawColorVertices();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        public void UpdateView()
        {
            View = Matrix.CreateLookAt(new Vector3(0, 0, 5), new Vector3(0, 0, -20), Vector3.Up);
        }

        public void CreateVerteces()
        {

            VertexPositionColor DL = new VertexPositionColor(new Vector3(-1, -1, 0), Color.OrangeRed);
            VertexPositionColor UP = new VertexPositionColor(new Vector3(0, (float)Math.Sqrt(3)-1, 0),Color.SeaGreen);
            VertexPositionColor DR = new VertexPositionColor(new Vector3(1, -1, 0), Color.DarkSlateBlue);

            Verteces = new VertexPositionColor[3] { DL, UP, DR };
            colorEffect = new BasicEffect(GraphicsDevice);
            colorEffect.VertexColorEnabled = true;

            colorTriangleWorld *= Matrix.CreateScale(2) * Matrix.CreateTranslation(0, 0, 0);
        }
        
        private void DrawColorVertices()
        {
            colorEffect.View = View;
            colorEffect.Projection = Projection;
            colorEffect.World = colorTriangleWorld;
            foreach (EffectPass pass in colorEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, Verteces, 0, Verteces.Length / 3, VertexPositionColor.VertexDeclaration);
            }
        }
    }
}
