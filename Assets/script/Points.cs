using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script à mettre sur les 2 Préfab qui afficheront les scores
/// </summary>
public class Points : MonoBehaviour {

    /// <summary>
    /// Entier enregistrant le score
    /// </summary>
    public int score = 0;

    /// <summary>
    /// séparation en dizaines et unités, pour permettres d'afficher les 
    /// </summary>
    private int dizaines = 0;

    /// <summary>
    /// séparation en dizaines et unités, pour permettres d'afficher les 
    /// </summary>
    private int unité = 0;

    private void Awake()
    {
        MajScore();   
    }

    /// <summary>
    /// Fonction métant à jour les dizaines et unités
    /// </summary>
    private void MajScore()
    {
        unité = score % 10;
        dizaines = score / 10;
    }
}
