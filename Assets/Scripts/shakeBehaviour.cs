using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeBehaviour : MonoBehaviour
{
    public bool isShake = false;
    // Transform of the GameObject you want to shake
    private Transform cameraTransform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.05f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 3.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;
    void Awake()
    {
        if (cameraTransform == null)
            cameraTransform = GetComponent<Transform>();
    }

    private void OnEnable() {
        initialPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        initialPosition = cameraTransform.localPosition;
        if (shakeDuration > 0)
        {
            isShake = true;
            cameraTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            isShake = false;
            shakeDuration = 0f;
            cameraTransform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 0.5f;
    }
    public void stopShake(){
        shakeDuration = 0.0f;
    }
}
