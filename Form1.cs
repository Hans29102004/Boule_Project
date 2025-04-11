using System; // Importation de l'espace de noms System pour utiliser des classes de base comme DateTime, Math, etc.
using System.Collections.Generic; // Importation de l'espace de noms System.Collections.Generic pour utiliser des collections génériques comme List<T>
using System.ComponentModel; // Importation de l'espace de noms System.ComponentModel pour utiliser des composants et des contrôles
using System.Data; // Importation de l'espace de noms System.Data pour utiliser des classes de gestion de données
using System.Drawing; // Importation de l'espace de noms System.Drawing pour utiliser des classes graphiques comme Point, Size, Color, etc.
using System.Linq; // Importation de l'espace de noms System.Linq pour utiliser des fonctionnalités de requête LINQ
using System.Text; // Importation de l'espace de noms System.Text pour utiliser des classes de manipulation de texte
using System.Threading.Tasks; // Importation de l'espace de noms System.Threading.Tasks pour utiliser des tâches asynchrones
using System.Windows.Forms; // Importation de l'espace de noms System.Windows.Forms pour créer des applications Windows Forms

namespace Boule_Project // Déclaration de l'espace de noms Boule_Project
{
    public partial class Form1 : Form // Déclaration de la classe partielle Form1 qui hérite de la classe Form
    {
            private List<Ball> balls = new List<Ball>(); // Déclaration d'une liste privée de Ball pour stocker les balles qui seront creer
            private Timer timer; // Déclaration d'un objet Timer privé pour gérer les mises à jour périodiques le rafraichisement de la fenetre
            private Random random = new Random(); // Déclaration d'un objet Random privé pour générer des valeurs aléatoires
            private bool isGameRunning = false; // Déclaration d'un booléen privé pour indiquer si le jeu est en cours d'exécution

        public Form1() // Constructeur de la classe Form1
        {
            InitializeComponent(); // Appel de la méthode InitializeComponent pour initialiser les composants de l'interface utilisateur
            timer = new Timer(); // Initialisation de l'objet Timer
            timer.Interval = 15; // Définition de l'intervalle du Timer à 15 millisecondes pour mettre à jour toutes les 15 ms
            timer.Tick += Timer_Tick; // Abonnement à l'événement Tick du Timer pour appeler la méthode Timer_Tick à chaque tick
        }

        private void button1_Click(object sender, EventArgs e) // Méthode appelée lorsque le bouton1 est cliqué
                                                              //ici moi j'ai voulu complexifié un peut avec le pause play 
                                                             //ici il faut juste garder le timer.Start car c'est ca qui lance le game
        {
            if (isGameRunning) // Vérification si le jeu est en cours d'exécution
            {
                timer.Stop(); // Arrêt du Timer
                isGameRunning = false; // Mise à jour de l'état du jeu à "non en cours"
                button1.Text = "Play ▶️"; // Mise à jour du texte du bouton1 à "Play ▶️"
            }
            else // Si le jeu n'est pas en cours d'exécution
            {
                if (balls.Count == 0) // Vérification si la liste de balles est vide
                {
                    AddNewBall(); // Ajout d'une nouvelle balle si la liste est vide
                }
                timer.Start(); // Démarrage du Timer
                isGameRunning = true; // Mise à jour de l'état du jeu à "en cours"
                button1.Text = "Pause ⏸️"; // Mise à jour du texte du bouton1 à "Pause ⏸️"
            }
        }

        private void button2_Click(object sender, EventArgs e) // Méthode appelée lorsque le bouton2 est cliqué
                                                              //C'etait un bouton pout ajouté manuellement des boule 
                                                             //donc quand tu clique sur le bouton il fait appelle a la 
                                                            //AddNewBall pour ajouter les ball a la fenettre
        {
            AddNewBall(); // Ajout d'une nouvelle balle
        }

        private void AddNewBall() // Méthode privée pour ajouter une nouvelle balle a la fenettre
                                
        {
            balls.Add(new Ball(new Point(random.Next(this.ClientSize.Width), random.Next(this.ClientSize.Height)), // Ajout d'une nouvelle balle avec une position aléatoire
                               new Size(random.Next(-10, 10), random.Next(-10, 10)), // Définition d'une vitesse aléatoire pour la balle
                               random.Next(20, 40), // Définition d'un diamètre aléatoire pour la balle
                               Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)))); // Définition d'une couleur aléatoire pour la balle
        }

        private void Timer_Tick(object sender, EventArgs e) // Méthode appelée à chaque tick du Timer .le Timer c'est comme une horloge donc le tick ici peut representer 
                                                           //les tick d'une horloge a chaque seconde . Ca permet d'avoir un deplacement fluide vu a chaque tick la position*
                                                          //des balles seront actualisé 
        {
            for (int i = 0; i < balls.Count; i++) // Boucle à travers toutes les balles.la boucle sert a s'assurer que toute les balls presnt dans la fenetre bouge
            {
                balls[i].Move(this.ClientRectangle); // Déplacement de chaque balle en fonction des limites du formulaire
                                                    //this.ClientRectangle passé en parametre corespond a notre fenetre comme 
                                                   //comme c'est prevu dans la deh=finition de la methode dans la class Ball
                                                  // (Voir le fichier Ball.cs) En gros ici il vas prendre chaque balle de la list balls
                                                 //et lui applique la methode move pour le deplacé
                for (int j = i + 1; j < balls.Count; j++) // Boucle pour vérifier les collisions entre les balles.En gros il vas verifier 
                                                         // a chaque tick sì il y a colission pour chaque ball et appliquer la meethode 
                                                        //bounce sur les balle concerné pour les faire rebondir
                {
                    if (balls[i].IsCollidingWith(balls[j])) // Vérification si deux balles sont en collision
                    {
                        balls[i].Bounce(); // Faire rebondir la première balle
                        balls[j].Bounce(); // Faire rebondir la deuxième balle
                    }
                }
            }
            this.Invalidate(); // Redessine le formulaire pour mettre à jour l'affichage des balles.
                              //c'est la methode que Timer_Tick utilise pour mette a jour l'affichage
        }

        protected override void OnPaint(PaintEventArgs e) // Méthode appelée pour dessiner le formulaire
        {
            base.OnPaint(e); // Appel de la méthode de base OnPaint
            foreach (var ball in balls) // Boucle à travers toutes les balles
            {
                ball.Draw(e.Graphics); // Dessin de chaque balle
            }
        }

        private void button3_Click(object sender, EventArgs e) // Méthode appelée lorsque le bouton3 est cliqué.Pour 
                                                              //reinitiallisé le game je pense tout le monde peut comprendre ca 
        {
            timer.Stop(); // Arrêt du Timer
            balls.Clear(); // Effacement de toutes les balles de la liste
            isGameRunning = false; // Mise à jour de l'état du jeu à "non en cours" . Pas essentielle vu que cette variable me servait a verifier l'etat du jeu pour savoir si il faut mettre pause ou play
            button1.Text = "Lancer >_"; // Mise à jour du texte du bouton1 à "Lancer >_" .Pas essentielle c'est toujour lié au pause play 
            this.Invalidate(); // Redessine le formulaire pour effacer les balles
        }

        private void Form1_Load(object sender, EventArgs e) // Méthode appelée lorsque le formulaire est chargé
        {
            //Vu qu'on a utilisé le concepteur visuelle de visual studio il y a eu du code genere 
            //par l'iA je visual studio bref c'est pour ca que c'est class est partial c'est comme 
            //visual studio ecrivait le code du formulaire et des bouton et tout dans un autre fichier 
            //et nous on definit les comportement des composant dans celui ci.D'ailleur c'est pas comme 
            //ci c'est comme ca 
            // Cette méthode est vide et n'a pas de comportement défini
        }
    }
}

//Brefffffff j'ai fait de mon mieux pour voi=us expliquer tout ca si il ya des incomprehension 
//ecriver moi je vais vous eclairer etant donner que je suis pas moi meme un prime dans le language
//il se peut qu'il y ait des erreur donc si vous en trouver dite moi et je ne parle pas de faute de francais
//Erika 👿 mais bien de faute dans l'explication .Sur ceux Bonne chance 