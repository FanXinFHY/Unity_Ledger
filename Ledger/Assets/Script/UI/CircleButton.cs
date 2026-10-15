using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CircleButton : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler, IPointerExitHandler
{
    private Image image;
    private Color normalColor;
    private bool isPressed = false;

    //显示
    [Header("半径")]
    [Range(0f,0.5f)]
    public float clickRadius = 0.5f;
    [Header("按下的颜色")]
    public Color pressedColor = new Color(0.7f, 0.7f, 0.7f, 1f);
    public UnityEvent onClick;
    //显示



    void Awake()
    {
        image = GetComponent<Image>();
        if (image != null)
        {
            normalColor = image.color;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //如果按下时不在圆内，直接返回，不变灰
        if (!IsInCircle(eventData)) return;

        isPressed = true;
        if (image != null) image.color = pressedColor; // 变灰
    }
    //松手时触发
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        if (image != null) image.color = normalColor; // 恢复原色
    }
    //按住后移出圆外触发
    public void OnPointerExit(PointerEventData eventData)
    {
        //如果之前是按下的，现在移出去了，也要恢复颜色
        if (isPressed)
        {
            isPressed = false;
            if (image != null) image.color = normalColor;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsInCircle(eventData))
        {
            onClick.Invoke();
        }
    }
    //判断是否在圆内的公共方法
    private bool IsInCircle(PointerEventData eventData)
    {
        if (image == null) return false;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            image.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        float radius = image.rectTransform.rect.width * clickRadius;
        float distance = Vector2.Distance(localPoint, image.rectTransform.rect.center);

        return distance <= radius; 
    }

    public void ControlActive(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);

    }
}
