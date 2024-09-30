using UnityEngine;
using UnityEngine.UI;

public class CookingMinigame : MonoBehaviour
{
    public Slider cookingSlider;      // Slider для шкалы
    public Image greenZoneImage;      // Изображение зеленой зоны
    public float sliderSpeed = 2.0f;  // Скорость движения ползунка
    public Button cookButton;         // Кнопка готовки
    private bool movingRight = true;  // Для отслеживания направления движения

    // Пределы зеленой зоны в процентах (от 0 до 1)
    public float greenZoneMin = 0.4f;
    public float greenZoneMax = 0.6f;

    private void Start()
    {
        cookButton.onClick.AddListener(OnCookButtonPressed); // Привязываем функцию к кнопке
        UpdateGreenZone(); // Обновляем отображение зеленой зоны
    }

    private void Update()
    {
        // Логика движения ползунка
        if (movingRight)
        {
            cookingSlider.value += sliderSpeed * Time.deltaTime;
            if (cookingSlider.value >= cookingSlider.maxValue)
            {
                movingRight = false;
            }
        }
        else
        {
            cookingSlider.value -= sliderSpeed * Time.deltaTime;
            if (cookingSlider.value <= cookingSlider.minValue)
            {
                movingRight = true;
            }
        }
    }

    private void OnCookButtonPressed()
    {
        // Проверяем, попал ли ползунок в зеленую зону
        if (cookingSlider.value >= greenZoneMin && cookingSlider.value <= greenZoneMax)
        {
            Debug.Log("Успех! Еда приготовлена правильно.");
        }
        else
        {
            Debug.Log("Неудача! Еда сгорела или недоготовлена.");
        }
    }

    // Функция для обновления размера и позиции зеленой зоны
    private void UpdateGreenZone()
    {
        // Рассчитываем ширину зеленой зоны
        float greenZoneWidth = greenZoneMax - greenZoneMin;

        // Преобразуем ширину в проценты от ширины слайдера
        RectTransform sliderRect = cookingSlider.GetComponent<RectTransform>();
        RectTransform greenZoneRect = greenZoneImage.GetComponent<RectTransform>();

        // Обновляем ширину и позицию зеленой зоны
        greenZoneRect.anchorMin = new Vector2(greenZoneMin, 0);
        greenZoneRect.anchorMax = new Vector2(greenZoneMax, 1);
        greenZoneRect.offsetMin = greenZoneRect.offsetMax = Vector2.zero;
    }
}