using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cineclub : MonoBehaviour {

    public Text Proposition1;
    public Text Proposition2;
    public Text LesDeux;
    private int etape = 0;

    /// <summary>
    /// Compte le nombre de sel ou poivre à faire
    /// </summary>
    private int Compte = 0;


    /// <summary>
    /// Met à jour le thème en cours
    /// </summary>
    public void MAJTexte()
    {
        if (Compte < GetComponent<Transform>().childCount)
        {
            Proposition1.text = GetComponent<Transform>().GetChild(Compte).GetComponent<SelPoivre>().Proposition1;            
            if (GetComponent<Transform>().GetChild(Compte).GetComponent<SelPoivre>().LesDeux)
            {
                Proposition2.text = GetComponent<Transform>().GetChild(Compte).GetComponent<SelPoivre>().Proposition2;
                LesDeux.text = "ou les deux ?";
                return;
            }
            else
            {
                Proposition2.text ="ou " + GetComponent<Transform>().GetChild(Compte).GetComponent<SelPoivre>().Proposition2 + " ?";
                LesDeux.text = "";
            }
        }
    }

    public int getetape()
    {
        return etape;
    }

    public void CinéClub()
    {
        if (etape ==0)
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log(etape);
                MAJTexte();
                etape = 1;
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
        if (etape == 1)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log(etape);
                etape = 0;
                AffichageTheme(false);
                Compte += 1;
            }
        }
    }

    public void ResetCompte()
    {
        Compte = 0;
    }

    public void AddCompte()
    {
        Compte += 1;
    }

    public void SubCompte()
    {
        Compte -= 1;
    }

    /// <summary>
    /// Afficher ou retirer "CinéClub"
    /// </summary>
    /// <param name="oui">true : afficher // false : retirer</param>
    public void AffichageTitre(bool oui)
    {
        if (oui)
        {
            GetComponent<Transform>().GetComponentInParent<Animation>().Play("AfficherCC");
            return;
        }
        if (oui == false)
        {
            GetComponent<Transform>().GetComponentInParent<Animation>().Play("RetirerCC");
            return;
        }
    }

    /// <summary>
    /// Afficher ou retirer les theme
    /// </summary>
    /// <param name="oui">true : afficher // false : retirer</param>
    public void AffichageTheme(bool oui)
    {
        if (oui)
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
    /// <param name="oui">true : afficher // false : retirer</param>
    public void Affichagetout(bool oui)
    {
        if (oui)
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
        switch(etape)
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
