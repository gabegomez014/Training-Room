using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
