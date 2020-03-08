 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform me;

    [SerializeField]
    private float x, y, z, time, delay;


    public void Shake(Vector3 pos, float time, float delay)
    {
        Hashtable ht = new Hashtable();

        ht.Add("x", pos.x);
        ht.Add("y", pos.y);
        ht.Add("z", pos.z);
        ht.Add("time", time);
        ht.Add("delay", delay);

        iTween.ShakePosition(gameObject, ht);
    }
}
