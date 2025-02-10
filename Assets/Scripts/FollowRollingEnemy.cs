using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class FollowRollingEnemy : Enemy
{

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //POLYMORPHISM
    //Follows the player
    protected override void Move()
    {
        base.Move();
        if (player == null)
        {
            return;
        }
        Vector3 followDirection = Vector3.ProjectOnPlane((player.transform.position - transform.position).normalized, Vector3.up).normalized;
        rb.AddForce(followDirection * speed);
    }
}
