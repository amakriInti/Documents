using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionStock
{
    class Program
    {
        private static Stock Inventairedumagasin = new Stock();

        static void Main(string[] args)
        {
            // Articles ajouté par défaut
            Inventairedumagasin.AjouterArticle(111, "BN", 2, 3); 
            Inventairedumagasin.AjouterArticle(145, "Chocolat",  3, 5);
            Inventairedumagasin.AjouterArticle(2036, "Ordinateur",  3480, 4500);
            Inventairedumagasin.AjouterArticle(1545, "Television", 400, 988);

            bool ask_user = true;
            int nbr_test_choix = 0;
            Console.WriteLine("Bienvenue \n");
            do
            {
                Console.WriteLine($"\nQue souhaitez vous faire? \n0 : Quitter l'apllication \n1 : Ajouter un nouvel article \n2 : Afficher tous les Articles du magasin \n3 : Rechercher un Article " +
                    $"\n4 : Afficher les Articles dont les prix d’achat est supérieur à une valeur saisie \n5 : Supprimer un Article \n6 : Modifier un Article");
                int fonctionnalité = int.Parse(Console.ReadLine());
                switch (fonctionnalité)
                {
                    case 0:
                        Console.WriteLine("Vous avez décidé de quittez l'application");
                        ask_user = false;
                        break;
                    case 1:
                        Console.WriteLine("\nVeuillez renseigner les caractéristique du nouvel article : ");
                        Inventairedumagasin.CreerNouvelArticle();
                        break;
                    case 2:
                        Console.WriteLine("\nVoici tout les articles du magasin : ");
                        Inventairedumagasin.AfficherArticles();
                        break;
                    case 3:
                        Console.Write("\nQuelle article souhaitez vous voir : ");
                        Inventairedumagasin.ChercheArticle();
                        break;
                    case 4:
                        Console.Write("\nVeuillez entrer une valeur dont les prix d’achat est supérieur seront affiché : "); int prix = int.Parse(Console.ReadLine());
                        Inventairedumagasin.Prix_Articles(prix);
                        break;
                    case 5:
                        Console.Write("\nQuelle article souhaitez vous supprimer : ");
                        Inventairedumagasin.SupprimerArticle();
                        break;
                    case 6:
                        Inventairedumagasin.ModifierArticle();
                        break;
                    default:
                        {
                            Console.WriteLine($"Mauvais choix, recommencez");
                            if (nbr_test_choix < 3)
                            {
                                Console.WriteLine("Vous vous êtes trompé plus de trois fois, au revoir");
                                ask_user = false;
                            }
                            nbr_test_choix += 1;
                            break;
                        }
                }
            } while (ask_user == true);
        }
    }
}
