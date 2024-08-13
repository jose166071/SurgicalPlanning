using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class scale : MonoBehaviour
{
    public InputActionProperty A;
    public InputActionProperty B;
    public InputActionProperty X;
    public InputActionProperty Y;
    Vector3 escala;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        escala = gameObject.transform.localScale;
        bool RightPrimaryButton = A.action.IsPressed();
        bool RightSecondaryButton = B.action.IsPressed();
        bool LeftPrimaryButton = X.action.IsPressed();
        bool LeftSecondaryyButton = Y.action.IsPressed();
        if (RightPrimaryButton == true)
        {
            escala = escala + new Vector3(0.001f, 0.001f, 0.001f);
            gameObject.transform.localScale = escala;
        }
        else if (LeftPrimaryButton == true)
        {
            escala -= new Vector3(0.001f, 0.001f, 0.001f);
            gameObject.transform.localScale = escala;
        }
    }
}
