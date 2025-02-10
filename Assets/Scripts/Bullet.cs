using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    private Transform target;
    //ENCAPSULATION
    private float speed = 15f;
    private bool homing;
    private float rocketStrangth = 15f;
    private float aliveTimer = 3f;

    private Vector3 moveDir = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        if (homing && target != null)
        {
            moveDir = (target.transform.position - transform.position).normalized;
            transform.position += moveDir * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    /// <summary>
    /// Handles firing a rocket and destroying it after specific time..
    /// </summary>
    /// <param name="newTarget"></param>
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    /// <summary>
    /// Handles collision with target and destroys itself after hitting.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;
                targetRb.AddForce(away * rocketStrangth, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
