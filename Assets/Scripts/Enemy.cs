using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float speed = 2f;
    protected Rigidbody rb;
    [SerializeField]//jest for inspection if assignment works.
    protected GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    protected virtual void Move()
    {
        Debug.Log($"Enemy ({gameObject.name}) is moving - base class");
        if (transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }

}
