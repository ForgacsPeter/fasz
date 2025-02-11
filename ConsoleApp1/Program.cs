
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Kártyák létrehozása
        List<string> deck = new List<string>
        {
            "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10", "A11", "A12", "A13", // Ászok
            "K1", "K2", "K3", "K4", "K5", "K6", "K7", "K8", "K9", "K10", "K11", "K12", "K13", // Királyok
            "V1", "V2", "V3", "V4", "V5", "V6", "V7", "V8", "V9", "V10", "V11", "V12", "V13", // Válaszok
            "J1", "J2", "J3", "J4", "J5", "J6", "J7", "J8", "J9", "J10", "J11", "J12", "J13"  // Játékosok
        };

        // Fekete Péter kártya
        string blackPeter = "J13";

        // Keverés
        Random rand = new Random();
        deck = deck.OrderBy(x => rand.Next()).ToList();

        // Kártyák kiosztása felhasználónak és gépnek
        List<string> playerCards = new List<string>();
        List<string> computerCards = new List<string>();

        for (int i = 0; i < deck.Count; i++)
        {
            if (i % 2 == 0)
                playerCards.Add(deck[i]);
            else
                computerCards.Add(deck[i]);
        }

        // Játék kezdése
        bool isGameRunning = true;
        while (isGameRunning)
        {
            Console.Clear();
            Console.WriteLine("Te kártyáid: " + string.Join(", ", playerCards));
            Console.WriteLine("A gép kártyái: " + string.Join(", ", computerCards.Select(x => x.Substring(0, 1) + "X"))); // Gépi kártyák elrejtése

            // Felhasználó választ
            Console.WriteLine("\nVálassz egy kártyát a feladáshoz: ");
            string playerCard = Console.ReadLine();
            if (playerCards.Contains(playerCard))
            {
                playerCards.Remove(playerCard);
                computerCards.Add(playerCard);
            }
            else
            {
                Console.WriteLine("Hibás kártya! Próbáld újra.");
                continue;
            }

            // Gép választása (véletlenszerű kártya)
            string computerCard = computerCards[rand.Next(computerCards.Count)];
            Console.WriteLine("\nA gép választott egy kártyát: " + computerCard);
            computerCards.Remove(computerCard);
            playerCards.Add(computerCard);

            // Párosítás
            PairCards(playerCards);
            PairCards(computerCards);

            // Ellenőrizzük, hogy van-e Fekete Péter kártya
            if (playerCards.Contains(blackPeter))
            {
                Console.WriteLine("Sajnálom, Te nyertél! A Fekete Péter nálad maradt.");
                isGameRunning = false;
            }
            else if (computerCards.Contains(blackPeter))
            {
                Console.WriteLine("Gratulálok, Te nyertél! A gépnek maradt a Fekete Péter.");
                isGameRunning = false;
            }
        }

        Console.WriteLine("\nJáték vége!");
    }

    static void PairCards(List<string> playerCards)
    {
        var pairs = playerCards.GroupBy(x => x.Substring(0, 1)).Where(g => g.Count() > 1).ToList();
        foreach (var pair in pairs)
        {
            playerCards.Remove(pair.First());
            playerCards.Remove(pair.Last());
        }
    }
}
