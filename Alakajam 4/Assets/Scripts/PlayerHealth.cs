using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerHealth")]
public class PlayerHealth : ScriptableObject
{
    public int MaxHealth = 6;
    public int CurrentHealth;
}
