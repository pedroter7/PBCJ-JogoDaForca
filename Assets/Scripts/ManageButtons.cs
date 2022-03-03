using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageButtons : MonoBehaviour
{
    /// <summary>
    /// esse classe seria o responsavel pelo controle dos botoes da jogo
    /// </summary>
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("score", 0); // zeramos o score
    }

    /*ButtonIniciarJogo
     * esse metedo chamado ao clicar no botao 'Inicar jogo' para carregar cena do Lab1
     */
    public void StartGame()
    {
        SceneManager.LoadScene("Lab1");
    }

    /*ReinciarButton
     * esse metedo chamado ao clicar no botao 'Reinciar o Jogo' para carregar cena do Lab1
     * e inciar um novo jogo
     */
    public void RestartGame()
    {
        StartGame();
    }

    /*HomeButton
     * esse metedo chamado ao clicar no botao 'Ir para tela inicial' para carregar cena do Lab1_start
     */
    public void backToHomeScene()
    {
        SceneManager.LoadScene("Lab1_start");
    }

    /*ButtonCreditos
     * esse metedo chamado ao clicar no botao 'Creditos' para carregar cena do Lab1_creditos
     */
    public void IniciaCreditos()
    {
        SceneManager.LoadScene("Lab1_creditos");
    }
}
