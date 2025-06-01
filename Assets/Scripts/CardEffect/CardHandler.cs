using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour, IPointerClickHandler
{
    public Image overlay; // 투명 파란색 이미지 (비활성화된 상태로 시작)
    [HideInInspector] public bool isSelected = false;

    public static int selectedCardCount = 0;
    private static int maxSelectableCards = 4;

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
            selectedCardCount--;
            if (overlay != null)
                overlay.gameObject.SetActive(false);
        }
    }

    public void UseCard()
    {
        Debug.Log("사용된 카드: " + gameObject.name);
        selectedCardCount--;
        Destroy(this.gameObject);
    }
}
