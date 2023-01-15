using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class SpellItem : Item
    {
        public GameObject spellWarmUpFX;
        public GameObject spellCastFX;
        public string spellAnimation;

        [Header("Spell Cost")]
        public int focusPointCost;

        [Header("Spell type")]
        public SpellType spellType;

        [Header("Spell Description")]
        [TextArea]
        public string spellDescription;

        public virtual void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats, WeaponSlotManager weaponSlotManager)
        {
            Debug.Log("Attempting to cast a spell.");
        }

        public virtual void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats, CameraHandler cameraHandler, WeaponSlotManager weaponSlotManager)
        {
            Debug.Log("Successfully cast spell.");
            playerStats.DeductFocusPoints(focusPointCost);
        }
    }

    public enum SpellType
    {
        FaithSpell,
        MagicSpell,
        PyroSpell,
        TotalSpells
    }
}
