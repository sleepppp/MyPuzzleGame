using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data.Utility;
using Core.Data;

namespace Core.Data
{
	public class GameData
	{
		public string EffectDataPath = "Assets/Data/Game/TSV/EffectData.tsv";
		public Dictionary<int,EffectData> EffectData;

		public string DialogueDataPath = "Assets/Data/Game/TSV/DialogueData.tsv";
		public Dictionary<int,DialogueData> DialogueData;

		public string StageDataPath = "Assets/Data/Game/TSV/StageData.tsv";
		public Dictionary<int,StageData> StageData;

		public GameData()
		{
			EffectData  =  TableStream.LoadTableByTSV(EffectDataPath).TableToDictionary<int,EffectData>();
			DialogueData  =  TableStream.LoadTableByTSV(DialogueDataPath).TableToDictionary<int,DialogueData>();
			StageData  =  TableStream.LoadTableByTSV(StageDataPath).TableToDictionary<int,StageData>();
		}
	}
}
