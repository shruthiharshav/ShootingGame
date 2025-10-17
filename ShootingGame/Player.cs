using System;

namespace ShootingGame
{
    internal class Player
    {
        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public int Bullets { get; private set; }

        public void Load() => Bullets = Math.Min(Bullets + 1, 3);

        public void Block()
        {
            // Nothing happens so i leave it empty
        }

        public bool TryShoot()
        {
            if (Bullets <= 0)
            {
                return false;
            }

            Bullets -= 1;
            return true;
        }

        public bool HasShotgun => Bullets >= 3;
    }
}
