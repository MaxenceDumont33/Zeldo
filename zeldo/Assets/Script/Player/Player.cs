using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private int life;
    private Move moveRef;
    private Bow bowRef;
    private SwitchWeapon switchWeaponRef;
    private Sword swordRef;
    private Interact interactRef;
    private void Start()
    {
        moveRef = GetComponent<Move>();
        bowRef = GetComponent<Bow>();
        switchWeaponRef = GetComponent<SwitchWeapon>();
        swordRef = GetComponent<Sword>();
        interactRef = GetComponent<Interact>();
    }
    public void Damage(int damage)
    {
        life -= damage;
        print("aie");
        if (life <=0)
        {
            PlayerDead();
        }
    }
    public void PlayerDead()
    {
        moveRef.enabled = false;
        bowRef.enabled = false;
        switchWeaponRef.enabled = false;
        swordRef.enabled = false;
        interactRef.enabled = false;
    }
}
