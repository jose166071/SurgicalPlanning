using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.UI;  
public class tutorial_script : MonoBehaviour
{
    public Canvas canvas;
    public GameObject player;
    public GameObject cubo;
    public GameObject area;
    public InputActionProperty joystick_push;
    int estado = 0;
    public Image imagen;
    public GameObject continuar;
    public TMP_Text panel;
    public Sprite[] imagenes;
    public InputActionProperty[] inputs;
    private GameObject derecha;
    int a = 0;

    // Start is called before the first frame update
    // void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        bool joystick_value = joystick_push.action.WasPressedThisFrame();
        switch (estado)
        {
            case 0:
                if (joystick_value == true)
                {
                    estado++;
                }
                break;
            case 1:
                panel.text = "Ahora Empecemos con lo básico \n Agarremos objetos";
                if (joystick_value == true)
                {
                    estado++;
                }
                break;
            case 2:
                cubo.SetActive(true);
                //cubo.transform.position = new Vector3(-14.8900003f, 1.01600003f, 0.360000014f);
                if(a == 0)
                {
                    SetupSliceComponent(cubo, 0);
                    a++;
                }
                estado++;
                break;
            case 3:
                panel.text = "Acerca una de tus manos al cubo y presiona:";
                imagen.sprite = imagenes[0];
                if (joystick_value == true)
                {
                    estado++;
                }
                break;
            case 4:
                area.SetActive(true);
                continuar.SetActive(false);
                panel.text = "Ahora, mueve el cubo al área verde!";
                if (cubo.transform.position.y < 0.851f && cubo.transform.position.y > 0.663f)
                {
                    if (cubo.transform.position.x < -13.824f && cubo.transform.position.x > -14.021f)
                    {
                        if (cubo.transform.position.z < -0.34f && cubo.transform.position.z > -0.537)
                        {
                            panel.text = "¡Bien hecho!";
                            estado++;
                        }
                    }
                }
                break;
            case 5:
                panel.text = "¡Muy bien!";
                estado++;
                break;
            case 6:
                continuar.SetActive(true);
                if (a == 1)
                {
                    SetupSliceComponent(cubo, 1);
                    a++;
                }
                panel.text = "Ahora vamos a cambiar el tamaño del cubo!" + "para ello presionemos el botón A o X";
                imagen.sprite = imagenes[1];
                if (joystick_value == true)
                {
                    estado++;
                }
                break;
            case 7:
                panel.text = "También puedes cambiar el tamaño del cubo agarrandolo con ambas manos y estirarlo o achicarlo";
                imagen.sprite = imagenes[0];
                if (joystick_value == true)
                {
                    estado++;
                }
                break;
            case 8:
                imagen.sprite = imagenes[4];
                panel.text = "Bien hecho! Ahora pasemos a los siguiente";
                if (joystick_value == true)
                {
                    estado++;
                }
                break;
            case 9:
                imagen.sprite = imagenes[2];
                if (a == 2)
                {
                    SetupSliceComponent(cubo, 2);
                    a++;
                }
                
                panel.text = "Ahora probemos rotar el objeto. Para ello mueve los joysticks";
                if (joystick_value == true)
                {
                    estado++;
                }
                break;
            case 10:
                imagen.sprite = imagenes[4];
                panel.text = "Por último vamos a tratar de medir. El cubo mide 2cm por cada lado. Tratemos de comprobarlo";
                if (a == 3)
                {
                    SetupSliceComponent(cubo, 3);
                    a++;
                }
                
                if (joystick_value == true)
                {
                    estado++;
                }
                break;
            case 11:
                imagen.sprite = imagenes[3];
                panel.text = "Para medir seleccionaremos 2 puntos en el espacio usando el botón en pantalla\n Presiona 1 vez en cada punto.";
                if (joystick_value == true)
                {
                    estado++;
                }
                break;
            case 12:
                imagen.sprite = imagenes[4];
                panel.text = "¡Bien! Pulsa continuar para emepzar la planficación real";
                if (joystick_value == true)
                {
                    SceneTransitionManager.singleton.GoToSceneAsync(1);
                }
                break;
        }



    }

    public void SetupSliceComponent(GameObject slicedobject, int fase)
    {
        if(fase == 0)
        {
            Rigidbody rb = slicedobject.AddComponent<Rigidbody>();
            rb.freezeRotation = true;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;

            MeshCollider collider = slicedobject.AddComponent<MeshCollider>();
            collider.convex = true;
            /*XRGeneralGrabTransformer grab = slicedobject.AddComponent<XRGeneralGrabTransformer>();*/

            XRGrabInteractable grab = slicedobject.AddComponent<XRGrabInteractable>();
            grab.useDynamicAttach = true;
            grab.selectMode = InteractableSelectMode.Multiple;
            Debug.Log("El cubo ya se puede agarrar");
        }
        else if(fase == 1)
        {
            XRGeneralGrabTransformer grab_Trans = slicedobject.AddComponent<XRGeneralGrabTransformer>();
            grab_Trans.allowTwoHandedScaling = true;
            grab_Trans.minimumScaleRatio = 0.01f;
            grab_Trans.maximumScaleRatio = 10;

            scale scale = slicedobject.AddComponent<scale>();
            scale.A = inputs[0];
            scale.X = inputs[1];

            Debug.Log("El cubo ya se puede escalar");
        }

        else if (fase == 2)
        {
            my_rotate rota = slicedobject.AddComponent<my_rotate>();
            rota.rightjoystick = inputs[2];
            rota.leftjoystick = inputs[3];
            Debug.Log("El cubo ya se puede rotar");
        }
        else if(fase == 3)
        {
            derecha = GameObject.Find("derecha");
            ruler_showe ruler = derecha.GetComponent<ruler_showe>();
            ruler.Anatomia = cubo;
            Debug.Log("El cubo ya se puede medir");

        }
    }
}