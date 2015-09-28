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
        int[] indices;
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
            IsMouseVisible = true;

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
            texture = Content.Load<Texture2D>("marker");

            colorTriangleWorld *= Matrix.CreateScale(2) * Matrix.CreateTranslation(0, 0, 0);
            CreateVerteces();
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
            DrawColorVertices();
            DrawTextureVertices();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        public void UpdateView()
        {
            View = Matrix.CreateLookAt(new Vector3(0, 0, 5), new Vector3(0, 0, -20), Vector3.Up);
        }

        public void CreateVerteces()
        {

            VertexPositionColor DL = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Cyan);
            VertexPositionColor UL = new VertexPositionColor(new Vector3(-1, 1, 0),Color.Magenta);
            VertexPositionColor UR = new VertexPositionColor(new Vector3(1, 1, 0), Color.Yellow);
            VertexPositionColor DR = new VertexPositionColor(new Vector3(1, -1, 0), Color.Black);


            colorVerteces = new VertexPositionColor[6] { DL, UL, UR, UR, DR, DL };
            colorEffect = new BasicEffect(GraphicsDevice);
            colorEffect.VertexColorEnabled = true;

        }

        public void SetupTextureVeritces()
        {
            textureVerteces = new VertexPositionTexture[4];

            textureVerteces[0] = new VertexPositionTexture(new Vector3(-4, -1, 0), new Vector2(1, 1));
            textureVerteces[1] = new VertexPositionTexture(new Vector3(-4, 1, 0), new Vector2(1, 0));
            textureVerteces[2] = new VertexPositionTexture(new Vector3(-2, 1, 0), Vector2.Zero);
            textureVerteces[3] = new VertexPositionTexture(new Vector3(-2, -1, 0), new Vector2(0, 1));

            indices = new int[6] { 0,1,2,2,3,0 };

            textureEffect = new BasicEffect(GraphicsDevice);
            textureEffect.TextureEnabled = true;
        }

        private void SetupNormalVertices()
        {

            VertexPositionNormalTexture DL = new VertexPositionNormalTexture(new Vector3(2, -1, 0),Vector3.Backward, new Vector2(1, 1));
            VertexPositionNormalTexture UL = new VertexPositionNormalTexture(new Vector3(2, 1, 0), Vector3.Backward, new Vector2(1, 0));
            VertexPositionNormalTexture UR = new VertexPositionNormalTexture(new Vector3(4, 1, 0), Vector3.Backward, Vector2.Zero);
            VertexPositionNormalTexture DR = new VertexPositionNormalTexture(new Vector3(4, -1, 0), Vector3.Backward, new Vector2(0, 1));


            normalVertices = new VertexPositionNormalTexture[6] { DL, UL, UR, UR, DR, DL };
            normalEffect = new BasicEffect(GraphicsDevice);
            normalEffect.TextureEnabled = true;
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

                GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, textureVerteces, 0, textureVerteces.Length, indices, 0, indices.Length / 3);
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
