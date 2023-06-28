using System;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    public event Action<int> OnCellClicked;
    [SerializeField] private Button cellButton;
    [SerializeField] private Image cellImage;
    [SerializeField] private bool isActive = true;
    public bool IsActive => isActive;
    [SerializeField] private int cellNumber;
    public int CellNumber => cellNumber;

    private static Color transparent = new Color(1f, 1f, 1f, 0f);
    private static Color opaque = new Color(1f, 1f, 1f, 1f);

    private void OnEnable()
    {
        cellButton.onClick.AddListener(OnButtonClicked);
    }

    private void Start()
    {
        cellImage.color = transparent;
    }

    private void OnDisable()
    {
        cellButton.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
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
