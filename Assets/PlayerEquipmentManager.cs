using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        [SerializeField]
        BlockingCollider blockingCollider;

        private void Awake()
        {
            inputHandler = GetComponentInParent<InputHandler>();
            playerInventory = GetComponentInParent<PlayerInventory>();
        }

        public void OpenBlockingCollider()
        {
            blockingCollider.SetColliderDamageAbsorption(playerInventory.primaryWeapon);
            blockingCollider.EnableBlockingCollider();
        }

        public void CloseBlockingCollider()
        {
            blockingCollider.DisableBlockingCollider();
        }
    }
}
