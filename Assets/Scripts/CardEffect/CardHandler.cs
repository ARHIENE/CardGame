using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour, IPointerClickHandler
{
    public Image overlay; // ���� �Ķ��� �̹��� (��Ȱ��ȭ�� ���·� ����)
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
        Debug.Log("���� ī��: " + gameObject.name);
        selectedCardCount--;
        Destroy(this.gameObject);
    }
}
