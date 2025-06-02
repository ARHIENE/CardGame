using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType { �Ϲ�, �� }

public class CardHandler : MonoBehaviour, IPointerClickHandler
{
    public Image overlay;                    // ���� ǥ�ÿ� �Ķ� �̹���
    [HideInInspector] public bool isSelected = false;

    public static int selectedCardCount = 0;
    private static int maxSelectableCards = 4;

    public int month;         // 1~10
    public CardType type;     // �� or �Ϲ�
    public int ggutValue;     // �� ���� �� (1~10)

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
