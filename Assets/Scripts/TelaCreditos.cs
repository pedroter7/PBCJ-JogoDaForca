using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// esta classe controla a tela de creditos 
/// </summary>
public class TelaCreditos : MonoBehaviour
{

    public float speed;//variavel indica a velocidade da transformacao da posicao 
    public int fps;////variavel indica a taxa de atualizacao da minha tela
    Vector3 posicao;
    // Start is called before the first frame update
    private void Start()
    {
        QualitySettings.vSyncCount = 0; //O numero de VSyncs que devem passar entre cada quadro
        Application.targetFrameRate = fps; //limitando os fps a minha tela
    }
    void Update()
    {
        LoadCreditos();
    }
    
    /*Metedo LoadCreditos() sera o responsavel para mudar posicao do meu objeto no eixo Y
     *de acordo com speed informado pelo jogador.    
     *sera chamado no metedo Update(), para que seja chamado cada frame
     */
    void LoadCreditos()
    {        
        posicao = new Vector3(transform.position.x , transform.position.y + speed, transform.position.z) ;
        transform.position = posicao;
        if (transform.position.y >= 1000)
        {
            posicao = new Vector3(transform.position.x, transform.position.y - 1000, transform.position.z);
            transform.position = posicao;
        }        
    }
}
