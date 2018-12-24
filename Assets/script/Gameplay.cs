using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Scritp gérant les inputs claviers, les différentes phases du jeu,
/// l'affichage des scores, bref, le gros du jeu
/// </summary>
public class Gameplay : MonoBehaviour {
    
    /// <summary>
    /// Score équipe 1
    /// </summary>
    public int score1;

    /// <summary>
    /// score équipe 2
    /// </summary>
    public int score2;


    /// <summary>
    /// tableau contenant les références aux préfabs des canevas de toutes les questions de la mosaïque
    /// </summary>
    public GameObject[] mosaïque;

    

    private void Awake()
    {
    }
}
