using Stride.Engine;
using Stride.Engine.Splines.Components;
using Stride.Engine.Splines.Models;
using Stride.Particles.Components;

namespace SplineTools
{
    public class SplineTraverserEventsDemo : StartupScript
    {
        public Entity SplineEndReachedParticle;
        public Entity SplineNodeReachedParticle;
        public bool ReverseOnEnd;
        private SplineTraverserComponent traverserComponent;
        private float orignalTravelSpeed = 0;

        public override void Start()
        {
            traverserComponent = Entity.Get<SplineTraverserComponent>();
            traverserComponent.SplineTraverser.OnSplineEndReached += SplineTraverser_OnSplineEndReached;
            traverserComponent.SplineTraverser.OnSplineNodeReached += SplineTraverser_OnSplineNodeReached;
            orignalTravelSpeed = traverserComponent.SplineTraverser.Speed;
        }

        private void SplineTraverser_OnSplineEndReached(SplineNode splineNode)
        {
            CloneParticleAndEnabled(SplineEndReachedParticle);

            if (ReverseOnEnd)
            {
                traverserComponent.SplineTraverser.Speed = orignalTravelSpeed * - 1;
            }
        }

        private void SplineTraverser_OnSplineNodeReached(SplineNode splineNode)
        {
            CloneParticleAndEnabled(SplineNodeReachedParticle);
        }

        private void CloneParticleAndEnabled(Entity particleEntity)
        {
            var particleEntityClone = particleEntity.Clone();
            Entity.AddChild(particleEntityClone);
            particleEntityClone.Get<ParticleSystemComponent>().Enabled = true;
        }
    }
}
