using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStatus : MonoBehaviour
{

    // Reference
    [SerializeField] private SpriteRenderer spriteRendererBall;
    [SerializeField] private BallScriptable ballVariant;
    
    
    private int demolition;
    private string nameBall;

    private void Start()
    {
        spriteRendererBall.sprite = ballVariant.artWork;
        nameBall = ballVariant.nameBall;
        demolition = ballVariant.demolition;
    }

    public int GetDemolition()
    {
        return demolition;
    }
}
