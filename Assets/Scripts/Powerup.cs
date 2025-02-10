using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private const float lifeTimeDuration = 7f;
    private static readonly Color defaultColor = new Color(1f, 09f, 0.1f);

    protected PlayerController player;
    protected Rigidbody playerRb;
    private GameObject powerUpIndicator;

    protected bool initialized = false;
    protected float timeEllapsed = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerRb = player.GetComponent<Rigidbody>();
        powerUpIndicator = player.powerupIndicator;
    }

    //Initializes itself
    public void InitializePowerup(Color color)
    {
        powerUpIndicator.gameObject.SetActive(true);
        powerUpIndicator.GetComponent<MeshRenderer>().sharedMaterial.color = color;
        initialized = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!initialized)
        {
            return;
        }
        powerUpIndicator.transform.position = playerRb.transform.position + (Vector3.up * -0.38f);
        timeEllapsed += Time.deltaTime;
        if (timeEllapsed > lifeTimeDuration)
        {

            RemovePowerup();
        }
    }

    //Deinitializes itself from player
    public void RemovePowerup()
    {
        player.hasPowerUp = false;
        powerUpIndicator.GetComponent<MeshRenderer>().sharedMaterial.color = defaultColor;
        powerUpIndicator.gameObject.SetActive(false);
        Destroy(this);
        player.OnPowerupDisabled();
    }
}
