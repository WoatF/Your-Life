using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{   
    public float range;
    Vector2 joystickPosition = Vector2.zero;
    void Start()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(Camera.main,background.position);;
    }

    public override void OnDrag(PointerEventData eventData)
    {   
        Vector2 direction = eventData.position - joystickPosition;
        Debug.Log(direction.magnitude);
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {   
        Movement Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();

        range = Player.range;
        
        Vector3 pos = Player.transform.position - new Vector3(Horizontal,Vertical)*range;

        Player.GetComponent<Movement>().StartHook(pos);

        inputVector = Vector2.zero;

        handle.anchoredPosition = Vector2.zero;

    }
    
}