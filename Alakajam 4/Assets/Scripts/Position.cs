using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Position")]
public class Position : ScriptableObject
{
    public Vector3 Default;
    public Vector3 Pos;
}
