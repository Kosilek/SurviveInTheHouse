using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image joystickBackGround;
    [SerializeField] private Image joystick;
    [SerializeField] private Image joystickArea;

    private Vector2 joystickBackGroundStartPosition;

    public Vector2 inputVector;

    [SerializeField] private Color inActivJoystick;
    [SerializeField] private Color activJoystick;

    private bool joystickIsAtive = false;

    private void Start()
    {
        ClickEffect();

        joystickBackGroundStartPosition = joystickBackGround.rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joystickPosition;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackGround.rectTransform, eventData.position, null, out joystickPosition))
        {
            joystickPosition.x = (joystickPosition.x * 2 / joystickBackGround.rectTransform.sizeDelta.x);
            joystickPosition.y = (joystickPosition.y * 2 / joystickBackGround.rectTransform.sizeDelta.y);

            inputVector = new Vector2(joystickPosition.x, joystickPosition.y);

            inputVector = (inputVector.magnitude > 1f) ? inputVector.normalized : inputVector;

            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBackGround.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBackGround.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickEffect();

        Vector2 joystickBackGroundPosition;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickArea.rectTransform, eventData.position, null, out joystickBackGroundPosition))
        {
            joystickBackGround.rectTransform.anchoredPosition = new Vector2(joystickBackGroundPosition.x, joystickBackGroundPosition.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickBackGround.rectTransform.anchoredPosition = joystickBackGroundStartPosition;

        ClickEffect();

        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    private void ClickEffect()
    {
        if (!joystickIsAtive) 
        {
            joystick.color = activJoystick;
            joystickIsAtive = true;
        }
        else
        {
            joystick.color = inActivJoystick;
            joystickIsAtive = false;
        }
    }
}
