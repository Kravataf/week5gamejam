using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public enum states {Idle, Move, Roll};
    [HideInInspector] public states state, prev;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    private GameObject cam;
    private Vector2 wishDirection;
    private Vector3 dir, move;

    private void Awake()
    {
        cam = GameObject.Find("Cam");
    }

    private void Update()
    {
        switch (state)
        {
            case states.Idle:
            case states.Move:
                wishDirection = InputSystem.actions["Move"].ReadValue<Vector2>();
                dir = new Vector3(0f, cam.transform.eulerAngles.y, 0f);

                if (InputSystem.actions["Roll"].WasPressedThisFrame())
                {
                    state = states.Roll;
                    StartCoroutine(PlayerRoll());
                }
            break;

            case states.Roll:
            break;
        }
        if (prev != state)
        {
            animator.SetInteger("State", (int)state);
            prev = state;
        }
    }

    private void FixedUpdate()
    {
        // fix move y by malo byt linear velocity y

        if (state != states.Roll)
        {
            move = (Quaternion.Euler(dir) * new Vector3(wishDirection.x, 0f, wishDirection.y)).normalized;
            if (move != Vector3.zero)
            {
                rb.MoveRotation(Quaternion.LookRotation(move));
                rb.linearVelocity = move * moveSpeed;
                state = states.Move;
            }
            else
            {
                state = states.Idle;
            }
        }
        else
        {
            rb.MoveRotation(Quaternion.LookRotation(move));
            rb.linearVelocity = move * moveSpeed * 1.5f;
        }
    }

    IEnumerator PlayerRoll()
    {
        yield return new WaitForSeconds(1f);
        state = states.Idle;
    }
}
