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
        public Entity closestPointOrb;

        public override void Start()
        {
        }

        public override void Update()
        {
            DebugText.Print($"Use WASD to move the sphere around. The red sphere indicates closest point in spline", new Int2(600, 20));

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
                    closestPointOrb.Transform.UseTRS = false;
                    closestPointOrb.Transform.WorldMatrix.TranslationVector = closestPositionInfo.Position;
                    closestPointOrb.Transform.UpdateLocalFromWorld();
                }
            }
        }
    }
}
