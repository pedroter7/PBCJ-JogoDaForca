using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// classe principal do jogo
/// controla a lógica e regras do jogo
/// </summary>
public class GameManager : MonoBehaviour
{
    private int numberAttempts;
    private int maxAttempts;
    private int score;

    public GameObject center;//prefab da letra no game
    public GameObject letter;//centro de teste que indica o centro da tela  

    private string hiddenWord = "";//palavra ser descoberta
    private int hiddenWordLength; //tamanho da palavra
    char[] hiddenLetters;//letras da palavra ocultas
    bool[] foundLetters;//idicador de quais letras foram descobertas

    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.Find("screenCenter");
        InitGame();
        InitLetter();
        score = 0;
        numberAttempts = 0;
        maxAttempts = hiddenWordLength + 3;
        UpdateNumberAttempts();
        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeyBoard();
        VerificaJogo();
    }

    void InitLetter()
    {
        int wordLenght = hiddenWordLength;
        for (int i = 0; i < wordLenght; i++)
        {
            Vector3 newPosition;
            newPosition = new Vector3(center.transform.position.x + ((i-wordLenght/2.0f)*80), center.transform.position.y, center.transform.position.z);
            GameObject newLetter = (GameObject)Instantiate(letter, newPosition, Quaternion.identity);
            newLetter.name = "letra" + (i + 1); // nomeia-se na hierarquia a gameObject com letra-(iesima+1), i=1.numLetras
            newLetter.transform.SetParent(GameObject.Find("Canvas").transform); //posiciona-se como filho do GameObject Canvas
        }
    }
    
    void InitGame() 
    {
        hiddenWord = RandomWord(); //pegando palavra a partir de uma funcao
        hiddenWord = hiddenWord.ToUpper();      //transforma-se a palavra el maiuscula
        hiddenWordLength = hiddenWord.Length;   //determina o quantidade das letras da palavra
        foundLetters = new bool[hiddenWordLength+1];//instancia-se o array bool do indicador de letras acertos
        hiddenLetters = hiddenWord.ToCharArray(); // copia-se a palavra no array de letras
    }

    // Verifica se alguma tecla foi pressionada
    void CheckKeyBoard()
    {
        if(Input.anyKeyDown)
        {
            char keyDown = Input.inputString.ToCharArray()[0];
            int keyInt = System.Convert.ToInt32(keyDown);

            if(keyInt >= 97 && keyInt <= 122)
            {
                numberAttempts++;
                UpdateNumberAttempts();

                bool letraEncontrada = false;
                
                for (int i=0; i<=hiddenWordLength-1; i++)
                {
                    if(!foundLetters[i])
                    {
                        keyDown = System.Char.ToUpper(keyDown);
                        if (keyDown == hiddenLetters[i])
                        {
                            letraEncontrada = true;
                            foundLetters[i] = true;
                            GameObject.Find("letra" + (i + 1)).GetComponent<Text>().text = keyDown.ToString();
                            score = PlayerPrefs.GetInt("score");
                            score++;
                            PlayerPrefs.SetInt("score", score);

                            UpdateScoreUI();
                        }                    
                    }                   
                }              
                EfeitoPosTentativa(letraEncontrada);
            }
        }
    }
    //carrga som de acordo se o jogador acertar ou nao a tecla
    private void EfeitoPosTentativa(bool letraEncontrada)
    {
        if (letraEncontrada)
        {
            GameObject.Find("RightEffect").GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("WrongEffect").GetComponent<AudioSource>().Play();
        }
    }
    //altera o numero das tentativas do jogador
    private void UpdateNumberAttempts()
    {
        GameObject.Find("attempts").GetComponent<Text>().text = "Tentativas: " + numberAttempts + " | " + maxAttempts;
        
    }
    /*verifica se o jogador ganhou ou perdeu
     *carrega a cena Lab1_salvo ou Lab1_forca dependendo do resultado
     */
    private void VerificaJogo()
    {
        if (score == hiddenWordLength)
        {
            PlayerPrefs.SetString("ultimaPalavraOculta", hiddenWord);
            SceneManager.LoadScene("Lab1_salvo");
        } else if (numberAttempts >= maxAttempts)
        {
            SceneManager.LoadScene("Lab1_forca");
        }
    }
    //Altera Score do jogador
    private void UpdateScoreUI()
    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Pontua��o: " + score;
    }
    /*importando um arquivo texto
     * onde esta armazenada nossas palavras do jogo 
     * respeitando que deve ter um delimitador de um eapco (' ') para separar as palavras
     */
    string[] PegaListaDePalavrasDoArquivo()
    {
        TextAsset palavrasTextAsset = (TextAsset)Resources.Load("palavras", typeof(TextAsset));
        string palavrasJuntas = palavrasTextAsset.text;
        string[] listaDePalavras = palavrasJuntas.Split(' ');
        return listaDePalavras;
    }
    //este funcao pega palavra de forma aleatoria 
    string RandomWord()
    {
        string[] randomWords = PegaListaDePalavrasDoArquivo();
        int randomIndex = Random.Range(0, randomWords.Length);
        return randomWords[randomIndex];
    }
}