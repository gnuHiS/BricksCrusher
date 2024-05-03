using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    // This Script use for Bottom button group to Control Middle Group Menu 

    public static MenuManager menuManager;

    [SerializeField] Menu[] menus;
    private void Awake()
    {
        menuManager = this;
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    public void OpenMenu(Menu menu)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if (menus[i].GetOpenStatus())
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }
    public void OpenMenu(string menuName)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if(menus[i].name == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].GetOpenStatus())
            {
                CloseMenu(menus[i]);
            }
        }
    }
    
}
