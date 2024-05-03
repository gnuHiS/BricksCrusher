using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFilter : MonoBehaviour
{
    bool done = false;


    private void Update()
    {
        if (!done && this.gameObject.active)
        {
            var rectTransform = GetComponent<RectTransform>();
            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            Debug.Log(anchorMax.y);
            anchorMax.y /= Screen.height;
            Debug.Log(anchorMax.y);

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            done = true;
        }
    }
}
