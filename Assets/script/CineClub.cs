using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CineClub : MonoBehaviour {

    public Text proposition1_;
    public Text proposition2_;
    public Text les_deux_;
    private int etape_ = 0;

    public int Etape_
    {
        get
        {
            return etape_;
        }
    }

    /// <summary>
    /// Compte le nombre de sel ou poivre à faire
    /// </summary>
    private int compte_ = 0;


    /// <summary>
    /// Met à jour le thème en cours
    /// </summary>
    public void MAJTexte()
    {
        if (compte_ < GetComponent<Transform>().childCount)
        {
            proposition1_.text = GetComponent<Transform>().GetChild(compte_).GetComponent<SelPoivre>().Proposition1;            
            if (GetComponent<Transform>().GetChild(compte_).GetComponent<SelPoivre>().LesDeux)
            {
                proposition2_.text = GetComponent<Transform>().GetChild(compte_).GetComponent<SelPoivre>().Proposition2;
                les_deux_.text = "ou les deux ?";
                return;
            }
            else
            {
                proposition2_.text ="ou " + GetComponent<Transform>().GetChild(compte_).GetComponent<SelPoivre>().Proposition2 + " ?";
                les_deux_.text = "";
            }
        }
    }

    public void CinéClub()
    {
        if (etape_ ==0)
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log(etape_);
                MAJTexte();
                etape_ = 1;
                AffichageTheme(true);
                return;
            }
            if(Input.GetKey(KeyCode.P))
            {
                if(Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    AddCompte();
                }
                if (Input.GetKeyDown(KeyCode.KeypadMinus))
                {
                    SubCompte();
                }
            }
        }
        if (etape_ == 1)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log(etape_);
                etape_ = 0;
                AffichageTheme(false);
                compte_ += 1;
            }
        }
    }

    public void ResetCompte()
    {
        compte_ = 0;
    }

    public void AddCompte()
    {
        compte_ += 1;
    }

    public void SubCompte()
    {
        compte_ -= 1;
    }

    /// <summary>
    /// Afficher ou retirer "CinéClub"
    /// </summary>
    /// <param name="afficher">true : afficher // false : retirer</param>
    public void AffichageTitre(bool afficher)
    {
        if (afficher)
        {
            GetComponent<Transform>().GetComponentInParent<Animation>().Play("AfficherCC");
            return;
        }
        else
        {
            GetComponent<Transform>().GetComponentInParent<Animation>().Play("RetirerCC");
            return;
        }
    }

    /// <summary>
    /// Afficher ou retirer les thèmes
    /// </summary>
    /// <param name="afficher">true : afficher // false : retirer</param>
    public void AffichageTheme(bool afficher)
    {
        if (afficher)
        {
            GetComponent<Transform>().GetComponentInParent<Animation>().Play("AfficherTheme");
            return;
        }
        else
        {
            GetComponent<Transform>().GetComponentInParent<Animation>().Play("RetirerTheme");
            return;
        }
    }

    /// <summary>
    /// Afficher ou retirer tout 
    /// </summary>
    /// <param name="afficher">true : afficher // false : retirer</param>
    public void Affichagetout(bool afficher)
    {
        if (afficher)
        {
            GetComponent<Transform>().GetComponentInParent<Animation>().Play("AfficherTout");
            return;
        }
        else
        {
            GetComponent<Transform>().GetComponentInParent<Animation>().Play("RetirerTout");
            return;
        }
    }

    public void GestionAffichage(bool oui)
    {
        switch(etape_)
        {
            case 0:
                AffichageTitre(oui);
                break;
            case 1:
                Affichagetout(oui);
                break;
        }

    }
}
