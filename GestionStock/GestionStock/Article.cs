using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GestionStock
{
    class Article
    {
        public int Reference { get; set; }
        public string Nom { get; set; }
        public int PrixAchat { get; set; }
        public int PrixVente { get; set; }

        public Article(int reference, string nom, int prixachat, int prixvente)
        {
            try
            {
                if (prixvente < prixachat)
                {
                    throw new PException($"Prix achat : {prixachat} > {prixvente}");
                    //throw new PException() ;
                }
                Reference = reference;
                Nom = nom;
                PrixAchat = prixachat;
                PrixVente = prixvente;
            }
            catch (PException cerrorprix)
            {
                Console.WriteLine(cerrorprix.Message);
            }
        }

        public override string ToString()
        {
            return $"Nom : {Nom} \nNumero de Reference : {Reference} \nPrix d'achat : {PrixAchat} euros \nPrix de vente : {PrixVente} euros.";
        }
    }

    class Stock
    {
        private Dictionary<int, Article> ArticleStock = new Dictionary<int, Article>();

        public Stock()
        {
        }

        public void CreerNouvelArticle()
        {
            /////////////////// Permet de créer et d'ajouter une nouvel article au dictionnaire ///////////////////

            Console.Write("Veuillez entrer un numero de reference : "); int reference = int.Parse(Console.ReadLine());
            Console.Write("Veuillez entrer un nom : "); string nom = Console.ReadLine();
            Console.Write("Veuillez entrer un prix d'achat : "); int prix_achat = int.Parse(Console.ReadLine());
            Console.Write("Veuillez entrer un prix de vente : "); int prix_vente = int.Parse(Console.ReadLine());

            AjouterArticle(reference, nom, prix_achat, prix_vente);
        }
        public void AjouterArticle(int reference, string nom, int pa, int pv)
        {
            Article a = new Article(reference, nom, pa, pv);
            ArticleStock.Add(reference, a);
            Console.WriteLine("L'article {0} à bien été ajouté.", a.Nom);
        }

        public void AfficherArticles()
        {
            foreach (var article in ArticleStock)
            {
                Console.WriteLine("Vous avez choisis de voir les Informations de l'article : {0}, \nInformations Article : \n{1}\n",
                    article.Key, article.Value);
            }
        }

        public void ChercheArticle()
        {
            var key_article = int.Parse(Console.ReadLine());
            Console.WriteLine($"Voici les informations sur {key_article} : \n{ArticleStock[key_article]}");

        }

        public void Prix_Articles(int prix_saisie)
        {
            Console.WriteLine("Les articles plus chères que {0} euros sont : ", prix_saisie);
            foreach (var articleDico in ArticleStock)
            {
                if (articleDico.Value.PrixAchat > prix_saisie)
                {
                    Console.WriteLine(articleDico.Value);
                }
            }
        }

        public void SupprimerArticle()
        {
            var supprimer_article = int.Parse(Console.ReadLine());
            ArticleStock.Remove(supprimer_article);
            if (!ArticleStock.ContainsKey(supprimer_article)) // ça veut dire if not(ArticleStock.ContainsKey(supprimer_article)) = si il n'a pas supprimer_article dans le dictionnaire.
            {
                Console.WriteLine($"La clé {supprimer_article} a bien été supprimé.");
            }
        }

        public void ModifierArticle()
        {
            Console.WriteLine("Ecrivez la référence de l'article à modifier : ");
            var ref_article = int.Parse(Console.ReadLine());
            Console.WriteLine($"Voici les informations sur {ref_article} : \n{ArticleStock[ref_article]}");
            Console.WriteLine($"\nQue souhaitez vous modifier? \n0 : Annuler la modification \n1 : Modifier la référence \n2 : Modifier le prix d'achat \n3 : Modifier le prix de vente");
            bool choix_modif = true;
            int nbr_test_choix = 0;
            do
            {
                int choix = int.Parse(Console.ReadLine());
                switch (choix)
                {
                    case 0:
                        {
                            Console.WriteLine("Vous avez choisi de ne rien modifier.");
                            choix_modif = false;
                            break;
                        }
                    case 1:
                        {
                            var article = ModifierReference(ArticleStock[ref_article]);
                            RemplacerValeurDico(ref_article, article);
                            choix_modif = false;
                            break;
                        }
                    case 2:
                        {
                            Article article_modifier = ModifierPrixAchat(ArticleStock[ref_article]);
                            ArticleStock.Remove(ref_article);
                            RemplacerValeurDico(ref_article, article_modifier);
                            choix_modif = false;
                            break;
                        }
                    case 3:
                        {
                            Article article_modifier = ModifierPrixVente(ArticleStock[ref_article]);
                            ArticleStock.Remove(ref_article);
                            RemplacerValeurDico(ref_article, article_modifier);
                            choix_modif = false;
                            break;
                        }
                    default:
                        Console.WriteLine($"Mauvais choix recommencer en écrivant 0,1,2 ou 3 : \n1 : Annuler la modification \n1 : Modifier la référence \n2 : Modifier le prix d'achat \n3 : Modifier le prix de vente");
                        if (nbr_test_choix < 3)
                        {
                            Console.WriteLine("Vous vous êtes trompé plus de trois fois, au revoir");
                            choix_modif = false;
                        }
                        nbr_test_choix += 1;
                        break;
                }
            } while (choix_modif == true);
        }

        public Article ModifierReference(Article modif_ref)
        {
            //////////////////// Calcule des différentes longueure de la chaine ///////////////////////////
            //int begin_num_ref = modif_ref.IndexOf("Numero de Reference :")+ 22; //le +22 correspond à la longeure de "Numero de Reference :" comme ça on commence après les ": "
            //int end_of_num_ref = modif_ref.IndexOf("\nPrix d'achat");// On cherche la fin de de la chaine de la variable {NumeroReference} et donc on prend la chaine "euros" juste après le variable

            //////////////////// Demande/Remplacement/Ajout du nouveau numero de reference ///////////////////////////
            //Console.Write("Entrez le nouveau numéro de référence : ");
            //string new_num_ref = int.Parse(Console.ReadLine()).ToString();//On demande un nouveau numéro de référence. Pour être sure que ce soit une string on le Parse en INT et on le retransforme directement en String
            //string old_ref = modif_ref.Substring(begin_num_ref, end_of_num_ref - begin_num_ref);// On isole la propriété {NumeroReference}.
            //string ref_modifie = modif_ref.Replace($"Numero de Reference : {old_ref}", $"Numero de Reference : {new_num_ref}");// On remplace l'ancien numéro de ref par le nouveau

            //return ref_modifie; // on renvoie la nouvelle chaine de caractère

            // TODO
            return null;
        }

        public Article ModifierPrixAchat(Article modif_prix_achat)
        {
            //////////////////// Calcule des différentes longueure de la chaine /////////////////////////// Pour plus d'info voir """"ModifierReference""""
            //int begin_prix_achat = modif_prix_achat.IndexOf("Prix d'achat : ") + 15; 
            //int end_of_prix_achat = modif_prix_achat.IndexOf("euros \nPrix de vente");

            //////////////////// Demande/Remplacement/Ajout du nouveau prix d'achat ///////////////////////////
            //Console.Write("Entrez le nouveau Prix d'achat : ");
            //string new_prix_achat = int.Parse(Console.ReadLine()).ToString();
            //string old_prix_achat = modif_prix_achat.Substring(begin_prix_achat, end_of_prix_achat - begin_prix_achat);
            //string prix_achat_modifie = modif_prix_achat.Replace($"Prix d'achat : {old_prix_achat}", $"Prix d'achat : {new_prix_achat} ");

            //return prix_achat_modifie; // on renvoie la nouvelle chaine de caractère

            // TODO
            return null;
        }

        public Article ModifierPrixVente(Article modif_prix_vente)
        {
            //////////////////// Calcule des différentes longueure de la chaine /////////////////////////// Pour plus d'info voir """"ModifierReference""""
            //int begin_prix_vente = modif_prix_vente.IndexOf("Prix de vente : ") + 16; 
            //int end_of_prix_vente = modif_prix_vente.IndexOf("euros.");

            //////////////////// Demande/Remplacement/Ajout du nouveauprix de vente ///////////////////////////
            //Console.Write("Entrez le nouveau prix de vente : ");
            //string new_prix_vente = int.Parse(Console.ReadLine()).ToString();
            //string old_prix_vente = modif_prix_vente.Substring(begin_prix_vente, end_of_prix_vente - begin_prix_vente);
            //string prix_vente_modifie = modif_prix_vente.Replace($"Prix de vente : {old_prix_vente}", $"Prix de vente : {new_prix_vente} ");

            //return prix_vente_modifie; // on renvoie la nouvelle chaine de caractère

            // TODO
            return null;
        }

        public void RemplacerValeurDico(int ref_article, Article article)
        {
            ////////////////// Permet de remplacer les valeurs du dictionnaire /////////////////////////// 
            ArticleStock.Remove(ref_article);
            if (!ArticleStock.ContainsKey(ref_article)) // ça veut dire if not(ArticleStock.ContainsKey(supprimer_article)) = si il n'a pas la clé dans le dictionnaire.
            {
                ArticleStock.Add(ref_article, article);
                Console.WriteLine($"La valeur de la clé {ref_article} a bien été modifié: \n{article} ");
            }
        }
    }

    class PException : Exception
    {
        public PException(string message) : base(message)
        {
            Console.WriteLine("Erreur le prix de vente est inférieur celui d'achat!!!");
        }
    }
}
