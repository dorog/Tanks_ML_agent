using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractTankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public GameObject m_ExplosionPrefab;

    private ParticleSystem m_ExplosionParticles;
    private float m_CurrentHealth;

    private void Start()
    {
        m_CurrentHealth = m_StartingHealth;
        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;

        SetHealthUI();

        Extra();

        if (m_CurrentHealth <= 0f)
        {
            OnDeath();
        }
    }

    protected virtual void Extra()
    {

    }

    private void SetHealthUI()
    {
        m_Slider.value = m_CurrentHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        GameObject explosion = Instantiate(m_ExplosionPrefab);

        m_ExplosionParticles = explosion.GetComponent<ParticleSystem>();
        m_ExplosionParticles.transform.position = transform.position;

        AudioSource m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.Play();

        //m_ExplosionAudio.Play();

        LastWish();

        //Destroy(gameObject);
    }

    protected abstract void LastWish();
}
