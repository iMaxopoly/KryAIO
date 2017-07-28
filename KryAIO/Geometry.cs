using Aimtec;
using MathNet.Spatial.Euclidean;
using System.Collections.Generic;

namespace KryAIO
{
    internal static class Geometry
    {
        public static void GetMaxHitAngleFromConvexHullSkillShot
        (
            Vector3 heroPosition,
            List<Obj_AI_Base> gameObjectsList,
            float skillShotAngleAttribute,
            float skillShotDistanceAttribute
        )
        {
            var heroPositionPoint2D = new Point2D(heroPosition.X, heroPosition.Z);

            var polygon2D = new Polygon2D(new List<Point2D> {heroPositionPoint2D});
            var convexHullFromPoints = Polygon2D.GetConvexHullFromPoints(polygon2D);
        }
    }
}