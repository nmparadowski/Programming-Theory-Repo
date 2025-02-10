using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashPowerup : Powerup
{
    private bool smashing = false;
    private float floorY;
    [SerializeField]
    private float hangTime;
    [SerializeField]
    private float smashSpeed;
    [SerializeField]
    private float explosionForce;
    [SerializeField]
    private float explosionRadius;

    private void Start()
    {
        hangTime = 0.25f;
        smashSpeed = 20f;
        explosionForce = 30f;
        explosionRadius = 10f;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(SmashRoutine());
        }
    }

    /// <summary>
    /// Performs smash effect that affect enemies to move back.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SmashRoutine()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }

        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2f);
            yield return null;
        }

        //cycle through enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                continue;
            }
            enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,
                transform.position,
                explosionRadius,
                0.0f,
                ForceMode.Impulse);
        }
        smashing = false;
    }
}
