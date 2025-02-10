using System;
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
    [SerializeField]
    private MainManager mainManager;
    private SpawnManager spawnManager;
    private Rigidbody playerRb;
    [field: SerializeField]
    public GameObject powerupIndicator { get; private set; }
    public GameObject bulletPrefab;

    public bool hasPowerUp = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(true);
        playerRb = GetComponent<Rigidbody>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
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

        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(transform.position.x, -10f, transform.position.z);
            gameObject.SetActive(false);
            mainManager.GameOver();
        }
    }

    //Checks if collided with collectible
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            Collectible collectible = other.GetComponent<Collectible>();
            TryAddingPowerup(collectible);
        }
    }

    //Attempts to assign powerup
    private void TryAddingPowerup(Collectible collectible)
    {
        if (collectible == null)
        {
            return;
        }
        if (hasPowerUp)
        {
            return;
        }
        PowerupType collectedType = collectible.powerupType;
        if (collectedType != PowerupType.None)
        {
            hasPowerUp = true;
            Color powerupColor = collectible.GetComponent<MeshRenderer>().sharedMaterial.color;
            Destroy(collectible.gameObject);
            Instantiate(collectible.particles, collectible.transform.position, collectible.transform.rotation);

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
            mainManager.SetPowerup(collectedType);
        }
    }

    //Broadcast necessary infos to managers when detaching powerup
    public void OnPowerupDisabled()
    {
        mainManager.SetPowerup(PowerupType.None);
         spawnManager.OnResetPowerup();
    }
}
