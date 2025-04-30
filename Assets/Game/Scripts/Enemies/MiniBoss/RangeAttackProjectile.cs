using UnityEngine;

public class RangeAttackProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    // [SerializeField] private float yOffset = 2f;

    private Transform _target;
    private Vector3 _direction;

    public void Initialize(Transform target)
    {
        _target = target;
        Transform transform1 = transform;
        transform1.LookAt(target);
        _direction = (_target.position - transform1.position).normalized;
        // _direction.y = yOffset; // Keep the projectile level
    }

    private void Update()
    {
        if (_target != null)
        {
            transform.position += _direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, _target.position) < 0.1f)
            {
                HitTarget();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void HitTarget()
    {
        // Implement damage logic here
        Destroy(gameObject);
    }
}