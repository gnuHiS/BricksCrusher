using System;
using UnityEngine;
using UnityEngine.UI;
public static class ButtonExtensions
{
    public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}
