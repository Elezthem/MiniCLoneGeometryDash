using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private const float Speed = 6;
    private const float JumpForce = 7;
    private const float BackgroundSpeed = 1;  // Скорость фона
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private AudioSource audioSource;
    private bool _isGrounded;

    [SerializeField] private Transform groundCheckObject;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform bg;
    [SerializeField] private GameObject DeadSong;
    [SerializeField] private Text scoreText;  // Text field for displaying the score
    [SerializeField] private GameObject panel;
    [SerializeField] private Button continueButton;
    [SerializeField] private Transform stopCameraAt;
    [SerializeField] private Button menuButton;
    private int score = 0;  // Score variable
    private float initialBgPositionX;  // Начальная позиция фона

    private Vector3 initialPosition;  // Добавляем переменную для хранения начальной позиции

    private void Start()
    {
        initialPosition = transform.position;  // Сохраняем начальную позицию при запуске сцены
        initialBgPositionX = bg.position.x;
        continueButton.onClick.AddListener(ContinueGame);  // Добавляем слушатель для кнопки продолжения игры
        continueButton.gameObject.SetActive(false);
        menuButton.onClick.AddListener(ReloadScene);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Если нажата клавиша Esc, то показываем/скрываем панель
            panel.SetActive(!panel.activeSelf);
            continueButton.gameObject.SetActive(panel.activeSelf);

            if (panel.activeSelf)
            {
                // Если панель активна, останавливаем игру и музыку
                Time.timeScale = 0;
                audioSource.Pause();
            }
            else
            {
                // Если панель не активна, возобновляем игру и музыку
                Time.timeScale = 1;
                audioSource.UnPause();
            }
        }

        if (!Global.Playmode) return;
        _isGrounded = Physics2D.OverlapCircle(groundCheckObject.position, 0.1f, layerMask);

        if (_isGrounded)
        {
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
        }

        transform.Translate(new Vector2(Speed * Time.deltaTime, 0));
        // Двигаем фон с меньшей скоростью
        float bgMove = Speed * BackgroundSpeed * Time.deltaTime;
        bg.Translate(new Vector2(bgMove, 0));

        Jump();

        ParticleSystemSetSpeed();
    }

    private void ParticleSystemSetSpeed()
    {
        var velocityOverLifeTime = particleSystem.velocityOverLifetime;
        velocityOverLifeTime.x = rigidbody2D.velocity.x;
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0) && _isGrounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpForce);

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            DieAndRespawn();
            audioSource.Play();
            Invoke("HideDeadSong", 0.1f);

            score++;
            UpdateScoreText();
        }
    }

    private void DieAndRespawn()
    {
        transform.position = initialPosition;

        bg.position = new Vector3(initialBgPositionX, bg.position.y, bg.position.z);
        DeadSong.SetActive(false);
    }

    private void HideDeadSong()
    {
        // Инвертируем состояние активности объекта DeadSong
        DeadSong.SetActive(!DeadSong.activeSelf);
    }

    private void UpdateScoreText()
    {
        // Update the text field with the current score
        scoreText.text = "ПОПЫТКИ: " + score.ToString();
    }

    private void ContinueGame()
    {
        // Продолжаем игру, скрываем панель и кнопку
        panel.SetActive(false);
        continueButton.gameObject.SetActive(false);
        Time.timeScale = 1;
        audioSource.UnPause();
    }

    private void ReloadScene()
    {
        // Перезагружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
