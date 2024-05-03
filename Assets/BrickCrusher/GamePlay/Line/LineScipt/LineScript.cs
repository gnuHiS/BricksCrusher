using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public static LineScript lineScript;

    // var- Reference var
    private Vector2 startPos,finalDirection,reflectDirection,reflectDirectionDouble;
    private float constantSpeed,minAngle,maxAngle;

    [Header("Line- Prefab Reference:")]
    // var- Line Preview + Line Preview_Reflect
    [SerializeField] GameObject dotPrefab;
    GameObject[] dots;
    public int numberOfDot;
    [SerializeField] GameObject dotReflectPrefab;
    GameObject[] dotsReflect;
    public int numberOfDotReflect;

    
    [Header("Line- Preview Mask")]
    // var- Line Preview_mask
    [SerializeField] Transform spriteMasked;
    [SerializeField] Transform spriteMaskDouble;
    private RaycastHit2D hit2D,hit2D_double;
    [SerializeField] LayerMask hitLayer;

    [Header("Line- Property")]
    [SerializeField] private float distanceOfEachDot;

    private void Awake()
    {
        if (lineScript == null)
        {
            lineScript = this;
        }
    }

    // Start 
    private void Start()
    {
        // Command- Initial dots array
        dots = new GameObject[numberOfDot*2];
        dotsReflect = new GameObject[numberOfDotReflect*2];

        // Execute- Instantiate and Attach to dots, dotsReflect
        dots = CreatAndAttach(dots, dotPrefab);
        dotsReflect = CreatAndAttach(dotsReflect, dotReflectPrefab);
        DisableDouble();

        GroupChild();
    }

    // func- Creat And Attach Dots
    private GameObject[] CreatAndAttach(GameObject[] dotsArray, GameObject prefabDot)
    {
        for (int i = 0; i < dotsArray.Length; i++)
        {
            dotsArray[i] = Instantiate(prefabDot, startPos, Quaternion.identity);
        }
        return dotsArray;
    }

    // func- Predicted line dots
    private Vector2 DotPosPerTime(float t)
    {
        Vector2 calculate = startPos + (finalDirection.normalized * constantSpeed * t);
        return calculate;
    }
    // func- Predicted line dots Reflect
    private Vector2 DotPosReflectPerTime(float t)
    {
        Vector2 calculate = hit2D.centroid + (reflectDirection.normalized * constantSpeed * t);
        return calculate;
    }
    private Vector2 DotPosPerTime_Double(float t)
    {
        Vector2 calculate = startPos + (new Vector2(-finalDirection.x,finalDirection.y).normalized * constantSpeed * t);
        return calculate;
    }
    // func- Predicted line dots Reflect
    private Vector2 DotPosReflectPerTime_Double(float t)
    {
        Vector2 calculate = hit2D_double.centroid + (reflectDirectionDouble.normalized * constantSpeed * t);
        return calculate;
    }

    private void GroupChild()
    {
        for (int i = 0; i < numberOfDot*2; i++)
        {
            dots[i].transform.SetParent(this.transform);
        }
        for (int i = 0; i < numberOfDotReflect*2; i++)
        {
            dotsReflect[i].transform.SetParent(this.transform);
        }
    }

    // public func- Activate Dots
    internal void ActivateDots()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < dotsReflect.Length; i++)
        {
            dotsReflect[i].gameObject.SetActive(true);
        }
    }
    // public func- DeActivate Dots
    internal void DeActivateDots()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < dotsReflect.Length; i++)
        {
            dotsReflect[i].gameObject.SetActive(false);
        }
    }

    protected internal void GetReferenceVar(Vector2 startPos, float constantSpeed, float minAngle, float maxAngle)
    {
        this.startPos = startPos;
        this.constantSpeed = constantSpeed;
        this.minAngle = minAngle;
        this.maxAngle = maxAngle;
    }

    // func- Re Default Dots Pos
    internal void SetDotsPosDefault(Vector2 startPos)
    {
        this.startPos = startPos;
        // ReNew the Preview Line Pos
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].transform.position = startPos;
        }
        for (int i = 0; i < dotsReflect.Length; i++)
        {
            dotsReflect[i].transform.position = startPos;
        }
    }

    protected internal void CircleCasting(Vector2 finalDirection, float angleB)
    {
        float offsetRadius = 0.005f;
        hit2D = Physics2D.CircleCast(startPos, LevelManager.levelManager.GetBallRadiusScaled()-offsetRadius, finalDirection, 20f, hitLayer);
        hit2D_double = Physics2D.CircleCast(startPos, LevelManager.levelManager.GetBallRadiusScaled()-offsetRadius, new Vector2(-finalDirection.x,finalDirection.y), 20f, hitLayer);

        spriteMasked.position = new Vector3(hit2D.centroid.x, hit2D.centroid.y - 0.0f, spriteMasked.position.z);
        spriteMaskDouble.position = new Vector3(hit2D_double.centroid.x, hit2D_double.centroid.y - 0.0f, spriteMaskDouble.position.z);

        // rotate mask
        spriteMasked.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(angleB, minAngle, maxAngle));
        spriteMaskDouble.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(-angleB, minAngle, maxAngle));

        this.finalDirection = finalDirection;
        this.reflectDirection = Vector2.Reflect(finalDirection, hit2D.normal);
        this.reflectDirectionDouble = Vector2.Reflect(new Vector2(-finalDirection.x, finalDirection.y), hit2D_double.normal);
    }

    protected internal void RotateLine()
    {
        // dots
        for (int i = 0; i < numberOfDot; i++)
        {
            dots[i].transform.position = DotPosPerTime(i * distanceOfEachDot);
        }
        // dots Reflect
        for (int i = 0; i < numberOfDotReflect; i++)
        {
            dotsReflect[i].transform.position = DotPosReflectPerTime(i * distanceOfEachDot);
        }
        if (ItemEffect.itemEffect.isDouble)
        {
            // dots
            for (int i = numberOfDot; i < numberOfDot*2; i++)
            {
                dots[i].transform.position = DotPosPerTime_Double((i-numberOfDot) * distanceOfEachDot);
            }
            // dots Reflect
            for (int i = numberOfDotReflect; i < numberOfDotReflect*2; i++)
            {
                dotsReflect[i].transform.position = DotPosReflectPerTime_Double((i-numberOfDotReflect) * distanceOfEachDot);
            }
        }
    }
    internal void DisableDouble()
    {
        for (int i = numberOfDot; i < numberOfDot * 2; i++)
        {
            dots[i].gameObject.SetActive(false);
        }
        for (int i = numberOfDotReflect; i < numberOfDotReflect * 2; i++)
        {
            dotsReflect[i].gameObject.SetActive(false);
        }
        spriteMaskDouble.gameObject.SetActive(false);
    }
    internal void EnableDouble()
    {
        for (int i = numberOfDot; i < numberOfDot * 2; i++)
        {
            dots[i].gameObject.SetActive(true);
        }
        for (int i = numberOfDotReflect; i < numberOfDotReflect * 2; i++)
        {
            dotsReflect[i].gameObject.SetActive(true);
        }
        spriteMaskDouble.gameObject.SetActive(true);
    }
}
