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
using System.ComponentModel.Design;
using System.IO;
using EXS;

namespace Jigsaw
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : ExsGame
    {
        public List<string> availablePuzzleImages;

        public Game1()
            : base()
        {

            JigsawCore.specificGame = this;
        
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
            base.LoadContent();

            availablePuzzleImages = new List<string>();

            JigsawCore.GlobalBackground = new Background();

            DirectoryInfo d = Directory.CreateDirectory(@"D:\Projects\Games\Jigsaw\Assets\production\");
            foreach(var f in d.EnumerateFiles("*.jpg"))
            {
                string name = f.Name;
                
                dynamicContentBuilder.Add(f.FullName, name, null, null);

                availablePuzzleImages.Add(name);
            }

            var errors = dynamicContentBuilder.Build();
            if (errors != null)
            {
                Console.WriteLine("*************************************");
                Console.WriteLine("ERRORS!");
                Console.WriteLine(errors);
                Console.WriteLine("*************************************");
                Exit();
                return;
            }

            SetScene(new PlayScene());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            base.UnloadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            

            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}