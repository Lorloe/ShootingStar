using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private bool destroyOnHit = true;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (destroyOnHit)
        {
            Destroy(gameObject);
        }
    }
}
