using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public enum EnemyType
{
    Regular,
    Fast,
    Healthy,
    HaveLoot
}
public class EnemyAi : MonoBehaviour,IDamagable
{
    [SerializeField] GameObject lootPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform aimPosition;
    [SerializeField] float attackSpeed=1f;

    [SerializeField] AIDestinationSetter destinationSetter;

    [SerializeField] SpriteRenderer enemySkin;
     int enemyHealth = 1;
    EnemyType enemyType;
    bool isAlreadyShooted;

    bool isHaveLoot;
    // Start is called before the first frame update
    void Start()
    {
        SetTarget();
    }

    public void SetEnemyType(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Regular:
                enemySkin.color = Color.white;
                break;

            case EnemyType.Fast:
                enemySkin.color = Color.yellow;
                AIPath aI = GetComponent<AIPath>();
                aI.maxSpeed = 5;
                break;
            case EnemyType.Healthy:
                enemySkin.color = Color.blue;
                enemyHealth = 2;
                break;

            case EnemyType.HaveLoot:
                enemySkin.color = Color.magenta;
                isHaveLoot = true;
                break;


        }
    }
    public void SetTarget()
    {
        destinationSetter.target = MazeGenerator.instance.playerBase.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isAlreadyShooted)
        {
            StartCoroutine(ShootingSpeed());
        }
    }
    IEnumerator ShootingSpeed()
    {
        isAlreadyShooted = true;
        yield return new WaitForSeconds(attackSpeed);
        Shoot();
        isAlreadyShooted = false;
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, aimPosition.position, Quaternion.identity);
        bulletScript script = bullet.GetComponent<bulletScript>();
        script.isEnemyBullet = true;
        var dir = aimPosition.position - transform.position;
        script.MoveDirection(dir);
    }

    public void  TakeDamage()
    {
        TookDamage();
    }

   public virtual void TookDamage()
    {
        enemyHealth--;
        if (isHaveLoot)
        {
            DropLoot();
        }
        if (enemyHealth <= 0)
        {
            EnemySpawner.instance.EnemyKilled();
            Destroy(gameObject);
        }
       
    }

    public void DropLoot()
    {
        GameObject loot = Instantiate(lootPrefab, transform.position, Quaternion.identity);
    }
}
