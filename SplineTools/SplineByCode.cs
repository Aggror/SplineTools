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
        public bool Looping = false;
        private SplineComponent splineComponent;

        public override void Start()
        {
            var r = new Random(Game.TargetElapsedTime.Milliseconds);
            var nodePositions = new Vector3[3]
            {
                new Vector3(4, 1, 0),
                new Vector3(0, 1, 2),
                new Vector3(-2, 1, -1)
            };

            var tangents = new Vector3[6]
            {
                new Vector3(-2, 0, -4), //Node 1 - in
                new Vector3(1, 0, 2),   //Node 2 - in
                new Vector3(2, 0, 1),   //Node 3 - in
                new Vector3(2, 0, 5),   //Node 3 - out
                new Vector3(-3, 0, 1),  //Node 2 - out
                new Vector3(1, 0, 1)    //Node 1 - out
            };

            splineComponent = new SplineComponent();
            Entity.Add(splineComponent);

            for (int i = 0; i < nodePositions.Length; i++)
            {
                var nodeEntity = new Entity(nodePositions[i]);
                var nodeComponent = new SplineNodeComponent(20, tangents[i], tangents[^1]);
                //var nodeComponent = new SplineNodeComponent();

                //nodeComponent.SplineNode.Segments = 20;
                //nodeComponent.SplineNode.TangentOutLocal = new Vector3(r.Next(-4, 4), 0, r.Next(-4, 4));
                //nodeComponent.SplineNode.TangentInLocal = new Vector3(r.Next(-4, 4), 0, r.Next(-4, 4));
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
            if (Input.IsKeyPressed(Keys.Space))
            {
                Looping = !Looping;
                splineComponent.Loop = Looping;
            }
        }
    }
}
