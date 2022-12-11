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
        public int weaponType;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;
    }
}
