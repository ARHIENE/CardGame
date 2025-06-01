using UnityEngine;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    private Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f);
    private bool isSelected = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = hoverScale;
        transform.localPosition += Vector3.up * 20f;  // ���� ��¦ �̵�
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            transform.localScale = originalScale;
            transform.localPosition -= Vector3.up * 20f;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            isSelected = !isSelected;
            // ���� �� �׵θ� ���� ��
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isSelected)
            {
                UseCard();
            }
        }
    }

    void UseCard()
    {
        // ī�� ��� ����
        Destroy(gameObject);
    }
}
