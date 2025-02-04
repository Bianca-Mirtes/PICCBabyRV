using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ClickEventSymptoms : MonoBehaviour
{
    private Button btn;

    public TextMeshProUGUI SymptomTextCanva;

    public Canvas canvaResult; // Canva que mostrará os resultados da escolha de caso picc

    public Canvas canvaSymptom; // Canva do painel de sintomas do bebe

    public Transform formulario;

    private Baby BabySelect = null;

    private readonly string correct_sound_name = "correct_sound";

    private readonly string incorrect_sound_name = "incorrect_sound";


    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (SymptomTextCanva != null)
        {
            GameObject ResultGameObject = canvaResult.gameObject;
            Transform ResultChild = ResultGameObject.transform.Find("Result");
            TextMeshProUGUI TextResult = ResultChild.GetComponent<TextMeshProUGUI>();

            if (TextResult != null && ResultGameObject != null)
            {
                ResultGameObject.SetActive(true);

                TextMeshProUGUI nameBaby = canvaSymptom.transform.Find("Name").GetComponent<TextMeshProUGUI>();

                BabySelect = SymptomCollection.Instance.FindUniqueBaby(nameBaby.text);
  
                if (btn.tag.Equals("Sim"))
                {
                    if (BabySelect.StateCasePicc)
                    {
                        RenderForm();
                        BlockOthersCanvasBaby();
                        FindObjectOfType<ControllerSymptoms>().FindIncubator(BabySelect);
                        StateController.Instance.SetState(State.PrepararCampo);
                        AudioManager.instance.Play(correct_sound_name);
                        TextResult.text = "Isso mesmo! Agora pegue o formulário que estará na mesa e leve para fora, para que a mãe assine!";
                    }
                    else
                    {
                        TextResult.text = "Incorreto! Esse não é um caso PICC!";
                        AudioManager.instance.Play(incorrect_sound_name);
                        FindObjectOfType<ControllerUTI>().GetButtonSelect().enabled = true;
                        Invoke("ResetCase", 7f);
                        FindObjectOfType<ControllerUTI>().FinishProcediment(false);
                    }
                }
                else if (btn.tag.Equals("Nao"))
                {
                    if (!BabySelect.StateCasePicc)
                    {
                        TextResult.text = "Isso mesmo! Esse não é um caso PICC";
                        AudioManager.instance.Play(correct_sound_name);
                        canvaSymptom.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
                        FindObjectOfType<ControllerUTI>().FinishProcediment(true);
                    }
                    else
                    {
                        TextResult.text = "Incorreto! É um caso PICC!";
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

        canvaSymptom.GetComponent<CanvasGroup>().alpha = 1;
        canvaResult.GetComponent<CanvasGroup>().alpha = 0;
        canvaSymptom.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        canvaResult.gameObject.transform.localScale = new Vector3(2.25f, 2.25f, 2.25f);
    }

    /** Renderizar o formulário que irá surgir com as informações do paciente */
    void RenderForm()
    {
        Transform canvaCasePICC = transform.parent;
        Transform childCanvaTransform = formulario.Find("Canvas");

        // take the informations from Canva "CasePICC"
        TextMeshProUGUI babysName = canvaCasePICC.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        Sprite babyIconSprite = canvaCasePICC.transform.Find("IconPatient").GetComponent<Image>().sprite;
        TextMeshProUGUI relatorioText = canvaCasePICC.transform.Find("DescriptionSymptoms").GetComponent<TextMeshProUGUI>();

        Baby baby = SymptomCollection.Instance.FindUniqueBaby(babysName.text);

        // spends the informations from Canva "CasePICC" for the Canva "Formulario".
        TextMeshProUGUI babysNameForm = childCanvaTransform.Find("Patient").GetComponent<TextMeshProUGUI>();
        babysNameForm.text = babysName.text;

        Transform iconForm = childCanvaTransform.Find("Image");
        iconForm.GetComponent<Image>().sprite = babyIconSprite;

        TextMeshProUGUI relatorioForm = childCanvaTransform.Find("Relatorio").GetComponent<TextMeshProUGUI>();
        relatorioForm.text = relatorioText.text;

        TextMeshProUGUI idade = childCanvaTransform.Find("Age").GetComponent<TextMeshProUGUI>();
        idade.text = baby.Age.ToString();

        TextMeshProUGUI responsavel = childCanvaTransform.Find("Responsavel").GetComponent<TextMeshProUGUI>();
        responsavel.text = baby.MotherName;
    }

    void BlockOthersCanvasBaby() {
        BabySelect.ModifyStateProcess(true);
    }

}
