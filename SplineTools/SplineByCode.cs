using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Splines.Components;
using Stride.Input;
using Stride.Rendering;
using System;

namespace SplineTools
{
    public class SplineByCode : SyncScript
    {
        public Material splineMaterial;
        public Material boundingBoxMaterial;
        private SplineComponent splineComponent;
        private Random random;

        public override void Start()
        {
            random = new Random((int)Game.TargetElapsedTime.TotalMilliseconds);
            GenerateSpline();
        }

        private void GenerateSpline()
        {
            var nodePositions = new Vector3[]
            {
                new Vector3(4, 1, 0),
                new Vector3(0, 2, 2),
                new Vector3(-2, 1, -1)
            };

            var tangents = new Vector3[]
            {
                new Vector3(0,  RandomY(), 4),   //Node 1 - out
                new Vector3(-1, RandomY(), -3), //Node 1 - in
                new Vector3(4,  RandomY(), -2), //Node 2 - out
                new Vector3(-3, RandomY(), 2),  //Node 2 - in
                new Vector3(-5, RandomY(), -1), //Node 3 - out
                new Vector3(4,  RandomY(), 0)    //Node 3 - in
            };

            splineComponent = new SplineComponent();
            Entity.Add(splineComponent);

            for (int i = 0; i < nodePositions.Length; i++)
            {
                var nodeEntity = new Entity(nodePositions[i]);
                var nodeComponent = new SplineNodeComponent(50, tangents[i * 2], tangents[i * 2 + 1]);
                nodeEntity.Add(nodeComponent);

                Entity.AddChild(nodeEntity);
                splineComponent.Nodes.Add(nodeComponent);
            }

            // We use a spline renderer if we want to show our spline in the game
            splineComponent.SplineRenderer.SegmentsMaterial = splineMaterial;
            splineComponent.SplineRenderer.Segments = true;
            splineComponent.SplineRenderer.BoundingBoxMaterial = boundingBoxMaterial;
            splineComponent.SplineRenderer.BoundingBox = true;
        }

        public int RandomY()
        {
            return random.Next(-2, 2);
        }

        public override void Update()
        {
            DebugText.Print($"Press Space to create spline", new Int2(600, 20));
            DebugText.Print($"Press L to toggle spline 'looping'", new Int2(600, 40));

            //Generate a new spline by pressing space
            if (Input.IsKeyPressed(Keys.Space))
            {
                Entity.Remove(splineComponent);
                GenerateSpline();
            }

            //Press S to toggle Looping of the spline
            if (Input.IsKeyPressed(Keys.L))
            {
                splineComponent.Loop = !splineComponent.Loop;
            }
        }
    }
}
