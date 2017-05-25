using Nez;

using CloneRoids.Scenes;

namespace CloneRoids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {
        protected override void Initialize()
        {
            base.Initialize();

            scene = new MainScene();
        }
    }
}
