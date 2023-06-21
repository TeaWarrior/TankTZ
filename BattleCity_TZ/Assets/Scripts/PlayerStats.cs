using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour, IDamagable
{

    public event EventHandler OnPlayerDie;
    public event EventHandler OnPlayerLoseLive;
    public int liveAmount=3;
    private MazeGenerator mazeGenerator;
    private void Start()
    {
        mazeGenerator = MazeGenerator.instance;
        OnPlayerLoseLive?.Invoke(this, EventArgs.Empty);
    }
    public void TakeDamage()
    {
        liveAmount--;
        OnPlayerLoseLive?.Invoke(this, EventArgs.Empty);
        if (liveAmount < 0)
        {
            OnPlayerDie?.Invoke(this, EventArgs.Empty);
            transform.position = mazeGenerator.PlayerSpawnPoint;
        }
        else
        {
            transform.position = mazeGenerator.PlayerSpawnPoint;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Loot"))
        {

            liveAmount++;
            OnPlayerLoseLive?.Invoke(this, EventArgs.Empty);
            Destroy(collision.gameObject);
        }
    }





}
