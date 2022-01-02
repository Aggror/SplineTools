using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Splines.Components;
using Stride.Input;

namespace SplineTools
{

    public class ClosestPointDemo : SyncScript
    {
        public float MoveSpeed = 4.0f;
        public SplineComponent spline;
        public Entity closesPointOrb;

        public override void Start()
        {
        }

        public override void Update()
        {
            float deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;
            Vector3 dir = new();

            if (Input.HasKeyboard)
            {
                // Move with keyboard
                // Forward/Backward
                if (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up))
                {
                    dir.Z += 1;
                }
                if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
                {
                    dir.Z -= 1;
                }

                // Left/Right
                if (Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left))
                {
                    dir.X += 1;
                }
                if (Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right))
                {
                    dir.X -= 1;
                }

                if (dir.Length() != 0)
                {
                    //Update movement
                    dir = Vector3.Normalize(dir);
                    Entity.Transform.Position += dir * MoveSpeed * deltaTime;

                    //Show closest point on spline
                    var closestPositionInfo = spline.GetClosestPointOnSpline(Entity.Transform.WorldMatrix.TranslationVector);
                    closesPointOrb.Transform.UseTRS = false;
                    closesPointOrb.Transform.WorldMatrix.TranslationVector = closestPositionInfo.ClosestPosition;
                    closesPointOrb.Transform.UpdateLocalFromWorld();
                }
            }
        }
    }
}
