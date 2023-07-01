using System;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    public event Action<int> OnCellClicked;
    [SerializeField] private Button cellButton;
    [SerializeField] private Image cellImage;
    [SerializeField] private bool isActive = true;
    [SerializeField] private int cellNumber;

    private Color transparent = new Color(1f, 1f, 1f, 0f);
    private Color opaque = new Color(1f, 1f, 1f, 1f);

    private void OnEnable()
    {
        cellButton.onClick.AddListener(OnButtonClicked);
        Initialize();
    }
    
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        cellImage.color = transparent;
        isActive = true;
    }

    private void OnDisable()
    {
        cellButton.onClick.RemoveListener(OnButtonClicked);
    }

    public void SetSprite(Sprite spriteToSet)
    {
        cellImage.sprite = spriteToSet;
    }

    public void OnButtonClicked()
    {
        if (!isActive)
        {
            return;
        }
        cellImage.color = opaque;
        isActive = false;
        OnCellClicked?.Invoke(cellNumber);
    }
}
