using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject criticalHitIcon;

    [SerializeField] private float nonCritFontSize;
    [SerializeField] private float critFontSize;

    [SerializeField] private float moveUpSpeed = 1f;
    [SerializeField] private float fadeOutSpeed = 2f;
    [SerializeField] private float lifetime = 1f;

    private Color textColor;
    private float remainingLifetime;

    public void Setup(int damageAmount, Color color, bool isCrit = false)
    {
        textMesh.text = damageAmount.ToString();

        textMesh.fontSize = isCrit ? critFontSize : nonCritFontSize;
        textMesh.color = color;
        criticalHitIcon.SetActive(isCrit);

        textColor = textMesh.color;
        remainingLifetime = lifetime;
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveUpSpeed * Time.deltaTime, 0);

        remainingLifetime -= Time.deltaTime;
        if (remainingLifetime < 0)
        {
            textColor.a -= fadeOutSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
