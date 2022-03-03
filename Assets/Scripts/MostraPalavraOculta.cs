using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostraPalavraOculta : MonoBehaviour
{
    /// <summary>
    /// esse classse seria o responsavel por alterar o texto do objeto 'ultimaPalavraOculta' com a palavra
    /// que foi descuberta
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "A palavra oculta era: " + PlayerPrefs.GetString("ultimaPalavraOculta");
    }
}
