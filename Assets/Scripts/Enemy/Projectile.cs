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
        //operacion para comparar layers de forma mas rapida y robusta 
        //transforma layer en layermask para comparar con bits de layermask groundlayer y si coincide algun bit el resultado es diferente de 0
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
