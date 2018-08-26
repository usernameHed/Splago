using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Fonctions utile
/// <summary>
public static class GameData
{
    #region core script

    public enum Event
    {
        GameOver,
        AdditiveJustFinishLoad, //used by SceneManagerGlobal
        ResolutionChange,   //used by UILandscape
    };

    public enum Prefabs
    {
        Link,
        Player,
    };

    public enum PoolTag
    {
        ParticleBump,
        Link,
        ParticleRopeTense,
    }

    public enum Layers
    {
        Player,
        Obstacle,
        Default,
        Object,
    }

    public enum Sounds
    {
        Bump,
        SpiksOn,
        SpiksOff,
    }

    /// <summary>
    /// retourne vrai si le layer est dans la list
    /// </summary>
    public static bool IsInList(List<Layers> listLayer, int layer)
    {
        string layerName = LayerMask.LayerToName(layer);
        for (int i = 0; i < listLayer.Count; i++)
        {
            if (listLayer[i].ToString() == layerName)
            {
                return (true);
            }
        }
        return (false);
    }
    /// <summary>
    /// retourne vrai si le layer est dans la list
    /// </summary>
    public static bool IsInList(List<Prefabs> listLayer, string tag)
    {
        for (int i = 0; i < listLayer.Count; i++)
        {
            if (listLayer[i].ToString() == tag)
            {
                return (true);
            }
        }
        return (false);
    }
    #endregion
}
