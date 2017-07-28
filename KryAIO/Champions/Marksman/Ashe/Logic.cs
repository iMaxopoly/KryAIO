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
using Aimtec.SDK.Prediction.Skillshots;
using Aimtec.SDK.Util.Cache;
using System;
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
            if (!WSpell.Ready || !target.IsValidTarget(WSpell.Range)) return;

            try
            {
                WSpell.Cast(target);
            }
            catch (NullReferenceException exception)
            {
                Console.WriteLine($"Null Reference Encountered: {exception.Message}");
            }
        }

        /// <summary>
        /// Processes the combo mechanics.
        /// </summary>
        /// <param name="target">The target.</param>
        private void ProcessComboMechanics(Obj_AI_Base target)
        {
            if (WSpell.Ready && target.IsValidTarget(WSpell.Range))
            {
                try
                {
                    WSpell.Cast(target);
                }
                catch (NullReferenceException exception)
                {
                    Console.WriteLine($"Null Reference Encountered: {exception.Message}");
                }
            }

            if (!QSpell.Ready || !target.IsValidTarget(LocalHero.AttackRange)) return;
            {
                try
                {
                    QSpell.Cast(target);
                }
                catch (NullReferenceException exception)
                {
                    Console.WriteLine($"Null Reference Encountered: {exception.Message}");
                }
            }
        }

        /// <summary>
        /// Processes the farm mechanics.
        /// </summary>
        private static void ProcessFarmMechanics()
        {
            // Todo Finish ProcessFarmMechanics
        }

        /// <summary>
        /// Processes the instagib mechanics.
        /// </summary>
        /// <returns>Task.</returns>
        private async Task ProcessInstagibMechanics()
        {
            foreach (var enemyHero in GameObjects.EnemyHeroes)
            {
                if (LocalHero.Distance(enemyHero) >= LocalHero.BoundingRadius + 1200F) continue;

                var taskCheckKillableWithSpellWAndSpellRAndAuto =
                    await IsKillableWithSpellWAndSpellRAndAutoAttack(enemyHero);

                var taskCheckKillableWithSpellW = await IsKillableWithSpellW(enemyHero);
                var taskCheckKillableWithSpellR = await IsKillableWithSpellR(enemyHero);

                if (taskCheckKillableWithSpellWAndSpellRAndAuto)
                {
                    try
                    {
                        RSpell.Cast(enemyHero);
                        WSpell.Cast(enemyHero);
                        continue;
                    }
                    catch (NullReferenceException exception)
                    {
                        Console.WriteLine($"Null Reference Encountered: {exception.Message}");
                    }
                }

                if (taskCheckKillableWithSpellW)
                {
                    try
                    {
                        WSpell.Cast(enemyHero);
                        continue;
                    }
                    catch (NullReferenceException exception)
                    {
                        Console.WriteLine($"Null Reference Encountered: {exception.Message}");
                    }
                }

                if (!taskCheckKillableWithSpellR) continue;
                {
                    try
                    {
                        RSpell.Cast(enemyHero);
                    }
                    catch (NullReferenceException exception)
                    {
                        Console.WriteLine($"Null Reference Encountered: {exception.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// Processes the trick trap mechanics.
        /// </summary>
        private void ProcessTrickTrapMechanics()
        {
            var underTowerMechanicsTask = Task.Factory.StartNew(() =>
            {
                foreach (var enemyHero in GameObjects.EnemyHeroes)
                {
                    if (LocalHero.Distance(enemyHero) > LocalHero.BoundingRadius + WSpell.Range) continue;

                    if (!enemyHero.IsUnderAllyTurret() ||
                        !enemyHero.IsValidTarget(LocalHero.BoundingRadius + WSpell.Range) ||
                        !RSpell.Ready) continue;

                    try
                    {
                        RSpell.Cast(enemyHero);
                    }
                    catch (NullReferenceException exception)
                    {
                        Console.WriteLine($"Null Reference Encountered: {exception.Message}");
                    }
                }
            });

            var onOpportunityMechanicsTask = Task.Factory.StartNew(() =>
            {
                foreach (var enemyHero in GameObjects.EnemyHeroes)
                {
                    if (LocalHero.Distance(enemyHero) > WSpell.Range ||
                        !enemyHero.IsValidTarget(WSpell.Range)) continue;

                    if (WSpell.Ready)
                    {
                        try
                        {
                            WSpell.Cast(enemyHero);
                        }
                        catch (NullReferenceException exception)
                        {
                            Console.WriteLine($"Null Reference Encountered: {exception.Message}");
                        }
                    }

                    if (LocalHero.Distance(enemyHero) >= LocalHero.AttackRange ||
                        enemyHero.HealthPercent() > LocalHero.HealthPercent()) continue;
                    {
                        var zone = new Rectangle(LocalHero.Position - LocalHero.AttackRange / 2,
                            enemyHero.Position + LocalHero.AttackRange / 2, LocalHero.AttackRange);

                        var allyHeroesCountTask = Task<int>.Factory.StartNew(() =>
                        {
                            var allyHeroesCount = 0;
                            foreach (var hero in GameObjects.AllyHeroes)
                            {
                                if (!zone.Contains(hero.Position)) continue;

                                allyHeroesCount++;
                            }
                            return allyHeroesCount;
                        });

                        var enemyHeroesCountTask = Task<int>.Factory.StartNew(() =>
                        {
                            var enemyHeroesCount = 0;
                            foreach (var hero in GameObjects.EnemyHeroes)
                            {
                                if (!zone.Contains(hero.Position)) continue;

                                enemyHeroesCount++;
                            }
                            return enemyHeroesCount;
                        });

                        var allyMinionsCountTask = Task<int>.Factory.StartNew(() =>
                        {
                            var allyMinionsCount = 0;
                            foreach (var minion in GameObjects.AllyMinions)
                            {
                                if (!zone.Contains(minion.Position)) continue;

                                allyMinionsCount++;
                            }
                            return allyMinionsCount;
                        });

                        var enemyMinionsCountTask = Task<int>.Factory.StartNew(() =>
                        {
                            var enemyMinionsCount = 0;
                            foreach (var minion in GameObjects.EnemyMinions)
                            {
                                if (!zone.Contains(minion.Position)) continue;

                                enemyMinionsCount++;
                            }
                            return enemyMinionsCount;
                        });

                        if (allyHeroesCountTask.Result < enemyHeroesCountTask.Result &&
                            allyMinionsCountTask.Result < enemyMinionsCountTask.Result &&
                            LocalHero.GetAutoAttackDamage(enemyHero) <
                            enemyHero.GetAutoAttackDamage(LocalHero) && !LocalHero.IsUnderEnemyTurret()) continue;
                        if (LocalHero.Distance(enemyHero) < WSpell.Range / 2 ||
                            RSpell.HitChance == HitChance.VeryHigh)
                        {
                            try
                            {
                                RSpell.Cast(enemyHero);
                            }
                            catch (NullReferenceException exception)
                            {
                                Console.WriteLine($"Null Reference Encountered: {exception.Message}");
                            }
                        }

                        try
                        {
                            QSpell.Cast(enemyHero);
                        }
                        catch (NullReferenceException exception)
                        {
                            Console.WriteLine($"Null Reference Encountered: {exception.Message}");
                        }

                        Krywalker.Orbwalk(enemyHero);
                        Console.WriteLine("COMBO MODE WORK");
                    }
                }
            });

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
                                         enemyHero.IsValidTarget(RSpell.Range));
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