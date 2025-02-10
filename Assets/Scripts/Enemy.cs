using UnityEngine;

public class Enemy : MonoBehaviour
{

    [field: SerializeField]
    public float speed { get; private set; } = 2f;
    [field: SerializeField]
    public Rigidbody rb { get; private set; }
    [SerializeField]
    protected GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            return;
        }
        player = GameObject.Find("Player");
    }

    protected virtual void Move()
    {
        if (transform.position.y < -5f)
        {
            player = null;
            Destroy(gameObject);
        }
    }

}
