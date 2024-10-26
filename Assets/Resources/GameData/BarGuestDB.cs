using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExcelAsset]
public class BarGuestDB : ScriptableObject
{
	public List<BarGuestEntity> Guests; // Replace 'EntityType' to an actual type that is serializable.
	public List<CocktailRejectScriptEntity> CocktailRejectScripts;
	public List<BarDialogueEntity> BarScripts; // Replace 'EntityType' to an actual type that is serializable.
	public List<StepEntity> Steps;
	public List<NPCScriptEntity> NPCScripts;
}
