namespace ShootingGame
{
    internal class Program
    {

       static List<string> actions = new List<string>() { "Ladda", "Blocka", "Skjuta" };
        public static string mymove(Player obj1)
        {
            Console.WriteLine($"Enter the input from {obj1.Name}:");
            string? userInput = Console.ReadLine();
            Console.WriteLine("You chose : " + userInput);
            return userInput;
            
        }

        public static string computermove()
        {
            Random r1 = new Random();
            int randomInt = r1.Next(0, 2);
            string computerChoice = actions[randomInt];
            Console.WriteLine("Computer chose " + computerChoice);
            return computerChoice;
        }
        static void Main(string[] args)
        {
            Player computer = new Player("Computer");
            Player player1 = new Player("Shruthi");



            while (player1.Bullet < 3 && computer.Bullet < 3)
            {
                string myAction = mymove(player1);
                string computerAction = computermove();

                if (myAction == "Ladda" && computerAction == "Ladda")
                {
                    player1.Ladda();
                    computer.Ladda();
                }
                if (myAction == "Ladda" && computerAction == "Blocka")
                {
                    player1.Ladda();
                    computer.Blocka();
                }
                if (myAction == "Blocka" && computerAction == "Blocka")
                {
                    player1.Blocka();
                    computer.Blocka();
                }
                if (myAction == "Skjuta" && computerAction == "Blocka")
                {
                    if (player1.Bullet >= 1) player1.Skjuta();
                    computer.Blocka();
                }
                if (myAction == "Skjuta" && computerAction == "Skjuta")
                {
                    if (player1.Bullet >= 1) player1.Skjuta();
                    if (computer.Bullet >= 1) computer.Skjuta();
                }
                if (myAction == "Skjuta" && computerAction == "Ladda")
                {
                    if (player1.Bullet >= 1)
                    {
                        player1.Skjuta();
                        Console.WriteLine("You are the winner !");
                        break;
                    }
                    computer.Ladda();
                }
                if (myAction == "Ladda" && computerAction == "Skjuta")
                {
                    player1.Ladda();
                    if (computer.Bullet >= 1)
                    {
                        computer.Skjuta();
                        Console.WriteLine("Computer is the winner !");
                        break;
                    }
                    computer.Ladda();
                }
                if (myAction == "Blocka" && computerAction == "Ladda")
                {
                    player1.Blocka();
                    computer.Ladda();
                }
                Console.WriteLine("You have " + player1.Bullet);
                Console.WriteLine("Computer have " + computer.Bullet);
            }
            if(player1.Bullet == 3)
            {
                player1.Shotgun();
            }
            else if(computer.Bullet == 3)
            {
                computer.Shotgun();
            }
        }
    }
}
