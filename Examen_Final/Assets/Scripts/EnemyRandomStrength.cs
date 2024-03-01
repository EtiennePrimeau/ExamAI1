using UnityEngine;

public class EnemyRandomStrength : MonoBehaviour
{
    [SerializeField] private int m_strength = 0;
    [SerializeField] private int m_minimumStrength = 0;
    [SerializeField] private int m_maximumStrength = 100;

    void Start()
    {
        m_strength = GenerateRandomStrength();
    }


    private int GenerateRandomStrength()
    {
        int strength = Random.Range(m_minimumStrength, m_maximumStrength);
        return strength;
    }

    public int GetStrength()
    {
        return m_strength;
    }

}
