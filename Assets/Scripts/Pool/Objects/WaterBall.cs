using UnityEngine;

public class WaterBall : MonoBehaviour, IPoolObject
{
    private Rigidbody _rigidbody;
    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void GetFromPool()
    {
        gameObject.SetActive(true);
        _rigidbody.isKinematic = false;

    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        _rigidbody.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ReturnToPool();
        var scarecrow = collision.gameObject.GetComponent<Scarecrow>();
        if(scarecrow != null) 
        {
            scarecrow.TakeDamage(type: DamageType.Water);
        }
    }
}
