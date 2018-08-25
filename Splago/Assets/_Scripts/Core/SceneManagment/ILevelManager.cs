using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManager
{
    /// <summary>
    /// initialise ce scripts
    /// </summary>
	void InitScene();

    void Play();    //passe à la scène suivante
    void Previous();    //passe à la scene précédente
    void InputLevel();   //input du level
}
