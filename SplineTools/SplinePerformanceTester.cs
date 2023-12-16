using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Splines.Components;
using Stride.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Stride.Input;

namespace SplineTools
{
    public class SplinesPerformanceTest : SyncScript
    {
        public int splineAmount = 1000;
        public int splineNodesPerSpline = 1000;
        public int splineSegmentCount = 100;
        public Vector3 splineGenerationArea = new (1000, 200, 1000);

        public List<Material> splineMaterials = new List<Material>();
        private SplineComponent splineComponent;
        private Random random;
        private Entity[] splineEntities;

        private Stopwatch stopwatch;

        public override void Start()
        {
            splineEntities = new Entity[splineAmount];
            random = new Random((int)Game.TargetElapsedTime.TotalMilliseconds);
            GenerateSplines();
        }

        private void GenerateSplines()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            
            ClearSplines();
            for (var i = 0; i < splineAmount; i++)
            {
                GenerateSpline(i);
            }
            stopwatch.Stop();
        }

        private void GenerateSpline(int iteration)
        {
            var nodePositions = new Vector3[splineNodesPerSpline];
            for (var i = 0; i < splineNodesPerSpline; i++)
            {
                nodePositions[i] = RandomVector3();
            }

            //In and Out tangent
            var tangents = new Vector3[splineNodesPerSpline * 2];
            for (var i = 0; i < splineNodesPerSpline * 2; i++)
            {
                tangents[i] = RandomOffsetVector3();
            }
            
            var splineEntity = new Entity($"Spline{iteration}");
            splineComponent = new SplineComponent
            {
                Loop = false
            };
            splineEntity.Add(splineComponent);
            Entity.Scene.Entities.Add(splineEntity);
            splineEntities[iteration] = splineEntity;
            
            for (var i = 0; i < nodePositions.Length; i++)
            {
                var nodeEntity = new Entity("node" + i, nodePositions[i]);
                var nodeComponent = new SplineNodeComponent(splineSegmentCount, tangents[i * 2], tangents[i * 2 + 1]);
                nodeEntity.Add(nodeComponent);

                splineEntity.AddChild(nodeEntity);
                splineComponent.Nodes.Add(nodeComponent);
            }

            // We use a spline renderer if we want to view our spline in the game
            splineComponent.SplineRenderer.SegmentsMaterial = splineMaterials[random.Next(0, splineMaterials.Count)];
            splineComponent.SplineRenderer.Segments = true;
            splineComponent.SplineRenderer.BoundingBox = false;
        }

        public override void Update()
        {
            DebugText.Print($"Press C to clean splines", new Int2(1600, 20));
            DebugText.Print($"Press G to generate {splineAmount} splines ", new Int2(1600, 40));
            DebugText.Print($"Last generation run took {stopwatch?.ElapsedMilliseconds} milliseconds", new Int2(1600, 80));
            
            //Clean existing splines
            if (Input.IsKeyPressed(Keys.C))
            {
                ClearSplines();
            }

            //Generate new splines
            if (Input.IsKeyPressed(Keys.G))
            {
                GenerateSplines();
            }

        }

        private void ClearSplines()
        {
            for (var i = 0; i < splineAmount; i++)
            {
                Entity.Scene.Entities.Remove(splineEntities[i]);
            }
        }
        
        private Vector3 RandomVector3()
        {
            return new(
                Random(-(int)splineGenerationArea.X, (int)splineGenerationArea.X),
                Random(-(int)splineGenerationArea.Y, (int)splineGenerationArea.Y),
                Random(-(int)splineGenerationArea.Z, (int)splineGenerationArea.Z));
        }

        private Vector3 RandomOffsetVector3()
        {
            const int offset = 50;
            return new Vector3(Random(-offset, offset), Random(-offset, offset), Random(-offset, offset));
        }
        
        private int Random(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}