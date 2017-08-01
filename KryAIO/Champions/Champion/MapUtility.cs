// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-31-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-31-2017
// ***********************************************************************
// <copyright file="MapUtility.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Util.Cache;
using ceometric.VectorGeometry;

namespace KryAIO.Champions.Champion
{
    /// <summary>
    /// Class Champion.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    partial class Champion
    {
        /// <summary>
        /// Gets the ally heroes count in zone.
        /// </summary>
        /// <param name="zoneCircle">The zone circle.</param>
        /// <returns>System.Int32.</returns>
        protected static int GetAllyHeroesCountInZone(ref Circle zoneCircle)
        {
            var allyHeroesCount = 0;
            foreach (var hero in GameObjects.AllyHeroes)
            {
                if (!zoneCircle.Contains(new Point(hero.Position.X, 0, hero.Position.Z)) || hero.IsDead)
                    continue;

                allyHeroesCount++;
            }
            return allyHeroesCount;
        }

        /// <summary>
        /// Gets the enemy heroes count in zone.
        /// </summary>
        /// <param name="zoneCircle">The zone circle.</param>
        /// <returns>System.Int32.</returns>
        protected static int GetEnemyHeroesCountInZone(ref Circle zoneCircle)
        {
            var enemyHeroesCount = 0;
            foreach (var hero in GameObjects.EnemyHeroes)
            {
                if (!zoneCircle.Contains(new Point(hero.Position.X, 0, hero.Position.Z)) || hero.IsDead)
                    continue;

                enemyHeroesCount++;
            }
            return enemyHeroesCount;
        }

        /// <summary>
        /// Gets the ally minions count in zone.
        /// </summary>
        /// <param name="zoneCircle">The zone circle.</param>
        /// <returns>System.Int32.</returns>
        protected static int GetAllyMinionsCountInZone(ref Circle zoneCircle)
        {
            var allyMinionsCount = 0;
            foreach (var minion in GameObjects.AllyMinions)
            {
                if (!zoneCircle.Contains(new Point(minion.Position.X, 0, minion.Position.Z))) continue;

                allyMinionsCount++;
            }
            return allyMinionsCount;
        }

        /// <summary>
        /// Gets the enemy minions count in zone.
        /// </summary>
        /// <param name="zoneCircle">The zone circle.</param>
        /// <returns>System.Int32.</returns>
        protected static int GetEnemyMinionsCountInZone(ref Circle zoneCircle)
        {
            var enemyMinionsCount = 0;
            foreach (var minion in GameObjects.EnemyMinions)
            {
                if (!zoneCircle.Contains(new Point(minion.Position.X, 0, minion.Position.Z))) continue;

                enemyMinionsCount++;
            }
            return enemyMinionsCount;
        }

        protected bool IsValidTargetLocked(AttackableUnit target, float range = float.MaxValue)
        {
            return TargetValidateLocker.RunWithLock(target.NetworkId, () => target.IsValidTarget(range));
        }
    }
}