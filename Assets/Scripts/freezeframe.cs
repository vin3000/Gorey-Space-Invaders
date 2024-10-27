using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezeframe : MonoBehaviour
{
    [Range(0f, 1.5f)]
    float Duration = 1f;
    bool _isfrozen = false;
    float _pendingFreezeduration = 0f;
    // Update is called once per frame
    void Update()
    {
        
        {
            if (_pendingFreezeduration > 0 && !_isfrozen)
                StartCoroutine(Dofreeze());
        }
    }
   
    public void freeze(float duration) 
    {
        Duration = duration;
        _pendingFreezeduration = Duration;
    }
    IEnumerator Dofreeze()
           
    {
         _isfrozen = true;
        var original = Time.timeScale;
    Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(Duration);

    Time.timeScale = original;

        _pendingFreezeduration = 0;
        _isfrozen = false;
    }
}