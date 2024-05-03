using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockStatus : MonoBehaviour
{
    
    [SerializeField]
    private DefineBlockType blockType;

    private bool canDrop=true;

    // var- HP
    [UnityEngine.Header("Standard Group Attribute:")]
    [SerializeField] private int maxHealth;
    private int currentHealth;

    // var- Portal Block
    private int keyForPortalBlock;

    BlockView blockView;
    public string[] blockLink;


    // Start is called before the first frame update
    void Start()
    {
        
        if (blockType == DefineBlockType.NONE)
        {
            Destroy(gameObject);
        }
        else if (blockType == DefineBlockType.STANDARD ||
                 blockType == DefineBlockType.TRIANGLE_TOP_RIGHT ||
                 blockType == DefineBlockType.TRIANGLE_RIGHT_BOTTOM ||
                 blockType == DefineBlockType.TRIANGLE_BOTTOM_LEFT ||
                 blockType == DefineBlockType.TRIANGLE_LEFT_TOP ||
                 blockType == DefineBlockType.PINNED ||
                 blockType == DefineBlockType.GEM ||
                 blockType == DefineBlockType.ROCKET ||
                 blockType == DefineBlockType.LIGHTNING ||
                 blockType == DefineBlockType.ICICLE ||
                 blockType == DefineBlockType.FREEZING)
        {
            currentHealth = maxHealth;
            GameObject blockInstanted;
            
            blockInstanted = (GameObject)Instantiate(Resources.Load("BlockView/" + blockLink[(int)blockType - 1]), transform.position, Quaternion.identity);
            blockInstanted.transform.SetParent(this.transform);
            blockInstanted.transform.localScale = new Vector3(blockInstanted.transform.localScale.x, blockInstanted.transform.localScale.y, blockInstanted.transform.localScale.z) * LevelManager.levelManager.GetLevelScale();
            blockView = blockInstanted.GetComponent<BlockView>();
        }
    }

    protected void UpdateInfo()
    {
        blockView.UpdateText(currentHealth);
        blockView.UpdateSprite(currentHealth,blockType);
    }
    public void GrantUpdateInfo()
    {
        UpdateInfo();
    }

    private void TakeDamage(int damage)
    {
        {
            currentHealth -= damage;
            UpdateInfo();
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    internal void Boom()
    {
        currentHealth = currentHealth * 60 / 100;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        UpdateInfo();
    }
    internal bool CheckCanDrop()
    {
        return canDrop;
    }
    internal int GetCurrentHealth()
    {
        return currentHealth;
    }

    internal void OnBallEnter(BallStatus ballStatus)
    {
        if (blockType == DefineBlockType.STANDARD ||
            blockType == DefineBlockType.TRIANGLE_TOP_RIGHT ||
            blockType == DefineBlockType.TRIANGLE_RIGHT_BOTTOM ||
            blockType == DefineBlockType.TRIANGLE_BOTTOM_LEFT ||
            blockType == DefineBlockType.TRIANGLE_LEFT_TOP ||
            blockType == DefineBlockType.PINNED ||
            blockType == DefineBlockType.GEM ||
            blockType == DefineBlockType.ROCKET ||
            blockType == DefineBlockType.LIGHTNING ||
            blockType == DefineBlockType.ICICLE ||
            blockType == DefineBlockType.FREEZING)
        {
            TakeDamage(ballStatus.GetDemolition());
        }
    }
    public bool CheckBlockTypeCanHit()
    {
        if (blockType == DefineBlockType.STANDARD ||
            blockType == DefineBlockType.TRIANGLE_TOP_RIGHT ||
            blockType == DefineBlockType.TRIANGLE_RIGHT_BOTTOM ||
            blockType == DefineBlockType.TRIANGLE_BOTTOM_LEFT ||
            blockType == DefineBlockType.TRIANGLE_LEFT_TOP ||
            blockType == DefineBlockType.PINNED ||
            blockType == DefineBlockType.GEM ||
            blockType == DefineBlockType.ROCKET ||
            blockType == DefineBlockType.LIGHTNING ||
            blockType == DefineBlockType.ICICLE ||
            blockType == DefineBlockType.FREEZING)
        {
            return true;
        }
        else return false;
    }
}
public enum DefineBlockType
{
    NONE,
    STANDARD,
    TRIANGLE_TOP_RIGHT,
    TRIANGLE_RIGHT_BOTTOM,
    TRIANGLE_BOTTOM_LEFT,
    TRIANGLE_LEFT_TOP,
    PINNED,
    METAL,
    PLUS_BALL,
    GEM,
    ROCKET,
    THREE_DIRRECTION_UP,
    CANON,
    PORTAL_IN,
    PORTAL_OUT,
    LASER_VERTICAL,
    LASER_HORIZONTAL,
    LASER_BOTH,
    WIPE_BAR_CLOCKWISE,
    WIPE_BAR_COUNTER_CLOCKWISE,
    LOCKABLE,
    LIGHTNING,
    ICICLE,
    FREEZING
}