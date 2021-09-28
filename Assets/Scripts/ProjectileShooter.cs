using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public Ability projectileAbility;
    public float maxLength = 40;

    [SerializeField]
    private Transform _shootingPosition;
    private Vector3 _shootingDirection;
    private float _cooldown = 0;

    private void Start()
    {
        projectileAbility.Initialize(this.gameObject);
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

        else
        {
            var pos = ray.GetPoint(maxLength);
            Quaternion rotation = Quaternion.LookRotation(pos - transform.position);
            transform.rotation = rotation;
            _shootingDirection = (pos - transform.position).normalized;

        }

        if (Input.GetMouseButtonDown(0) && _cooldown <= 0)
        {
            projectileAbility.TriggerAbility();
        }

        else if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
        }

    }

    public void Launch(GameObject emissionVFX, GameObject projectile, float projectileForce)
    {
        _cooldown += projectileAbility.aBaseCoolDown;

        GameObject clone = Instantiate(projectile);
        clone.transform.position = _shootingPosition.position;
        clone.transform.rotation = transform.rotation;

        GameObject cloneEmission = Instantiate(emissionVFX);
        cloneEmission.transform.position = _shootingPosition.position;
        cloneEmission.transform.rotation = transform.rotation;
        Destroy(cloneEmission, 1);

        Rigidbody rb = clone.GetComponent<Rigidbody>();
        rb.AddForce(_shootingDirection * projectileForce);
    }
}
