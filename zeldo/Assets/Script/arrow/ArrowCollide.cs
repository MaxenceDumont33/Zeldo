using UnityEngine;

public class ArrowCollide : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<IDamageable>(out IDamageable objectTouch))
        {
            collision.gameObject.GetComponent<IDamageable>();
            objectTouch.Damage(damage);
            Destroy(this.gameObject);
        }
    }
}
