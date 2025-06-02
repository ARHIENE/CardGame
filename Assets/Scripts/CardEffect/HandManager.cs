using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform handPanel;
    public Sprite[] cardSprites; // 48개 스프라이트 미리 세팅

    private List<int> deck = new List<int>();

    public RectTransform deckOrigin; // 덱 묶음 위치 (UI상 오른쪽 하단)

    void Start()
    {
        InitDeck();
        DrawCards(5);
    }

    void InitDeck()
    {
        deck.Clear();
        for (int i = 0; i < 40; i++) deck.Add(i); // 40장 한정
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
        StartCoroutine(AnimateDraw(count));
    }

    IEnumerator AnimateDraw(int count)
    {
        // 광 카드 ID 리스트 (너 카드 스프라이트에 맞게 수정 가능)
        List<int> 광카드들 = new List<int> { 0, 8, 28 }; // 예: 1광, 3광, 8광

        for (int i = 0; i < count; i++)
        {
            if (deck.Count == 0) yield break;

            int cardId = deck[0];
            deck.RemoveAt(0);

            GameObject card = Instantiate(cardPrefab, handPanel);
            card.GetComponent<Image>().sprite = cardSprites[cardId];


            CardHandler handler = card.GetComponent<CardHandler>();
            if (handler != null)
            {
                handler.month = (cardId / 4) + 1; // 예: 0~3 → 1월, 4~7 → 2월
                handler.type = 광카드들.Contains(cardId) ? CardType.광 : CardType.일반;
                handler.ggutValue = (cardId % 10) + 1; // 1~10 범위 끗값 부여
            }

            // 애니메이션
            RectTransform rt = card.GetComponent<RectTransform>();

            Vector2 worldStart = deckOrigin.position;
            Vector2 localStart;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                handPanel as RectTransform, worldStart, null, out localStart
            );
            rt.anchoredPosition = localStart;

            CanvasGroup cg = card.GetComponent<CanvasGroup>();
            if (cg == null) cg = card.AddComponent<CanvasGroup>();
            cg.alpha = 0f;

            Vector2 targetPos = Vector2.zero;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 4f;
                rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPos, t);
                cg.alpha = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }

            rt.anchoredPosition = targetPos;
            cg.alpha = 1f;

            yield return new WaitForSeconds(0.05f); // 카드 간격
        }
    }
}
