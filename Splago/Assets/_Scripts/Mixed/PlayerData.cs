using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class PlayerData : PersistantData
{
    #region Attributes

    [FoldoutGroup("GamePlay"), Tooltip("checkpoint"), SerializeField]
    private int checkpoint = 0;

    #endregion

    #region Core
    public void SetCheckpoint(int index)
    {
        checkpoint = index;
    }
    public int GetCheckpoint()
    {
        return (checkpoint);
    }
    
    /// <summary>
    /// reset toute les valeurs à celle d'origine pour le jeu
    /// </summary>
    public void SetDefault()
    {
        checkpoint = 0;
    }

    public override string GetFilePath ()
	{
		return "playerData.dat";
	}

	#endregion
}