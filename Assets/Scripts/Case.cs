using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Case", menuName = "ScriptableObjects/Case")]
public class Case : ScriptableObject
{
    public string name;
    public bool isCasePicc;
    public string medicalHistory;
    public int age;
    public string motherName;
    public string medicalRecord;
    public int numberMedicalRecord;
    public int weight;
    public int incubator;
}
