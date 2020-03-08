using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShakeConfig", menuName = "Config")]
public class ShakeConfig : ScriptableObject
{
    public Vector3 pos;
    public float time;
    public float delay;
}