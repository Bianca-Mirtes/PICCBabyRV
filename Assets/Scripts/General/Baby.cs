using UnityEngine;
public class Baby 
{
    public string name { get; }
   
    public bool isCasePicc { get; }

    public string medicalHistory { get; }

    public int age { get; }

    public string motherName { get; }

    public string medicalRecord { get; }

    public int numberMedicalRecord { get; }

    public int weight { get; }

    public int incubator { get; }

   public bool ProcessIsActive {get; set;}

   public Baby(bool state, string descriptionSymptom, string babyName, int babyAge, string mother, bool processIsActive, string record, int babyWeight, int leito)
   {
      isCasePicc = state;
      name = babyName;
      motherName = mother;
      medicalHistory = descriptionSymptom;
      age = babyAge;
      ProcessIsActive = processIsActive;
      medicalRecord = record;
      weight = babyWeight;
      incubator = leito;
      numberMedicalRecord = Random.Range(1000, 9999);
   }

   public void ModifyStateProcess(bool newState) {
      ProcessIsActive = newState;
   }
   
}
