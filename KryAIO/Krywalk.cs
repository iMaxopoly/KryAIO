// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-26-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-26-2017
// ***********************************************************************
// <copyright file="Krywalk.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary>Simple Orbwalker | Credits: Manciuszz and eXtragoZ</summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Extensions;

namespace KryAIO
{
    /// <summary>
    /// Class Krywalk.
    /// </summary>
    public class Krywalk
    {
        /// <summary>
        /// The last wind up time
        /// </summary>
        private float _lastWindUpTime;

        /// <summary>
        /// The last attack cd
        /// </summary>
        private float _lastAttackCooldown;

        /// <summary>
        /// The last attack
        /// </summary>
        private float _lastAttack;

        /// <summary>
        /// Gets the hero true range.
        /// </summary>
        /// <value>The hero true range.</value>
        public float HeroTrueRange { get; }

        /// <summary>
        /// The local hero
        /// </summary>
        private readonly Obj_AI_Hero _localHero;

        /// <summary>
        /// Initializes a new instance of the <see cref="Krywalk"/> class.
        /// </summary>
        public Krywalk()
        {
            _localHero = ObjectManager.GetLocalPlayer();
            HeroTrueRange = _localHero.BoundingRadius + _localHero.AttackRange;

            _lastWindUpTime = 0;
            _lastAttackCooldown = 0;
            _lastAttack = Game.Ping;

            GameObject.OnCreate += sender =>
            {
                if (_localHero.IsDead) return;

                var missileClient = sender as MissileClient;
                if (missileClient == null) return;

                if (!sender.IsMe) return;
                if ((missileClient.Name.Contains("attack") || IsSpellAttack(missileClient.Name)) &&
                    !IsNotAttack(missileClient.Name))
                {
                    _lastAttack = Game.TickCount - Game.Ping / 2;
                    _lastWindUpTime = (_localHero.AttackCastDelay + 90) * 1000;
                    _lastAttackCooldown = _localHero.AttackDelay * 1000;
                }
                else if (RefreshAttack(missileClient.Name))
                {
                    _lastAttack = Game.TickCount - Game.Ping / 2 - _lastAttackCooldown;
                }
            };
        }

        private bool RefreshAttack(string missileClientName)
        {
            return false;
        }

        private bool IsNotAttack(string missileClientName)
        {
            return false;
        }

        private bool IsSpellAttack(string spellName)
        {
            return spellName == "frostarrow";
        }

        /// <summary>
        /// Orbwalks the specified target hero.
        /// </summary>
        /// <param name="targetHero">The target hero.</param>
        public void Orbwalk(GameObject targetHero)
        {
            if (targetHero == null)
                return;

            if (TimeToShoot())
                _localHero.IssueOrder(OrderType.AttackUnit, targetHero);
            else if (HeroCanMove())
                MoveToCursor();
        }

        /// <summary>
        /// Moves to cursor.
        /// </summary>
        private void MoveToCursor()
        {
            var moveToPos = _localHero.Position + (Game.CursorPos - _localHero.Position).Normalized() * 300;
            _localHero.IssueOrder(OrderType.MoveTo, moveToPos);
        }

        /// <summary>
        /// Heroes the can move.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool HeroCanMove()
        {
            return Game.TickCount + Game.Ping / 2 > _lastAttack + _lastWindUpTime + 20;
        }

        /// <summary>
        /// Times to shoot.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool TimeToShoot()
        {
            return Game.TickCount + Game.Ping / 2 > _lastAttack + _lastAttackCooldown;
        }
    }
}