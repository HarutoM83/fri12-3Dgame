using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;
    [SerializeField] int hp = 2;
    [SerializeField] float invincibleTimeMax = 0.5f;
    [SerializeField] float knockbackSpeed = 5;

    Rigidbody rb;
    Animator animator;
    Vector3 rotateTarget;
    private float invincibleTime;

    public Collider playerCollider { get; set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var subVec = playerCollider.bounds.center - rb.position;
        subVec.y = 0;
        rb.linearVelocity = subVec.normalized * moveSpeed;
        var direction = playerCollider.bounds.center - rb.position;

        bool isSeenPlayer = true;
        if (isSeenPlayer && invincibleTime <= 0)
        if (Physics.Raycast(rb.position, direction.normalized,
            out var hitInfo))
        {
            if (hitInfo.collider != playerCollider)
            {
                // プレイヤー以外の障害物に当たった場合は見えない1
                isSeenPlayer = false;
            }
        }

        if (subVec != Vector3.zero)
        {
            rotateTarget = subVec.normalized;
        }

        Vector3 forward = transform.forward;

        transform.forward =
            Vector3.Slerp(forward, rotateTarget, moveSpeed * Time.deltaTime);
        
        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        var attackObj = collision.gameObject.GetComponent<AttackObject>();
        if (attackObj != null)
        {
            hp -= attackObj.power;
            if (hp <= 0)
            {
                animator.SetTrigger("Die");
                Destroy(gameObject);
            }
        }
        
        if (attackObj != null && invincibleTime <= 0)
        {
            hp -= attackObj.power;

            invincibleTime = invincibleTimeMax;

            if (hp <= 0)
            {
                animator.SetTrigger("Die");
                Destroy(gameObject);
            }
        }
        
        // ノックバック
        var dir = transform.position - collision.transform.position;
        dir.y = 0;
        var knockbackVec = dir.normalized * knockbackSpeed;
        rb.linearVelocity = knockbackVec;
    }


}
