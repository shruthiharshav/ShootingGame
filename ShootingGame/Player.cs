

namespace ShootingGame
{
    internal class Player
    {
        public int Bullet = 0;
        public string Name;

        public Player(string name) 
        { 
            Name = name;
        }

        public void Ladda()
        {
            Bullet += 1;
        }

        public void Blocka()
        {
            
        }
        public void Skjuta()
        {
            Bullet -= 1;
        }

        public void Shotgun()
        {
            Console.WriteLine(Name + " have the shotgun and is the Winner!");
        }

    }
}
