using System.Collections.Generic;

namespace Settings
{
    public class ScenesOrder
    {
        public ScenesOrder(IReadOnlyList<SceneReference> scenes)
        {
            Scenes = scenes;
        }

        public IReadOnlyList<SceneReference> Scenes { get; }
    }
}