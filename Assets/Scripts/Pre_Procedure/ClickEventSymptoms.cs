using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using BabyData;

public class ClickEventSymptoms : MonoBehaviour
{
    private Button btn;

    public Canvas canvaResult; // Canva que mostrará os resultados da escolha de caso picc

    public Transform formulario;

    private BabyCase BabySelect;

    private Canvas canvaSymptom;

    public Transform objectsForm;

    private readonly string correct_sound_name = "correct_sound";

    private readonly string incorrect_sound_name = "incorrect_sound";


    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        canvaSymptom = transform.parent.GetComponent<Canvas>();
    }

    void TaskOnClick()
    {
        if (canvaSymptom != null)
        {
            GameObject ResultGameObject = canvaResult.gameObject;
            Transform ResultChild = ResultGameObject.transform.Find("Result");
            TextMeshProUGUI TextResult = ResultChild.GetComponent<TextMeshProUGUI>();

            if (TextResult != null && ResultGameObject != null)
            {
                ResultGameObject.SetActive(true);

                TextMeshProUGUI nameBaby = canvaSymptom.transform.Find("BabyName").GetComponent<TextMeshProUGUI>();
                
                string name = nameBaby.text.Split(":")[1];
                BabySelect = SymptomCollection.Instance.FindUniqueBaby(name.Trim());
  
                if (btn.tag.Equals("Sim"))
                {
                    if (BabySelect.baby.isCasePicc)
                    {
                        RenderForm();
                        BlockOthersCanvasBaby();
                        FindObjectOfType<ControllerSymptoms>().FindIncubator(BabySelect.baby);
                        StateController.Instance.SetState(State.ColetarAutorização);
                        AudioManager.instance.Play(correct_sound_name);
                        TextResult.text = "Isso mesmo! " + BabySelect.justification +
                            "\n\nAgora pegue o formulário que está na bancada e leve para fora, para que a mãe assine!";

                    }
                    else
                    {
                        TextResult.text = "Incorreto! " + BabySelect.justification;
                        AudioManager.instance.Play(incorrect_sound_name);
                        FindObjectOfType<ControllerUTI>().GetButtonSelect().enabled = true;
                        Invoke("ResetCase", 7f);
                        FindObjectOfType<ControllerUTI>().FinishProcediment(false);
                    }
                }
                else if (btn.tag.Equals("Nao"))
                {
                    if (!BabySelect.baby.isCasePicc)
                    {
                        TextResult.text = "Isso mesmo! " + BabySelect.justification;
                        AudioManager.instance.Play(correct_sound_name);
                        canvaSymptom.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
                        FindObjectOfType<ControllerUTI>().FinishProcediment(true);
                    }
                    else
                    {
                        TextResult.text = "Incorreto! " + BabySelect.justification;
                        AudioManager.instance.Play(incorrect_sound_name);
                        FindObjectOfType<ControllerUTI>().GetButtonSelect().enabled = true;
                        Invoke("ResetCase", 7f);
                        FindObjectOfType<ControllerUTI>().FinishProcediment(false);
                    }
                }
            }
        }
    }

    private void ResetCase() // for to give the chance from user try again
    {
        canvaSymptom.GetComponent<Animator>().SetBool("isFadeOut", false);
        canvaSymptom.GetComponent<Animator>().SetBool("isFadeIn", false);

        canvaResult.GetComponent<CanvasGroup>().alpha = 0;
        canvaSymptom.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        canvaResult.gameObject.transform.localScale = new Vector3(2.25f, 2.25f, 2.25f);
    }

    /** Renderizar o formulário que irá surgir com as informações do paciente */
    void RenderForm()
    {
        Transform childCanvaTransform = formulario.Find("Canvas");

        // take the informations from Canva "CasePICC"
        TextMeshProUGUI babysName = canvaSymptom.transform.Find("BabyName").GetComponent<TextMeshProUGUI>();
        Sprite babyIconSprite = canvaSymptom.transform.Find("Header").Find("IconPatient").GetComponent<Image>().sprite;
        TextMeshProUGUI medicalHis = canvaSymptom.transform.Find("History").GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI currentState = canvaSymptom.transform.Find("CurrentState").GetChild(0).GetComponent<TextMeshProUGUI>();

        string name = babysName.text.Split(":")[1];
        BabyCase baby = SymptomCollection.Instance.FindUniqueBaby(name.Trim());

        // spends the informations from Canva "CasePICC" for the Canva "Formulario".
        TextMeshProUGUI babysNameForm = childCanvaTransform.Find("Patient").GetComponent<TextMeshProUGUI>();
        babysNameForm.text = name.Trim();

        Transform iconForm = childCanvaTransform.Find("Image");
        iconForm.GetComponent<Image>().sprite = babyIconSprite;

        TextMeshProUGUI relatorioForm = childCanvaTransform.Find("Relatorio").GetComponent<TextMeshProUGUI>();
        relatorioForm.text = medicalHis.text;
        relatorioForm.text += currentState.text;
        relatorioForm.text += baby.generalDiagnosis;
        relatorioForm.text +=  baby.justification;

        TextMeshProUGUI idade = childCanvaTransform.Find("Age").GetComponent<TextMeshProUGUI>();
        idade.text = baby.baby.age.ToString();

        TextMeshProUGUI responsavel = childCanvaTransform.Find("Responsavel").GetComponent<TextMeshProUGUI>();
        responsavel.text = baby.baby.motherName;

        formulario.gameObject.SetActive(true);
        objectsForm.GetChild(1).gameObject.SetActive(true); // Ativa a seta sinalizadora do formulário
        objectsForm.GetChild(0).gameObject.SetActive(true);
        FindFirstObjectByType<MotherController>().gameObject.transform.GetChild(4).gameObject.SetActive(true);
    }

    void BlockOthersCanvasBaby() {
        BabySelect.baby.ModifyStateProcess(true);
    }

}
