using System.Collections;
using UnityEngine;

public class ImpulseRollingEnemy : Enemy
{
    private const float MinimalMagnitude = 0.001f;
    private Vector3 rollToDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        Move();
    }

    //Apply impulse force when not moving
    protected override void Move()
    {
        if (rb.velocity.magnitude > MinimalMagnitude)
        {
            return;
        }
        rollToDirection = Vector3.ProjectOnPlane((player.transform.position - transform.position).normalized, Vector3.up).normalized;
        rb.AddForce(rollToDirection * speed, ForceMode.Impulse);
    }


}
