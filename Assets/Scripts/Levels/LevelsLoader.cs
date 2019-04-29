using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

public class LevelsLoader : MonoBehaviour
{
    [SerializeField] string levelsFileName;
    [SerializeField] LevelSet levelSet;
    [SerializeField] private UnityEvent OnLevelsLoaded;
    private StoredLevels storedLevels;

    private void Awake()
    {
        if (levelSet.Items.Count > 0)
            return;
            
        LoadLevels();
    }

    private void LoadLevels()
    {
        TextAsset txtAsset = Resources.Load<TextAsset>(levelsFileName);
        storedLevels = JsonUtility.FromJson<StoredLevels>(txtAsset.text);
        SaveLevelToSet();
    }

    private void SaveLevelToSet()
    {
        for (int i = 0; i < storedLevels.levels.Length; i++)
        {
            Level level = new Level(storedLevels.levels[i].level);
            levelSet.AddItem(level);
        }

        OnLevelsLoaded?.Invoke();
    }
}
