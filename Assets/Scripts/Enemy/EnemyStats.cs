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

        public IdleState idleState;
        public UIEnemyHealthBar enemyHealthBar;

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
            enemyHealthBar.SetMaxHealth(maxHealth);
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

            enemyHealthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                inputHandler.DisableLockOn();
                isDead = true;
            }
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            enemyHealthBar.SetHealth(currentHealth);

            if (enemyManager.isBlocking)
                enemyAnimatorManager.PlayTargetAnimation(enemyWeaponSlotManager.primaryWeapon.blockGuardAnimation, true);
            else
                enemyAnimatorManager.PlayTargetAnimation(enemyWeaponSlotManager.primaryWeapon.hitAnimation, true);

            enemyManager.currentRecoveryTime = 2;

            // Enemy to engage player upon getting hit
            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            if (enemyManager.currentState == idleState)
                idleState.SetEnemyTarget(enemyManager, playerStats);

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
