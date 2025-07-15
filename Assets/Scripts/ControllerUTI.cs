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
    private GameObject hideObject;
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
    private bool isSuperiorMembers;

    public List<GameObject> mayosTablePICC;
    public GameObject currentMayosTablePICC;

    [Header("Setas")]
    [SerializeField] private List<GameObject> setasBaby;
    [SerializeField] private List<GameObject> setasLavarMaos;

    // Singleton 
    private static ControllerUTI _instance;
    public static ControllerUTI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ControllerUTI>();
                if (Instance == null)
                {
                    GameObject obj = new GameObject("ControllerUTI");
                    _instance = obj.AddComponent<ControllerUTI>();
                }
            }
            return _instance;
        }
    }

    private void Start()
    {
        AudioManager.instance.Play("background");
        Uniforme.SetActive(false);
        foreach (GameObject obj in mayosTablePICC)
            obj.SetActive(false);
    }

    void Update()
    {
        if (buttonSelect != null)
        {
            for (var ii = 0; ii < buttons.Count; ii++)
            {
                buttons[ii].enabled = false;
                setasBaby[ii].SetActive(false);
            }
        }
        VerifyProcessStateNow();
    }

    public void VerifyProcessStateNow()
    {
        if (StateController.Instance.CompareStates(State.VerificarCasoPicc))
            ProcessCasePicc();
        else
        if (StateController.Instance.CompareStates(State.RecolherMateriais))
            ProcessRecolherMateriais();
        else
        if (StateController.Instance.CompareStates(State.PrepararUniforme))
            ProcessOrganizarUniforme();
        else
        if (StateController.Instance.CompareStates(State.PrepararCampo))
            ProcessPrepararCampo();
        else
        if (StateController.Instance.CompareStates(State.RealizarAntissepsia))
            ProcessRealizarAntissepsia();
    }

    public void SetCurrentMaterial(GameObject material)
    {
        tempMaterial = material;
    }

    public GameObject GetCurrentMaterial()
    {
        return tempMaterial;
    }

    public void GetConfetti(ParticleSystem confetti)
    {
        tempConfetti = confetti;
    }

    public void GetCurrentMayosTablePICC(GameObject mayosTablePICC)
    {
        currentMayosTablePICC = mayosTablePICC;
    }

    public Transform GetCurrentIncubator()
    {
        return currentIncubator;
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
    }

    public void SetRegion(bool isSuperiorMembers)
    {
        this.isSuperiorMembers = isSuperiorMembers;
        if (isSuperiorMembers)
        {
            currentSlider = currentIncubator.GetChild(0).GetChild(0).GetChild(0).gameObject;
            currentPoints = currentIncubator.GetChild(0).GetChild(1).GetChild(1).gameObject;

            Destroy(currentIncubator.GetChild(0).GetChild(0).GetChild(1).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(1).GetChild(2).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(2).GetChild(1).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(3).GetChild(1).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(4).GetChild(1).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(5).GetChild(1).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(6).GetChild(1).gameObject);

            currentIncubator.GetChild(0).GetChild(7).gameObject.SetActive(true);
            currentIncubator.GetChild(0).GetChild(8).gameObject.SetActive(true);
        }
        else
        {
            currentSlider = currentIncubator.GetChild(0).GetChild(0).GetChild(1).gameObject;
            currentPoints = currentIncubator.GetChild(0).GetChild(1).GetChild(2).gameObject;

            Destroy(currentIncubator.GetChild(0).GetChild(0).GetChild(0).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(1).GetChild(1).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(2).GetChild(0).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(3).GetChild(0).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(4).GetChild(0).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(5).GetChild(0).gameObject);
            Destroy(currentIncubator.GetChild(0).GetChild(6).GetChild(0).gameObject);

            currentIncubator.GetChild(0).GetChild(7).gameObject.SetActive(true);
            currentIncubator.GetChild(0).GetChild(9).gameObject.SetActive(true);
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

    public void ProcessColetarAutorização()
    {
        FindFirstObjectByType<MotherController>().gameObject.SetActive(false);
        HideObject(form, 5f);
        finishProcessCanvas.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Parabéns! Você concluiu a etapa de assinatura,\r\nagora prepare-se para a etapa de Coleta dos Materiais!";
        finishProcessCanvas.GetComponent<FadeController>().FadeInForFadeOut(8f);
        finishProcessCanvas.GetChild(0).GetChild(1).GetComponent<ParticleSystem>().Play();

        StateController.Instance.SetState(State.RecolherMateriais);
    }

    public void HideObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void HideObject(GameObject obj, float time)
    {
        hideObject = obj;
        Invoke("Hide", time);
    }

    public void Hide()
    {
        hideObject.SetActive(false);
    }

    public void ProcessRecolherMateriais()
    {
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

    public void HideMayoTable()
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
        Uniforme.transform.Find("Canvas").gameObject.SetActive(false);
        Uniforme.transform.Find("seta").gameObject.SetActive(false);
        Uniforme.transform.Find("uniforme").gameObject.SetActive(false);

        finishProcessCanvas.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Parabéns! Agora você está pronto para realizar o procedimento!";
        finishProcessCanvas.GetChild(0).GetChild(1).GetComponent<ParticleSystem>().Play();

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
        else
        {
            Debug.Log("Não achou as mãos");
        }
        GameObject.FindWithTag("MainCamera").transform.GetChild(2).gameObject.SetActive(true); // gorro
        GameObject.FindWithTag("MainCamera").transform.GetChild(3).gameObject.SetActive(true); // mascara
        currentIncubator.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
        currentMayosTablePICC.SetActive(true);
        StateController.Instance.SetState(State.MensurarCateter);
    }

    public void verifSocketsSurgicalField(XRSocketInteractor socket)
    {
        DestroyMaterial(socket);
        countSurgicalFields++;
    }

    public void DestroyMaterial(XRSocketInteractor socket)
    {
        IXRSelectInteractable selectInteractable = socket.GetOldestInteractableSelected();
        GameObject currentMaterial = selectInteractable.transform.gameObject;

        if (currentMaterial != null)
            Destroy(currentMaterial);
    }

    public void VerifUpdateSlider(UnityEngine.UI.Slider slider)
    {
        if (slider.value == 1)
            Invoke("NextStep", 3f);
    }

    private void NextStep()
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

    public void ProcessMensurarCateter()
    {
        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Faça a mensuração do tamanho do Cateter:";
        if (isSuperiorMembers)
            table.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(true);
        else
            table.GetChild(0).GetChild(2).GetChild(3).gameObject.SetActive(true);
        table.GetChild(0).GetChild(1).gameObject.SetActive(false);
        table.GetChild(0).GetChild(2).gameObject.SetActive(true);
        currentSlider.SetActive(true);
    }

    private void ProcessPrepararCampo()
    {
        if (countSurgicalFields == 2)
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
        if (currentSlider.GetComponent<UnityEngine.UI.Slider>().value == 1)
        {
            Transform leftHand = GameObject.FindWithTag("LeftHand").transform;
            Transform rightHand = GameObject.FindWithTag("RightHand").transform;
            if (leftHand != null && rightHand != null)
            {
                if (leftHand.name.Equals("LeftHand") && rightHand.name.Equals("RightHand"))
                {
                    leftHand.GetComponent<SkinnedMeshRenderer>().material = handMaterial;
                    rightHand.GetComponent<SkinnedMeshRenderer>().material = handMaterial;
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

            GameObject pincaAnatomica = GameObject.FindWithTag("Gaze").transform.parent.parent.gameObject;
            Destroy(pincaAnatomica);
            tempMaterial.SetActive(false);

            tempMaterial = luvas;
            tempMaterial.SetActive(true);
            tempConfetti.Play();
            StateController.Instance.SetState(State.TrocarLuvas);
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
        table.GetChild(0).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Prepare o conjunto introdutor:";
        table.GetChild(0).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Pegue a Seringa e preencha-a com soro fisiologico";
        table.GetChild(0).GetChild(4).GetChild(1).gameObject.SetActive(true);
        table.GetChild(0).GetChild(5).gameObject.SetActive(false);
        table.GetChild(0).GetChild(4).gameObject.SetActive(true);
        currentSlider.SetActive(true);
        tempMaterial.SetActive(true);
        tempConfetti.Play();
        StateController.Instance.SetState(State.PrepararConjuntoIntrodutor);
    }

    public void ProcessPrepararConjuntoIntrodutor()
    {
        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Aplique o Torniquete no paciente:";
        table.GetChild(0).GetChild(4).gameObject.SetActive(false);
        if (isSuperiorMembers)
            table.GetChild(0).GetChild(6).GetChild(1).gameObject.SetActive(true);
        else
            table.GetChild(0).GetChild(6).GetChild(2).gameObject.SetActive(true);

        table.GetChild(0).GetChild(6).gameObject.SetActive(true);
        tempMaterial.SetActive(true);
        tempConfetti.Play();
        StateController.Instance.SetState(State.AplicarTorniquete);
    }

    public void ProcessPrepararTorniquete(XRSocketInteractor socket)
    {
        IXRSelectInteractable selectInteractable = socket.GetOldestInteractableSelected();
        GameObject currentMaterial = selectInteractable.transform.gameObject;

        if (currentMaterial != null)
            Destroy(currentMaterial);

        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(7).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Venopunção: Utilize o GATILHO do controle ESQUERDO para inserir na " + "<color=green>área correta";
        table.GetChild(0).GetChild(7).GetChild(1).gameObject.SetActive(false);
        table.GetChild(0).GetChild(6).gameObject.SetActive(false);
        table.GetChild(0).GetChild(7).gameObject.SetActive(true);
        tempMaterial.SetActive(true);
        tempConfetti.Play();
        StateController.Instance.SetState(State.RealizarPunção);
    }

    public void ProcessRealizarPuncture()
    {
        Transform tabletInfo = currentMayosTablePICC.transform.Find("TabletInfos");

        tabletInfo.GetChild(0).GetChild(8).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Realize a Introdução Completa do Cateter: ";
        tabletInfo.GetChild(0).GetChild(8).gameObject.SetActive(true);
        tabletInfo.GetChild(0).GetChild(7).gameObject.SetActive(false);

        tempMaterial.transform.GetChild(0).gameObject.SetActive(true);
        tempMaterial.SetActive(true);
        //tempConfetti.Play();

        StateController.Instance.SetState(State.RealizarIntroduçãoCompleta);
    }

    public void ProcessRealizarCompleteIntroduction()
    {
        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Teste de Permeabilidade: ";
        table.GetChild(0).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Encha a seringa com soro fisiológico";
        table.GetChild(0).GetChild(8).gameObject.SetActive(false);
        table.GetChild(0).GetChild(4).GetChild(1).gameObject.SetActive(true);
        table.GetChild(0).GetChild(4).gameObject.SetActive(true);

        GameObject.Find("MinigameIntroduction").SetActive(false);

        //tempConfetti.Play();
        StateController.Instance.SetState(State.RealizarTesteDePermeabilidade);
    }

    public void ProcessRealizarTesteDePermeabilidade()
    {
        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(7).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Remoção do Introdutor: ";
        table.GetChild(0).GetChild(6).gameObject.SetActive(false);
        table.GetChild(0).GetChild(7).gameObject.SetActive(true);

        tempMaterial.SetActive(true);
        //tempConfetti.Play();
        StateController.Instance.SetState(State.RealizarRemoçãoIntrodutor);
    }

    public void ProcessRealizarRemoçãoIntrodutor()
    {
        Transform table = currentMayosTablePICC.transform.Find("TabletInfos");
        table.GetChild(0).GetChild(7).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Cobertura do Cateter: ";
        table.GetChild(0).GetChild(6).gameObject.SetActive(false);
        table.GetChild(0).GetChild(7).gameObject.SetActive(true);

        tempMaterial.SetActive(true);
        //tempConfetti.Play();
        StateController.Instance.SetState(State.FazerCoberturaDoCateter);
    }

    public void EndProcedure()
    {
        GameObject.FindWithTag("MainCamera").transform.GetChild(2).gameObject.SetActive(false); // gorro
        GameObject.FindWithTag("MainCamera").transform.GetChild(3).gameObject.SetActive(false); // mascara
        Transform leftHand = GameObject.FindWithTag("LeftHand").transform;
        Transform rightHand = GameObject.FindWithTag("RightHand").transform;

        if (leftHand != null && rightHand != null)
        {
            if (leftHand.name.Equals("LeftHand") && rightHand.name.Equals("RightHand"))
            {
                leftHand.GetComponent<SkinnedMeshRenderer>().material = handMaterial;
                rightHand.GetComponent<SkinnedMeshRenderer>().material = handMaterial;
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
        currentIncubator.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
    }
}
