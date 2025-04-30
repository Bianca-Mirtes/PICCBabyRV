using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerUTI : MonoBehaviour
{
    [Header("Main Objects of the Process")]
    public GameObject form;
    public GameObject tabletCountMaterial;
    public GameObject mayosTable;
    [SerializeField] private GameObject Uniforme;
    public GameObject faucet;
    public GameObject faucetTwo;
    public Material luvaMaterial;
    public Material handMaterial;
    public GameObject luvas;

    [Header("Buttons")]
    private UnityEngine.UI.Button buttonSelect;
    public List<UnityEngine.UI.Button> buttons;

    [SerializeField]
    private GameObject pointFinallyOfMesaMayo;

    [Header("Objetos Temporarios")]
    private ParticleSystem tempConfetti;
    private GameObject tempMaterial;
    private int countSurgicalFields = 0;
    private GameObject currentSlider;
    private GameObject currentPoints;

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

    public List<GameObject> mayosTablePICC;
    public GameObject currentMayosTablePICC;

    [Header("Setas")]
    [SerializeField] private List<GameObject> setasBaby;
    [SerializeField] private List<GameObject> setasLavarMaos;

    private void Start()
    {
        form.transform.Find("seta").gameObject.SetActive(true);
        AudioManager.instance.Play("background");
        Uniforme.SetActive(false);
        foreach(GameObject obj in mayosTablePICC)
            obj.SetActive(false);
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
        if (StateController.Instance.CompareStates(State.PrepararCampo))
            ProcessPrepararCampo();
        else
        if (StateController.Instance.CompareStates(State.RealizarAntissepsia))
            ProcessRealizarAntissepsia();
        else
        if (StateController.Instance.CompareStates(State.RealizarTesteDePermeabilidade))
            ProcessRealizarTesteDePermeabilidade();
        else
        if (StateController.Instance.CompareStates(State.FecharSistema))
            ProcessFecharSistema();
        else
        if (StateController.Instance.CompareStates(State.DescartarMateriais))
            ProcessDescartarMateriais();
    }

    public void GetCurrentMaterial(GameObject material)
    {
        tempMaterial = material;
    }

    public void GetConfetti(ParticleSystem confetti)
    {
        tempConfetti = confetti;
    }

    public void GetCurrentMayosTablePICC(GameObject mayosTablePICC)
    {
        currentMayosTablePICC = mayosTablePICC;
        currentMayosTablePICC.SetActive(true);
    }

    public void StartProcediment(UnityEngine.UI.Button btn)
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

    public UnityEngine.UI.Button GetButtonSelect()
    {
        return buttonSelect;
    }

    public void SetCurrentIncubator(Transform incubator)
    {
        currentIncubator = incubator;
        currentIncubator.GetChild(2).gameObject.SetActive(false);
        currentIncubator.GetChild(3).gameObject.SetActive(false);
        currentIncubator.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
    }

    public void SetRegion(bool isSuperiorMembers)
    {
        if (isSuperiorMembers)
        {
            currentSlider = currentIncubator.GetChild(0).GetChild(0).GetChild(0).gameObject;
            currentPoints = currentIncubator.GetChild(0).GetChild(1).GetChild(1).gameObject;

            Destroy(currentIncubator.GetChild(0).GetChild(0).GetChild(1).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(1).GetChild(2).gameObject);

            currentIncubator.GetChild(0).GetChild(2).gameObject.SetActive(true);
            currentIncubator.GetChild(0).GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            currentSlider = currentIncubator.GetChild(0).GetChild(0).GetChild(1).gameObject;
            currentPoints = currentIncubator.GetChild(0).GetChild(1).GetChild(2).gameObject;

            Destroy(currentIncubator.GetChild(0).GetChild(0).GetChild(0).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(1).GetChild(1).gameObject);

            currentIncubator.GetChild(0).GetChild(2).gameObject.SetActive(true);
            currentIncubator.GetChild(0).GetChild(4).gameObject.SetActive(true);
        }
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
            StateController.Instance.SetState(State.RecolherMateriais);
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

    public void SetMinigameAntissepsia()
    {
        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Se aproxime do paciente e aplique a clorexidina no local da inserção para realizar a antissepsia";
        currentSlider.SetActive(true);
        currentPoints.SetActive(true);
        tempConfetti.Play();
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
            if(leftHand.name.Equals("LeftHand") && rightHand.name.Equals("RightHand"))
            {
                leftHand.GetComponent<SkinnedMeshRenderer>().material = luvaMaterial;
                rightHand.GetComponent<SkinnedMeshRenderer>().material = luvaMaterial;
            }
            else
            {
                SkinnedMeshRenderer leftHandMesh = leftHand.Find("LeftHand").GetChild(1).GetComponent<SkinnedMeshRenderer>();
                SkinnedMeshRenderer rightHandMesh = rightHand.Find("RightHand").GetChild(1).GetComponent<SkinnedMeshRenderer>();
                if (leftHandMesh != null && rightHandMesh != null)
                {
                    leftHandMesh.material = luvaMaterial;
                    rightHandMesh.material = luvaMaterial;
                }
            }
        }
        else
        {
            Debug.Log("Não achou as mãos");
        }
        GameObject.FindWithTag("MainCamera").transform.GetChild(2).gameObject.SetActive(true); // gorro
        GameObject.FindWithTag("MainCamera").transform.GetChild(3).gameObject.SetActive(true); // mascara
        StateController.Instance.SetState(State.MensurarCateter);
    }

    public void verifSocketsSurgicalField(XRSocketInteractor socket)
    {
        IXRSelectInteractable selectInteractable = socket.GetOldestInteractableSelected();
        GameObject currentMaterial = selectInteractable.transform.gameObject;

        if(currentMaterial != null)
            Destroy(currentMaterial);

        countSurgicalFields++;
    }

    public void VerifUpdateSlider(UnityEngine.UI.Slider slider)
    {
        if (slider.value == 1)
        {
            Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
            table.GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Prepare o Campo Cirúrgico: Utilize os panos estereis";
            table.GetChild(0).GetChild(3).gameObject.SetActive(true);
            table.GetChild(0).GetChild(2).gameObject.SetActive(false);
            currentSlider.SetActive(false);
            tempMaterial.SetActive(true);
            tempConfetti.Play();
            StateController.Instance.SetState(State.PrepararCampo);
        }
    }

    public void ProcessMensurarCateter()
    {
        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Faça a mensuração do tamanho do Cateter:";
        table.GetChild(0).GetChild(1).gameObject.SetActive(false);
        table.GetChild(0).GetChild(2).gameObject.SetActive(true);
        currentSlider.SetActive(true);
    }

    private void ProcessPrepararCampo()
    {
       if(countSurgicalFields == 2)
        {
            Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
            table.GetChild(0).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Lubrificação do Cateter intravenoso:";
            table.GetChild(0).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Pegue a Seringa e encha-a com soro fisiologico";
            table.GetChild(0).GetChild(3).gameObject.SetActive(false);
            table.GetChild(0).GetChild(4).GetChild(1).gameObject.SetActive(true);
            table.GetChild(0).GetChild(4).gameObject.SetActive(true);
            tempMaterial.SetActive(true);
            tempConfetti.Play();
            StateController.Instance.SetState(State.LubrificarCateter);
        }
    }

    public void ProcessLubrificarCateter()
    {
        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Preparação do Cateter:";
        table.GetChild(0).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = " Pegue a tesoura de Mayo reta e realize o corte do cateter no comprimento certo";
        tempMaterial.SetActive(true);
        tempConfetti.Play();
        currentSlider = currentIncubator.GetChild(0).GetChild(1).GetChild(0).gameObject;
        StateController.Instance.SetState(State.PrepararCateter);
    }

    public void ProcessPrepararCateter()
    {
        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Antissepsia Local:";
        table.GetChild(0).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = " Pegue a pinça e encaixe um pedaço de compressa esteril na ponta";
        tempMaterial.SetActive(true);
        tempConfetti.Play();
        currentSlider = currentIncubator.GetChild(0).GetChild(1).GetChild(0).gameObject;
        StateController.Instance.SetState(State.RealizarAntissepsia);
    }

    private void ProcessRealizarAntissepsia()
    {
        if(currentSlider.GetComponent<UnityEngine.UI.Slider>().value == 1)
        {
            Transform leftHand = GameObject.FindWithTag("LeftHand").transform;
            Transform rightHand = GameObject.FindWithTag("RightHand").transform;
            if (leftHand != null && rightHand != null)
            {
                if (leftHand.name.Equals("LeftHand") && rightHand.name.Equals("RightHand"))
                {
                    leftHand.GetComponent<SkinnedMeshRenderer>().material = luvaMaterial;
                    rightHand.GetComponent<SkinnedMeshRenderer>().material = luvaMaterial;
                }
                else
                {
                    SkinnedMeshRenderer leftHandMesh = leftHand.Find("LeftHand").GetChild(1).GetComponent<SkinnedMeshRenderer>();
                    SkinnedMeshRenderer rightHandMesh = rightHand.Find("RightHand").GetChild(1).GetComponent<SkinnedMeshRenderer>();
                    if (leftHandMesh != null && rightHandMesh != null)
                    {
                        leftHandMesh.material = handMaterial;
                        rightHandMesh.material = handMaterial;
                    }
                }

            }
            Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
            table.GetChild(0).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Realize a troca de luvas:";
            table.GetChild(0).GetChild(4).gameObject.SetActive(false);
            table.GetChild(0).GetChild(5).gameObject.SetActive(true);
            tempMaterial.SetActive(false);
            tempMaterial = luvas;
            tempMaterial.SetActive(true);
            tempConfetti.Play();
        }
    }

    public void ProcessTrocarLuvas()
    {
        Transform leftHand = GameObject.FindWithTag("LeftHand").transform;
        Transform rightHand = GameObject.FindWithTag("RightHand").transform;
        if (leftHand != null && rightHand != null)
        {
            if (leftHand.name.Equals("LeftHand") && rightHand.name.Equals("RightHand"))
            {
                leftHand.GetComponent<SkinnedMeshRenderer>().material = luvaMaterial;
                rightHand.GetComponent<SkinnedMeshRenderer>().material = luvaMaterial;
            }
            else
            {
                SkinnedMeshRenderer leftHandMesh = leftHand.Find("LeftHand").GetChild(1).GetComponent<SkinnedMeshRenderer>();
                SkinnedMeshRenderer rightHandMesh = rightHand.Find("RightHand").GetChild(1).GetComponent<SkinnedMeshRenderer>();
                if (leftHandMesh != null && rightHandMesh != null)
                {
                    leftHandMesh.material = luvaMaterial;
                    rightHandMesh.material = luvaMaterial;
                }
            }
        }
        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Prepare o conjunto introdutor:";
        table.GetChild(0).GetChild(6).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Pegue a Seringa e preencha-a com soro fisiologico";
        table.GetChild(0).GetChild(6).GetChild(1).gameObject.SetActive(true);
        table.GetChild(0).GetChild(5).gameObject.SetActive(false);
        table.GetChild(0).GetChild(6).gameObject.SetActive(true);
        currentSlider.SetActive(true);
        tempMaterial.SetActive(true);
        tempConfetti.Play();
        StateController.Instance.SetState(State.PrepararConjuntoIntrodutor);
    }

    public void ProcessPrepararConjuntoIntrodutor()
    {
        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(7).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Aplique o Torniquete no paciente:";
        table.GetChild(0).GetChild(6).gameObject.SetActive(false);
        table.GetChild(0).GetChild(7).gameObject.SetActive(true);
        tempMaterial.SetActive(true);
        tempConfetti.Play();
        StateController.Instance.SetState(State.PrepararTorniquete);
    }

    public void ProcessPrepararTorniquete(XRSocketInteractor socket)
    {
        IXRSelectInteractable selectInteractable = socket.GetOldestInteractableSelected();
        GameObject currentMaterial = selectInteractable.transform.gameObject;

        if (currentMaterial != null)
            Destroy(currentMaterial);

        StateController.Instance.SetState(State.RealizarPuncture);
    }
    public void ProcessRealizarPuncture()
    {
    }

    private void ProcessRealizarTesteDePermeabilidade()
    {

    }

    private void ProcessFecharSistema()
    {

    }

    private void ProcessDescartarMateriais()
    {

    }
}
