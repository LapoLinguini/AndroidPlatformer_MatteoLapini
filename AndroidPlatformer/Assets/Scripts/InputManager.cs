using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    //  TOUCH / MOVEMENT ACTIONS  //
    public static Action<Vector3> OnTouchStarted; //touched the screen
    public static Action<Vector3> OnTouchMoved; //player is moving
    public static Action<Vector3> OnTouchEnded; //player released controls

    //  JUMP ACTION  //
    public static Action OnJumpPressed;
    public static Action OnAttackPressed;

    Touch touch;

    Vector2 startTPos;
    Vector2 endTPos;

    List<RaycastResult> raycastResults = new List<RaycastResult>();

    bool buttonPressed = false;

    private void Update()
    {
        if (Input.touchCount <= 0) return;
        if (GameManager.WonOrLost) return;
        touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:

                #region CheckForUIButtonOnTouch

                buttonPressed = false;
                EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) { position = touch.position }, raycastResults);

                if (raycastResults.Count > 0)
                {
                    foreach (var result in raycastResults)
                    {
                        if (result.gameObject.GetComponent<Button>()) { buttonPressed = true; return; };
                    }
                } 

                #endregion

                startTPos = touch.position;
                endTPos = touch.position;
                OnTouchStarted?.Invoke(startTPos);
                break;
            case TouchPhase.Moved:
                if (buttonPressed) return;
                endTPos = touch.position;
                Vector2 directionT = (endTPos - startTPos);
                OnTouchMoved?.Invoke(directionT);
                break;
            case TouchPhase.Ended:
                if (buttonPressed) return;
                directionT = Vector2.zero;
                OnTouchEnded?.Invoke(directionT);
                break;

            default:
                break;
        }
    }
    public void TriggerOnJumpPressed()
    {
        OnJumpPressed?.Invoke();
    }
    public void TriggerOnAttackPressed()
    {
        OnAttackPressed?.Invoke();
    }
}
