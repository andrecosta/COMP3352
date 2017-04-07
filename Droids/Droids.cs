using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KokoEngine;
using Microsoft.Xna.Framework.Graphics;
using Color = KokoEngine.Color;
using Texture2D = KokoEngine.Texture2D;

namespace Droids
{
    class Droids : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ISceneManager _sceneManager;
        private IAssetManager _assetManager;

        public Droids()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Add the InputManager Component
            Components.Add(new Input(this));
            Components.Add(new Debug(this));

            _sceneManager = new SceneManager();
            _assetManager = new AssetManager();
        }

        protected override void Initialize()
        {
            base.Initialize();

            var scene = _sceneManager.CreateScene("Test");

            var boidSprite = new Sprite(_assetManager.GetAsset<Texture2D>("boid"));

            // Create target
            var target = new GameObject();
            target.AddComponent<Rigidbody>();
            target.AddComponent<Target>();
            var sr = target.AddComponent<SpriteRenderer>();
            sr.sprite = boidSprite;
            sr.color = Color.Green;
            scene.AddGameObject(target);

            // Create boids controller
            var flock = new GameObject();
            var f = flock.AddComponent<Flock>();
            scene.AddGameObject(flock);

            // Create boids
            for (int i = 0; i < 25; i++)
            {
                var boid = new GameObject();
                boid.AddComponent<Rigidbody>();
                sr = target.AddComponent<SpriteRenderer>();
                sr.sprite = boidSprite;
                var b = boid.AddComponent<Boid>();
                b.Target = target.Transform;
                f.AddBoid(b);
            }

            // Load the scene
            _sceneManager.LoadScene(scene);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load and register resources with the Asset Manager
            var dummyTexture = new Microsoft.Xna.Framework.Graphics.Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new[] { Microsoft.Xna.Framework.Color.White });
            _assetManager.AddAsset("dummy", new Texture2D("dummy", dummyTexture, 1, 1));

            var boidTexture = Content.Load<Microsoft.Xna.Framework.Graphics.Texture2D>("boid");
            _assetManager.AddAsset("boid", new Texture2D("boid", boidTexture, boidTexture.Width, boidTexture.Height));

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            // Gets the number of elapsed seconds since the last update (for use in all movement calculations)
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the active scene's game objects
            _sceneManager.UpdateActiveScene(dt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Microsoft.Xna.Framework.Color(29, 29, 29));

            base.Draw(gameTime);
        }
    }
}
