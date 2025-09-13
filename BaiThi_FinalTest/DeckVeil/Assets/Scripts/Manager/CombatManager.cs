using UnityEngine;
using System.Collections.Generic;
public class CombatManager : MonoBehaviour
{
    public bool isGameStillPlay;

    public GameObject win_panel;
    public GameObject lose_panel;
    public static CombatManager Instance;
    private List<EnemyBase> enemies = new List<EnemyBase>();
    private List<CharacterBase> player = new List<CharacterBase>();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

    }
    // Đăng ký enemy khi spawn
    public void RegisterEnemy(EnemyBase enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    // Hủy đăng ký khi chết/destroy
    public void UnregisterEnemy(EnemyBase enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);
    }
    public void RegisterPlayer(CharacterBase Player)
    {
        if (!player.Contains(Player))
            player.Add(Player);
    }

    // Hủy đăng ký khi chết/destroy
    public void UnregisterPlayer(CharacterBase Player)
    {
        if (player.Contains(Player))
            player.Remove(Player);
    }

    // Gọi khi có attack
    public void DealDamageEnemy(EnemyBase target, int damage)
    {
        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }
    public void DealDamagePlayer(CharacterBase target, int damage)
    {
        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }

    //  buff toàn bộ enemy/player
    public void BuffAllEnemies(int bonusHP)
    {
        foreach (var e in enemies)
        {
            e.heath += bonusHP;
        }
    }
    public void BuffAllplayer(int bonusHP)
    {
        foreach (var e in player)
        {
            e.hp += bonusHP;
        }
    }
    public List<EnemyBase> GetEnemies()
    {
        return enemies;
    }

    public List<CharacterBase> GetPlayers()
    {
        return player;
    }
    public void CheckEnemyLive()
    {
        if (enemies.Count != 0) return;
        else
        {
            isGameStillPlay = false;
            Time.timeScale = 0;
            win_panel.gameObject.SetActive(true);
        }
    }
    public void CheckPlayerLive()
    {
        if (player.Count != 0) return;
        else
        {
            Time.timeScale = 0;
            isGameStillPlay = false;
            lose_panel.gameObject.SetActive(true);

        }
    }
}
