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

        // ÆÇÁ¤: 2ÀåÀÏ ¶§¸¸ Á·º¸ ºñ±³
        if (count == 2)
        {
            var c1 = usedCards[0];
            var c2 = usedCards[1];

            int m1 = c1.month;
            int m2 = c2.month;
            var set = new HashSet<int> { m1, m2 };

            // »ïÆÈ±¤¶¯
            if ((m1 == 3 && m2 == 8 || m1 == 8 && m2 == 3) &&
                c1.type == CardType.±¤ && c2.type == CardType.±¤)
            {
                Debug.Log("»ïÆÈ±¤¶¯!");
            }
            // ±¤¶¯
            else if (c1.type == CardType.±¤ && c2.type == CardType.±¤ &&
                    new[] { 1, 3, 8 }.Contains(m1) && new[] { 1, 3, 8 }.Contains(m2))
            {
                Debug.Log("±¤¶¯!");
            }
            // ¶¯
            else if (m1 == m2)
            {
                Debug.Log($"{m1}¶¯!");
            }
            // Æ¯¼ö Á¶ÇÕ
            else if (set.SetEquals(new[] { 1, 2 })) Debug.Log("¾Ë¸®!");
            else if (set.SetEquals(new[] { 1, 4 })) Debug.Log("µ¶»ç!");
            else if (set.SetEquals(new[] { 1, 9 })) Debug.Log("±¸»æ!");
            else if (set.SetEquals(new[] { 1, 10 })) Debug.Log("Àå»æ!");
            else if (set.SetEquals(new[] { 4, 10 })) Debug.Log("Àå»ç!");
            else if (set.SetEquals(new[] { 4, 6 })) Debug.Log("¼¼·ú!");
            else if (set.SetEquals(new[] { 2, 8 })) Debug.Log("¸ÁÅë!"); // ¸ÁÅë Æ¯Á¶ÇÕ ¿ì¼±
            else
            {
                // ²ý/°©¿À/¸ÁÅë
                int g1 = c1.ggutValue;
                int g2 = c2.ggutValue;
                int sum = (g1 + g2) % 10;

                if (sum == 9) Debug.Log("°©¿À!");
                else if (sum == 0) Debug.Log("¸ÁÅë!");
                else Debug.Log($"{sum}²ý");
            }
        }
        else if (count == 3)
        {
            if (usedCards.All(c => c.month == usedCards[0].month))
            {
                Debug.Log("ÆøÅº!");
            }
            else
            {
                Debug.Log("°°Àº Ä«µå¸¸ »ç¿ëÇÒ ¼ö ÀÖ½À´Ï´Ù.");
                return;
            }
        }
        else if (count == 4)
        {
            if (usedCards.All(c => c.month == usedCards[0].month))
            {
                Debug.Log("ÃÑÅë!");
            }
            else
            {
                Debug.Log("°°Àº Ä«µå¸¸ »ç¿ëÇÒ ¼ö ÀÖ½À´Ï´Ù.");
                return;
            }
        }
        else
        {
            Debug.Log("2~4Àå¸¸ »ç¿ëÇÒ ¼ö ÀÖ½À´Ï´Ù.");
            return;
        }

        // Ä«µå »ç¿ë
        foreach (var handler in usedCards)
        {
            handler.UseCard();
        }

        handManager.DrawCards(count);
    }
}
