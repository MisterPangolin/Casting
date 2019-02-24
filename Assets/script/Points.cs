using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Script à mettre sur les 2 Préfab qui afficheront les scores
/// </summary>
public class Points : MonoBehaviour {

    /// <summary>
    /// Entier enregistrant le score
    /// </summary>
    private int score = 0;

    /// <summary>
    /// séparation en dizaines et unités, pour permettres d'afficher les 
    /// </summary>
    private int dizaines = 0;
    public Text TDizaine;
    /// séparation en dizaines et unités, pour permettres d'afficher les 
    /// </summary>
    private int unité = 0;
    public Text TUnité;

    private void Update()
    {
        MajScore();
        MajAffichageScore();
    }

    public int GetScore()
    {
        return score;
    }

    /// <summary>
    /// Fonction métant à jour les dizaines et unités
    /// </summary>
    private void MajScore()
    {
        unité = score % 10;
        dizaines = score / 10;
    }

    /// <summary>
    /// met à jour l'affichage du texte du score
    /// </summary>
    private void MajAffichageScore()
    {
        TDizaine.text = dizaines.ToString();
        TUnité.text = unité.ToString();
    }

    /// <summary>
    /// augmente le score d'un point
    /// </summary>
    public void UpScore()
    {
        score += 1;
    }

    /// <summary>
    /// Diminue le score d'un point
    /// </summary>
    public void DownScore()
    {  
        if(score>0)
        {
            score -= 1;
        }
        
    }
}
