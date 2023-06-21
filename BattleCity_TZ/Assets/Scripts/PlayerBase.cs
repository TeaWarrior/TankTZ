using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBase : MonoBehaviour,IDamagable
{

    public event EventHandler OnBaseDestroyed;
    public void TakeDamage()
    {
        OnBaseDestroyed?.Invoke(this, EventArgs.Empty);
    }

    
}
