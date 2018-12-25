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
    public Points AffichagePointB;

    /// <summary>
    /// score équipe 2
    /// </summary>
    public Points AffichagePointR;

    /// <summary>
    /// booléen qui permet de savoir si le score est affiché ou non 
    /// </summary>
    private bool ScoreOverlay = true;


    /// <summary>
    /// tableau contenant les références aux préfabs des canevas de toutes les questions de la mosaïque
    /// </summary>
    public GameObject[] mosaïque;

    

    private void Update()
    {
        InputScore();
    }



    /// <summary>
    /// augmente le score de 1
    /// </summary>
    /// <param name="_UpScore">Score à augmenter</param>
    private void UpScore( Points _UpScore)
    {
        _UpScore.UpScore();
    }

    /// <summary>
    /// diminue le score de 1
    /// </summary>
    /// <param name="_DownScore">Score à diminuer</param>
    private void DownScore(Points _DownScore)
    {
        _DownScore.DownScore();
    }

    /// <summary>
    /// Gère les Input clavier pour la mise à jour des scores
    /// </summary>
    private void InputScore()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GestionOverlayScore();
        }
        if (Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            UpScore(AffichagePointB);
        }
        if (Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DownScore(AffichagePointB);
        }
        if (Input.GetKey(KeyCode.N) && Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            UpScore(AffichagePointR);
        }
        if (Input.GetKey(KeyCode.N) && Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DownScore(AffichagePointR);
        }
    }

    private void GestionOverlayScore()
    {
        if (ScoreOverlay)
        {
            GetComponent<Animation>().Play("RetirerScores");
            ScoreOverlay = false;
            return;
        }
        GetComponent<Animation>().Play("AfficherScores");
        ScoreOverlay = true;
        return;
    }
}
