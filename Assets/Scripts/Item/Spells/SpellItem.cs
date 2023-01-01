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
        public bool isFaithSpell;
        public bool isMagicSpell;
        public bool isPyroSpell;

        [Header("Spell Description")]
        [TextArea]
        public string spellDescription;

        public virtual void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playertStats)
        {
            Debug.Log("Attempting to cast a spell.");
        }

        public virtual void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playertStats)
        {
            Debug.Log("Successfully cast spell.");
            playertStats.DeductFocusPoints(focusPointCost);
        }
    }
}
