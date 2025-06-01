using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform handPanel;
    public Sprite[] cardSprites; // 48�� ��������Ʈ �̸� ����

    private List<int> deck = new List<int>();

    void Start()
    {
        InitDeck();
        DrawCards(4);
    }

    void InitDeck()
    {
        deck.Clear();
        for (int i = 0; i < 40; i++) deck.Add(i); //40������ �ϴ� => ���߿� ī�� 50����� �ø����� �̰� �ǵ�� ��
        Shuffle(deck);
    }

    void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int cardId = deck[0];
            deck.RemoveAt(0);
            GameObject card = Instantiate(cardPrefab, handPanel);
            card.GetComponent<Image>().sprite = cardSprites[cardId];
        }
    }
}
