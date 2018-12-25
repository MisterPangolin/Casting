using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    /// Entier gérant la phase de jeu : 
    /// 1 - Tirage au sort
    /// 2 - mosaïque
    /// 3 - sel ou poivre
    /// 4 - Menu
    /// 5 - additionù
    /// 6 - Burger de la mort
    /// </summary>
    private int Phase;
    private int PhaseMax = 6;

    /// <summary>
    /// tableau contenant les références aux préfabs des canevas de toutes les questions de la mosaïque
    /// </summary>
    public GameObject[] mosaïque;

    /// <summary>
    /// Bordure Rouge indiquant quelle équipe joue
    /// </summary>
    public Image BordR;

    /// <summary>
    /// Bordure Bleue indiquant quelle équipe joue
    /// </summary>
    public Image BordB;

    /// <summary>
    /// liste des canevas de chaques phases
    /// </summary>
    public GameObject[] Phases;


    private void Awake()
    {
        Phases[0].GetComponent<Animation>().Play("RetirerTOS");
        GetComponent<Animation>().Play("AfficherScores");
        ScoreOverlay = true;
        Phase = 1;
        BordB.color = new Vector4(255, 255, 255, 0);
        BordR.color = new Vector4(255, 255, 255, 0);
    }

    private void Update()
    {
        switch(Phase)
        {
            case 1:
                TOS();
                break;

            case 2:
                ChangementPhases();
                break;

        }
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

    /// <summary>
    /// Gère l'apparition et la disparition des scores
    /// </summary>
    private void GestionOverlayScore()
    {
        if (ScoreOverlay)
        {
            SwitchAfficher();
            GetComponent<Animation>().Play("RetirerScores");
            ScoreOverlay = false;
            return;
        }
        GetComponent<Animation>().Play("AfficherScores");
        SwitchRetirer();
        ScoreOverlay = true;
        return;
    }

    private void TOS()
    {
        ChangementPhases();
        if (Phases[0].transform.GetChild(0).gameObject.GetComponent<Text>().color.a == 1)
        {
            if (Input.GetKeyDown(KeyCode.B) && Input.GetKey(KeyCode.M))
            {
                AfficherBord(BordB);
                RetirerBord(BordR);
            }
            if (Input.GetKeyDown(KeyCode.N) && Input.GetKey(KeyCode.M))
            {
                AfficherBord(BordR);
                RetirerBord(BordB);
            }
            if(Phase == 2)
            {
                Phases[0].GetComponent<Animation>().Play("RetirerTOS");
            }
        }
    }

    /// <summary>
    /// Fonction gérant la disparition de l'affichage lorsque que l'on souhaite afficher les scores
    /// </summary>
    private void SwitchRetirer()
    {
        switch (Phase)
        {
            case 1:
                Phases[0].GetComponent<Animation>().Play("RetirerTOS");
                break;
        }
    }

    /// <summary>
    /// Fonction gérant l'aparition de l'affichage lorsque que l'on souhaite revenir au jeu
    /// </summary>
    private void SwitchAfficher()
    {
        switch (Phase)
        {
            case 1:
                Phases[0].GetComponent<Animation>().Play("AfficherTOS");
                break;
        }
    }

    /// <summary>
    /// Permet d'afficher un bord d'équipe
    /// </summary>
    /// <param name="bord">Bord à afficher</param>
    private void AfficherBord (Image bord)
    {
        bord.color = new Vector4(255, 255, 255, 255);
    }

    /// <summary>
    /// Permet de retirer un bord d'équipe
    /// </summary>
    /// <param name="bord">Bord à retirer</param>
    private void RetirerBord(Image bord)
    {
        bord.color = new Vector4(255, 255, 255, 0);
    }

    private void ChangementPhases()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.KeypadPlus) && Phase < PhaseMax)
        {
            Phase += 1;
            Debug.Log("phase = " + Phase);
        }
        if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.KeypadMinus) && Phase > 1)
        {
            Phase -= 1;
            Debug.Log("phase = " + Phase);
        }
    }

}
