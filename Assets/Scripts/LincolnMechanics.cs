using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LincolnMechanics : MonoBehaviour
{
    private int attack_increment = 0;
    [SerializeField]
    private Animator playerController;
    [SerializeField]
    private float delay = 0.5f, delay_time = 0;

    private void Update()
    {
        AttackInput();
        delay_time += Time.deltaTime;
    }

    private void FixedUpdate()
    {

    }

    private void AttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            delay_time = 0;
            attack_increment++;
        }

        if (attack_increment > 3 || delay_time > delay)
            attack_increment = 0;

        playerController.SetInteger("Attack", attack_increment);
    }
}
