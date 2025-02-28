using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct Data
{ 
    public string Key;
    public GameObject StartVFX;
    public GameObject ExplosionVFX;
    public float LiveTime;
    public float ReactionTime;
    public int GunIndex;
}

public class Bullet : MonoBehaviour
{
    public int gun = 0;
    public List<Data> Data;
    public bool isCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isCollided == true)
        {
            return;
        }
        isCollided = true;
        var data = Data.Single(s => gameObject.name.Contains(s.Key));

        var timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = data.ReactionTime;
        timer.OnTime = () =>
        {
            var data = Data.Single(s => gameObject.name.Contains(s.Key));
            var effectInstance = Instantiate(data.ExplosionVFX, collision.GetContact(0).point, Quaternion.identity);
            effectInstance.transform.rotation *= Quaternion.FromToRotation(effectInstance.transform.up, collision.GetContact(0).normal);
            Destroy(effectInstance, effectInstance.GetComponent<ParticleSystem>().main.duration);
        };

        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = data.LiveTime;
        timer.OnTime = () =>
        {
            Destroy(gameObject);
        };
    }
}

