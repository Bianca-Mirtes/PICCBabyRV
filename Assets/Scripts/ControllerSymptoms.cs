using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSymptoms : MonoBehaviour
{
    protected List<Baby> Babys = new List<Baby>();
    public List<Canvas> canvasSympton = new List<Canvas>();

    /** 
        Aqui é inicializado e processado os sintomas para os resceptivos canvas (incubadoras)
    */
    void Start()
    {
        processNewCaseOfSymptom(true,
            "Ela é um bebê de 17 dias de vida, peso de nascimento 1100g, idade gestacional de 32 semanas e 5 dias, parto cesáreo por sofrimento fetal e pré-eclâmpsia. Extubação no 12º dia de vida, há 5 dias em CPAP nasal , com FiO2 de 40%, PEEP de 6 mmHg, dieta por sonda orogástrica desde 3º dia de vida. Apresentou nas últimas 24 horas, 2 episódios de apneia, bradicardia e cianose, pele redilhada, hipotensão, hipoatividade, hipertermia, distensão abdominal e 3 episódios de resíduo gástrico esverdeado, sendo suspensa a dieta, mantendo SOG aberta.  Foi coletado exames de sangue e hemocultura, realizado raio-x. Iniciou hidratação, aguarda iniciar oxacilina, amicacina e dobutamina.  Está com acesso venoso periférico em membro inferior direito pérvio e sem sinais flogísticos.\r\n",
            "Maria Alice Silva",
            "Marta Freitas Lopes",
            32,
            "Incubadora aquecida para temperatura axilar 36,5-37,5° C\r\n Em uso\r\nOximetria de pulso contínua | Em uso\r\nCabeceira elevada em 30° \r\nAtenção\r\nCPAP Nasal: FiO2 40%, PEEP 6 mmHg\r\nAtenção\r\n05. Dieta zero, registrar resíduo gástrico em balanço hídrico\r\n12h  15h  18h  21h  24h  03h  06h  9h \r\n06. Oxacilina (50mg/1ml) ​fazer 0,6 ml  + 9,4 ml  SF 0,9%  , 8/8 horas,  EV,  em 30 minutos  D0/7\r\n08h                 16h              24h\r\n07. Amicacina ( 5 mg/ml), fazer 3,6 ml + 1,4 ml SF 0,9%, EV, 24h. D0/7\r\n                        20h\r\n08.Hidratação Venosa para 24h\r\nSG 10% —-------------- 99,3 ml \r\nNaCl 0,9% —----------19,5 ml\r\nKCL 19,1%------------1,2\r\nVolume Total —------ 120ml\r\nVelocidade de infusão em BIC:5 ml/h\r\n(VIG 6,9 Concentração de glicose menor que 12,5%)\r\n\r\n",
            1200,
            1
            );

        processNewCaseOfSymptom(false,
            "Esse paciente, Recém-nascido de Rosenia está sobre bom estado",
            "Baby2",
            "Rosenia",
            35,
            "Incubadora aquecida para temperatura axilar 36,5-37,5° C\r\n Em uso\r\nOximetria de pulso contínua | Em uso\r\nCabeceira elevada em 30° \r\nAtenção\r\nCPAP Nasal: FiO2 40%, PEEP 6 mmHg\r\nAtenção\r\n05. Dieta zero, registrar resíduo gástrico em balanço hídrico\r\n12h  15h  18h  21h  24h  03h  06h  9h \r\n06. Oxacilina (50mg/1ml) ​fazer 0,6 ml  + 9,4 ml  SF 0,9%  , 8/8 horas,  EV,  em 30 minutos  D0/7\r\n08h                 16h              24h\r\n07. Amicacina ( 5 mg/ml), fazer 3,6 ml + 1,4 ml SF 0,9%, EV, 24h. D0/7\r\n                        20h\r\n08.Hidratação Venosa para 24h\r\nSG 10% —-------------- 99,3 ml \r\nNaCl 0,9% —----------19,5 ml\r\nKCL 19,1%------------1,2\r\nVolume Total —------ 120ml\r\nVelocidade de infusão em BIC:5 ml/h\r\n(VIG 6,9 Concentração de glicose menor que 12,5%)\r\n\r\n",
            1200,
            2
            );

        processNewCaseOfSymptom(true,
            "Esse paciente, Recém-nascido de Marina Antonieta, nasceu com 33 semanas de gestação, já tem 8 dias de gestação, evoluir nas últimas 20 horas com piora no quadro clínico: regurgitações amareladas, episódios de hiperglicemia e apneias, precisando de pressão positiva contínua em vias aéreas. Optamos por iniciar outro esquema de antibióticos. Ele já está em jejum e iniciou hidratação venosa por acesso periférico.",
            "Baby3",
            "Marina Antonieta",
            33,
            "Incubadora aquecida para temperatura axilar 36,5-37,5° C\r\n Em uso\r\nOximetria de pulso contínua | Em uso\r\nCabeceira elevada em 30° \r\nAtenção\r\nCPAP Nasal: FiO2 40%, PEEP 6 mmHg\r\nAtenção\r\n05. Dieta zero, registrar resíduo gástrico em balanço hídrico\r\n12h  15h  18h  21h  24h  03h  06h  9h \r\n06. Oxacilina (50mg/1ml) ​fazer 0,6 ml  + 9,4 ml  SF 0,9%  , 8/8 horas,  EV,  em 30 minutos  D0/7\r\n08h                 16h              24h\r\n07. Amicacina ( 5 mg/ml), fazer 3,6 ml + 1,4 ml SF 0,9%, EV, 24h. D0/7\r\n                        20h\r\n08.Hidratação Venosa para 24h\r\nSG 10% —-------------- 99,3 ml \r\nNaCl 0,9% —----------19,5 ml\r\nKCL 19,1%------------1,2\r\nVolume Total —------ 120ml\r\nVelocidade de infusão em BIC:5 ml/h\r\n(VIG 6,9 Concentração de glicose menor que 12,5%)\r\n\r\n",
            1200,
            3
            );

        processNewCaseOfSymptom(true,
            "Esse paciente, Recém-nascido de Julia Freitas, nasceu com 40 semanas de gestação, já tem 8 dias de geestação, evoluir nas últimas 20 horas com piora no quadro clínico: regurgitações amareladas, episódios de hiperglicemia e apneias, precisando de pressão positiva contínua em vias aéreas. Optamos por iniciar outro esquema de antibióticos. Ele já está em jejum e iniciou hidratação venosa por acesso periférico.",
            "Baby4",
            "Julia Freitas",
            40,
            "Incubadora aquecida para temperatura axilar 36,5-37,5° C\r\n Em uso\r\nOximetria de pulso contínua | Em uso\r\nCabeceira elevada em 30° \r\nAtenção\r\nCPAP Nasal: FiO2 40%, PEEP 6 mmHg\r\nAtenção\r\n05. Dieta zero, registrar resíduo gástrico em balanço hídrico\r\n12h  15h  18h  21h  24h  03h  06h  9h \r\n06. Oxacilina (50mg/1ml) ​fazer 0,6 ml  + 9,4 ml  SF 0,9%  , 8/8 horas,  EV,  em 30 minutos  D0/7\r\n08h                 16h              24h\r\n07. Amicacina ( 5 mg/ml), fazer 3,6 ml + 1,4 ml SF 0,9%, EV, 24h. D0/7\r\n                        20h\r\n08.Hidratação Venosa para 24h\r\nSG 10% —-------------- 99,3 ml \r\nNaCl 0,9% —----------19,5 ml\r\nKCL 19,1%------------1,2\r\nVolume Total —------ 120ml\r\nVelocidade de infusão em BIC:5 ml/h\r\n(VIG 6,9 Concentração de glicose menor que 12,5%)\r\n\r\n",
            1200,
            4
            );

        processNewCaseOfSymptom(false,
            "Esse paciente, Recém-nascido de Rosenia está sobre bom estado",
            "Baby5",
            "Rosenia",
            35,
            "Incubadora aquecida para temperatura axilar 36,5-37,5° C\r\n Em uso\r\nOximetria de pulso contínua | Em uso\r\nCabeceira elevada em 30° \r\nAtenção\r\nCPAP Nasal: FiO2 40%, PEEP 6 mmHg\r\nAtenção\r\n05. Dieta zero, registrar resíduo gástrico em balanço hídrico\r\n12h  15h  18h  21h  24h  03h  06h  9h \r\n06. Oxacilina (50mg/1ml) ​fazer 0,6 ml  + 9,4 ml  SF 0,9%  , 8/8 horas,  EV,  em 30 minutos  D0/7\r\n08h                 16h              24h\r\n07. Amicacina ( 5 mg/ml), fazer 3,6 ml + 1,4 ml SF 0,9%, EV, 24h. D0/7\r\n                        20h\r\n08.Hidratação Venosa para 24h\r\nSG 10% —-------------- 99,3 ml \r\nNaCl 0,9% —----------19,5 ml\r\nKCL 19,1%------------1,2\r\nVolume Total —------ 120ml\r\nVelocidade de infusão em BIC:5 ml/h\r\n(VIG 6,9 Concentração de glicose menor que 12,5%)\r\n\r\n",
            1200,
            5
            );

        renderCaseOfSymptomInCanva();
        identifyCasePICC();
    }

    private void processNewCaseOfSymptom(bool state, string description, string name, string motherName, int age, string record, int weight, int leito)
    {
        Baby baby = new Baby(state, description, name, age, motherName, false, record, weight, leito);
        Babys.Add(baby);
        SymptomCollection.Instance.AddSymptom(Babys);
    }

    private void renderCaseOfSymptomInCanva()
    {
        if (Babys.Count == canvasSympton.Count) //Para contar se a quantidade de dados é maior ou superior a quantidade de incubadoras
        {
            for (int i = 0; i < Babys.Count; i++)
            {   
                TextMeshProUGUI babyName = canvasSympton[i].transform.Find("Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI babyDescription = canvasSympton[i].transform.Find("DescriptionSymptoms").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI recordText = canvasSympton[i].transform.Find("Record").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI numberRecord = canvasSympton[i].transform.Find("NRecord").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI numberLeito = canvasSympton[i].transform.Find("NLeito").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI numberPeso = canvasSympton[i].transform.Find("NPeso").GetComponent<TextMeshProUGUI>();

                babyName.text = Babys[i].Name;
                babyDescription.text = Babys[i].DescriptionSymptom;
                recordText.text = Babys[i].Record;
                numberRecord.text = Babys[i].NRecord.ToString();
                numberLeito.text = Babys[i].Leito.ToString();
                numberPeso.text = Babys[i].Weight.ToString()+" g".ToString();
            }
        }
        else
            Debug.Log("A quantidade de sintomas cadastrados precisa ser igual à quantidade de text canvas!");
    }

    private void identifyCasePICC()
    {
        Canvas canvas = canvasSympton[0].GetComponent<Canvas>();
        Baby FindBaby;

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
                    Baby baby = Babys.Find(s => s.DescriptionSymptom.Contains(texto));
                    FindBaby = baby;
                }
            }
        }
    }

}
