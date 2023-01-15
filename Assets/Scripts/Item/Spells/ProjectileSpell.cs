using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    [CreateAssetMenu(menuName = "Spells/Projectile Spell")]
    public class ProjectileSpell : SpellItem
    {
        public float baseDamage;
        public float projectileVelocity;
        Rigidbody rigidBody;

        public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats, WeaponSlotManager weaponSlotManager)
        {
            base.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);
            GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlotManager.primarySlot.transform);
            instantiatedWarmUpSpellFX.gameObject.transform.localScale = new Vector3(3, 3, 3);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
            // Instantiate the spell in the casting hand of the player
        }

        public override void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats)
        {
            base.SuccessfullyCastSpell(animatorHandler, playerStats);
        }
    }
}
