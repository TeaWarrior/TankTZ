using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform aimPosition;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, aimPosition.position, Quaternion.identity);
        bulletScript script = bullet.GetComponent<bulletScript>();
        var dir =  aimPosition.position - transform.position ;
        script.MoveDirection(dir);
    }
}
