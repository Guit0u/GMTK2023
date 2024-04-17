using UnityEngine;
using UnityEngine.UI;

public class Erasable : MonoBehaviour
{
    [SerializeField] private float removalSpeed = 1f;
    [SerializeField] private float mouseMaxBonus = 1f;
    [SerializeField] private float mouseSpeedBonus = 2f;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private EraseCount bloodCount;

    [SerializeField] private Slider slider;

    private float opacity = 1f;

    private Vector2 lastMousePosition;
    private Vector2 screenSize;

    private void OnMouseDrag()
    {
        if (opacity == 1f)
        {
            screenSize = new Vector2(Screen.width, Screen.height);
            lastMousePosition = MouseScreenPosition();
        }
        float mouseBonus = MouseSpeedBonus(lastMousePosition);

        opacity -= (removalSpeed + mouseBonus) * Time.deltaTime;
        sprite.color = new Color(1, 1, 1, opacity);

        if (opacity < 0)
        {
            opacity = 0;
            bloodCount.RemoveBloodstain();

            Destroy(gameObject);
        }
    }

    private float MouseSpeedBonus(Vector2 lastMousePos)
    {
        Vector2 mousePos = MouseScreenPosition();
        float mouseBonus = Mathf.Clamp01((mousePos - lastMousePos).magnitude * mouseSpeedBonus) * mouseMaxBonus;
        
        slider.maxValue = mouseMaxBonus;
        slider.value = mouseBonus;
        
        return mouseBonus;
    }

    private Vector2 MouseScreenPosition()
    {
        float x = (screenSize.x - Input.mousePosition.x) / screenSize.x;
        float y = (screenSize.y - Input.mousePosition.y) / screenSize.y;

        return new Vector2(x, y);
    }
}
