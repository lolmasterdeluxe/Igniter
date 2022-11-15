using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        public int attackCount;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHandler = GetComponent<InputHandler>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);
                animatorHandler.PlayTargetAnimation(weapon.lightAttack[attackCount + 1], true);
                attackCount += 1;
            }
        }
        public void HandleLightAttack(WeaponItem weapon)
        {
            animatorHandler.PlayTargetAnimation(weapon.lightAttack[0], true);
            attackCount = 0;
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            animatorHandler.PlayTargetAnimation(weapon.heavyAttack[0], true);
            attackCount = 0;
        }

        public void HandleSheath(WeaponItem weapon, bool isTrue)
        {
            if (isTrue)
                animatorHandler.PlayTargetAnimation(weapon.sheathAnimation, true);
            else
                animatorHandler.PlayTargetAnimation(weapon.unsheathAnimation, true);

            animatorHandler.SetSheath(isTrue);
        }

        public void HandleLocomotionType(WeaponItem weapon)
        {
            animatorHandler.SetLocomotionType(weapon.weaponType);
        }
    }
}
