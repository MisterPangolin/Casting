using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Classe permetant de gérer les questions, les propositions et la bonne réponse
/// </summary>
public class Question : MonoBehaviour {
    // la question
    public string question;

    // le nombre de proposition (peut être utile pour les menus si plus de 4 réponses possibles)
    public int nbProposition;

    // Tableau de chaine de caractère contenant toutes les propostions
    public string[] propositions;

    // entier correspondant à la référence de la bonne réponse dans le tableau de bonnes réponses
    public int réponse;

    // constructeur par défaut
    Question()
    {
        question = "question";
        nbProposition = 4;
        propositions = new string[nbProposition];
        propositions[0] = "réponse 0";
        propositions[1] = "réponse 1";
        propositions[2] = "réponse 2";
        propositions[3] = "réponse 3";
        réponse = 0;
    }

    /// <summary>
    /// Constructeur complet 4 propositions
    /// </summary>
    /// <param name="_question"> question </param>
    /// <param name="_a"> réponse a</param>
    /// <param name="_b"> réponse b</param>
    /// <param name="_c"> réponse c</param>
    /// <param name="_d"> réponse d</param>
    /// <param name="_réponse"></param>
    Question(string _question, string _a, string _b, string _c, string _d, int _réponse )
    {
        question = _question;
        nbProposition = 4;
        propositions = new string[nbProposition];
        propositions[0] = _a;
        propositions[1] = _b;
        propositions[2] = _c;
        propositions[3] = _d;
        réponse = _réponse;
    }

    /// <summary>
    /// constructuer partiel question à réponse != 4
    /// </summary>
    /// <param name="_question"> question</param>
    /// <param name="_nbProposition"> nombre de propostion</param>
    /// <param name="_réponse"> réf bonne réponse</param>
    Question(string _question, int _nbProposition, int _réponse)
    {
        question = _question;
        nbProposition = _nbProposition;
        propositions = new string[nbProposition];
        for (int i = 0; i< nbProposition; i++ )
        {
            propositions[i] = "réponse " + i;
        }
        réponse = _réponse;
    }
}
