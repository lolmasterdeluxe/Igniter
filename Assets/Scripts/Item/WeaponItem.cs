using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab, sheathModelPrefab;
        public bool isUnarmed;

        [Header("Damage")]
        public int baseDamage = 25;
        public int criticalDamageMultiplier = 4;

        [Header("One Handed Attack Animations")]
        public List<string> lightAttack;
        public List<string> heavyAttack;

        [Header("Movement Animations")]
        public string dodgeAnimation;
        public string rollAnimation;

        [Header("Hit Animations")]
        public string hitAnimation;

        [Header("Weapon Animations")]
        public string unsheathAnimation;
        public string sheathAnimation;
        public int weaponAnimationType;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;

        [Header("Weapon Type")]
        public WeaponType weaponType;
    }

    public enum WeaponType
    {
        MeleeWeapon,
        SpellCaster,
        FaithCaster,
        PyroCaster,
        TotalWeaponType
    }

}
