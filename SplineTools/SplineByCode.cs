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

        public override void Start()
        {
            var nodePositions = new Vector3[]
            {
                new Vector3(4, 1, 0),
                new Vector3(0, 2, 2),
                new Vector3(-2, 1, -1)
            };

            var tangents = new Vector3[]
            {
                new Vector3(0, 1, 4),   //Node 1 - out
                new Vector3(-1, 0, -3), //Node 1 - in
                new Vector3(4, 1, -2), //Node 2 - out
                new Vector3(-3, 0, 2),  //Node 2 - in
                new Vector3(-5, -1, -1), //Node 3 - out
                new Vector3(4, 0, 0)    //Node 3 - in
            };

            splineComponent = new SplineComponent();
            Entity.Add(splineComponent);

            for (int i = 0; i < nodePositions.Length; i++)
            {
                var nodeEntity = new Entity(nodePositions[i]);
                var nodeComponent = new SplineNodeComponent(50, tangents[i*2], tangents[i*2+1]);
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

        public override void Update()
        {
            //Press Space to toggle Looping of the spline
            if (Input.IsKeyPressed(Keys.Space))
            {
                splineComponent.Loop = !splineComponent.Loop;
            }
        }
    }
}
