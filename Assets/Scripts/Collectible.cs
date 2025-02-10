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
    [field:SerializeField]
    public ParticleSystem particles { get; private set; }
    public PowerupType powerupType;
}
