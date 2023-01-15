using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class PlayerEffectsManager : MonoBehaviour
    {
        PlayerStats playerStats;
        PlayerManager playerManager;
        WeaponSlotManager weaponSlotManager;
        public GameObject currentParticleFX; // The particles that will play of the current effect that is effecting the player (drinking estus, poison etc..)
        public GameObject instantiatedFXModel;
        public int amountToBeHealed;
        private void Awake()
        {
            playerStats = GetComponentInParent<PlayerStats>();
            playerManager = GetComponentInParent<PlayerManager>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }

        public void HealPlayerFromEffect()
        {
            playerStats.HealPlayer(amountToBeHealed);
            GameObject healParticles = Instantiate(currentParticleFX, playerStats.transform);
            Destroy(instantiatedFXModel.gameObject);
            Destroy(healParticles, 3);
            if (!playerManager.isSheathed)
                weaponSlotManager.LoadWeaponOnSlot();
        }
    }
}
