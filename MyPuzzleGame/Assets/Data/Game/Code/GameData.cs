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

		public GameData()
		{
			EffectData  =  TableStream.LoadTableByTSV(EffectDataPath).TableToDictionary<int,EffectData>();
		}
	}
}
