using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class EnemyStats : CharacterStats
    {
        Animator animator;
        EnemyWeaponSlotManager enemyWeaponSlotManager;
        EnemyManager enemyManager;
        InputHandler inputHandler;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
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

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;

            currentHealth = currentHealth - damage;

            animator.Play(enemyWeaponSlotManager.primaryWeapon.hitAnimation);

            enemyManager.currentRecoveryTime = 1;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Death");

                inputHandler.DisableLockOn();

                isDead = true;
            }
        }
    }
}
