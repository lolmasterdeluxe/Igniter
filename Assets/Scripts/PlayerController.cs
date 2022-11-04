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
    private float speed = 5, turnSpeed = 360, dashSpeed = 10, dashDistance = 1;
    private Vector3 input, newPosition;
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
            Dash(newPosition);
    }

    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space) && !playerController.GetBool("IsDashing"))
        {
            newPosition = transform.position + (input.ToIso() * dashDistance);
            playerController.SetBool("IsDashing", true);
        }
    }

    private void Look()
    {
        if (input == Vector3.zero)
        {
            playerController.SetBool("IsRunning", false);
            return;
        }

        playerController.SetBool("IsRunning", true);

        Quaternion rot = Quaternion.LookRotation(input.ToIso(), Vector3.up);

        model.rotation = Quaternion.RotateTowards(model.rotation, rot, turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        if (!playerController.GetBool("IsDashing"))
            rb.MovePosition(transform.position + input.ToIso().normalized * speed * Time.deltaTime);
    }

    private void Dash(Vector3 newPosition)
    {
        if (Vector3.Distance(transform.position, newPosition) > 0.1f)
            rb.MovePosition(Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * dashSpeed));
        else
            playerController.SetBool("IsDashing", false);
    }
}

public static class Helpers
{
    private static Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => isoMatrix.MultiplyPoint3x4(input);
}