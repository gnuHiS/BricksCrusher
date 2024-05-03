using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSizeControl : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] Canvas canvasParent;
    private const float stdWidth = 978.5886f,stdHeight=2118.97f;
    private float wScaleResponsive,hScaleResponsive;

    private void Awake()
    {
        canvasParent = GetComponentInParent<Canvas>();
        Debug.Log(wScaleResponsive = canvasParent.GetComponent<RectTransform>().rect.width);
        wScaleResponsive = canvasParent.GetComponent<RectTransform>().rect.width / stdWidth;
        hScaleResponsive = canvasParent.GetComponent<RectTransform>().rect.height / stdHeight;
    }
    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(182.25f*wScaleResponsive, 206.8251f);
        }
        buttons[2].GetComponent<RectTransform>().sizeDelta = new Vector2(250f*wScaleResponsive, 280f);
    }
    public void OnClickBigger(Button button)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(182.25f*wScaleResponsive, 206.8251f);  
        }
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(250f*wScaleResponsive,280f);
    }
}
