using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHeath : MonoBehaviour
{
    public float safeTime = 2f;
    public HeathBar heathBar;
    public UnityEvent onDeath;

    float safeTimeCoolDown = 1f;
    [SerializeField] int maxHeath;
    int currentHeath;

    private void OnEnable()
    {
        onDeath.AddListener(Death);
    }

    private void OnDisable()
    {
        onDeath.RemoveListener(Death);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHeath = maxHeath;
        heathBar.UpdateHeath(currentHeath, maxHeath);
    }

    // Update is called once per frame
    void Update()
    {
        safeTimeCoolDown -= Time.deltaTime;
    }

    void Death() { 
        Destroy(heathBar.gameObject);
        Destroy(this.gameObject);
    }

    public void TakeDamge(int damage) {
        if (safeTimeCoolDown <= 0)
        {
            currentHeath -= damage;

            if (currentHeath <= 0)
            {
                currentHeath = 0;
                onDeath.Invoke();
            }
            else {
                safeTimeCoolDown = safeTime;
            }

            heathBar.UpdateHeath(currentHeath, maxHeath);
        }
    }
}
