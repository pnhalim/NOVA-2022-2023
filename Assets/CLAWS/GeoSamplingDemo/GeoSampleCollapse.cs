using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoSampleCollapse : MonoBehaviour
{
    public GameObject sampleView;
    public GeoSampleListController controller;
    private void Start()
    {
        sampleView.SetActive(true);
    }

    public void Toggle(GameObject newView)
    {
        StartCoroutine(ToggleCoroutine(newView));
    }

    IEnumerator ToggleCoroutine(GameObject newView)
    {
        yield return new WaitForSeconds(1f);
        newView.SetActive(!newView.activeSelf);
        sampleView.SetActive(!sampleView.activeSelf);
        controller.refresh();
    }
}