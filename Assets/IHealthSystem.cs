using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthSystem
{
    public bool GetHit(float dmgAmmount);

    public bool isInvincible();

    public bool Heal(float healAmmount);
}
