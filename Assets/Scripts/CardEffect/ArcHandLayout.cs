using UnityEngine;

public class ArcHandLayout : MonoBehaviour
{
    public float radius = 300f;
    public float maxAngle = 40f;
    public float hoverYOffset = 30f;
    public float lerpSpeed = 10f;

    void LateUpdate()
    {
        LayoutCards();
    }

    void LayoutCards()
    {
        int count = transform.childCount;
        if (count == 0) return;

        float angleStep = count > 1 ? maxAngle / (count - 1) : 0;

        for (int i = 0; i < count; i++)
        {
            Transform card = transform.GetChild(i);
            float angle = -maxAngle / 2f + angleStep * i;
            float rad = Mathf.Deg2Rad * angle;

            Vector3 targetPos = new Vector3(Mathf.Sin(rad), Mathf.Cos(rad) - 1f, 0) * radius;

            CardHoverEffect hover = card.GetComponent<CardHoverEffect>();
            if (hover != null && hover.isHovered)
                targetPos += Vector3.up * hoverYOffset;

            // 부드럽게 이동
            card.localPosition = Vector3.Lerp(card.localPosition, targetPos, Time.deltaTime * lerpSpeed);
            card.localRotation = Quaternion.Lerp(card.localRotation, Quaternion.Euler(0, 0, -angle), Time.deltaTime * lerpSpeed);
        }
    }
}
