using UnityEngine;

//INHERITANCE
public class ForwardRollingEnemy : Enemy
{
    private Vector3 initialDirection;

    private void Start()
    {
        initialDirection = Vector3.ProjectOnPlane((player.transform.position - transform.position).normalized, Vector3.up).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //Moves in one direction only
    //POLYMORPHISM
    protected override void Move()
    {    
        base.Move();
        if (player == null)
        {
            return;
        }
        rb.AddForce(initialDirection * speed);
    }
}
