// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-22-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-23-2017
// ***********************************************************************
// <copyright file="CombatLogic.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Util.Cache;
using ceometric.ComputationalGeometry;
using ceometric.VectorGeometry;
using System.Threading.Tasks;

namespace KryAIO.Champions.Marksman.Ashe
{
    /// <summary>
    /// Class Ashe.
    /// </summary>
    /// <seealso cref="Champion" />
    /// <seealso cref="KryAIO.IChampion" />
    public partial class Ashe
    {
        /// <summary>
        /// Processes the harass mechanics.
        /// </summary>
        /// <param name="target">The target.</param>
        private void ProcessHarassMechanics(Obj_AI_Base target)
        {
            CastSpellW(target, SpellPriority.Harass);
        }

        /// <summary>
        /// Processes the combo mechanics.
        /// </summary>
        /// <param name="target">The target.</param>
        private void ProcessComboMechanics(Obj_AI_Base target)
        {
            CastSpellW(target, SpellPriority.Combo);
            CastSpellQ(target, SpellPriority.Combo);
        }

        /// <summary>
        /// Processes the farm mechanics.
        /// </summary>
        private void ProcessFarmMechanics()
        {
            if (!WSpell.Ready) return;

            var zoneCircle = new Circle(new Point(LocalHero.Position.X, 0, LocalHero.Position.Z),
                LocalHeroTrueRange * 3, new Vector3d(LocalHero.Position.X, 0, LocalHero.Position.Z));

            var enemyHeroesCountTask = Task<int>.Factory.StartNew(() => GetEnemyHeroesCountInZone(ref zoneCircle));

            if (enemyHeroesCountTask.Result > 0) return;

            var minionPointSet = new PointSet();
            foreach (var enemyMinion in GameObjects.EnemyMinions)
            {
                if (enemyMinion.Team != GameObjectTeam.Chaos || !enemyMinion.IsValidTarget(WSpell.Range)) continue;

                minionPointSet.Add(new Point(enemyMinion.Position.X, enemyMinion.Position.Z, 0));
            }

            if (minionPointSet.Count < 2) return;

            var smallestEnclosingCircle = new SmallestEnclosingCircle(minionPointSet);
            var computedCircle = smallestEnclosingCircle.ComputeCircle();

            CastSpellW(new Vector2((float) computedCircle.Center.X, (float) computedCircle.Center.Y),
                SpellPriority.Farm);
        }

        /// <summary>
        /// Processes the instagib mechanics.
        /// </summary>
        /// <returns>Task.</returns>
        private async Task ProcessInstagibMechanics()
        {
            foreach (var enemyHero in GameObjects.EnemyHeroes)
            {
                if (LocalHero.Distance(enemyHero) > WSpell.Range) continue;

                var taskCheckKillableWithSpellWAndSpellRAndAuto =
                    await IsKillableWithSpellWAndSpellRAndAutoAttack(enemyHero);

                var taskCheckKillableWithSpellW = await IsKillableWithSpellW(enemyHero);
                var taskCheckKillableWithSpellR = await IsKillableWithSpellR(enemyHero);

                if (taskCheckKillableWithSpellWAndSpellRAndAuto)

                {
                    CastSpellR(enemyHero, SpellPriority.Combo);
                    CastSpellW(enemyHero, SpellPriority.Force);
                    continue;
                }

                if (taskCheckKillableWithSpellW)
                {
                    CastSpellW(enemyHero, SpellPriority.Force);
                    continue;
                }

                if (!taskCheckKillableWithSpellR) continue;
                {
                    CastSpellR(enemyHero, SpellPriority.Combo);
                }
            }
        }

        /// <summary>
        /// Processes the trick trap mechanics.
        /// </summary>
        private void ProcessTrickTrapMechanics()
        {
            var panicMechanicsTask = Task.Factory.StartNew(() =>
            {
                if (LocalHero.HealthPercent() > 33) return;

                foreach (var enemyHero in GameObjects.EnemyHeroes)
                {
                    if (LocalHero.Distance(enemyHero) >= LocalHeroTrueRange / 2 || !enemyHero.IsFacing(LocalHero) ||
                        enemyHero.HealthPercent() <= LocalHero.HealthPercent()) continue;

                    CastSpellR(enemyHero, SpellPriority.Force);
                    Krywalker.Orbwalk(enemyHero);
                }
            });

            var underTowerMechanicsTask = Task.Factory.StartNew(() =>
            {
                foreach (var enemyHero in GameObjects.EnemyHeroes)
                {
                    if (!enemyHero.IsUnderAllyTurret() || !enemyHero.IsValidTarget(LocalHeroTrueRange) ||
                        !LocalHero.IsUnderAllyTurret()) continue;

                    CastSpellR(enemyHero, SpellPriority.Force);
                }
            });

            var onOpportunityMechanicsTask = Task.Factory.StartNew(() =>
            {
                foreach (var enemyHero in GameObjects.EnemyHeroes)
                {
                    if (!enemyHero.IsValidTarget(WSpell.Range)) continue;

                    CastSpellW(enemyHero, SpellPriority.Combo);

                    if (!enemyHero.IsValidTarget(LocalHeroTrueRange) ||
                        enemyHero.HealthPercent() > LocalHero.HealthPercent()) continue;
                    {
                        var zoneCircle = new Circle(new Point(LocalHero.Position.X, 0, LocalHero.Position.Z),
                            LocalHeroTrueRange * 3, new Vector3d(LocalHero.Position.X, 0, LocalHero.Position.Z));

                        var allyHeroesCountTask =
                            Task<int>.Factory.StartNew(() => GetAllyHeroesCountInZone(ref zoneCircle));
                        var enemyHeroesCountTask =
                            Task<int>.Factory.StartNew(() => GetEnemyHeroesCountInZone(ref zoneCircle));
                        var allyMinionsCountTask =
                            Task<int>.Factory.StartNew(() => GetAllyMinionsCountInZone(ref zoneCircle));
                        var enemyMinionsCountTask =
                            Task<int>.Factory.StartNew(() => GetEnemyMinionsCountInZone(ref zoneCircle));

                        if (allyHeroesCountTask.Result < enemyHeroesCountTask.Result ||
                            allyMinionsCountTask.Result < enemyMinionsCountTask.Result ||
                            LocalHero.GetAutoAttackDamage(enemyHero) <= enemyHero.GetAutoAttackDamage(LocalHero) ||
                            LocalHero.IsUnderEnemyTurret()) continue;

                        if (LocalHero.Distance(enemyHero) < WSpell.Range / 2)
                        {
                            CastSpellR(enemyHero, SpellPriority.Combo);
                        }

                        if (!enemyHero.IsValidTarget(LocalHeroTrueRange)) continue;

                        CastSpellQ(enemyHero, SpellPriority.Combo);

                        Krywalker.Orbwalk(enemyHero);
                    }
                }
            });

            panicMechanicsTask.Wait();
            underTowerMechanicsTask.Wait();
            onOpportunityMechanicsTask.Wait();
        }

        /// <summary>
        /// Determines whether [is killable with spell w] [the specified enemy hero].
        /// </summary>
        /// <param name="enemyHero">The enemy hero.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        private async Task<bool> IsKillableWithSpellW(Obj_AI_Base enemyHero)
        {
            return await Task.FromResult(WSpell.Ready && LocalHero.GetSpellDamage(enemyHero, SpellSlot.W) >
                                         enemyHero.Health + enemyHero.PhysicalShield + 20 &&
                                         enemyHero.IsValidTarget(WSpell.Range));
        }

        /// <summary>
        /// Determines whether [is killable with spell r] [the specified enemy hero].
        /// </summary>
        /// <param name="enemyHero">The enemy hero.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        private async Task<bool> IsKillableWithSpellR(Obj_AI_Base enemyHero)
        {
            return await Task.FromResult(RSpell.Ready && LocalHero.GetSpellDamage(enemyHero, SpellSlot.R) >
                                         enemyHero.Health + enemyHero.MagicalShield + 20 &&
                                         enemyHero.IsValidTarget(RSpell.Range) && enemyHero.HasBuff("recall"));
        }

        /// <summary>
        /// Determines whether [is killable with spell w and spell r and automatic attack] [the specified enemy hero].
        /// </summary>
        /// <param name="enemyHero">The enemy hero.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        private async Task<bool> IsKillableWithSpellWAndSpellRAndAutoAttack(Obj_AI_Base enemyHero)
        {
            return await Task.FromResult(WSpell.Ready && RSpell.Ready &&
                                         LocalHero.Distance(enemyHero) < WSpell.Range &&
                                         LocalHero.GetSpellDamage(enemyHero, SpellSlot.W) +
                                         LocalHero.GetSpellDamage(enemyHero, SpellSlot.R) +
                                         LocalHero.GetAutoAttackDamage(enemyHero) >
                                         enemyHero.Health + enemyHero.PhysicalShield &&
                                         enemyHero.IsValidTarget(WSpell.Range));
        }
    }
}