using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class bloodparticle : MonoBehaviour
{

    public ParticleSystem Bloodparticle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(Bloodparticle);
    }
}

