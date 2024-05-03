using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResponsive : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        float orthoSize = 5.625f*Screen.height/Screen.width*0.5f;
        Camera.main.orthographicSize = orthoSize;
    }

}
