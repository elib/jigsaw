using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ThirdParty;
using System.ComponentModel.Design;
using System.IO;

namespace Jigsaw
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private ContentBuilder dynamicContentBuilder;
        public ContentManager dynamicContentManager;

        public List<string> availablePuzzleImages;

        private float _zoomFactor = 2;

        public Scene CurrentScene { get; private set; }

        private Scene _nextScene = null;

        private void SetDimensions(bool fullscreen)
        {
            if (fullscreen)
            {
                //get default dimensions used by screen
                DisplayMode dm = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
                graphics.PreferredBackBufferFormat = dm.Format;
                graphics.PreferredBackBufferHeight = dm.Height;
                graphics.PreferredBackBufferWidth = dm.Width;
                graphics.IsFullScreen = true;
            }
            else
            {
                graphics.PreferredBackBufferWidth = 1100;
                graphics.PreferredBackBufferHeight = 600;
            }
        }

        public Game1()
        {
            Core.game = this;

            graphics = new GraphicsDeviceManager(this);
            SetDimensions(false);

            dynamicContentBuilder = new ContentBuilder();
            ServiceContainer services = new ServiceContainer();
            // Register the service, so components like ContentManager can find it.
            services.AddService(typeof(IGraphicsDeviceService), graphics);

            dynamicContentManager = new ContentManager(services, dynamicContentBuilder.OutputDirectory);
            Content.RootDirectory = "Content";
        }

        public int Width
        {
            get
            {
                return (int)(GraphicsDevice.Viewport.Width / _zoomFactor);
            }
        }

        public int Height
        {
            get
            {
                return (int)(GraphicsDevice.Viewport.Height / _zoomFactor);
            }
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

            availablePuzzleImages = new List<string>();

            DirectoryInfo d = Directory.CreateDirectory(@"D:\Projects\Games\Jigsaw\Assets\dynamic\");
            foreach(var f in d.EnumerateFiles("*.jpg"))
            {
                string name = f.Name;
                
                dynamicContentBuilder.Add(f.FullName, name, null, null);

                availablePuzzleImages.Add(name);
            }

            dynamicContentBuilder.Build();

            CurrentScene = new PlayScene();
            _nextScene = CurrentScene;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            Core.Update(gameTime);

            if (_nextScene != CurrentScene)
            {
                CurrentScene = _nextScene;
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            InputManager.update();

            CurrentScene.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix scaleMatrix = Matrix.CreateScale(_zoomFactor);
            // Draw the sprite.
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, scaleMatrix);

            //draw current scene
            CurrentScene.Draw(spriteBatch, gameTime);
            
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        internal void SetScene(Scene newScene)
        {
            _nextScene = newScene;
        }
    }
}
