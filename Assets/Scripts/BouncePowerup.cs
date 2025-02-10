using UnityEngine;

//INHERITANCE
public class BouncePowerup : Powerup
{
    private float bounceMultiplier = 5f;

    //Attempts to bounce back the enemy with appropriate larger force
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyComponent = collision.gameObject.GetComponent<Enemy>();
            Vector3 awayFromPlayer = (enemyComponent.transform.position - player.transform.position);
            enemyComponent.rb.AddForce(awayFromPlayer * enemyComponent.speed * bounceMultiplier, ForceMode.Impulse);
        }
    }
}
