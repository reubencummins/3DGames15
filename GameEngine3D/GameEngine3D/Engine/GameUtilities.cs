using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine3D.Engine
{
    public static class GameUtilities
    {
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static GameTime Time { get; set; }
        public static float Delta { get; set; }
        public static int MyProperty { get { return Time.ElapsedGameTime.Milliseconds; } }
        public static Random Random { get; set; }
        public static SpriteFont DebugFont { get; set; }
        public static SpriteBatch DebugBatch { get; set; }

        public static ContentManager PersistentContent { get; set; }
        public static ContentManager NonPersistentContent { get; set; }

        public static void UnloadNonPersistentContent()
        {
            if (NonPersistentContent != null)
            {
                NonPersistentContent = null;
            }
        }
    }
}
