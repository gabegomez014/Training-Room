using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectile;
    public float projectileForce = 5;

    private Transform _shootingPosition;
    private Vector3 _shootingDirection;

    private void Start()
    {
        _shootingPosition = transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green, 0.1f, false);
            Quaternion rotation = Quaternion.LookRotation(hit.point - transform.position);
            transform.rotation = rotation;
            _shootingDirection = (hit.point - transform.position).normalized;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject spawnedProjectile = Instantiate(projectile, _shootingPosition.position, Quaternion.identity);
            spawnedProjectile.transform.position = _shootingPosition.position;
            Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();

            rb.AddForce(_shootingDirection * projectileForce);
        }

    }
}
