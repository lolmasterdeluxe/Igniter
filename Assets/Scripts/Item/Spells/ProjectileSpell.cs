using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    [CreateAssetMenu(menuName = "Spells/Projectile Spell")]
    public class ProjectileSpell : SpellItem
    {
        public float baseDamage;

        [Header("Projectile Physics")]
        public float projectileForwardVelocity;
        public float projectileUpwardVelocity;
        public float projectileMass;
        public bool isEffectedByGravity;
        Rigidbody rigidBody;

        public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats, WeaponSlotManager weaponSlotManager)
        {
            base.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);
            GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlotManager.primarySlot.transform);
            instantiatedWarmUpSpellFX.gameObject.transform.localScale = new Vector3(3, 3, 3);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
            // Instantiate the spell in the casting hand of the player
        }

        public override void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats, CameraHandler cameraHandler, WeaponSlotManager weaponSlotManager)
        {
            base.SuccessfullyCastSpell(animatorHandler, playerStats, cameraHandler, weaponSlotManager);
            GameObject instantiatedSpellFX = Instantiate(spellCastFX, weaponSlotManager.primarySlot.transform.position, cameraHandler.cameraPivotTransform.rotation);
            rigidBody = instantiatedSpellFX.GetComponent<Rigidbody>();
            // spell damage collider = InstantiatedSpellFX.GetComponent<SpellDamageCollider>();

            if (cameraHandler.currentLockOnTarget != null)
            {
                instantiatedSpellFX.transform.LookAt(cameraHandler.currentLockOnTarget.transform);
            }
            else
            {
                instantiatedSpellFX.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTransform.eulerAngles.x, playerStats.transform.eulerAngles.y, 0);
            }

            rigidBody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
            rigidBody.AddForce(instantiatedSpellFX.transform.up * projectileUpwardVelocity);
            rigidBody.useGravity = isEffectedByGravity;
            rigidBody.mass = projectileMass;
            instantiatedSpellFX.transform.parent = null;
        }
    }
}
