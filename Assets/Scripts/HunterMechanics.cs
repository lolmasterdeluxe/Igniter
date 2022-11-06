using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterMechanics : MonoBehaviour
{
    private int attack_increment = 0;
    [SerializeField]
    private Animator playerController;
    [SerializeField]
    private float attackDelay = 1f, lungePeriod = 0.5f, lungeSpeed = 1f;
    [SerializeField]
    private Rigidbody rb;
    private float attackTime = 0, lungeTime;
    private Vector3 input;

    private void Update()
    {
        AttackInput();
        Lunge();
        attackTime += Time.deltaTime;
        lungeTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {

    }

    private void AttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            lungeTime = 0;
            attackTime = 0;
            attack_increment++;
            
            if (playerController.GetNextAnimatorStateInfo(0).IsName("Standing Melee Combo Attack 3"))
                attack_increment = 0;
        }

        if (attackTime > attackDelay)
            attack_increment = 0;

        playerController.SetInteger("Attack", attack_increment);
    }

    private void Lunge()
    {
        if (lungeTime < lungePeriod)
            rb.MovePosition(transform.position + input.ToIso().normalized * lungeSpeed * Time.deltaTime);
    }
}
