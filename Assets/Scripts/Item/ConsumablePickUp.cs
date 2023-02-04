using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class ConsumablePickUp : Interactable
    {
        public ConsumableItem consumable;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            PlayerAnimatorManager animatorHandler;
            UIManager uiManager;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManager.GetComponentInChildren<PlayerAnimatorManager>();
            uiManager = FindObjectOfType<UIManager>();

            playerLocomotion.rigidbody.velocity = Vector3.zero; // Stops the player from moving whilst picking up the item
            animatorHandler.PlayTargetAnimation("Relax-Pickup", true); // Plays the animation of looting the item
            playerInventory.currentConsumable.currentItemAmount++;
            uiManager.interactableUI.itemText.text = consumable.itemName;
            uiManager.interactableUI.itemImage.texture = consumable.itemIcon.texture;
            uiManager.itemPopUp.SetActive(true);
            Destroy(gameObject);
        }
    }
}
