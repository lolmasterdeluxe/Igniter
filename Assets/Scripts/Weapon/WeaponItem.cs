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

        public string dodgeAnimation;
        public string rollAnimation;
        public string unsheathAnimation, sheathAnimation;
        public int weaponType;
    }
}
