using System.Collections;
using UnityEngine;

public class InteractableTrigger : Trigger<Interactable>
{
    [SerializeField] private float _enableDelay = 0f;

    private void Start()
    {
        if(_enableDelay == 0)
            return;
        
        Disable();

        StartCoroutine(EnableWithDelay(_enableDelay));
    }

    public IEnumerator EnableWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Enable();
    }
}
