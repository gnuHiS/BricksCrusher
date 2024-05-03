using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] string menuName;
    [SerializeField] bool isOpen;
    public void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
    }
    public void Close()
    {
        isOpen = false;
        gameObject.SetActive(false);
    }

    public bool GetOpenStatus()
    {
        return isOpen;
    }
}
