using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class BarGuestDB : ScriptableObject
{
	public List<BarGuestEntity> Guests; // Replace 'EntityType' to an actual type that is serializable.
	public List<BarDialogueEntity> Scripts; // Replace 'EntityType' to an actual type that is serializable.
	public List<CocktailAlcoholAnswerEntity> AlcoholAnswers;
	public List<StepEntity> Steps;
}
