using System;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace mis.Editor
{
    public sealed class ObjectLandingTool
    {
        private const string MENU_PATH = "Tools/Land Selected Objects ^#L";
        private const float CHECK_HEIGHT = 1000f;
        private const float CHECK_DISTANCE = 5000f;
        private const int CHECK_LAYER = 1; // Default

        private static Transform[] GetSelectedTransforms() =>
            Selection.GetFiltered<Transform>(SelectionMode.ExcludePrefab);

        [MenuItem(MENU_PATH, isValidateFunction: true)]
        private static bool CanLandObjects()
        {
            return GetSelectedTransforms().Any();
        }

        [MenuItem(MENU_PATH, isValidateFunction: false)]
        private static void TryToLandObjects()
        {
            var transforms = GetSelectedTransforms();

            foreach (var transform in transforms)
            {
                var originalPoint = transform.position;
                var checkPoint = originalPoint;
                checkPoint.y = CHECK_HEIGHT;

                var hits = Physics.RaycastAll(checkPoint, Vector3.down, CHECK_DISTANCE, CHECK_LAYER)
                    .OrderByDescending(hit => Vector3.Distance(checkPoint, hit.point))
                    .ToArray();

                if (!hits.Any())
                {
                    continue;
                }

                hits = hits.Select(hit => new { hit, distance = Vector3.Distance(hit.point, originalPoint) })
                    .OrderBy(tuple => tuple.distance)
                    .Select(tuple => tuple.hit)
                    .ToArray();

                var hit = hits[0];

                if (hit.point.y > originalPoint.y)
                {
                    if (hits.Length == 0)
                    {
                        continue;
                    }

                    hit = hits[1];
                }

                transform.position = hit.point;
            }
        }
    }
}