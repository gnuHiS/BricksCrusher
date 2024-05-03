using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomButton : MonoBehaviour
{
    public void Button_Bomb()
    {
        ItemEffect.itemEffect.UseBomb();
    }
    public void Button_Double()
    {
        ItemEffect.itemEffect.UseDouble();
    }
    public void Button_BounceGround()
    {
        ItemEffect.itemEffect.UseBounce_Ground();
    }
    public void Button_ChainSaw()
    {
        ItemEffect.itemEffect.UseChainSaw();
    }
    public void Button_BallSpeedUp()
    {
        ItemEffect.itemEffect.UseBallSpeedUp();
    }
    public void Button_BallComeBack()
    {
        ItemEffect.itemEffect.UseBallComeBack();
    }

}

