using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask targetLayer;

    public float LifeTime { get; set; } = 5f;
    public int Damage { get; set; } = 1;

    private void Start()
    {
        Destroy(gameObject, LifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        //operation to efficiently compare layers 
        //transforms layer in layermask and compares for coincidences in layermask
        if(((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            Destroy(gameObject);
            return;
        }
        if (((1 << collision.gameObject.layer) & targetLayer) == 0)
            return;
        Health health = collision.GetComponent<Health>();
        if (health)
        {
            health.ChangeHeatlh(-Damage, transform.position);
            Destroy(gameObject);
        }
    }
}
