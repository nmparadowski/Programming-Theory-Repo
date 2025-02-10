using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private GameObject powerupIndicator;
    private Rigidbody playerRb;

    public bool hasPowerUp = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float sideInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(Vector3.forward * speed * forwardInput);
        playerRb.AddForce(Vector3.right * speed * sideInput);
    }
}
