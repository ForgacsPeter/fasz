using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<string> deck = new List<string>();
    static List<string> playerHand = new List<string>();
    static List<string> botHand = new List<string>();
    static List<string> playerPile = new List<string>();
    static List<string> botPile = new List<string>();
    static string centerCard = "";
    static Random rnd = new Random();

    static void Main()
    {
        InitializeDeck();
        DealCards();
        centerCard = DrawCard();
        Console.WriteLine("Welcome to Zsírozás!");

        while (playerHand.Count > 0 && botHand.Count > 0)
        {
            Console.WriteLine("\nCenter card: " + centerCard);
            Console.WriteLine("Your hand: " + string.Join(", ", playerHand));
            Console.Write("Choose a card to play (enter number 1-" + playerHand.Count + "): ");
            int choice = int.Parse(Console.ReadLine()) - 1;
            string playerCard = playerHand[choice];
            playerHand.RemoveAt(choice);

            PlayTurn(playerCard, playerPile, "Player");
            if (playerHand.Count == 0) break;

            string botCard = botHand[rnd.Next(botHand.Count)];
            botHand.Remove(botCard);
            Console.WriteLine("Bot plays: " + botCard);
            PlayTurn(botCard, botPile, "Bot");
        }

        DetermineWinner();
    }

    static void InitializeDeck()
    {
        string[] suits = { "Acorns", "Hearts", "Leaves", "Bells" };
        string[] values = { "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
        foreach (var suit in suits)
        {
            foreach (var value in values)
            {
                deck.Add(value + " of " + suit);
            }
        }
        deck = deck.OrderBy(x => rnd.Next()).ToList();
    }

    static void DealCards()
    {
        for (int i = 0; i < 4; i++)
        {
            playerHand.Add(DrawCard());
            botHand.Add(DrawCard());
        }
    }

    static string DrawCard()
    {
        if (deck.Count == 0) return "";
        string card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    static void PlayTurn(string playedCard, List<string> pile, string player)
    {
        if (IsWinningCard(playedCard, centerCard))
        {
            pile.Add(playedCard);
            pile.Add(centerCard);
            Console.WriteLine(player + " wins the trick!");
            centerCard = DrawCard();
        }
        else
        {
            centerCard = playedCard;
        }
    }

    static bool IsWinningCard(string playedCard, string center)
    {
        if (center == "") return false;
        if (playedCard.Split(' ')[0] == center.Split(' ')[0] || playedCard.StartsWith("7")) return true;
        return false;
    }

    static void DetermineWinner()
    {
        int playerPoints = playerPile.Count(c => c.StartsWith("10") || c.StartsWith("Ace")) * 10;
        int botPoints = botPile.Count(c => c.StartsWith("10") || c.StartsWith("Ace")) * 10;

        Console.WriteLine("\nFinal Score: Player " + playerPoints + " - Bot " + botPoints);
        Console.WriteLine(playerPoints >= 50 ? "You win!" : "Bot wins!");
    }
}
