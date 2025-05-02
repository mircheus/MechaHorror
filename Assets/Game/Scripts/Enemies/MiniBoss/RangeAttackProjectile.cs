using TMPro;
using UnityEngine;

public class RangeAttackProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 23f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float yOffset = 0f;

    private Transform _target;
    private Vector3 _direction;

    public void ShootTo(Transform target)
    {
        _target = target;
        Transform transform1 = transform;
        transform1.LookAt(target);
        _direction = (_target.position - transform1.position).normalized;
        _direction = new Vector3(_direction.x, 0, _direction.z);
        Destroy(gameObject, 10f);
    }

    public void ShootTo(Vector3 direction)
    {
        Destroy(gameObject, 10f);
        _direction = new Vector3(direction.x, 0, direction.z);
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
            // Destroy(gameObject);
        }
    }

    private void HitTarget()
    {
        // Implement damage logic here
        Destroy(gameObject);
    }
    
    
}