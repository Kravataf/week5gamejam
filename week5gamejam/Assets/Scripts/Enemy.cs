using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;

    private Rigidbody rb;
    private Vector3 origin, fwd;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        origin = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        float dist = Vector3.Distance(origin, player.position);

        if (dist <= 8f)
        {
            fwd = player.position - transform.position;
            fwd.y = 0f;
            fwd.Normalize();

            rb.MoveRotation(Quaternion.LookRotation(fwd));
            rb.linearVelocity = fwd * moveSpeed + new Vector3(0f, rb.linearVelocity.y, 0f);
        }
        else if (dist <= 10f)
        {
            // idle
        }
        else
        {
            fwd = origin - transform.position;
            fwd.y = 0f;
            fwd.Normalize();

            if (Vector3.Distance(origin, transform.position) > 1f)
            {
                rb.MoveRotation(Quaternion.LookRotation(fwd));
                rb.linearVelocity = fwd * moveSpeed + new Vector3(0f, rb.linearVelocity.y, 0f);
            }
        }
    }
}
