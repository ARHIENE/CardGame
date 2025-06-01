using UnityEngine;
using UnityEngine.EventSystems;

public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public bool isHovered = false;

    private float exitCooldown = 0.15f; // 150ms 안에는 exit 안 됨
    private float exitTimer = 0f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        exitTimer = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 바로 false로 하지 않고 잠깐 유지
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
