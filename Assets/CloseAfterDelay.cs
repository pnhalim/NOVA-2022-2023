using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAfterDelay : MonoBehaviour
{
    public enum Mode {
        onStart, 
        onEnable
    }

    public Mode mode;
    public float delay = 1;
    public bool destroy = false;

    void Start()
    {
        if (mode == Mode.onStart)
        {
            StartCoroutine(Delay());
        }
    }

    private void OnEnable()
    {
        if (mode == Mode.onEnable)
        {
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        if (destroy)
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
    }
}
