﻿using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Splines.Components;
using Stride.Input;
using System;

namespace SplineTools
{
    public class SplineTraverserByCode : SyncScript
    {
        public SplineComponent splineComponent;
        private SplineTraverserComponent splineTraverserComponent;

        public override void Start()
        {
            if (splineComponent == null)
            {
                throw new NullReferenceException("splineComponent is empty");
            }

            splineTraverserComponent = new SplineTraverserComponent();
            splineTraverserComponent.SplineTraverser.Entity = Entity;
            splineTraverserComponent.SplineComponent = splineComponent;
            Entity.Add(splineTraverserComponent);
        }


        public override void Update()
        {
            DebugText.Print($"Press Space to stop/resume traverser. Moving:{splineTraverserComponent.SplineTraverser.IsMoving}", new Int2(600, 20));
            DebugText.Print($"Use mouse wheel to adjust speed {splineTraverserComponent.SplineTraverser.Speed:0.00}", new Int2(600, 40));

            if (Input.IsKeyPressed(Keys.Space))
            {
                splineTraverserComponent.IsMoving = !splineTraverserComponent.IsMoving;
            }

            var scrollValue = Input.MouseWheelDelta;
            if (scrollValue != 0)
            {
                splineTraverserComponent.Speed += (float)Math.Round(scrollValue * 0.1f, 1);
            }
        }

    }
}
