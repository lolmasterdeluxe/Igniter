using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Damage")]
        public int baseDamage = 25;
        public int criticalDamageMultiplier = 4;

        [Header("Absorption")]
        public float physicalDamageAbsorption;

        [Header("One Handed Attack Animations")]
        public List<string> lightAttack;
        public List<string> heavyAttack;

        [Header("Movement Animations")]
        public string dodgeAnimation;
        public string rollAnimation;

        [Header("Hit Animations")]
        public string hitAnimation;
        public string blockGuardAnimation;

        [Header("Weapon Animations")]
        public string unsheathAnimation;
        public string sheathAnimation;
        public int weaponAnimationType;

        [Header("Animation Offsets")]
        public GameObject defaultOffset;
        public GameObject blockingOffset;

        [Header("Weapon Art")]
        public string weaponArt;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;

        [Header("Weapon Type")]
        public WeaponType weaponType;
        public SheathType sheathType;
    }

    public enum WeaponType
    {
        MeleeWeapon,
        ShieldWeapon,
        SpellCaster,
        FaithCaster,
        PyroCaster,
        TotalWeaponType
    }

    public enum SheathType
    {
        Back,
        Hip,
        TotalSheathType
    }

}
