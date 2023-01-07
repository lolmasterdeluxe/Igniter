using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class PlayerStats : CharacterStats
    {
        PlayerManager playerManager;

        [SerializeField]
        private HealthBar healthBar;
        [SerializeField]
        private StaminaBar staminaBar;
        [SerializeField]
        private FocusPointBar focusPointsBar;

        PlayerAnimatorManager animatorHandler;
        PlayerInventory playerInventory;

        public float staminaRegenerationAmount = 1;
        public float staminaRegenTimer = 0;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<PlayerAnimatorManager>();
            playerManager = GetComponent<PlayerManager>();
            playerInventory = GetComponent<PlayerInventory>();
            focusPointsBar = FindObjectOfType<FocusPointBar>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentHealth = maxHealth;
            currentStamina = maxStamina;
            healthBar.SetMaxHealth(maxHealth);
            staminaBar.SetMaxStamina(maxStamina);


            maxFocusPoints = SetMaxFocusPointsFromFocusLevel();
            currentFocusPoints = maxFocusPoints;
            focusPointsBar.SetMaxFocusPoints(maxFocusPoints);
            focusPointsBar.SetCurrentFocusPoints(currentFocusPoints);
        }                                                                                           

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        private float SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        private float SetMaxFocusPointsFromFocusLevel()
        {
            maxFocusPoints = focusLevel * 10;
            return maxFocusPoints;
        }

        public void TakeDamageNoAnimation(int damage)
        {
            currentHealth -= damage;
            healthBar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public void TakeDamage(int damage)
        {
            if (isDead || playerManager.isInvulnerable)
                return;

            currentHealth -= damage;

            healthBar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation(playerInventory.primaryWeapon.hitAnimation, true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Death", true);
                isDead = true;
            }
        }

        public void TakeStaminaDamage(int damage)
        {
            currentStamina -= damage;
            // Set Bar
            staminaBar.SetCurrentStamina(currentStamina);
        }

        public void RegenerateStamina()
        {
            if (playerManager.isInteracting)
            {
                staminaRegenTimer = 0;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;
                if (currentStamina < maxStamina && staminaRegenTimer > 1f)
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }

        public void HealPlayer(int healAmount)
        {
            currentHealth = currentHealth + healAmount;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            healthBar.SetCurrentHealth(currentHealth);
        }

        public void DeductFocusPoints(int focusPoints)
        {
            currentFocusPoints = currentFocusPoints - focusPoints;

            if (currentFocusPoints < 0)
                currentFocusPoints = 0;

            focusPointsBar.SetCurrentFocusPoints(currentFocusPoints);
        }

        public void AddSouls(int souls)
        {
            soulCount = soulCount + souls;
        }
    }
}
