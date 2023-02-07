using Godot;
using System;
using System.Runtime.Serialization.Formatters.Binary; 

public class Settings : Node
{
    public static PlayerSettings currentSettings;

	[Export] 
	Godot.Environment postEffectsReference;
    
    static Godot.Environment postEffects;




	public override void _Ready()
	{
        // set statics
        postEffects = postEffectsReference;

		LoadSettings();
	}



	public static void SaveSettings() 
	{
		ApplySettings();
        
		BinaryFormatter bf = new BinaryFormatter();
		System.IO.FileStream file = System.IO.File.Create (OS.GetUserDataDir() + "/settings.dwg");
		bf.Serialize(file, currentSettings);
		file.Close();
	}



	public static void LoadSettings() 
	{
		if(System.IO.File.Exists(OS.GetUserDataDir() + "/settings.dwg")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			System.IO.FileStream file = System.IO.File.Open(OS.GetUserDataDir() + "/settings.dwg", System.IO.FileMode.Open);
			currentSettings = (PlayerSettings)bf.Deserialize(file);
			file.Close();
		}
		else
		{
			// no settings exist
			currentSettings = new PlayerSettings();
		}

		ApplySettings();
	}



	public static void ApplySettings()
	{
		CameraControllerSimple.sensitivity = currentSettings.mouse * 0.1f; // ui value is 10 times higher than real value for easier use
		postEffects.GlowEnabled = Settings.currentSettings.bloom;
		postEffects.SsaoEnabled = Settings.currentSettings.ssao;
	}



	[System.Serializable]
	public class PlayerSettings
	{
		public float mouse = 1.5f;
		public bool bloom = true,
            ssao = true;
	}
}
