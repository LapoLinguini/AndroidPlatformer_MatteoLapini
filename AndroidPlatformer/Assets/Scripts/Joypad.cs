using UnityEngine;

public class Joypad : MonoBehaviour
{
    [Header("Joypad:")]
    [SerializeField] float _joypadClamp = 75;
    [Space]
    [SerializeField] GameObject JoypadUI;
    [SerializeField] GameObject JoypadCenter;
    private void Start()
    {
        JoypadUI.SetActive(false);
    }
    private void OnEnable()
    {
        InputManager.OnTouchStarted += OnTouchStarted;
        InputManager.OnTouchMoved += OnTouchMoved;
        InputManager.OnTouchEnded += OnTouchEnded;
    }
    private void OnDisable()
    {
        InputManager.OnTouchStarted -= OnTouchStarted;
        InputManager.OnTouchMoved -= OnTouchMoved;
        InputManager.OnTouchEnded -= OnTouchEnded;
    }
    void OnTouchStarted(Vector3 _touchPos)
    {
        transform.position = _touchPos;
        JoypadCenter.transform.localPosition = Vector3.zero;
        JoypadUI.SetActive(true);
    }
    void OnTouchMoved(Vector3 _pos)
    {
        Vector2 joypadPos = transform.position + _pos;
        JoypadCenter.transform.position = joypadPos;
        JoypadCenter.transform.localPosition = Vector2.ClampMagnitude(JoypadCenter.transform.localPosition, _joypadClamp);
    }
    void OnTouchEnded(Vector3 _pos)
    {
        transform.position = _pos;
        JoypadUI.SetActive(false);
    }
}
