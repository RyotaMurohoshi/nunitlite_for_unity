using UnityEditor;
using UnityEngine;

namespace NUnitLiteForUnity.Utility
{
	public class UnityPackageExporter
	{
		[MenuItem("Assets/Export NUnitLiteForUnity")]
		public static void ExportUnityPackage ()
		{
			Debug.Log ("Export NUnitLiteForUnity");
			AssetDatabase.ExportPackage ("Assets/NUnitLiteForUnity", "nunitlite_for_unity.unitypackage", ExportPackageOptions.Recurse);
		}
	}
}