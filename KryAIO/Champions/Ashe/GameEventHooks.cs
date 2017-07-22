using Aimtec;

namespace KryAIO.Champions.Ashe
{
    public partial class Ashe
    {
        /// <summary>
        /// Initializes the events.
        /// </summary>
        private void InitializeEvents()
        {
            Game.OnStart += OnGameOnStart;
            Game.OnUpdate += OnGameOnUpdate;
            Game.OnEnd += OnGameOnEnd;

            Render.OnPresent += OnPresent;
        }
    }
}
