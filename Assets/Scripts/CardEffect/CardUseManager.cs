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

        // ����: 2���� ���� ���� ��
        if (count == 2)
        {
            var c1 = usedCards[0];
            var c2 = usedCards[1];

            int m1 = c1.month;
            int m2 = c2.month;
            var set = new HashSet<int> { m1, m2 };

            // ���ȱ���
            if ((m1 == 3 && m2 == 8 || m1 == 8 && m2 == 3) &&
                c1.type == CardType.�� && c2.type == CardType.��)
            {
                Debug.Log("���ȱ���!");
            }
            // ����
            else if (c1.type == CardType.�� && c2.type == CardType.�� &&
                    new[] { 1, 3, 8 }.Contains(m1) && new[] { 1, 3, 8 }.Contains(m2))
            {
                Debug.Log("����!");
            }
            // ��
            else if (m1 == m2)
            {
                Debug.Log($"{m1}��!");
            }
            // Ư�� ����
            else if (set.SetEquals(new[] { 1, 2 })) Debug.Log("�˸�!");
            else if (set.SetEquals(new[] { 1, 4 })) Debug.Log("����!");
            else if (set.SetEquals(new[] { 1, 9 })) Debug.Log("����!");
            else if (set.SetEquals(new[] { 1, 10 })) Debug.Log("���!");
            else if (set.SetEquals(new[] { 4, 10 })) Debug.Log("���!");
            else if (set.SetEquals(new[] { 4, 6 })) Debug.Log("����!");
            else if (set.SetEquals(new[] { 2, 8 })) Debug.Log("����!"); // ���� Ư���� �켱
            else
            {
                // ��/����/����
                int g1 = c1.ggutValue;
                int g2 = c2.ggutValue;
                int sum = (g1 + g2) % 10;

                if (sum == 9) Debug.Log("����!");
                else if (sum == 0) Debug.Log("����!");
                else Debug.Log($"{sum}��");
            }
        }
        else if (count == 3)
        {
            if (usedCards.All(c => c.month == usedCards[0].month))
            {
                Debug.Log("��ź!");
            }
            else
            {
                Debug.Log("���� ī�常 ����� �� �ֽ��ϴ�.");
                return;
            }
        }
        else if (count == 4)
        {
            if (usedCards.All(c => c.month == usedCards[0].month))
            {
                Debug.Log("����!");
            }
            else
            {
                Debug.Log("���� ī�常 ����� �� �ֽ��ϴ�.");
                return;
            }
        }
        else
        {
            Debug.Log("2~4�常 ����� �� �ֽ��ϴ�.");
            return;
        }

        // ī�� ���
        foreach (var handler in usedCards)
        {
            handler.UseCard();
        }

        handManager.DrawCards(count);
    }
}
