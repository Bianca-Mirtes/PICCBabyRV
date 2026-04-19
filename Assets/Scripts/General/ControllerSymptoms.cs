using BabyData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSymptoms : MonoBehaviour
{
    protected List<BabyCase> Babys = new List<BabyCase>();
    public List<Canvas> canvasSympton = new List<Canvas>();
    [SerializeField] private List<Transform> incubators;

    void Start()
    {
        processNewCaseOfSymptom(true,
            "Recém-nascido pré-termo extremo, parto cesáreo de emergência por pré-eclâmpsia materna grave. Necessitou de ventilação mecânica nas primeiras horas de vida. " +
            "Evoluiu com instabilidade hemodinâmica e sinais laboratoriais sugestivos de infecção sistêmica (PCR elevado, leucocitose).",
            "Gabriel Ribeiro Santos",
            "Ana Paula Ribeiro",
            28,
            "Paciente em UTI neonatal, em uso de antibioticoterapia endovenosa prolongada (vancomicina e gentamicina), nutrição parenteral total (NPT) e reposição eletrolítica frequente." +
            " Acesso venoso periférico com dificuldade de manutenção e múltiplas punções prévias.",
            1120,
            1,
            "Prematuridade extrema associada a sepse neonatal precoce.",
            "Necessidade de acesso venoso central para infusão segura de NPT e antibioticoterapia prolongada, reduzindo risco de extravasamento e novas punções periféricas."
            );

        processNewCaseOfSymptom(false,
            "RN a termo, parto normal, boa adaptação neonatal. Evoluiu com icterícia leve no segundo dia de vida, compatível com quadro fisiológico.",
            "Sofia Costa Menezes",
            "Renata Costa Menezes",
            37,
            "Paciente em bom estado geral, ativo, mamando exclusivamente ao seio materno. Bilirrubinas dentro de limites esperados para a idade.",
            3040,
            2,
            "Icterícia neonatal fisiológica.",
            "Não há indicação de PICC, pois não há necessidade de medicações endovenosas prolongadas ou nutrição parenteral."
            );

        processNewCaseOfSymptom(true,
            "RN pré-termo moderado, parto vaginal. Evoluiu com taquipneia, retrações subcostais e necessidade de CPAP nasal. Apresenta intolerância à dieta enteral, com resíduos gástricos frequentes e distensão abdominal.",
            "Helena Ferreira Lima",
            "Juliana Ferreira Lima",
            30,
            "Paciente estável hemodinamicamente, em suporte respiratório não invasivo. Em uso de nutrição parenteral parcial e antibiótico profilático. Acesso venoso periférico com sinais iniciais de flebite.",
            1200,
            3,
            "Síndrome do desconforto respiratório neonatal associada à intolerância alimentar.",
            "Acesso venoso central indicado para administração contínua de nutrição parenteral e medicações vesicantes, evitando complicações periféricas."
            );

        processNewCaseOfSymptom(false,
            "RN a termo, parto cesáreo eletivo. Evoluiu com desconforto respiratório leve nas primeiras horas de vida, compatível com TTN.",
            "Livia Oliveira Rocha",
            "Patrícia Oliveira Rocha",
            36,
            "Paciente em observação em berçário, em ar ambiente, com melhora progressiva do padrão respiratório. Alimentação enteral tolerada. Utiliza apenas acesso periférico temporário para hidratação venosa leve.;/////////",
            2910,
            4,
            "Taquipneia transitória do recém-nascido (TTN).",
            "Não há indicação de PICC, pois o tratamento é de curta duração e não requer acesso venoso central."
            );

        processNewCaseOfSymptom(true,
            "RN pré-termo tardio, parto vaginal. Evoluiu após o 5º dia de vida com distensão abdominal, vômitos biliosos e sangue oculto nas fezes. Radiografia abdominal evidenciou pneumatose intestinal.",
            "Lucas Nogueira Alves",
            "Camila Nogueira Alves",
            33,
            "Paciente em jejum absoluto, em uso de antibióticos de amplo espectro e nutrição parenteral total. Apresenta necessidade de infusão contínua de soluções hipertônicas.",
            2180,
            5,
            "Enterocolite necrosante estágio II (Bell).",
            "Acesso venoso central necessário para NPT prolongada e antibioticoterapia, com previsão de uso superior a 7 dias."
            );

        renderCaseOfSymptomInCanva();
        identifyCasePICC();
    }

    private void processNewCaseOfSymptom(bool isPiccCase, string description, string name, string motherName, int age, string currentState, int weight, int leito, string generalDiag, string justification)
    {
        Baby baby = new Baby(isPiccCase, description, name, age, motherName, false, currentState, weight, leito);

        BabyCase babyCase = new BabyCase();
        babyCase.baby = baby;
        babyCase.justification = justification;
        babyCase.generalDiagnosis = generalDiag;

        Babys.Add(babyCase);
        SymptomCollection.Instance.AddSymptom(Babys);
    }

    public void FindIncubator(Baby baby)
    {
        foreach(Transform incubator in incubators)
        {
            string incubadora = "Incubadora" + baby.incubator;
            if (incubadora.Equals(incubator.name))
            {
                FindObjectOfType<ControllerUTI>().SetCurrentIncubator(incubator);
            }
        }
    }

    private void renderCaseOfSymptomInCanva()
    {
        if (Babys.Count == canvasSympton.Count) //Para contar se a quantidade de dados é maior ou superior a quantidade de incubadoras
        {
            for (int ii = 0; ii < Babys.Count; ii++)
            {   
                TextMeshProUGUI babyName = canvasSympton[ii].transform.Find("BabyName").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI babyDescription = canvasSympton[ii].transform.Find("History").GetChild(0).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI recordText = canvasSympton[ii].transform.Find("CurrentState").GetChild(0).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI numberRecord = canvasSympton[ii].transform.Find("MedicalRecord").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI numberIncubator = canvasSympton[ii].transform.Find("Incubator").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI numberWeight = canvasSympton[ii].transform.Find("Weight").GetComponent<TextMeshProUGUI>();

                babyName.text = "NOME DO PACIENTE: " + "<color=black>" + Babys[ii].baby.name;
                babyDescription.text = Babys[ii].baby.medicalHistory;
                recordText.text = Babys[ii].baby.medicalRecord;
                numberRecord.text = "PRONTUARIO: " + "<color=black>" + Babys[ii].baby.numberMedicalRecord.ToString();
                numberIncubator.text = "LEITO: " + "<color=black>" + Babys[ii].baby.incubator.ToString();
                numberWeight.text = "PESO (em gramas): " + "<color=black>" + Babys[ii].baby.weight.ToString()+" g".ToString();
            }
        }
        else
            Debug.Log("A quantidade de sintomas cadastrados precisa ser igual à quantidade de text canvas!");
    }

    private void identifyCasePICC()
    {
        Canvas canvas = canvasSympton[0].GetComponent<Canvas>();
        BabyCase FindBaby;

        //Find the symptom on canvas array
        if (canvas != null)
        {
            Transform childDescriptionCanva = canvas.transform.Find("Symptoms");
            if (childDescriptionCanva != null)
            {
                Text propertyChildDescriptionCanva = childDescriptionCanva.GetComponent<Text>();
                if (propertyChildDescriptionCanva != null)
                {
                    string texto = propertyChildDescriptionCanva.text;
                    BabyCase baby = Babys.Find(s => s.baby.medicalHistory.Contains(texto));
                    FindBaby = baby;
                }
            }
        }
    }

}
