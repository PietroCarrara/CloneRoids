using Nez;

using CloneRoids.Scenes;

namespace CloneRoids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {
        public Game1() : base(Constants.ScreenWidth, Constants.ScreenHeight, true, false, "CloneRoids")
        {
            Core.pauseOnFocusLost = false;
        } 

        protected override void Initialize()
        {
            base.Initialize();

            scene = new MainScene();
        }
    }
}
