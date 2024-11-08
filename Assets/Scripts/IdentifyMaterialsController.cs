using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using Unity.VisualScripting;
using System.Net.Sockets;

//Controller para lidar com identificar colisões.
public class IdentifyMaterialsController : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int QuantidadeNecessaria = 13;
    [SerializeField] private int QuantidadeIncorretos = 0;
    [SerializeField] private int QuantidadeCorretos = 0;
    [SerializeField] private int QuantidadePreenchida = 0;

    public TextMeshProUGUI MostrarQuantidadeNecessaria;
    public TextMeshProUGUI MostrarQuantidadePreenchida;
    private List<(GameObject, GameObject)> SocketMaterialCollections = new List<(GameObject, GameObject)>();

     void Start()
     {
          MostrarQuantidadeNecessaria.text = QuantidadeNecessaria.ToSafeString();
     }

     public void IdentifyTouch(XRSocketInteractor socket)
     {
        GameObject currentHook = socket.transform.parent.gameObject;

        IXRSelectInteractable selectInteractable = socket.GetOldestInteractableSelected();
        GameObject currentMaterial = selectInteractable.transform.gameObject;

        SocketMaterialCollections.Add((currentHook, currentMaterial));
        currentMaterial.transform.parent = transform.GetChild(0);

        if (currentMaterial != null)
        {
            if (currentMaterial.tag == "CasoPicc")
            {
                QuantidadeCorretos++;
            }
            else if (currentMaterial.tag == "NaoPicc")
            {
                QuantidadeIncorretos++;
            }
            else
                Debug.LogError("Objeto não pertence aos materiais");
            QuantidadePreenchida++;
            MostrarQuantidadePreenchida.text = QuantidadePreenchida.ToString();
        }
         else
            Debug.LogError("iteractable é null");
     }

     public void RemoveObject(XRSocketInteractor socket)
     {
          (GameObject, GameObject) objFound = new();
          foreach (var obj in SocketMaterialCollections)
          {
               if (obj.Item1.name == socket.transform.parent.gameObject.name)
               {

                    if (obj.Item2.CompareTag("CasoPicc"))
                         QuantidadeCorretos = QuantidadeCorretos - 1;
                    else if (obj.Item2.CompareTag("NaoPicc"))
                         QuantidadeIncorretos = QuantidadeIncorretos - 1;
                    else
                         Debug.LogError("Objeto não pertence aos materiais");

                    objFound = obj;
                    QuantidadePreenchida--;
               }
          }

          bool statusRemove = SocketMaterialCollections.Remove(objFound);

          if (statusRemove)
          {
               Debug.Log("Material Removido com sucesso");
               MostrarQuantidadePreenchida.text = QuantidadePreenchida.ToString();
          }
          else
               Debug.LogError("Error ao remover material");
     }

     /*Quando verificar que todos os items foram preenchidos e estão corretos, entao o enfermeiro pode pegar a caixa de materiais*/
     public void IsRightToAllowGrabOfMaterialTable(TextMeshProUGUI result)
     {
          if (QuantidadePreenchida == QuantidadeNecessaria)
          {
               if (QuantidadeCorretos == QuantidadeNecessaria && QuantidadeIncorretos == 0)
               {
                    result.color = Color.black; 
                    result.text = "Parabéns, siga para a proxima etapa!!!";
                    StateController.Instance.SetState(State.LavarMaos);
                    AudioManager.instance.Play("correct_sound");
                }
               else
               {
                    result.color = Color.red; 
                    result.text = "Há materiais incorretos!";
                    AudioManager.instance.Play("incorrect_sound");
               }
        }
        else
        {
            int corrects = 0;
            int incorrects = 0;
            foreach (var obj in SocketMaterialCollections)
            {
                GameObject objFound = obj.Item2;
                if (objFound.CompareTag("CasoPicc"))
                {
                    corrects++;
                }
                else if (objFound.CompareTag("NaoPicc"))
                {
                    incorrects++;
                }
            }
            if(corrects > 0 && incorrects == 0)
            {
                result.color = Color.green;
                result.text = "Correto!!!";
                AudioManager.instance.Play("correct_sound");
            }else if(incorrects > 0)
            {
                result.color = Color.red;
                result.text = "Há materiais incorretos!!!";
                AudioManager.instance.Play("incorrect_sound");
            }
        }
     }
}