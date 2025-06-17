using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMovement : MonoBehaviour
{
    public Transform mainCamera;
    public Transform middleBackground;
    public Transform sideBackground;
    public float height;
    public float length;

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.position.x > middleBackground.position.x)
            sideBackground.position = middleBackground.position + Vector3.right*length;
        if (mainCamera.position.x < middleBackground.position.x)
            sideBackground.position = middleBackground.position + Vector3.left*length;
        if (mainCamera.position.x > sideBackground.position.x || mainCamera.position.x < sideBackground.position.x)
        {
            Transform x = middleBackground;
            middleBackground = sideBackground;
            sideBackground = x;
        }
    }
}
