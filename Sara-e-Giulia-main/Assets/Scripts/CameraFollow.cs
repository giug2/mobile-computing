using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(2, 10)]
    public float smoothFactor;
    public Vector3 minVal;
    public Vector3 maxVal;
    public Vector3 minBossVal;
    public Vector3 maxBossVal;

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        if (GameplayManager.metBoss == true)
        {
            minVal = minBossVal;
            maxVal = maxBossVal;
        }
        Vector3 boundPosition = new Vector3(Mathf.Clamp(targetPosition.x, minVal.x, maxVal.x), 
            Mathf.Clamp(targetPosition.y, minVal.y, maxVal.y), Mathf.Clamp(targetPosition.z, minVal.z, maxVal.z));
        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
