using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class CameraShake : MonoBehaviour {
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos=transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed<duration)
        {
            float x = Random.Range((transform.localPosition.x) - 0.125f, (transform.localPosition.x) + 0.125f) * magnitude;
            float y = Random.Range((transform.localPosition.y), (transform.localPosition.y)) * magnitude;


            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;

    }
    public void ShakeCamera()
    {
        Vector3 shakeVector = new Vector3(0.1f, 0f, 0);
        transform.DOShakePosition(0.2f, 0.5f, 14, 90, false, true);
        
    }

}
