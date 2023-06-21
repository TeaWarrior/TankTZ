using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLogic : MonoBehaviour,IDamagable
{

    bool isDamagable;
    [SerializeField] SpriteRenderer sprite;
    public void TakeDamage()
    {
        if (isDamagable)
        {
            WallDamage();
        }
    }


    private void Start()
    {
        RandomizeDistractableWall();
    }
    void WallDamage()
    {

        Destroy(gameObject);
        AstarPath.active.Scan();
        Debug.Log("TRUE");
    }

   void RandomizeDistractableWall()
    {
        int randomNumber = Random.Range(0, 10);
        if (randomNumber > 2)
        {
            isDamagable = true;
            sprite.color = Color.green;
        }
    }
}
