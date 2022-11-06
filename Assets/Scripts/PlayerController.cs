using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform model;
    [SerializeField]
    private float speed = 5, turnSpeed = 360, dashSpeed = 10, dashPeriod = 1, dashTime = 0;
    private Vector3 input;
    [SerializeField]
    private Animator playerController;

    private void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
        if (playerController.GetBool("IsDashing"))
            Dash();
        else
            dashTime = 0;
    }

    private void GatherInput()
    {
        dashTime += Time.deltaTime;

        if (playerController.GetInteger("Attack") == 0)
            input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        else
            input = Vector3.zero;

        if (input == Vector3.zero)
            playerController.SetBool("IsRunning", false);
        else
            playerController.SetBool("IsRunning", true);

        if (Input.GetKeyDown(KeyCode.Space) && !playerController.GetBool("IsDashing") && playerController.GetBool("IsRunning"))
            playerController.SetBool("IsDashing", true);
    }

    private void Look()
    {
        if (input == Vector3.zero)
            return;

        Quaternion rot = Quaternion.LookRotation(input.ToIso(), Vector3.up);

        model.rotation = Quaternion.RotateTowards(model.rotation, rot, turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        if (!playerController.GetBool("IsDashing"))
            rb.MovePosition(transform.position + input.ToIso().normalized * speed * Time.deltaTime);
    }

    private void Dash()
    {
        if (dashTime < dashPeriod)
            rb.MovePosition(transform.position + input.ToIso().normalized * dashSpeed * Time.deltaTime);
        else
            playerController.SetBool("IsDashing", false);
    }
}

public static class Helpers
{
    private static Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => isoMatrix.MultiplyPoint3x4(input);
}