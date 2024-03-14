// Designed by KINEMATION, 2024.

using Kinemation.FPSFramework.Runtime.Core.Types;
using UnityEngine;

namespace Kinemation.FPSFramework.Runtime.FPSAnimator
{
    public static class FPSAnimLib
    {
        public static Vector2 GetLookAtInput(Transform root, Transform from, Vector3 to)
        {
            Vector2 result = Vector2.zero;
            
            Quaternion rot = Quaternion.LookRotation(to - from.position);
            rot = Quaternion.Inverse(root.rotation) * rot;

            Vector3 euler = rot.eulerAngles;
            result.x = CoreToolkitLib.NormalizeAngle(euler.y);
            result.y = CoreToolkitLib.NormalizeAngle(euler.x);

            return result;
        }
        
        public static float ExpDecayAlpha(float speed, float deltaTime)
        {
            return 1 - Mathf.Exp(-speed * deltaTime);
        }
    }
}