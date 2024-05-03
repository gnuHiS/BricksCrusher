using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockView : MonoBehaviour
{
    // var- Visualize HP
    TextMeshProUGUI textBlock;

    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected BlockScriptable blockSpriteVariant;
    protected Sprite[] artworks;

    private void Start()
    {
        Init_Self();
    }
    protected void Init_Self()
    {
        artworks = blockSpriteVariant.GetArtworks();
        textBlock = GetComponentInChildren<TextMeshProUGUI>();
        textBlock.text = GetComponentInParent<BlockStatus>().GetCurrentHealth().ToString();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.GetComponentInParent<BlockStatus>().GrantUpdateInfo();
    }

    public void UpdateText(int currentHealth)
    {
        textBlock.text = currentHealth.ToString();
    }

    public void UpdateSprite(int currentHealth, DefineBlockType blockType)
    {
        if (blockType == DefineBlockType.STANDARD)
        {
            if (currentHealth <= 2 && currentHealth > 0 && spriteRenderer.sprite != artworks[2])
            {
                spriteRenderer.sprite = artworks[2];
            }
            else if (currentHealth <= 5 && currentHealth > 2 && spriteRenderer.sprite != artworks[1])
            {
                spriteRenderer.sprite = artworks[1];
            }
            else if (currentHealth <= 10 && currentHealth > 5 && spriteRenderer.sprite != artworks[5])
            {
                spriteRenderer.sprite = artworks[5];
            }
            else if (currentHealth <= 15 && currentHealth > 10 && spriteRenderer.sprite != artworks[4])
            {
                spriteRenderer.sprite = artworks[4];
            }
            else if (currentHealth > 15 && spriteRenderer.sprite != artworks[3])
            {
                spriteRenderer.sprite = artworks[3];
            }
        }
    }

}
