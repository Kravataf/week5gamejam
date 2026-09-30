using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Animator animator;

    private Transform player;
    private Rigidbody rb;
    private Vector3 origin, fwd;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        origin = gameObject.transform.position;

        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        float dist = Vector3.Distance(origin, player.position);
        float range = Vector3.Distance(transform.position, player.position);

        if (dist <= 18f)
        {
            fwd = player.position - transform.position;
            fwd.y = 0f;
            fwd.Normalize();

            rb.MoveRotation(Quaternion.LookRotation(fwd));
            rb.linearVelocity = fwd * moveSpeed + new Vector3(0f, rb.linearVelocity.y, 0f);
        }
        else if (dist <= 20f)
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

    // toto "attackovanie" je ass mohol by som ho prerobit
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerMovement>().state == PlayerMovement.states.Roll) return;
            SceneManager.LoadScene(0);
        }
    }
}
