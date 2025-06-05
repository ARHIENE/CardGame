using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardUseManager : MonoBehaviour
{
    public Transform handPanel;
    public HandManager handManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseAllSelectedCards();
        }
    }

    void UseAllSelectedCards()
    {
        List<CardHandler> usedCards = new List<CardHandler>();

        for (int i = handPanel.childCount - 1; i >= 0; i--)
        {
            var card = handPanel.GetChild(i);
            var handler = card.GetComponent<CardHandler>();
            if (handler != null && handler.isSelected)
            {
                usedCards.Add(handler);
            }
        }

        int count = usedCards.Count;

        if (count < 2 || count > 4)
        {
            Debug.Log("2~4장만 사용할 수 있습니다.");
            return;
        }

        if (count == 2)
        {
            var c1 = usedCards[0];
            var c2 = usedCards[1];

            int m1 = c1.month;
            int m2 = c2.month;
            var set = new HashSet<int> { m1, m2 };

            Debug.Log($"카드1: {m1}월, 카드2: {m2}월");

            // 삼팔광땡
            if ((m1 == 3 && m2 == 8 || m1 == 8 && m2 == 3) &&
                c1.type == CardType.광 && c2.type == CardType.광)
            {
                Debug.Log("삼팔광땡");
            }
            // 광땡
            else if (c1.type == CardType.광 && c2.type == CardType.광 &&
                     new[] { 1, 3, 8 }.Contains(m1) && new[] { 1, 3, 8 }.Contains(m2))
            {
                Debug.Log("광땡");
            }
            // 땡
            else if (m1 == m2)
            {
                Debug.Log($"{m1}땡");
            }
            // 특수 조합
            else if (set.SetEquals(new[] { 1, 2 })) Debug.Log("알리");
            else if (set.SetEquals(new[] { 1, 4 })) Debug.Log("독사");
            else if (set.SetEquals(new[] { 1, 9 })) Debug.Log("구삥");
            else if (set.SetEquals(new[] { 1, 10 })) Debug.Log("장삥");
            else if (set.SetEquals(new[] { 4, 10 })) Debug.Log("장사");
            else if (set.SetEquals(new[] { 4, 6 })) Debug.Log("세륙");
            else if (set.SetEquals(new[] { 2, 8 })) Debug.Log("망통"); // 특조합 망통
            else
            {
                // 끗 계산은 month 기반으로 처리
                int sum = (m1 + m2) % 10;
                Debug.Log($"끗 계산: {m1} + {m2} = {(m1 + m2)} → {sum}끗");

                if (sum == 9) Debug.Log("갑오");
                else if (sum == 0) Debug.Log("망통");
                else Debug.Log($"{sum}끗");
            }
        }
        else if (count == 3)
        {
            if (usedCards.All(c => c.month == usedCards[0].month))
            {
                Debug.Log("폭탄");
            }
            else
            {
                Debug.Log("같은 카드만 사용할 수 있습니다.");
                return;
            }
        }
        else if (count == 4)
        {
            if (usedCards.All(c => c.month == usedCards[0].month))
            {
                Debug.Log("총통");
            }
            else
            {
                Debug.Log("같은 카드만 사용할 수 있습니다.");
                return;
            }
        }

        foreach (var handler in usedCards)
        {
            handler.UseCard();
        }

        handManager.DrawCards(count);
    }
}
