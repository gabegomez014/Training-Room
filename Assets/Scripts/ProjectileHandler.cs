using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public GameObject _afterFX;
    public float _afterFXLifetime;

    private GameObject _projectileFX;
    private GameObject _spawnedAfterFX;
    private Rigidbody _rb;

    private void Start()
    {
        _projectileFX = transform.GetChild(0).gameObject;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            Vector3 collisionPoint = collision.contacts[0].point;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, collision.contacts[0].normal);

            _rb.velocity = new Vector3(0, 0, 0);

            _spawnedAfterFX = Instantiate(_afterFX, collisionPoint, rotation);
            
            Destroy(_spawnedAfterFX, _afterFXLifetime);
            Destroy(this.gameObject);
        }
    }
}
