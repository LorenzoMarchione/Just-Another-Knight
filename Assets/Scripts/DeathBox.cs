using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        health.ChangeHeatlh(-1000, transform.position);
    }
}
