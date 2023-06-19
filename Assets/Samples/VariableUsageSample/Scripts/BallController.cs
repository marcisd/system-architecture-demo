using MSD;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    [SerializeField] private FloatReference _speed = new FloatReference(1); 
    
    private Vector2 _direction;
    
    private void Start()
    {
        _direction = Random.insideUnitCircle;
    }

    private void FixedUpdate()
    {
        var offset = _direction * _speed * Time.deltaTime;
        transform.position = transform.position + (Vector3)offset;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var contact = col.contacts[0];
        _direction = Vector2.Reflect(_direction, contact.normal);
    }
}
