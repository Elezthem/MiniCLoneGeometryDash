using UnityEngine;

public class WheelOfFortune : MonoBehaviour
{
    public Transform wheel; // Ссылка на трансформ
    public AnimationCurve rotationCurve; // Кривая для ускорения и замедления

    private float startAngle = 0f; // Произвольный начальный угол
    private float endAngle = 90f; // Конечный угол (0 градусов)

    private float totalTime = 2f; // Общее время анимации
    private float elapsedTime = 0f; // Прошедшее время
    private bool _isGrounded;
    private const float JumpForce = 7;
    [SerializeField] private Rigidbody2D rigidbody2D;
    void Start()
    {
        startAngle = startAngle - 90;
        endAngle = endAngle - 90;
        elapsedTime = totalTime;
    }
    void Update()
    {
        #region Checking

        //if (Input.GetMouseButtonDown(0) && _isGrounded)
        //{
        //    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpForce);

        //}

        if (Input.GetMouseButtonDown(0) && elapsedTime == totalTime)
        {
            startAngle = startAngle + 90f;
            endAngle = endAngle + 90f;
            elapsedTime = 0f;
        }
        #endregion
        #region RotationMethod
        if (elapsedTime < totalTime)
        {
            float progress = elapsedTime / totalTime; // Прогресс от 0 до 1
            float curveValue = rotationCurve.Evaluate(progress); // Оценка значения кривой

            // Вычисление угла вращения на основе кривой
            float currentAngle = Mathf.Lerp(startAngle, endAngle, curveValue);

            // Применение вращения 
            wheel.rotation = Quaternion.Euler(0f, 0f, currentAngle);

            elapsedTime += Time.deltaTime; // Увеличение прошедшего времени
        }
        if (elapsedTime > totalTime)
        {
            elapsedTime = totalTime;
            wheel.rotation = Quaternion.Euler(0f, 0f, endAngle);
        }
        #endregion
    }
}
