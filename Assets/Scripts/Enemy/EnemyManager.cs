using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IG
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyStats enemyStats;

        public State currentState;
        public CharacterStats currentTarget;
        public NavMeshAgent navmeshAgent;
        public Rigidbody enemyRigidbody;

        public bool isPerformingAction;
        public bool isInteracting;
        public bool despawnAfterDeath;
        public float rotationSpeed = 15;
        public float maximumAttackRange = 1.5f;
        public int weaponType = 0;

        [Header("Combat Timers")]
        public float despawnTimer = 3;
        public float stunDuration = 3;
        public float stunTimer = 3;
        public float currentRecoveryTime = 0;

        [Header("Combat Flags")]
        public bool canDoCombo;

        [Header("A.I Settings")]
        public float detectionRadius = 20;
        // The higher, and lower, respectively these angles are, the greater detection FIELDS OF VIEW (like eye sight)
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;

        [Header("A.I. Combat Settings")]
        public bool allowAIToPerformCombos;
        public float comboLikelyHood;

        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyStats = GetComponent<EnemyStats>();
            navmeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            navmeshAgent.enabled = false;
            enemyRigidbody.isKinematic = false;
            enemyAnimatorManager.anim.SetBool("isSheathed", false);
            enemyAnimatorManager.anim.SetInteger("weaponType", weaponType);
            stunTimer = stunDuration;
        }

        private void Update()
        {
            HandleRecoveryTimer();
            HandleDespawnAfterDeath();
            HandleStateMachine();

            isInteracting = enemyAnimatorManager.anim.GetBool("isInteracting");
            canDoCombo = enemyAnimatorManager.anim.GetBool("canDoCombo");
            enemyAnimatorManager.anim.SetBool("isDead", enemyStats.isDead);
            enemyAnimatorManager.anim.SetBool("isStunned", isStunned);
        }

        private void LateUpdate()
        {
            navmeshAgent.transform.localPosition = Vector3.zero;
            navmeshAgent.transform.localRotation = Quaternion.identity;
        }

        private void HandleStateMachine()
        {
            if (currentState != null && !enemyStats.isDead && !isStunned)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);

                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPerformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPerformingAction = false;
                }
            }

            if (isStunned)
            {
                stunTimer -= Time.deltaTime;

                if (stunTimer <= 0)
                {
                    stunTimer = stunDuration;
                    isStunned = false;
                }    
            }
        }

        private void HandleDespawnAfterDeath()
        {
            if (despawnAfterDeath && enemyStats.isDead)
            {
                despawnTimer -= Time.deltaTime;
                if (despawnTimer <= 0)
                    Destroy(gameObject);
            }
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red; //replace red with whatever color you prefer
            Gizmos.DrawWireSphere(transform.position, detectionRadius);

            Vector3 fovLine1 = Quaternion.AngleAxis(maximumDetectionAngle, transform.up) * transform.forward * detectionRadius;
            Vector3 fovLine2 = Quaternion.AngleAxis(minimumDetectionAngle, transform.up) * transform.forward * detectionRadius;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);
        }
    }
}
