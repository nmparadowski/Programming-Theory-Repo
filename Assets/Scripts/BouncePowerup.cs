using UnityEngine;

public class BouncePowerup : Powerup
{
    //private float powerUpStrength = 15f;
    private float bounceMultiplier = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyComponent = collision.gameObject.GetComponent<Enemy>();
            //Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (enemyComponent.transform.position - player.transform.position);
            enemyComponent.rb.AddForce(awayFromPlayer * enemyComponent.speed * bounceMultiplier, ForceMode.Impulse);
        }
    }
}
