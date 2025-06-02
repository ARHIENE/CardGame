using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType { 일반, 광 }

public class CardHandler : MonoBehaviour, IPointerClickHandler
{
    public Image overlay;                    // 선택 표시용 파란 이미지
    [HideInInspector] public bool isSelected = false;

    public static int selectedCardCount = 0;
    private static int maxSelectableCards = 4;

    public int month;         // 1~10
    public CardType type;     // 광 or 일반
    public int ggutValue;     // 끗 계산용 값 (1~10)

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ToggleSelection();
        }
    }

    void ToggleSelection()
    {
        if (!isSelected)
        {
            if (selectedCardCount >= maxSelectableCards) return;

            isSelected = true;
            selectedCardCount++;

            if (overlay != null)
                overlay.gameObject.SetActive(true);
        }
        else
        {
            isSelected = false;
            selectedCardCount = Mathf.Max(0, selectedCardCount - 1);

            if (overlay != null)
                overlay.gameObject.SetActive(false);
        }
    }

    public void UseCard()
    {
        selectedCardCount = Mathf.Max(0, selectedCardCount - 1);
        Destroy(this.gameObject);
    }
}
