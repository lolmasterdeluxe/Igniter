using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IG
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        }
    }
}
