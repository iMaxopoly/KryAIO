// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-21-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-31-2017
// ***********************************************************************
// <copyright file="ChampionSpells.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Prediction.Skillshots;
using KryAIO.Logger;
using Spell = Aimtec.SDK.Spell;

namespace KryAIO.Champions.Marksman.Ashe
{
    /// <summary>
    /// Class Ashe.
    /// </summary>
    /// <seealso cref="KryAIO.Champions.Champion.Champion" />
    /// <seealso cref="Champion" />
    /// <seealso cref="KryAIO.IChampion" />
    public partial class Ashe
    {
        /// <summary>
        /// Initializes the spells.
        /// </summary>
        private void InitializeSpells()
        {
            QSpell = new Spell(SpellSlot.Q);
            WSpell = new Spell(SpellSlot.W, LocalHero.BoundingRadius + 1200F);
            ESpell = new Spell(SpellSlot.E, 2000F);
            RSpell = new Spell(SpellSlot.R, 2000F);

            WSpell.SetSkillshot(0.25f, 1.125F, 1500F, true, SkillshotType.Cone, false, HitChance.None);
            ESpell.SetSkillshot(0.25f, 130f, 1600f, false, SkillshotType.Line, false, HitChance.None);
            RSpell.SetSkillshot(0.25f, 130f, 1600f, false, SkillshotType.Line, false, HitChance.None);
        }

        /// <summary>
        /// Casts the spell q.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="priority">The priority.</param>
        private void CastSpellQ(AttackableUnit target, SpellPriority priority)
        {
            SpellCastLocker.RunWithLock((int) QSpell.Slot, () =>
            {
                if (!QSpell.Ready || !IsValidTargetLocked(target, LocalHeroTrueRange)) return;

                var localHeroMana = 100 * LocalHero.Mana / LocalHero.MaxMana;

                switch (priority)
                {
                    case SpellPriority.Harass:
                        if (localHeroMana > 70)
                            QSpell.Cast();
                        break;
                    case SpellPriority.Farm:
                        if (localHeroMana > 80)
                            QSpell.Cast();
                        break;
                    case SpellPriority.Combo:
                    case SpellPriority.Force:
                        QSpell.Cast();
                        break;
                    default:
                        Logger.Log($"Caught ArgumentOutOfRangeException: {nameof(priority)}, {priority}", LogType.Error,
                            EventType.OnGameOnUpdateEvent);
                        break;
                }
            });
        }

        /// <summary>
        /// Casts the spell w.
        /// </summary>
        /// <param name="castPosition">The cast position.</param>
        /// <param name="priority">The priority.</param>
        private void CastSpellW(Vector2 castPosition, SpellPriority priority)
        {
            SpellCastLocker.RunWithLock((int) WSpell.Slot, () =>
            {
                if (!WSpell.Ready) return;

                var localHeroMana = 100 * LocalHero.Mana / LocalHero.MaxMana;

                switch (priority)
                {
                    case SpellPriority.Harass:
                        if (localHeroMana > 55)
                            WSpell.Cast(castPosition);
                        else if (localHeroMana > 30 && localHeroMana <= 55)
                            WSpell.Cast(castPosition);
                        else if (localHeroMana <= 30)
                            WSpell.Cast(castPosition);
                        break;
                    case SpellPriority.Farm:
                        if (localHeroMana > 30)
                            WSpell.Cast(castPosition);
                        break;
                    case SpellPriority.Combo:
                    case SpellPriority.Force:
                        WSpell.Cast(castPosition);
                        break;

                    default:
                        Logger.Log($"Caught ArgumentOutOfRangeException: {nameof(priority)}, {priority}", LogType.Error,
                            EventType.OnGameOnUpdateEvent);
                        break;
                }
            });
        }

        /// <summary>
        /// Casts the spell w.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="priority">The priority.</param>
        private void CastSpellW(Obj_AI_Base target, SpellPriority priority)
        {
            SpellCastLocker.RunWithLock((int) WSpell.Slot, () =>
            {
                if (!WSpell.Ready || !IsValidTargetLocked(target, WSpell.Range)) return;

                var localHeroMana = 100 * LocalHero.Mana / LocalHero.MaxMana;

                var targetPrediction = WSpell.GetPrediction(target);
                switch (priority)
                {
                    case SpellPriority.Harass:
                        if (localHeroMana > 55 && targetPrediction.HitChance >= HitChance.Medium)
                            WSpell.Cast(targetPrediction.CastPosition);
                        else if (localHeroMana > 30 && localHeroMana <= 55 &&
                                 targetPrediction.HitChance >= HitChance.High)
                            WSpell.Cast(targetPrediction.CastPosition);
                        else if (localHeroMana <= 30 && targetPrediction.HitChance >= HitChance.VeryHigh)
                            WSpell.Cast(targetPrediction.CastPosition);
                        break;
                    case SpellPriority.Farm:
                        if (localHeroMana > 30 && targetPrediction.HitChance >= HitChance.Low)
                            WSpell.Cast(targetPrediction.CastPosition);
                        break;
                    case SpellPriority.Combo:
                        if (targetPrediction.HitChance >= HitChance.Medium)
                            WSpell.Cast(targetPrediction.CastPosition);
                        break;
                    case SpellPriority.Force:
                        if (targetPrediction.HitChance >= HitChance.Low)
                            WSpell.Cast(targetPrediction.CastPosition);
                        break;

                    default:
                        Logger.Log($"Caught ArgumentOutOfRangeException: {nameof(priority)}, {priority}", LogType.Error,
                            EventType.OnGameOnUpdateEvent);
                        break;
                }
            });
        }

        /// <summary>
        /// Casts the spell r.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="priority">The priority.</param>
        private void CastSpellR(Obj_AI_Base target, SpellPriority priority)
        {
            SpellCastLocker.RunWithLock((int) RSpell.Slot, () =>
            {
                if (!RSpell.Ready) return;

                var targetPrediction = RSpell.GetPrediction(target);
                switch (priority)
                {
                    case SpellPriority.Harass:
                        break;
                    case SpellPriority.Combo:
                        if (targetPrediction.HitChance >= HitChance.High)
                            RSpell.Cast(targetPrediction.CastPosition);
                        break;
                    case SpellPriority.Force:
                        if (targetPrediction.HitChance >= HitChance.Low)
                            RSpell.Cast(targetPrediction.CastPosition);
                        break;
                    case SpellPriority.Farm:
                        break;
                    default:
                        Logger.Log($"Caught ArgumentOutOfRangeException: {nameof(priority)}, {priority}", LogType.Error,
                            EventType.OnGameOnUpdateEvent);
                        break;
                }
            });
        }
    }
}