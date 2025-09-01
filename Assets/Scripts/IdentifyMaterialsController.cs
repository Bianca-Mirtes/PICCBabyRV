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
    private int QuantidadeNecessaria = 14;
    private bool isCorrect;
    private int QuantidadePreenchida = 0;

    public TextMeshProUGUI MostrarQuantidadeNecessaria;
    public TextMeshProUGUI MostrarQuantidadePreenchida;

    private GameObject currentMaterial;
    private List<int> materialsCollected = new List<int>();

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
    public void RemoveMaterial()
    {
        QuantidadePreenchida--;
        MostrarQuantidadePreenchida.text = QuantidadePreenchida.ToString();
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
                Destroy(currentMaterial);
                FindFirstObjectByType<ControllerUTI>().ProcessLavarAsMaos();
                AudioManager.instance.Play("correct_sound");
                QuantidadePreenchida = 0;
                materialsCollected.Clear();
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
                if (materialsCollected.Contains(currentMaterial.layer))
                {
                    result.text = "Material já coletado!";
                    return;
                }
                result.text = "Material Correto!";
                AudioManager.instance.Play("correct_sound");
                materialsCollected.Add(currentMaterial.layer);
                Destroy(currentMaterial);
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