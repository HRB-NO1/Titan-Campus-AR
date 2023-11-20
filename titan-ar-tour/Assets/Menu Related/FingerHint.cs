using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FingerHint : MonoBehaviour
{
    public Vector2[] movePoints;
    public float moveSpeed = 2f;
    public float waitTime = 1f;
    public int repetitions = 2; // Number of times to repeat the movement

    private RectTransform rectTransform;
    private Vector2 initialPosition;
    private int currentPoint = 0;
    private int completedRepetitions = 0;
    private bool isMoving = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition; // Record the initial position
        StartCoroutine(AnimateFinger());
    }

    IEnumerator AnimateFinger()
    {
        while (completedRepetitions < repetitions)
        {
            yield return new WaitForSeconds(waitTime);

            for (int i = 0; i < movePoints.Length; i++)
            {
                isMoving = true;
                StartCoroutine(MoveToPoint(movePoints[i]));
                yield return new WaitForSeconds(waitTime);
            }

            // Move back to the starting point
            isMoving = true;
            StartCoroutine(MoveToPoint(initialPosition));
            yield return new WaitForSeconds(waitTime);

            currentPoint = 0;
            isMoving = false;
            completedRepetitions++;
        }

        gameObject.SetActive(false); // Deactivate the finger after completing repetitions
    }

    IEnumerator MoveToPoint(Vector2 targetPosition)
    {
        Vector2 startPosition = rectTransform.anchoredPosition; // Record the starting position

        float elapsedTime = 0f;

        while (elapsedTime < waitTime)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
        isMoving = false;
    }
}
