using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PowerUpItem
{
    public Powerup powerup;
    public PowerupType type;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    private Rigidbody playerRb;
    [field: SerializeField]
    public GameObject powerupIndicator { get; private set; }
    public GameObject bulletPrefab;

    public bool hasPowerUp = false;

    // Start is called before the first frame update
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        powerupIndicator.gameObject.SetActive(false);
        hasPowerUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float sideInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(Vector3.forward * speed * forwardInput);
        playerRb.AddForce(Vector3.right * speed * sideInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            Collectible collectible = other.GetComponent<Collectible>();
            PowerupType collectedType = collectible.powerupType;
            if (collectedType != PowerupType.None)
            {
                hasPowerUp = true;
                Color powerupColor = other.GetComponent<MeshRenderer>().sharedMaterial.color;
                Destroy(other.gameObject);
                Instantiate(collectible.particles, other.transform.position, other.transform.rotation);

                Powerup newPowerup = null;
                switch (collectedType)
                {
                    case PowerupType.Bounce:
                        newPowerup = gameObject.AddComponent<BouncePowerup>();
                        break;
                    case PowerupType.Shoot:
                        newPowerup = gameObject.AddComponent<FirePowerup>();
                        break;
                    case PowerupType.Smash:
                        newPowerup = gameObject.AddComponent<SmashPowerup>();
                        break;
                    default:
                        break;
                }
                newPowerup.InitializePowerup(powerupColor);
            }
        }
    }
}
