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
    [Header("Attributes")]
    [SerializeField] private int QuantidadeNecessaria = 1;
    [SerializeField] private bool isCorrect;
    [SerializeField] private int QuantidadePreenchida = 0;

    public TextMeshProUGUI MostrarQuantidadeNecessaria;
    public TextMeshProUGUI MostrarQuantidadePreenchida;

    private GameObject currentMaterial;

     void Start()
     {
          MostrarQuantidadeNecessaria.text = QuantidadeNecessaria.ToSafeString();
     }

     public void IdentifyTouch(XRSocketInteractor socket)
     {
        GameObject currentHook = socket.transform.parent.gameObject;

        IXRSelectInteractable selectInteractable = socket.GetOldestInteractableSelected();
        GameObject material = selectInteractable.transform.gameObject;

        currentMaterial = material;

        if (currentMaterial != null)
        {
            if (currentMaterial.tag == "CasoPicc")
            {
                isCorrect = true;
            }
            else if (currentMaterial.tag == "NaoPicc")
            {
                isCorrect = false;
            }
            else
                Debug.LogError("Objeto não pertence aos materiais");
            QuantidadePreenchida++;
            MostrarQuantidadePreenchida.text = QuantidadePreenchida.ToString();
        }
         else
            Debug.LogError("iteractable é null");
     }

     /*Quando verificar que todos os items foram preenchidos e estão corretos, entao o enfermeiro pode pegar a caixa de materiais*/
     public void IsRightToAllowGrabOfMaterialTable(TextMeshProUGUI result)
     {
        if (QuantidadePreenchida == QuantidadeNecessaria)
        {
            if (isCorrect)
            {
                result.color = Color.white;
                result.text = "Parabéns, siga para a proxima etapa!";
                StateController.Instance.SetState(State.LavarMaos);
                FindFirstObjectByType<ControllerUTI>().ProcessLavarAsMaos();
                AudioManager.instance.Play("correct_sound");
            }
            else
            {
                result.color = Color.red;
                result.text = "Material incorreto!";
                QuantidadePreenchida--;
                Destroy(currentMaterial);
                AudioManager.instance.Play("incorrect_sound");
            }
        }
        else
        {

            if (isCorrect)
            {
                result.color = Color.green;
                result.text = "Material Correto!";
                AudioManager.instance.Play("correct_sound");
            }
            else
            {
                result.color = Color.red;
                result.text = "Material incorreto!";
                QuantidadePreenchida--;
                Destroy(currentMaterial);
                AudioManager.instance.Play("incorrect_sound");
            }
        }
        MostrarQuantidadePreenchida.text = QuantidadePreenchida.ToString();
    }
}