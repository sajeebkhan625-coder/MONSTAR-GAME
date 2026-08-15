using System;

// Interface 1
interface ITrainable
{
    void Train();
}

// Interface 2
interface IBattle
{
    void Attack(Monster enemy);
}

// Abstract Class
abstract class Monster
{
    public string Name;
    public int Health;
    public int XP;
    public int Level;

    // Constructor
    public Monster(string name)
    {
        Name = name;
        Health = 100;
        XP = 0;
        Level = 1;
    }

    // Abstract method
    public abstract void SpecialSkill();

    // Train method
    public void Train()
    {
        XP += 20;

        Console.WriteLine(Name + " is training...");
        Console.WriteLine("XP increased by 20.");

        if (XP >= 100)
        {
            Level++;
            XP = 0;

            Console.WriteLine(Name + " leveled up!");
            Console.WriteLine("Current Level: " + Level);
        }
    }

    // Attack method
    public virtual void Attack(Monster enemy)
    {
        enemy.Health -= 20;

        if (enemy.Health < 0)
            enemy.Health = 0;

        Console.WriteLine(Name + " attacked " + enemy.Name);
        Console.WriteLine(enemy.Name + " Health: " + enemy.Health);
    }

    // Heal method
    public void Heal()
    {
        Health += 20;

        if (Health > 100)
            Health = 100;

        Console.WriteLine(Name + " healed.");
        Console.WriteLine("Health: " + Health);
    }

    // Operator Overloading
    public static Monster operator +(Monster m1, Monster m2)
    {
        m1.XP += m2.XP;
        return m1;
    }

    // Display information
    public void ShowInfo()
    {
        Console.WriteLine("\n--- Monster Information ---");
        Console.WriteLine("Name   : " + Name);
        Console.WriteLine("Health : " + Health);
        Console.WriteLine("XP     : " + XP);
        Console.WriteLine("Level  : " + Level);
    }
}

// Fire Monster
class FireMonster : Monster, ITrainable, IBattle
{
    public FireMonster(string name) : base(name)
    {
    }

    public override void SpecialSkill()
    {
        Console.WriteLine(Name + " used FIRE BALL!");
    }

    public override void Attack(Monster enemy)
    {
        enemy.Health -= 30;

        if (enemy.Health < 0)
            enemy.Health = 0;

        Console.WriteLine(Name + " used Fire Attack!");
        Console.WriteLine(enemy.Name + " Health: " + enemy.Health);
    }
}

// Water Monster
class WaterMonster : Monster, ITrainable, IBattle
{
    public WaterMonster(string name) : base(name)
    {
    }

    public override void SpecialSkill()
    {
        Console.WriteLine(Name + " used WATER BLAST!");
    }

    public override void Attack(Monster enemy)
    {
        enemy.Health -= 25;

        if (enemy.Health < 0)
            enemy.Health = 0;

        Console.WriteLine(Name + " used Water Attack!");
        Console.WriteLine(enemy.Name + " Health: " + enemy.Health);
    }
}

// Main Game
class MonsterGame
{
    static void Main()
    {
        Console.WriteLine("===== MONSTER BATTLE GAME =====");

        Console.WriteLine("\nChoose your Monster:");
        Console.WriteLine("1. Fire Monster");
        Console.WriteLine("2. Water Monster");

        Console.Write("Enter choice: ");
        int choice = Convert.ToInt32(Console.ReadLine());

        Monster player;

        if (choice == 1)
        {
            player = new FireMonster("Blaze");
        }
        else
        {
            player = new WaterMonster("Aqua");
        }

        // Enemy
        Monster enemy = new FireMonster("Enemy");

        Console.WriteLine("\nYour Monster:");
        player.ShowInfo();

        Console.WriteLine("\nEnemy:");
        enemy.ShowInfo();

        // Training
        Console.WriteLine("\n===== TRAINING =====");

        player.Train();
        player.Train();
        player.Train();

        player.ShowInfo();

        // Battle
        Console.WriteLine("\n===== BATTLE START =====");

        while (player.Health > 0 && enemy.Health > 0)
        {
            Console.WriteLine("\nChoose Action:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Special Skill");
            Console.WriteLine("3. Heal");

            Console.Write("Enter choice: ");
            int action = Convert.ToInt32(Console.ReadLine());

            if (action == 1)
            {
                player.Attack(enemy);
            }
            else if (action == 2)
            {
                player.SpecialSkill();

                // Special skill damage
                enemy.Health -= 35;

                if (enemy.Health < 0)
                    enemy.Health = 0;

                Console.WriteLine("Enemy Health: " + enemy.Health);
            }
            else if (action == 3)
            {
                player.Heal();
            }
            else
            {
                Console.WriteLine("Invalid choice!");
            }

            // Enemy attack
            if (enemy.Health > 0)
            {
                enemy.Attack(player);
            }

            Console.WriteLine("\nYour HP: " + player.Health);
            Console.WriteLine("Enemy HP: " + enemy.Health);
        }

        // Result
        Console.WriteLine("\n===== GAME OVER =====");

        if (player.Health > 0)
        {
            Console.WriteLine("Congratulations!");
            Console.WriteLine(player.Name + " WON!");
        }
        else
        {
            Console.WriteLine("You LOST!");
        }
    }
}