using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Scritp gérant les inputs claviers, les différentes phases du jeu,
/// l'affichage des scores, bref, le gros du jeu
/// </summary>
public class Gameplay : MonoBehaviour {


    #region Variables

    /// <summary>
    /// Score équipe 1
    /// </summary>
    public Points affichage_points_bleus_;

    /// <summary>
    /// score équipe 2
    /// </summary>
    public Points affichage_points_rouges_;

    /// <summary>
    /// booléen qui permet de savoir si le score est affiché ou non 
    /// </summary>
    private bool score_overlay_ = true;

    /// <summary>
    /// Entier gérant la phase de jeu : 
    /// 1 - Tirage au sort
    /// 2 - mosaïque
    /// 3 - sel ou poivre
    /// 4 - Menu
    /// 5 - additionù
    /// 6 - Burger de la mort
    /// </summary>
    private int phase_;
    private readonly int phase_max_ = 6;

    /// <summary>
    /// tableau contenant les références aux préfabs des canvas de toutes les questions de la mosaïque
    /// </summary>
    public GameObject[] mosaique_;

    /// <summary>
    /// Bordure Rouge indiquant que l'équipe rouge joue
    /// </summary>
    public Image bord_rouge_;

    /// <summary>
    /// Bordure Bleue indiquant que l'équipe bleue joue
    /// </summary>
    public Image bord_bleu_;

    /// <summary>
    /// Booléén servant à se souvenir de qui a la main à l'instant T
    /// Vraie : Main à l'équipe B
    /// Faux : Main à l'équipe R
    /// </summary>
    private bool main_equipe_bleue_;

    /// <summary>
    /// liste des canvas de chaque phase
    /// </summary>
    public GameObject[] phases_;

    /// <summary>
    /// Entier de validation de choix de question de mosaïque ou de menu
    /// 0 = pas sélectioné
    /// 1 = sélectioné 
    /// 2 = validé
    /// </summary>
    private int validation_ = 0;

    #endregion

    #region Functions

    // =======================================================================================================================================================================
    // =========   Début des fonctions parce que quand même c'est un peu le bordel avec tout ces commentaires                                                    =============
    // =======================================================================================================================================================================

    #region MonoBehaviour callbacks

    private void Awake()
    {
        GetComponent<Animation>().Play("AfficherScores");
        score_overlay_ = true;
        phase_ = 1;
        bord_bleu_.color = new Vector4(255, 255, 255, 0);
        bord_rouge_.color = new Vector4(255, 255, 255, 0);
        validation_ = 0;
    }

    private void Update()
    {
        switch(phase_)
        {
            case 1:
                TOS();
                break;

            case 2:
                PhaseMosaique();
                break;
            case 3:
                GestionChangementPhaseCC();
                phases_[3].GetComponent<Cineclub>().CinéClub();
                PrendreMainCineClub();
                break;
        }
        InputScore();
    }

    #endregion

    private void GestionChangementPhaseCC()
    {
        ChangementPhases();
        if(phase_ == 4)
        {
            phases_[3].GetComponent<Cineclub>().GestionAffichage(false);
        }
    }

    private void PrendreMainCineClub()
    {
        if (phases_[3].GetComponent<Cineclub>().getetape() == 1)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                AfficherBord(bord_bleu_);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                AfficherBord(bord_rouge_);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (bord_bleu_.color.a > 1)
                {
                    RetirerBord(bord_bleu_);
                }
                if (bord_rouge_.color.a > 1)
                {
                    RetirerBord(bord_rouge_);
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
    /// Gère les Inputs clavier pour la mise à jour des scores
    /// </summary>
    private void InputScore()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GestionOverlayScore();
        }
        if (Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            UpScore(affichage_points_bleus_);
        }
        if (Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DownScore(affichage_points_bleus_);
        }
        if (Input.GetKey(KeyCode.N) && Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            UpScore(affichage_points_rouges_);
        }
        if (Input.GetKey(KeyCode.N) && Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DownScore(affichage_points_rouges_);
        }
    }

    /// <summary>
    /// Gestion de la phase de Tirage au sort
    /// </summary>
    private void TOS()
    {
        ChangementPhases();
        if (phases_[0].transform.GetChild(0).gameObject.GetComponent<Text>().color.a == 1)
        {
            if (Input.GetKeyDown(KeyCode.B) && Input.GetKey(KeyCode.M))
            {
                AfficherBord(bord_bleu_);
                RetirerBord(bord_rouge_);
                main_equipe_bleue_ = true;
            }
            if (Input.GetKeyDown(KeyCode.N) && Input.GetKey(KeyCode.M))
            {
                AfficherBord(bord_rouge_);
                RetirerBord(bord_bleu_);
                main_equipe_bleue_ = false;
            }
            if(phase_ == 2)
            {
                phases_[0].GetComponent<Animation>().Play("RetirerTOS");
                phases_[1].GetComponent<Animation>().Play("AfficherImagesMosaique");
                validation_ = 0;
            }
        }
    }

    /// <summary>
    /// Fonction gérant la phase de Mosaïque
    /// </summary>
    private void PhaseMosaique()
    {        
        if (validation_ == 0 && !score_overlay_)
        {
            ChangementPhases();
            GestionInputMosaiqueAff();
            if(phase_ == 3)
            {
                phases_[1].GetComponent<Animation>().Play("RetirerImagesMosaique");
                phases_[3].GetComponent<Cineclub>().AffichageTitre(true);
                ChangementBord(true);
            }
            if (phase_ == 1)
            {
                phases_[1].GetComponent<Animation>().Play("RetirerImagesMosaique");
                phases_[0].GetComponent<Animation>().Play("AfficherTOS");
                ChangementBord(true);
            }
            return;
        }
        if (validation_ == 1 && !score_overlay_)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                validation_ = 2;
                phases_[2].GetComponent<Animation>().Play("AfficherQuestion");
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                validation_ = 0;
                phases_[2].GetComponent<Animation>().Play("RetirerIQ");
                phases_[1].GetComponent<Animation>().Play("AfficherImagesMosaique");
                return;
            }
        }    
        if (validation_ >= 2 && !score_overlay_)
        {
            GestionReponseMosaique();
        }
    }

    /// <summary>
    /// Gère l'affichage de la proposition choisie, et de la bonne réponse,
    /// le retour à la mosaïque et le changement de bordure.
    /// </summary>
    private void GestionReponseMosaique()
    {
        if (validation_ ==2)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                SwitchCouleurTexteSelection(phases_[2].GetComponent<Mosaique>().ReponseA);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                SwitchCouleurTexteSelection(phases_[2].GetComponent<Mosaique>().ReponseB);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                SwitchCouleurTexteSelection(phases_[2].GetComponent<Mosaique>().ReponseC);
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                SwitchCouleurTexteSelection(phases_[2].GetComponent<Mosaique>().ReponseD);
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                validation_ = 3;
                AffichageBonneRep();
            }
        }
        if (validation_ == 3)
        {
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                mosaique_[phases_[2].GetComponent<Mosaique>().QChoisie].GetComponent<Image>().color = new Vector4(0.5f, 0.5f, 0.5f, 0f);
                phases_[2].GetComponent<Animation>().Play("RetirerTout");
                phases_[1].GetComponent<Animation>().Play("AfficherImagesMosaique");
                ChangementBord();
                validation_ = 0;
            }
        }
    }

    private void AffichageBonneRep()
    {
        int[] bonne_reponse = phases_[2].GetComponent<Mosaique>().Rep; // numéro(s) de la (des) bonne(s) réponse(s)
        for (int i = 0; i < bonne_reponse.Length; i++)
        {
            switch (bonne_reponse[0])
            {
                case 0:
                    phases_[2].GetComponent<Mosaique>().ReponseA.color = Color.green;
                    break;
                case 1:
                    phases_[2].GetComponent<Mosaique>().ReponseB.color = Color.green;
                    break;
                case 2:
                    phases_[2].GetComponent<Mosaique>().ReponseC.color = Color.green;
                    break;
                case 3:
                    phases_[2].GetComponent<Mosaique>().ReponseD.color = Color.green;
                    break;
            }
        }
    }

    private void SwitchCouleurTexteSelection(Text texte_couleur_a_changer )
    {
        if(texte_couleur_a_changer.color == Color.yellow)
        {
            texte_couleur_a_changer.color = Color.white;
            return;
        }
        else
        {
            texte_couleur_a_changer.color = Color.yellow;
            return;
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
        phases_[2].GetComponent<Mosaique>().image = mosaique_[Q].GetComponent<Image>();
        phases_[2].GetComponent<Mosaique>().Question.text = mosaique_[Q].GetComponent<Question>().question;
        phases_[2].GetComponent<Mosaique>().ReponseA.text = mosaique_[Q].GetComponent<Question>().propositions[0];
        phases_[2].GetComponent<Mosaique>().ReponseB.text = mosaique_[Q].GetComponent<Question>().propositions[1];
        phases_[2].GetComponent<Mosaique>().ReponseC.text = mosaique_[Q].GetComponent<Question>().propositions[2];
        phases_[2].GetComponent<Mosaique>().ReponseD.text = mosaique_[Q].GetComponent<Question>().propositions[3];
        phases_[2].GetComponent<Mosaique>().Rep = mosaique_[Q].GetComponent<Question>().réponse;
        validation_ = 1;
        phases_[2].GetComponent<Mosaique>().QChoisie = Q;
        phases_[1].GetComponent<Animation>().Play("RetirerImagesMosaique");
        phases_[2].GetComponent<Animation>().Play("AfficherIQ");
    }

    /// <summary>
    /// Gère l'apparition et la disparition des scores
    /// </summary>
    private void GestionOverlayScore()
    {
        if (score_overlay_)
        {
            SwitchAfficher();
            GetComponent<Animation>().Play("RetirerScores");
            score_overlay_ = false;
            return;
        }
        GetComponent<Animation>().Play("AfficherScores");
        SwitchRetirer();
        score_overlay_ = true;
        return;
    }

    /// <summary>
    /// Fonction gérant la disparition de l'affichage lorsque que l'on souhaite afficher les scores
    /// </summary>
    private void SwitchRetirer()
    {
        switch (phase_)
        {
            case 1:
                phases_[0].GetComponent<Animation>().Play("RetirerTOS");
                break;
            case 2:
                if(validation_ == 0)
                {
                    phases_[1].GetComponent<Animation>().Play("RetirerImagesMosaique");
                    break;
                }
                if (validation_ == 1)
                {
                    phases_[2].GetComponent<Animation>().Play("RetirerIQ");
                    break;
                }
                if (validation_ >= 2)
                {
                    phases_[2].GetComponent<Animation>().Play("RetirerTout");
                    break;
                }
                break;
            case 3:
                phases_[3].GetComponent<Cineclub>().GestionAffichage(false);
                break;
        }
    }

    /// <summary>
    /// Fonction gérant l'aparition de l'affichage lorsque que l'on souhaite revenir au jeu
    /// </summary>
    private void SwitchAfficher()
    {
        switch (phase_)
        {
            case 1:
                phases_[0].GetComponent<Animation>().Play("AfficherTOS");
                break;
            case 2:
                if (validation_ == 0)
                {
                    phases_[1].GetComponent<Animation>().Play("AfficherImagesMosaique");
                    break;
                }
                if (validation_ == 1)
                {
                    phases_[2].GetComponent<Animation>().Play("AfficherIQ");
                    break;
                }
                if (validation_ >= 2)
                {
                    phases_[2].GetComponent<Animation>().Play("AfficherTout");
                    break;
                }
                break;
            case 3:
                phases_[3].GetComponent<Cineclub>().GestionAffichage(true);
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
        if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.KeypadPlus) && phase_ < phase_max_)
        {
            phase_ += 1;
            Debug.Log("phase = " + phase_);
        }
        if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.KeypadMinus) && phase_ > 1)
        {
            phase_ -= 1;
            Debug.Log("phase = " + phase_);
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
            if (!main_equipe_bleue_)
            {
                AfficherBord(bord_bleu_);
                RetirerBord(bord_rouge_);
                main_equipe_bleue_ = true;
                return;
            }
            else
            {
                AfficherBord(bord_rouge_);
                RetirerBord(bord_bleu_);
                main_equipe_bleue_ = false;
                return;
            }
        }
        if (désactiver)
        {
            if (!main_equipe_bleue_)
            {
                RetirerBord(bord_rouge_);
                main_equipe_bleue_ = true;
                return;
            }
            else
            {
                RetirerBord(bord_bleu_);
                main_equipe_bleue_ = false;
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

    #endregion

}
