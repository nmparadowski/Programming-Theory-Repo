using UnityEngine;

public enum PowerupType
{
    None,
    Bounce,
    Shoot,
    Smash
}

public class Collectible : MonoBehaviour
{
    private float rotationSpeed = 50f;
    [field: SerializeField]
    public ParticleSystem particles { get; private set; }
    public PowerupType powerupType;

    //Updates collectible's rotation
    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
