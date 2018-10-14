using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ScrollSpeed")]
public class ScrollSpeed : ScriptableObject {
    [SerializeField]
    public float Value;
}
