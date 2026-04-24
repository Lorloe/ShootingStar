using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeMagnitude = 0.2f;

    private Vector3 initialPosition;
    private Coroutine shakeCoroutine;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    public void Play()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            transform.localPosition = initialPosition;
        }

        shakeCoroutine = StartCoroutine(ShakeCoroutine());
    }

    private void OnDisable()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            shakeCoroutine = null;
        }

        transform.localPosition = initialPosition;
    }

    private IEnumerator ShakeCoroutine()
    {
        float timeElapsed = 0f;
        while(timeElapsed < shakeDuration)
        {
            transform.localPosition = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = initialPosition;
        shakeCoroutine = null;
    }
}
