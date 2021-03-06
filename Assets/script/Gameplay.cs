﻿using System.Collections;
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
    private bool MainEquipeBleueTampon;

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
                break;
            case 3:
                GestionChangementPhaseCC();
                Phases[3].GetComponent<Cineclub>().CinéClub();
                PrendreMainCineClub();
                break;
            case 4:
                MainEquipeBleue = MainEquipeBleueTampon;
                if (!MainEquipeBleue)
                {
                    AfficherBord(BordR);
                }
                else
                {
                    AfficherBord(BordB);
                }
                Phase = 5;
                break;
            case 5:
                Phases[4].GetComponent<Menus>().Affiche();
                ChangementPhases();
                if (Phase == 6)
                {
                    Phases[4].GetComponent<Animation>().Play("RetirerToutMenus");
                    GestionOverlayScore();
                    Phases[5].GetComponent<TimeAttack>().SetMainTampon(MainEquipeBleueTampon);
                    Phases[5].GetComponent<TimeAttack>().ReadScore(AffichagePointB.GetScore(), AffichagePointR.GetScore());
                }
                break;
            case 6:
                Phases[5].GetComponent<TimeAttack>().TA();
                break;
        }
        InputScore();
    }
    
    private void GestionChangementPhaseCC()
    {
        ChangementPhases();
        if(Phase == 4)
        {
            Phases[3].GetComponent<Cineclub>().GestionAffichage(false);
            Phases[4].GetComponent<Animation>().Play("AfficherAl'AFf");
        }
        if(Phase == 2)
        {
            Phases[3].GetComponent<Cineclub>().GestionAffichage(false);
            Phases[1].GetComponent<Animation>().Play("AfficherImagesMosaique");
        }
    }

    /// <summary>
    /// Gestion de la première équipe qui a buzzé
    /// </summary>
    private void PrendreMainCineClub()
    {
        if (Phases[3].GetComponent<Cineclub>().getetape() == 1)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                AfficherBord(BordB);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                AfficherBord(BordR);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (BordB.color.a > 1)
                {
                    RetirerBord(BordB);
                }
                if (BordR.color.a > 1)
                {
                    RetirerBord(BordR);
                }
            }
        }
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
                MainEquipeBleueTampon = true;
            }
            if (Input.GetKeyDown(KeyCode.N) && Input.GetKey(KeyCode.M))
            {
                AfficherBord(BordR);
                RetirerBord(BordB);
                MainEquipeBleue = false;
                MainEquipeBleueTampon = false;
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
        if (Validation == 0 && !ScoreOverlay)
        {
            ChangementPhases();
            GestionInputMosaiqueAff();
            if(Phase == 3)
            {
                Phases[1].GetComponent<Animation>().Play("RetirerImagesMosaique");
                Phases[3].GetComponent<Cineclub>().AffichageTitre(true);
                ChangementBord(true);
            }
            if (Phase == 1)
            {
                Phases[1].GetComponent<Animation>().Play("RetirerImagesMosaique");
                Phases[0].GetComponent<Animation>().Play("AfficherTOS");
            }
            return;
        }
        if (Validation == 1 && !ScoreOverlay)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Validation = 2;
                Phases[2].GetComponent<Animation>().Play("AfficherQuestion");
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Validation = 0;
                Phases[2].GetComponent<Animation>().Play("RetirerIQ");
                Phases[1].GetComponent<Animation>().Play("AfficherImagesMosaique");
                return;
            }
        }    
        if (Validation >= 2 && !ScoreOverlay)
        {
            GestionReponseMosaique();
        }
    }

    /// <summary>
    /// Gère l'affichage de la proposition choisie, et de la bonne réponse, le retour à la mosaïque et le changment de bord
    /// </summary>
    private void GestionReponseMosaique()
    {
        if (Validation ==2)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                Question.PassageJaunePolice(Phases[2].GetComponent<Momo>().ReponseA);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                Question.PassageJaunePolice(Phases[2].GetComponent<Momo>().ReponseB);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                Question.PassageJaunePolice(Phases[2].GetComponent<Momo>().ReponseC);
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                Question.PassageJaunePolice(Phases[2].GetComponent<Momo>().ReponseD);
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                Validation = 3;
                AffichageBonneRep();
            }
        }
        if (Validation == 3)
        {
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                mosaïque[Phases[2].GetComponent<Momo>().QChoisie].GetComponent<Image>().color = new Vector4(0.8f, 0.5f, 0.5f, 0f);
                Phases[2].GetComponent<Animation>().Play("RetirerTout");
                Phases[1].GetComponent<Animation>().Play("AfficherImagesMosaique");
                ChangementBord();
                Validation = 0;
            }
        }
    }

    private void AffichageBonneRep()
    {
        int[] bR = Phases[2].GetComponent<Momo>().Rep;
        for (int i = 0; i < bR.Length; i++)
        {
            switch (bR[i])
            {
                case 0:
                    Phases[2].GetComponent<Momo>().ReponseA.color = Color.green;
                    break;
                case 1:
                    Phases[2].GetComponent<Momo>().ReponseB.color = Color.green;
                    break;
                case 2:
                    Phases[2].GetComponent<Momo>().ReponseC.color = Color.green;
                    break;
                case 3:
                    Phases[2].GetComponent<Momo>().ReponseD.color = Color.green;
                    break;
            }
        }
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


    /// <summary>
    /// enregistre toutes les informations sur la question.
    /// </summary>
    /// <param name="Q"></param>
    private void EnregistrementQuestionMomo(int Q)
    {
        Phases[2].GetComponent<Momo>().image.overrideSprite = mosaïque[Q].GetComponent<Image>().sprite;
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
                    Phases[2].GetComponent<Animation>().Play("RetirerIQ");
                    break;
                }
                if (Validation >= 2)
                {
                    Phases[2].GetComponent<Animation>().Play("RetirerTout");
                    break;
                }
                break;
            case 3:
                Phases[3].GetComponent<Cineclub>().GestionAffichage(false);
                break;
            case 5:
                Phases[4].GetComponent<Menus>().GestionRetirerAff();
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
                    Phases[2].GetComponent<Animation>().Play("AfficherIQ");
                    break;
                }
                if (Validation >= 2)
                {
                    Phases[2].GetComponent<Animation>().Play("AfficherTout");
                    break;
                }
                break;
            case 3:
                Phases[3].GetComponent<Cineclub>().GestionAffichage(true);
                break;
            case 5:
                Phases[4].GetComponent<Menus>().GestionAfficher();
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
    public void ChangementBord()
    {
        ChangementBord(false);
    }

}
