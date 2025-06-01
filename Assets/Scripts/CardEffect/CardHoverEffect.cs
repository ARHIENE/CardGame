using UnityEngine;
using UnityEngine.EventSystems;

public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public bool isHovered = false;

    private float exitCooldown = 0.15f; // 150ms �ȿ��� exit �� ��
    private float exitTimer = 0f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        exitTimer = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // �ٷ� false�� ���� �ʰ� ��� ����
        exitTimer = exitCooldown;
    }

    void Update()
    {
        if (exitTimer > 0f)
        {
            exitTimer -= Time.deltaTime;
            if (exitTimer <= 0f)
                isHovered = false;
        }
    }
}
