using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private PlayerControls m_PlayerControls;

    public delegate void StartTouch(Vector2 position);
    public static event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position);
    public static event EndTouch OnEndTouch;

    public delegate void StartMultiTouch(bool multipress);
    public static event StartMultiTouch OnStartMulti;

    public delegate void EndMultiTouch(bool multipress);
    public static event EndMultiTouch OnEndMulti;


    private void Awake()
    {
        m_PlayerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        m_PlayerControls.Enable();
    }

    private void OnDisable()
    {
        m_PlayerControls.Disable();
    }

    private void Start()
    {
        m_PlayerControls.Press.ScreenPress.started += ctx => StartPress(ctx);
        m_PlayerControls.Press.ScreenPress.canceled += ctx => EndPress(ctx);
    }

    private void StartPress(InputAction.CallbackContext context)
    {
        //Debug.Log("Screen Pressed at " + m_PlayerControls.Press.TouchPosition.ReadValue<Vector2>());
        if (OnStartTouch != null)
        {
            OnStartTouch?.Invoke(m_PlayerControls.Press.TouchPosition.ReadValue<Vector2>());
            
        }
    }

    private void EndPress(InputAction.CallbackContext context)
    {
        //Debug.Log("Touch Ended");
        if (OnEndTouch != null)
        {
            OnEndTouch?.Invoke(m_PlayerControls.Press.TouchPosition.ReadValue<Vector2>());
        }
    }

    private void StartMulti(InputAction.CallbackContext context)
    {
        OnStartMulti?.Invoke(true);
    }

    private void EndMulti(InputAction.CallbackContext context)
    {
        OnEndMulti?.Invoke(false);
    }

}