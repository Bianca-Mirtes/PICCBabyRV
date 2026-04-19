
using BabyData;
using System.Collections.Generic;
using UnityEngine;

/**
	Design Pattern para processar e armazenar os dados globais dos pacientes e seus sintomas.
*/
public class SymptomCollection : MonoBehaviour
{
	public static SymptomCollection Instance { get; private set; }

	protected List<BabyCase> Babys = new List<BabyCase>();

	private void Awake()
	{
		Instance = this;
	}

	public void AddSymptom(List<BabyCase> babys)
	{
		Babys = babys;
	}

	public List<BabyCase> GetSymptoms()
	{
		return Babys;
	}

	public BabyCase FindUniqueBaby(string name)
	{
		return Babys.Find(d => d.baby.name == name);
	}
}