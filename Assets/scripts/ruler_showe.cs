using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class ruler_showe : MonoBehaviour
{
    public Material material;
    public InputActionProperty PinchAnimationAction;
    public InputActionProperty GripAnimationAction;
    public InputActionProperty pinchValue_numerico;
    public Animator handAnimator;
    public GameObject mano;
    public TextMeshPro distancia;
    public GameObject Anatomia;

    public XRRayInteractor ray;

    private float escala;
    LineRenderer line;
    public GameObject esfera;
    //TextMesh Text;
    int contador;

    Vector3 reticlePosition;

    float dist;
    bool wasRendered = false;
    Vector3 pos1;
    Vector3 pos2;
    //GameObject myText;
    //Text text;
    // Start is called before the first frame update
    void Start()
    {
        contador = 1;
    }

    // Update is called once per frame
    void Update()
    {
        bool Pinchvalue= PinchAnimationAction.action.WasPressedThisFrame();
        float pinch_numerico = pinchValue_numerico.action.ReadValue<float>();
        //Debug.Log(pinch_numerico);
        //GripAnimationAction.action.ReadValue<bool>();
        if(pinch_numerico >= 0.1)
        {
            //Debug.Log("Se muestra la esfera");
            Show_esfera(esfera);
        }
        else
        {
            //Debug.Log("Se esconde la esfera");
            hide_Esfera(esfera);
        }
        if (pinch_numerico >= 0.6 && Pinchvalue == true)
        {
            if (contador == 1)
            {
                pos1 = mano.transform.position;
                Debug.Log("Punto 1:"+ pos1);
                ray.TryGetHitInfo(out reticlePosition, out _, out _, out _);
                Debug.Log("Raycast distance" + reticlePosition);
                contador++;
            }
            else if (contador == 2)
            {
                pos2 = mano.transform.position;
                Debug.Log("Punto 2:"+pos2);
                contador = 1;
                dist = Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2) + Mathf.Pow(pos1.z - pos2.z, 2));
                escala = Anatomia.transform.localScale.x;
                ray.TryGetHitInfo(out reticlePosition, out _, out _, out _);
                Debug.Log("Raycast distance" + reticlePosition);
                Debug.Log(dist);
                ShowRuler(pos1, pos2,dist,escala);
            }
        }
    }
    void ShowRuler(Vector3 a, Vector3 b, float dist, float escala) 
    {
        if(wasRendered == false)
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.widthMultiplier = 0.01f;
            var points = new Vector3[2];
            points[0] = a;
            points[1] = b;
            dist = dist * (10) * (1 / escala);
            line.SetPositions(points);
            line.startColor = Color.red;
            line.endColor = Color.red;
            line.material = material;
            //distancia.rectTransform.position = a - b / 2;
            distancia.text = dist.ToString("F2")+"cm";
            wasRendered = true;
        }
        else
        {
            dist = dist *10* (1 / escala);
            var points = new Vector3[2];
            points[0] = a;
            points[1] = b;
            line.SetPositions(points);
            line.startColor = Color.red;
            line.endColor = Color.red;
            line.material = material;
            //distancia.rectTransform.position = a - b / 2;
            //distancia.rectTransform.position = new Vector3(distancia.rectTransform.position.x, distancia.rectTransform.position.y, distancia.rectTransform.position.z - 0.446f);
            distancia.text = dist.ToString("F2") + "cm";
        }

        //if (isRender == false)
        //{
        //    line = gameObject.AddComponent<LineRenderer>();
        //    line.widthMultiplier = 0.01f;
        //    var points = new Vector3[2];
        //    points[0] = a;
        //    points[1] = b;
        //    line.SetPositions(points);
        //}
        //else if (isRender == true)
        //{

        //    //Destroy(line.gameObject);
        //    isRender = false;
        //}
    }
    void Show_esfera(GameObject esfera)
    {
        esfera.SetActive(true);
    }
    void hide_Esfera(GameObject esfera)
    {
        esfera.SetActive(false);
    }
}
