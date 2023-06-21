using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class bulletScript : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {

            if (isEnemyBullet)
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    Destroy(gameObject);
                    return;
                }
            }
            damagable.TakeDamage();

        }

        Destroy(gameObject);

    }
    public float speed = 10f; // Скорость движения пули

    [SerializeField] Rigidbody2D rb;

    private Vector3 direction;

  public  bool isEnemyBullet;
  

    public void MoveDirection(Vector3 dir)
    {
        direction = dir;
        rb.AddForce(direction * speed);
    }
   
}
