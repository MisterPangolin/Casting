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
    /// Booléén servant à se souvenir de qui à la main à l'instant T
    /// Vraie : Main à l'éaquipe B
    /// Faux : Main à l'équpe R
    /// </summary>
    private bool MainEquipeBleue;

    /// <summary>
    /// liste des canevas de chaques phases
    /// </summary>
    public GameObject[] Phases;

    /// <summary>
    /// Booléen de validation de choix de question de mosaïque ou de menu
    /// 0 = pas sélectioné
    /// 1 = sélectioné 
    /// 2 = validé
    /// </summary>
    private int Validation = 0;

    // =======================================================================================================================================================================
    // =========   Début des fonctions parceque quand même c'est un peut le bordel avec tout ces commentaires                                                    =============
    // =======================================================================================================================================================================

    private void Awake()
    {
        GetComponent<Animation>().Play("AfficherScores");
        ScoreOverlay = true;
        Phase = 1;
        BordB.color = new Vector4(255, 255, 255, 0);
        BordR.color = new Vector4(255, 255, 255, 0);
        Validation = 0;
    }

    private void Update()
    {
        switch(Phase)
        {
            case 1:
                TOS();
                break;

            case 2:
                Momo();
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
    /// Gestion de la phase de Tirage au sort
    /// </summary>
    private void TOS()
    {
        ChangementPhases();
        if (Phases[0].transform.GetChild(0).gameObject.GetComponent<Text>().color.a == 1)
        {
            if (Input.GetKeyDown(KeyCode.B) && Input.GetKey(KeyCode.M))
            {
                AfficherBord(BordB);
                RetirerBord(BordR);
                MainEquipeBleue = true;
            }
            if (Input.GetKeyDown(KeyCode.N) && Input.GetKey(KeyCode.M))
            {
                AfficherBord(BordR);
                RetirerBord(BordB);
                MainEquipeBleue = false;
            }
            if(Phase == 2)
            {
                Phases[0].GetComponent<Animation>().Play("RetirerTOS");
                Phases[1].GetComponent<Animation>().Play("AfficherImagesMosaique");
                Validation = 0;
            }
        }
    }

    /// <summary>
    /// Fonction Gérant la phase de Mosaïque
    /// </summary>
    private void Momo()
    {
        if(Validation == 0 && !ScoreOverlay)
        {
            GestionInputMosaiqueAff();
            return;
        }
        if(Validation == 1 && !ScoreOverlay)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Validation = 2;
                Phases[2].GetComponent<Animation>().Play("AfficherQuestion");
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Validation = 0;
                Phases[2].GetComponent<Animation>().Play("RetirerImage");
                Phases[1].GetComponent<Animation>().Play("AfficherImagesMosaique");
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Validation = 2;
                Phases[2].GetComponent<Animation>().Play("AfficherQestion");
                return;
            }
        }    
        if (Validation == 2 && !ScoreOverlay)
        {
            GestionReponseMosaique();
        }
    }

    /// <summary>
    /// Gère l'affichage de la proposition choisie, et de la bonne réponse, le retour à la mosaïque et le changment de bord
    /// </summary>
    private void GestionReponseMosaique()
    {

    }

    /// <summary>
    /// gère les input quand la mosaïque est affichée
    /// </summary>
    private void GestionInputMosaiqueAff()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EnregistrementQuestionMomo(0);
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            EnregistrementQuestionMomo(1);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EnregistrementQuestionMomo(2);
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EnregistrementQuestionMomo(3);
            return;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            EnregistrementQuestionMomo(4);
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            EnregistrementQuestionMomo(5);
            return;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            EnregistrementQuestionMomo(6);
            return;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            EnregistrementQuestionMomo(7);
            return;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            EnregistrementQuestionMomo(8);
            return;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            EnregistrementQuestionMomo(9);
            return;
        }
    }

    private void EnregistrementQuestionMomo(int Q)
    {
        Phases[2].GetComponent<Momo>().image = mosaïque[Q].GetComponent<Image>();
        Phases[2].GetComponent<Momo>().Question.text = mosaïque[Q].GetComponent<Question>().question;
        Phases[2].GetComponent<Momo>().ReponseA.text = mosaïque[Q].GetComponent<Question>().propositions[0];
        Phases[2].GetComponent<Momo>().ReponseB.text = mosaïque[Q].GetComponent<Question>().propositions[1];
        Phases[2].GetComponent<Momo>().ReponseC.text = mosaïque[Q].GetComponent<Question>().propositions[2];
        Phases[2].GetComponent<Momo>().ReponseD.text = mosaïque[Q].GetComponent<Question>().propositions[3];
        Phases[2].GetComponent<Momo>().Rep = mosaïque[Q].GetComponent<Question>().réponse;
        Validation = 1;
        Phases[2].GetComponent<Momo>().QChoisie = Q;
        Phases[1].GetComponent<Animation>().Play("RetirerImagesMosaique");
        Phases[2].GetComponent<Animation>().Play("AfficherIQ");
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
            case 2:
                if(Validation == 0)
                {
                    Phases[1].GetComponent<Animation>().Play("RetirerImagesMosaique");
                    break;
                }
                if (Validation == 1)
                {
                    Phases[2].GetComponent<Animation>().Play("RetirerImages");
                    break;
                }
                if (Validation == 2)
                {
                    Phases[2].GetComponent<Animation>().Play("RetirerTout");
                    break;
                }
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
            case 2:
                if (Validation == 0)
                {
                    Phases[1].GetComponent<Animation>().Play("AfficherImagesMosaique");
                    break;
                }
                if (Validation == 1)
                {
                    Phases[2].GetComponent<Animation>().Play("AfficherImages");
                    break;
                }
                if (Validation == 2)
                {
                    Phases[2].GetComponent<Animation>().Play("AfficherTout");
                    break;
                }
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

    /// <summary>
    /// Gère le changement de bordure une fois le TOS fait
    /// </summary>
    /// <param name="désactiver">Vraie Si l'on veut arréter d'aaficher le bord</param>
    private void ChangementBord(bool désactiver)
    {
        if(!désactiver)
        {
            if (!MainEquipeBleue)
            {
                AfficherBord(BordB);
                RetirerBord(BordR);
                MainEquipeBleue = true;
                return;
            }
            else
            {
                AfficherBord(BordR);
                RetirerBord(BordB);
                MainEquipeBleue = false;
                return;
            }
        }
        if (désactiver)
        {
            if (!MainEquipeBleue)
            {
                RetirerBord(BordR);
                MainEquipeBleue = true;
                return;
            }
            else
            {
                RetirerBord(BordB);
                MainEquipeBleue = false;
                return;
            }
        }

    }

    /// <summary>
    /// surcharge de la flemme
    /// </summary>
    private void ChangementBord()
    {
        ChangementBord(false);
    }

}
