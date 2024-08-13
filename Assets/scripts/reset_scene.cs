using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class reset_scene : MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;
    Vector3 scale;
    public GameObject anatomia;
    public InputActionProperty B;
    private void Start()
    {
        position = anatomia.transform.position;
        rotation = anatomia.transform.rotation;
        //scale = anatomia.transform.lossyScale;
        Debug.Log($"posicion={position},rotacion={rotation},scale={scale}");
    }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        bool RightSecondaryButton = B.action.WasPressedThisFrame();
        if (RightSecondaryButton == true)
        {
            ResetGame();
        }
    }
    public void ResetGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //anatomia.transform.position = new Vector3(position.x,position.y,position.z);
        //anatomia.transform.SetPositionAndRotation(new Vector3(-9.435f, -0.089f, 0.6f), rotation);
        anatomia.transform.SetPositionAndRotation(position, rotation);
        //anatomia.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f); 
        print("Se reinicio el juego");

    }
}
