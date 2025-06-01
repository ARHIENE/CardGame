using UnityEngine;

public class CardUseManager : MonoBehaviour
{
    public Transform handPanel;
    public HandManager handManager; // 드로우 기능이 들어있는 스크립트

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 좌클릭 감지
        {
            UseAllSelectedCards();
        }
    }

    void UseAllSelectedCards()
    {
        int usedCount = 0;

        for (int i = handPanel.childCount - 1; i >= 0; i--)
        {
            Transform card = handPanel.GetChild(i);
            CardHandler handler = card.GetComponent<CardHandler>();
            if (handler != null && handler.isSelected)
            {
                handler.UseCard();
                usedCount++;
            }
        }

        if (usedCount > 0)
        {
            handManager.DrawCards(usedCount);
        }
    }
}
