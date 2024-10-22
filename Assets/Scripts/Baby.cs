using UnityEngine;
public class Baby 
{
   public string Name { get; }
   
   public bool StateCasePicc { get; }

   public string DescriptionSymptom { get; }

   public int Age { get; }

   public string MotherName { get; }

    public string Record { get; }

    public int NRecord { get; }

    public int Weight { get; }

    public int Leito { get; }

   public bool ProcessIsActive {get; set;}

   public Baby(bool state, string descriptionSymptom, string name, int age, string motherName, bool processIsActive, string record, int weight, int leito)
   {
      StateCasePicc = state;
      Name = name;
      MotherName = motherName;
      DescriptionSymptom = descriptionSymptom;
      Age = age;
      ProcessIsActive = processIsActive;
      Record = record;
      Weight = weight;
      Leito = leito;
   }

   public void ModifyStateProcess(bool newState) {
      ProcessIsActive = newState;
   }
   
}
