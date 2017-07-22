// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-22-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-22-2017
// ***********************************************************************
// <copyright file="CombatLogic.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.TargetSelector;
using Aimtec.SDK.Util.Cache;
using System.Threading.Tasks;

namespace KryAIO.Champions.Ashe
{
    /// <summary>
    /// Class Ashe.
    /// </summary>
    /// <seealso cref="KryAIO.Champions.Champion" />
    /// <seealso cref="KryAIO.IChampion" />
    public partial class Ashe
    {
        /// <summary>
        /// Processes the harass mechanics.
        /// </summary>
        private void ProcessHarassMechanics()
        {
            var target = TargetSelector.GetTarget(LocalHero.AttackRange);

            if (_spellW.Ready && target.IsValidTarget(_spellW.Range))
            {
                _spellW.Cast(target);
            }
        }

        /// <summary>
        /// Processes the combo mechanics.
        /// </summary>
        private void ProcessComboMechanics()
        {
            var target = TargetSelector.GetTarget(LocalHero.AttackRange);

            if (_spellW.Ready && target.IsValidTarget(_spellW.Range))
            {
                _spellW.Cast(target);
            }

            if (_spellQ.Ready && target.IsValidTarget(LocalHero.AttackRange))
            {
                _spellQ.Cast(target);
            }
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

                var taskCheckKillableWithSpellW = await IsKillableWithSpellW(enemyHero);
                if (taskCheckKillableWithSpellW)
                {
                    _spellW.Cast(enemyHero);
                }

                var taskCheckKillableWithSpellR = await IsKillableWithSpellR(enemyHero);
                if (taskCheckKillableWithSpellR)
                {
                    _spellR.Cast(enemyHero);
                }

                var taskCheckKillableWithSpellWAndSpellRAndAuto =
                    await IsKillableWithSpellWAndSpellRAndAutoAttack(enemyHero);
                if (!taskCheckKillableWithSpellWAndSpellRAndAuto) continue;
                _spellR.Cast(enemyHero);
                _spellW.Cast(enemyHero);
            }
        }

        /// <summary>
        /// Determines whether [is killable with spell w] [the specified enemy hero].
        /// </summary>
        /// <param name="enemyHero">The enemy hero.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        private async Task<bool> IsKillableWithSpellW(Obj_AI_Base enemyHero)
        {
            return await Task.FromResult(_spellW.Ready && LocalHero.GetSpellDamage(enemyHero, SpellSlot.W) >
                                         enemyHero.Health + enemyHero.PhysicalShield + 20 &&
                                         enemyHero.IsValidTarget(_spellW.Range));
        }

        /// <summary>
        /// Determines whether [is killable with spell r] [the specified enemy hero].
        /// </summary>
        /// <param name="enemyHero">The enemy hero.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        private async Task<bool> IsKillableWithSpellR(Obj_AI_Base enemyHero)
        {
            return await Task.FromResult(_spellR.Ready && LocalHero.GetSpellDamage(enemyHero, SpellSlot.R) >
                                         enemyHero.Health + enemyHero.MagicalShield + 20 &&
                                         enemyHero.IsValidTarget(_spellR.Range));
        }

        /// <summary>
        /// Determines whether [is killable with spell w and spell r and automatic attack] [the specified enemy hero].
        /// </summary>
        /// <param name="enemyHero">The enemy hero.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        private async Task<bool> IsKillableWithSpellWAndSpellRAndAutoAttack(Obj_AI_Base enemyHero)
        {
            return await Task.FromResult(_spellW.Ready && _spellR.Ready &&
                                         LocalHero.Distance(enemyHero) < _spellW.Range &&
                                         LocalHero.GetSpellDamage(enemyHero, SpellSlot.W) +
                                         LocalHero.GetSpellDamage(enemyHero, SpellSlot.R) +
                                         LocalHero.GetAutoAttackDamage(enemyHero) >
                                         enemyHero.Health + enemyHero.PhysicalShield &&
                                         enemyHero.IsValidTarget(_spellW.Range));
        }
    }
}