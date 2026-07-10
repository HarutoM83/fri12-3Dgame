using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;

    Rigidbody rb;
    Vector3 rotateTarget;

    public Collider playerCollider { get; set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var subVec = playerCollider.bounds.center - rb.position;
        subVec.y = 0;
        rb.linearVelocity = subVec.normalized * moveSpeed;
        var direction = playerCollider.bounds.center - rb.position;

        bool isSeenPlayer = true;
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
    }
}
