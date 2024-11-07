using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerUTI : MonoBehaviour
{
    [Header("Main Objects of the Process")]
    public GameObject form;
    public GameObject tabletCountMaterial;
    public GameObject mayosTable;
    public GameObject mayosTablePICC;   
    [SerializeField] private GameObject Uniforme;
    public GameObject faucet;
    public GameObject faucetTwo;
    public Material luvaMaterial;

    [Header("Buttons")]
    private Button buttonSelect;
    public List<Button> buttons;

    [SerializeField]
    private GameObject pointFinallyOfMesaMayo;

    [SerializeField]
    private List<XRSimpleInteractable> interactablesBabys;

    [Header("Teleportations")]
    [SerializeField] private List<GameObject> teleportationBabys;
    [SerializeField] private List<GameObject> teleportationMae;
    [SerializeField] private List<GameObject> teleportationLavarMaos;
    [SerializeField] private List<GameObject> teleportationUniforme;
    [SerializeField] private List<GameObject> teleportationRecolherMateriais;
    [SerializeField] private List<GameObject> teleportationToForm;

    public Transform finishProcessCanvas;
    private Transform currentIncubator;

    [Header("Setas")]
    [SerializeField] private List<GameObject> setasBaby;
    [SerializeField] private List<GameObject> setasLavarMaos;


    private void Start()
    {
        form.transform.Find("seta").gameObject.SetActive(true);
        AudioManager.instance.Play("background");
        Uniforme.SetActive(false);
        mayosTablePICC.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonSelect != null)
            foreach (var button in buttons)
                button.enabled = false;
        VerifyProcessStateNow();
    }

    public void VerifyProcessStateNow()
    {
        if (StateController.Instance.CompareStates(State.VerificarCasoPicc))
            ProcessCasePicc();
        else
        if (StateController.Instance.CompareStates(State.EntregarFormulario))
            ProcessEntregarFormulario();
        else
        if (StateController.Instance.CompareStates(State.RecolherMateriais))
            ProcessRecolherMateriais();
        else
        if (StateController.Instance.CompareStates(State.LavarMaos))
            ProcessLavarAsMaos();
        else
        if (StateController.Instance.CompareStates(State.PrepararUniforme))
            ProcessOrganizarUniforme();
        else
        if (StateController.Instance.CompareStates(State.ProcedimentoPICC))
            ProcessProcedimentoPICC();
    }

    public void StartProcediment(Button btn)
    {
        buttonSelect = btn;
    }

    public void FinishProcediment(bool itIsRight)
    {
        foreach (var button in buttons)
            if (button != buttonSelect)
                button.enabled = true;

        if (itIsRight)
            buttons.Remove(buttonSelect);
        buttonSelect = null;
    }

    public Button GetButtonSelect()
    {
        return buttonSelect;
    }

    public void SetCurrentIncubator(Transform incubator)
    {
        currentIncubator = incubator;
    }

    public void ToStateDevolverFormulario()
    {
        StateController.Instance.SetState(State.DevolverFormulario);
    }

    public void ToStateRecolherMateriais(ParticleSystem sucess)
    {
        if (StateController.Instance.CompareStates(State.DevolverFormulario))
        {
            sucess.Play();
            StateController.Instance.SetState(State.RecolherMateriais);
            finishProcessCanvas.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Parabéns! Você concluiu a etapa de assinatura,\r\nagora prepare-se para a etapa de Coleta dos Materiais!";
            finishProcessCanvas.GetComponent<FadeController>().FadeInForFadeOut(5f);
        }
    }

    public void ProcessCasePicc()
    {
        form.SetActive(false);
        tabletCountMaterial.SetActive(false);
        mayosTable.SetActive(false);
        faucet.SetActive(false);
        faucetTwo.SetActive(false);

        for (int i = 0; i < teleportationToForm.Count; i++)
            teleportationToForm[i].SetActive(false);

        for (int i = 0; i < teleportationMae.Count; i++)
            teleportationMae[i].SetActive(false);
    }

    public void ProcessEntregarFormulario()
    {
        if (teleportationBabys.Count == setasBaby.Count && setasBaby.Count == interactablesBabys.Count)
        {
            for (int i = 0; i < teleportationBabys.Count; i++)
            {
                setasBaby[i].SetActive(false);
                teleportationBabys[i].SetActive(false);
                interactablesBabys[i].enabled = false;
            }
        }
        else
            Debug.LogError("É preciso que a quantidade de incubadoras seja válido com outras variáveis");

        form.SetActive(true);
 
        for (int i = 0; i < teleportationToForm.Count; i++)
            teleportationToForm[i].SetActive(true);

        for (int i = 0; i < teleportationMae.Count; i++)
            teleportationMae[i].SetActive(true);
    }

    public void ProcessRecolherMateriais()
    {
        form.SetActive(false);
        tabletCountMaterial.SetActive(true);
        mayosTable.SetActive(true);
        for (int i = 0; i < teleportationToForm.Count; i++)
            teleportationToForm[i].SetActive(false);
        
        for (int i = 0; i < teleportationMae.Count; i++)
            teleportationMae[i].SetActive(false);
        
        for (int i = 0; i < teleportationRecolherMateriais.Count; i++)
            teleportationRecolherMateriais[i].SetActive(true);
    }


    public void ProcessLavarAsMaos()
    {
        for (int i = 0; i < teleportationRecolherMateriais.Count; i++)
            teleportationRecolherMateriais[i].SetActive(false);

        for (int i = 0; i < teleportationLavarMaos.Count; i++)
        {
            teleportationLavarMaos[i].SetActive(true);
            setasLavarMaos[i].SetActive(true);
        }

        faucet.SetActive(true);
        faucetTwo.SetActive(true);
        ParticleSystem particleSystem = mayosTable.transform.Find("Confetti").GetComponent<ParticleSystem>();
        particleSystem.Play();
        Invoke("HideMayoTable", 5f);
    }

    public void ProcessOrganizarUniforme()
    {
        faucet.SetActive(false);
        faucetTwo.SetActive(false);

        for (int i = 0; i < teleportationLavarMaos.Count; i++)
            teleportationLavarMaos[i].SetActive(false);

        for (int i = 0; i < teleportationUniforme.Count; i++)
            teleportationUniforme[i].SetActive(true);
        Uniforme.SetActive(true);
    }

    private void HideMayoTable()
    {
       mayosTable.SetActive(false);
       tabletCountMaterial.SetActive(false);
    }

    public void ClickUniforme()
    {
        Canvas canvasUniform =  Uniforme.transform.Find("Canvas").GetComponent<Canvas>();
        Uniforme.transform.Find("seta").gameObject.SetActive(false);
        Uniforme.transform.Find("uniforme").gameObject.SetActive(false);
        canvasUniform.transform.Find("Button").gameObject.SetActive(false);

        finishProcessCanvas.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Parabéns! Agora você está pronto para realizar o procedimento cirúrgico...";

        Transform leftHand = GameObject.FindWithTag("LeftHand").transform;
        Transform rightHand = GameObject.FindWithTag("RightHand").transform;

        if(leftHand != null && rightHand != null)
        {
            SkinnedMeshRenderer leftHandMesh = leftHand.Find("LeftHand").GetChild(1).GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer rightHandMesh = rightHand.Find("RightHand").GetChild(1).GetComponent<SkinnedMeshRenderer>();
            if(leftHandMesh != null && rightHandMesh != null)
            {
                leftHandMesh.material = luvaMaterial;
                rightHandMesh.material = luvaMaterial;
            }
        }
        else
        {
            Debug.Log("Não achou as mãos");
        }
        GameObject.FindWithTag("MainCamera").transform.GetChild(2).gameObject.SetActive(true); // gorro
        GameObject.FindWithTag("MainCamera").transform.GetChild(3).gameObject.SetActive(true); // mascara

    }

    public void ProcessProcedimentoPICC()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        if(player != null)
        {
            player.position = currentIncubator.GetChild(1).position;
        }

        mayosTablePICC.SetActive(true);
    }
}
