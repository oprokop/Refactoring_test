using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    [SerializeField] Vector3 rotMin;
    [SerializeField] Vector3 rotMax;
    [SerializeField] Vector3 posMin;
    [SerializeField] Vector3 posMax;
    [SerializeField] List<GameObject> objects;

    void Awake() 
    { 
        Randomize();
    }
    
    void Randomize()
    {
        foreach(var obj in objects)
        {
            obj.transform.rotation = Quaternion.Euler(Random.Range(rotMin.x, rotMax.x), Random.Range(rotMin.y, rotMax.y), Random.Range(rotMin.z, rotMax.z));
            obj.transform.position += new Vector3(Random.Range(posMin.x, posMax.x), Random.Range(posMin.y, posMax.y), Random.Range(posMin.z, posMax.z));
        }
    }
}