using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using Unity.VisualScripting;

//Controller para lidar com identificar colisões.
public class IdentifyMaterialsController : MonoBehaviour
{

     public int QuantidadeNecessaria = 0;

     public TextMeshProUGUI MostrarQuantidadeNecessaria;

     public int QuantidadePreenchida = 0;

     public TextMeshProUGUI MostrarQuantidadePreenchida;

     private int QuantidadeIncorretos = 0;

     private int QuantidadeCorretos = 0;

     private List<(GameObject, GameObject)> SocketMaterialCollections = new List<(GameObject, GameObject)>();


     void Start()
     {
          MostrarQuantidadeNecessaria.text = QuantidadeNecessaria.ToSafeString();
     }

     public void IdentifyTouch(XRSocketInteractor socket)
     {

          GameObject currentHook = socket.transform.parent.gameObject;
          GameObject currentMaterial = socket.selectTarget.gameObject;

          SocketMaterialCollections.Add((currentHook, currentMaterial));

          if (socket.selectTarget.gameObject != null)
          {
               Debug.Log("name: " + currentMaterial.name);

               if (currentMaterial.tag == "CasoPicc")
               {
                    QuantidadeCorretos++;
               }
               else if (currentMaterial.tag == "NaoPicc")
               {
                    QuantidadeIncorretos++;
               }
               else
               {
                    Debug.LogError("Objeto não pertence aos materiais");
               }

               QuantidadePreenchida++;
               MostrarQuantidadePreenchida.text = QuantidadePreenchida.ToString();
          }
          else
          {
               Debug.LogError("iteractable é null");
          }
     }

     public void RemoveObject(XRSocketInteractor socket)
     {

          (GameObject, GameObject) objFound = new();

          foreach (var obj in SocketMaterialCollections)
          {

               if (obj.Item1.name == socket.transform.parent.gameObject.name)
               {

                    if (obj.Item2.tag == "CasoPicc")
                    {
                         QuantidadeCorretos = QuantidadeCorretos - 1;
                    }
                    else if (obj.Item2.tag == "NaoPicc")
                    {
                         QuantidadeIncorretos = QuantidadeIncorretos - 1;
                    }
                    else
                    {
                         Debug.LogError("Objeto não pertence aos materiais");
                    }

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
          {
               Debug.LogError("Error ao remover material");
          }
     }

     /*Quando verificar que todos os items foram preenchidos e estão corretos, entao o enfermeiro pode pegar a caixa de materiais*/
     public void IsRightToAllowGrabOfMaterialTable(TextMeshProUGUI result)
     {

          if (QuantidadeNecessaria <= QuantidadePreenchida)
          {

               if (QuantidadeCorretos == QuantidadeNecessaria && QuantidadeIncorretos == 0)
               {
                    Debug.Log("Ativar XRGrab");
                    result.color = UnityEngine.Color.black; 
                    result.text = "Correto!";
                    StateController.Instance.SetState(State.LavarMaos);
                    AudioManager.instance.Play("correct_sound");
                }
               else
               {
                    Debug.Log("Algum material incorreto");
                    result.color = UnityEngine.Color.red; 
                    result.text = "Há materiais incorretos!";
                    AudioManager.instance.Play("incorrect_sound");
               }
          }
          else
          {
               Debug.Log("O inventário ainda não está cheio");
               result.color = UnityEngine.Color.red; 
               result.text = "Quantidade insuficiente";
               AudioManager.instance.Play("incorrect_sound");
            }

     }

}