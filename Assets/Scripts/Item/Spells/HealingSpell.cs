using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IG
{
    [CreateAssetMenu(menuName = "Spells/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(AnimatorHandler animatorHandler, PlayerStats playertStats)
        {
            GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
            Debug.Log("Attempting to cast a spell.");
        }

        public override void SuccessfullyCastSpell(AnimatorHandler animatorHandler, PlayerStats playertStats)
        {
            GameObject instantiatedSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
            playertStats.HealPlayer(healAmount);
            Debug.Log("Successfully cast spell.");
        }
    }
}
