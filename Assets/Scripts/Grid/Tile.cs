using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;

    [SerializeField] private Sprite greenSprite;
    [SerializeField] private Sprite lavaSprite;
    [SerializeField] private Sprite diamondSprite;

    private TileType tileType;
    private bool interactable = true;

    public TileType Type => tileType;

    public void Setup(TileType type)
    {
        tileType = type;
        interactable = true;

        switch (tileType)
        {
            case TileType.Green:
                iconImage.sprite = greenSprite;
                break;

            case TileType.Lava:
                iconImage.sprite = lavaSprite;
                break;

            case TileType.Diamond:
                iconImage.sprite = diamondSprite;
                break;
        }

        iconImage.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable)
            return;

        if (GameManager.Instance == null)
        {
            // Debug.LogError("GameManager.Instance is NULL!");
            return;
        }

        GameManager.Instance.HandleTileClick(
            this,
            eventData.position
        );
    }

    public void CollectDiamond()
    {
        interactable = false;

        if (iconImage != null)
        {
            iconImage.enabled = false;
        }
    }

    public void DisableTile()
    {
        interactable = false;
    }
}