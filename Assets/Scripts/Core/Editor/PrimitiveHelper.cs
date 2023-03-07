using System;
using System.Collections.Generic;
using UnityEngine;

namespace mis.Core
{
    //  https://answers.unity.com/questions/514293/changing-a-gameobjects-primitive-mesh.html
    public static class PrimitiveHelper
    {
        private static readonly Dictionary<PrimitiveType, Mesh> _primitiveMeshCache = new();

        public static Mesh GetPrimitiveMesh(PrimitiveType primitiveType)
        {
            if (!_primitiveMeshCache.TryGetValue(primitiveType, out Mesh mesh))
            {
                var path = GetPrimitivePath(primitiveType);
                _primitiveMeshCache[primitiveType] = mesh = Resources.GetBuiltinResource<Mesh>(path);
            }

            return mesh;
        }

        private static string GetPrimitivePath(PrimitiveType primitiveType) =>
            primitiveType switch
            {
                PrimitiveType.Sphere => "New-Sphere.fbx",
                PrimitiveType.Capsule => "New-Capsule.fbx",
                PrimitiveType.Cylinder => "New-Cylinder.fbx",
                PrimitiveType.Cube => "Cube.fbx",
                PrimitiveType.Plane => "New-Plane.fbx",
                PrimitiveType.Quad => "Quad.fbx",
                _ => throw new ArgumentException($"There is no builtin path for PrimitiveType {primitiveType}"),
            };
    }
}