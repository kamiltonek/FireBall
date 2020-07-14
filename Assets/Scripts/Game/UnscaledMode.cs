using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledMode : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    // Update is called once per frame
    void Update()
    {

            particleSystem.Simulate(Time.unscaledDeltaTime, true, false);

    }
}
