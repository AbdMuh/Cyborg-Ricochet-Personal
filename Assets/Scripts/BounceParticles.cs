using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceParticles : MonoBehaviour
{
    private ParticleSystem _BounceParticles;
    // Start is called before the first frame update
    void Start()
    {
        _BounceParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _BounceParticles.Play();
    }
}
