using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class EnemyStats : CharacterStats
    {
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyWeaponSlotManager enemyWeaponSlotManager;
        EnemyManager enemyManager;
        InputHandler inputHandler;

        public int soulsAwardedOnDeath = 50;

        private void Awake()
        {
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyWeaponSlotManager = GetComponentInChildren<EnemyWeaponSlotManager>();
            enemyManager = GetComponent<EnemyManager>();
            inputHandler = FindObjectOfType<InputHandler>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamageNoAnimation(int damage)
        {
            currentHealth = currentHealth - damage;
            enemyManager.currentRecoveryTime = 2;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                inputHandler.DisableLockOn();
                isDead = true;
            }
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;

            currentHealth = currentHealth - damage;

            enemyAnimatorManager.PlayTargetAnimation(enemyWeaponSlotManager.primaryWeapon.hitAnimation, true);

            enemyManager.currentRecoveryTime = 2;

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        public void HandleDeath()
        {
            currentHealth = 0;
            enemyAnimatorManager.PlayTargetAnimation("Death", true);

            inputHandler.DisableLockOn();

            isDead = true;
        }
    }
}
