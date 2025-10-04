using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Chest : MonoBehaviour, IDamable
{
    private Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private float jumpStrength;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(DameInstance info)
    {
        animator.SetBool("chestOpen", true);

        rb.linearVelocity = new(rb.linearVelocity.x, jumpStrength);
        rb.angularVelocity = Random.Range(-200f, 200f);
    }
}
