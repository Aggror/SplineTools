using Stride.Core.Mathematics;
using Stride.Core.Serialization;
using Stride.Engine;
using Stride.Input;
using System.Collections.Generic;

namespace SplineTools
{

    public class SwitchScene: SyncScript
    {
        public List<UrlReference<Scene>> Scenes = new List<UrlReference<Scene>>();

        public override void Start()
        {

        }

        public override void Update()
        {
            int DrawY = 20;
            for (int i = 0; i < Scenes.Count; i++)
            {
                DebugText.Print($"Press {i+1} to load '{Scenes[i]}' ", new Int2(20, DrawY));

                if (Input.IsKeyPressed((Keys)i + 35)) //35 = D1, 36 = D2 etc
                {
                    Content.Unload(SceneSystem.SceneInstance.RootScene);
                    SceneSystem.SceneInstance.RootScene = Content.Load(Scenes[i]);
                }
                DrawY += 20;
            }
        }
    }
}
