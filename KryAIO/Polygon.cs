// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-23-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-23-2017
// ***********************************************************************
// <copyright file="Polygon.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Util.ThirdParty;
using System.Collections.Generic;
using System.Drawing;

namespace KryAIO
{
    /// <summary>
    /// Class DrawHelper.
    /// </summary>
    public class DrawHelper
    {
        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="color">The color.</param>
        public void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            var screenStart = start.ToScreenPosition();
            var screenEnd = end.ToScreenPosition();
            Render.Line(screenStart, screenEnd, color);
        }
    }

    /// <summary>
    /// Class Polygon.
    /// </summary>
    /// <seealso cref="KryAIO.DrawHelper" />
    public abstract class Polygon : DrawHelper
    {
        /// <summary>
        /// Draws the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        public abstract void Draw(Color color);

        /// <summary>
        /// Gets the clipper points.
        /// </summary>
        /// <value>The clipper points.</value>
        public List<IntPoint> ClipperPoints
        {
            get
            {
                var clipperpoints = new List<IntPoint>();

                foreach (var p in Points)
                {
                    clipperpoints.Add(new IntPoint(p.X, p.Z));
                }

                return clipperpoints;
            }
        }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        public List<Vector3> Points { get; set; } = new List<Vector3>();

        /// <summary>
        /// Determines whether [contains] [the specified point].
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if [contains] [the specified point]; otherwise, <c>false</c>.</returns>
        public bool Contains(Vector3 point)
        {
            var p = new IntPoint(point.X, point.Z);
            var inpolygon = Clipper.PointInPolygon(p, ClipperPoints);
            return inpolygon == 1;
        }
    }

    /// <summary>
    /// Class Rectangle.
    /// </summary>
    /// <seealso cref="KryAIO.Polygon" />
    public class Rectangle : Polygon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        /// <param name="endPosition">The end position.</param>
        /// <param name="width">The width.</param>
        public Rectangle(Vector3 startPosition, Vector3 endPosition, float width)
        {
            var direction = (startPosition - endPosition).Normalized();
            var perpendicular = Perpendicular(direction);

            var leftBottom = startPosition + width * perpendicular;
            var leftTop = startPosition - width * perpendicular;

            var rightBottom = endPosition - width * perpendicular;
            var rightLeft = endPosition + width * perpendicular;

            Points.Add(leftBottom);
            Points.Add(leftTop);
            Points.Add(rightBottom);
            Points.Add(rightLeft);
        }

        /// <summary>
        /// Perpendiculars the specified v.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns>Vector3.</returns>
        public Vector3 Perpendicular(Vector3 v)
        {
            return new Vector3(-v.Z, v.Y, v.X);
        }

        /// <summary>
        /// Draws the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        public override void Draw(Color color)
        {
            if (Points.Count < 4)
            {
                return;
            }

            for (var i = 0; i <= Points.Count - 1; i++)
            {
                var p2 = (Points.Count - 1 == i) ? 0 : (i + 1);
                DrawLine(Points[i], Points[p2], color);
            }
        }
    }
}