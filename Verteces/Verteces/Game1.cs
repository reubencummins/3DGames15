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
        BasicEffect textureEffect;
        BasicEffect normalEffect;
        VertexPositionColor[] colorVerteces;
        VertexPositionTexture[] textureVerteces;
        VertexPositionNormalTexture[] normalVertices;
        Texture2D texture;
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

            texture = Content.Load<Texture2D>("images");

            SetupTextureVeritces();
            SetupNormalVertices();
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
            DrawNormalVertices();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        public void UpdateView()
        {
            View = Matrix.CreateLookAt(new Vector3(0, 0, 5), new Vector3(0, 0, -20), Vector3.Up);
        }

        public void CreateVerteces()
        {

            VertexPositionColor DL = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Red);
            VertexPositionColor UL = new VertexPositionColor(new Vector3(-1, 1, 0),Color.SeaGreen);
            VertexPositionColor UR = new VertexPositionColor(new Vector3(1, 1, 0), Color.Goldenrod);
            VertexPositionColor DR = new VertexPositionColor(new Vector3(1, -1, 0), Color.DarkSlateBlue);


            colorVerteces = new VertexPositionColor[6] { DL, UL, UR, UR, DR, DL };
            colorEffect = new BasicEffect(GraphicsDevice);
            colorEffect.VertexColorEnabled = true;

            colorTriangleWorld *= Matrix.CreateScale(2) * Matrix.CreateTranslation(0, 0, 0);
        }

        public void SetupTextureVeritces()
        {

            VertexPositionTexture tDL = new VertexPositionTexture(new Vector3(-1, -1, 0), new Vector2(1, 1));
            VertexPositionTexture tUL = new VertexPositionTexture(new Vector3(-1, 1, 0), new Vector2(1, 0));
            VertexPositionTexture tUR = new VertexPositionTexture(new Vector3(1, 1, 0), Vector2.Zero);
            VertexPositionTexture tDR = new VertexPositionTexture(new Vector3(1, -1, 0), new Vector2(0, 1));


            textureVerteces = new VertexPositionTexture[6] { tDL, tUL, tUR, tUR, tDR, tDL };
            textureEffect = new BasicEffect(GraphicsDevice);
            textureEffect.TextureEnabled = true;
            colorTriangleWorld *= Matrix.CreateScale(2) * Matrix.CreateTranslation(0, 0, 0);
        }

        private void SetupNormalVertices()
        {

            VertexPositionNormalTexture tDL = new VertexPositionNormalTexture(new Vector3(-1, -1, 0),Vector3.Up, new Vector2(1, 1));
            VertexPositionNormalTexture tUL = new VertexPositionNormalTexture(new Vector3(-1, 1, 0), Vector3.Up, new Vector2(1, 0));
            VertexPositionNormalTexture tUR = new VertexPositionNormalTexture(new Vector3(1, 1, 0), Vector3.Up, Vector2.Zero);
            VertexPositionNormalTexture tDR = new VertexPositionNormalTexture(new Vector3(1, -1, 0), Vector3.Up, new Vector2(0, 1));


            normalVertices = new VertexPositionNormalTexture[6] { tDL, tUL, tUR, tUR, tDR, tDL };
            normalEffect = new BasicEffect(GraphicsDevice);
            normalEffect.TextureEnabled = true;
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

                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, colorVerteces, 0, colorVerteces.Length / 3, VertexPositionColor.VertexDeclaration);

            }
        }

        private void DrawTextureVertices()
        {
            textureEffect.View = View;
            textureEffect.Projection = Projection;
            textureEffect.World = colorTriangleWorld;
            textureEffect.Texture = texture;
            foreach (EffectPass pass in textureEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, textureVerteces, 0, textureVerteces.Length / 3, VertexPositionTexture.VertexDeclaration);
            }
            
        }

        private void DrawNormalVertices()
        {
            normalEffect.View = View;
            normalEffect.Projection = Projection;
            normalEffect.World = colorTriangleWorld;
            normalEffect.Texture = texture;
            foreach (EffectPass pass in normalEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, normalVertices, 0, normalVertices.Length / 3, VertexPositionNormalTexture.VertexDeclaration);
            }

        }
    }
    
}
