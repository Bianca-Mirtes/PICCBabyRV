
using System.Collections.Generic;
using UnityEngine;

/**
	Design Pattern para processar e armazenar os dados globais dos pacientes e seus sintomas.
*/
public class SymptomCollection : MonoBehaviour
{
	public static SymptomCollection Instance { get; private set; }

	protected List<Baby> Babys = new List<Baby>();

	private void Awake()
	{
		Instance = this;
	}

	public void AddSymptom(List<Baby> babys)
	{
		Babys = babys;
	}

	public List<Baby> GetSymptoms()
	{
		return Babys;
	}

	public Baby FindUniqueBaby(string name)
	{
		return Babys.Find(d => d.Name == name);
	}
}