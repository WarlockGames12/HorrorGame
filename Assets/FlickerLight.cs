using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{

    private Light _lightFlicker;
    
    [Header("Flicker Settings: ")]
    [SerializeField] private float minSpeedFlicker = 0.1f;
    [SerializeField] private float maxSpeedFlicker = 0.5f;
    [SerializeField] private float minIntensity = 0.01f;
    [SerializeField] private float maxIntensity = 0.2f;
    
    private void Start()
    {
        _lightFlicker = GetComponent<Light>();
        StartCoroutine(isFlickering());
    }

    private IEnumerator isFlickering()
    {
        while (true)
        {
            _lightFlicker.enabled = true;
            _lightFlicker.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(Random.Range(minSpeedFlicker, maxSpeedFlicker));
            _lightFlicker.enabled = false;
            yield return new WaitForSeconds(Random.Range(minSpeedFlicker, maxSpeedFlicker));
        }
    }
}
