using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct EffectsKeyValue
{ 
    public string Key;
    public GameObject VFX;
    public float LiveTime;
    public float ReactionTime;
}

public class Bullet : MonoBehaviour
{
    public List<EffectsKeyValue> effects;

    void Start() { }

    void Update() { }

    private void OnCollisionEnter(Collision collision)
    {
        var effect = effects.Single(s => gameObject.name.Contains(s.Key));
        var effectInstance = Instantiate(effect.VFX, collision.GetContact(0).point, Quaternion.identity);
        effectInstance.transform.rotation *= Quaternion.FromToRotation(effectInstance.transform.up, collision.GetContact(0).normal);
    }
}
