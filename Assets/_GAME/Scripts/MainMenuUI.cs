using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private List<Button> LevelBtnList;

    private void Start()
    {
        foreach(LevelData level in LevelManager.Instance.LevelDatabase.levels)
        {
            if (LevelManager.Instance.IsUnlocked(level.levelIndex))
            {
                LevelBtnList[level.levelIndex - 1].interactable = true;
            }
            else
            {
                LevelBtnList[level.levelIndex - 1].interactable = false;
            }
        }
    }

    public void LoadLevel(int index)
    {
        LevelManager.Instance.LoadLevel(index);
    }
}
