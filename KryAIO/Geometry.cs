// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-25-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-31-2017
// ***********************************************************************
// <copyright file="Geometry.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using ceometric.VectorGeometry;

namespace KryAIO
{
    /// <summary>
    /// Class Geometry.
    /// </summary>
    internal static class Geometry
    {
        /// <summary>
        /// Gets the new circle at point.
        /// </summary>
        /// <param name="centerPoint">The center point.</param>
        /// <param name="radius">The radius.</param>
        /// <returns>Circle.</returns>
        public static Circle GetNewCircleAtPoint(Point centerPoint, double radius)
        {
            return new Circle(centerPoint, radius, new Vector3d(centerPoint.X, 0, centerPoint.Z));
        }
    }
}