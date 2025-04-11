using System; // Importation de l'espace de noms System
using System.Drawing; // Importation de l'espace de noms System.Drawing
//Je tient a note que beaucoup de type de variable utilise ici sont issue de ''System.Drawing''(ntament :point size rectangle Graphics etc)

namespace Boule_Project // Déclaration de l'espace de noms Boule_Project indispensable pour ecrire du code dans plusieur fichier comme c'est le cas pour mon projet (en gros tout tes
                        //codes de tout tes fichier douvient etre ecrit dans le meme espace de noms afin qu'il se reconnaisse )
{
    public class Ball // Déclaration de la classe publique Ball qui sera utille pour creer la bouble avec c'est comportement(deplacement ;se cogner etc)
    {
        public Point Position; // Déclaration d'un variable  public de type Point pour la position de la balle(les coordonne X,Y de notre boule)
        public Size Velocity; // Déclaration d'un variable  public de type Size pour la vitesse de la balle(La vitesse initialle de notre boule)
        public int Diameter; // Déclaration d'un variable  public de type int pour le diamètre de la balle(la grosseur de notre boule)
        public Color Color; // Déclaration d'un variable  public de type Color pour la couleur de la balle(la couleur de notre boule)

        public Ball(Point position, Size velocity, int diameter, Color color)  // Constructeur de la classe Ball qui sera appellé pour || 
        {                                                                     //creer les boule (le constructeur est une methode d'une ||
            Position = position; // Initialisation de la position de la ball // classe utliser pour creer une instance de la classe.En || 
            Velocity = velocity; // Initialisation de la vitesse de la ball //C# on utilise le mot clé ''new'' pour faire appelle au   ||
            Diameter = diameter; // Initialisation du diamètre de la balle //constructeur afin de creer un novelle instance(Dans notre ||
            Color = color; // Initialisation de la couleur de la balle    // cas pour creer un nouvelle balle ).Le constructeur doit   ||
                                                                         //toujour initialisé les variable de la classe on a donc la   ||
                                                                        //possiblité de demander les parametre a affecter au variable  ||
                                                                       //en le demandant dans les parenthese de la methode comme c'est || 
                                                                      // le cas ici(nb:les variable dans les parenthese sont differente||
                                                                     //des variable reels .cels dans les parenthese ne sont la que     ||
                                                                    //pour stocket temporaire les donner entrer afin d'etre ensuite    ||
                                                                   //affecter au variable c'est pour cela qu'on les declare dans les   ||
                                                                  //parenthese )                                                       ||
                                                                 //Nb:le constructeur d'une class a toujour le meme nom que la class   ||
                                                                //Pour revenir a notre cas ici notre contructeur prend donc enparametre|| 
                                                               //de maniere literal la position de notre boule sa vitesse sa grosseur et|| 
                                                              //sa couleur (en d'autre terme pour chaque boule creer avec le constructeur||
                                                             // vous devez lui passer ses 4 parametre)                                 ||
        
        }

        public void Move(Rectangle fenetre) // Méthode publique pour déplacer la balle .Pour cette methode concentration ||
                                          // . Ce qu'il faut garder ici c'est que cette methode prend en parametre      ||
                                         // un objet de type Rectangle et modifie la  position de la balle en ajoutant  ||
                                        //la vitesse horizontale a la position X actuelle et pareile pour Y  . ET les if||
                                       // pour verifier la collision avec les bord de la fenetre et si oui renvoi dans  ||
                                      // le sens en inversant sa vitesse horizonatal ou vertialle celon le cas en multi par -1||
                                    //Au faite le rectangle prit en parametre ici repesente la fenetre et intervient seulement||
                                   //dans la verif de la collision avec les bord
        {
            Position = new Point(Position.X + Velocity.Width, Position.Y + Velocity.Height); // Mise à jour de la position de la balle en fonction de sa vitesse
            if (Position.X <= fenetre.Left || Position.X >= fenetre.Right - Diameter){ // Vérification des collisions horizontales avec les bord
                Velocity = new Size(-Velocity.Width, Velocity.Height)}; // Inversion de la vitesse horizontale en cas de collision avec les bord
            
            if (Position.Y <= fenetre.Top || Position.Y >= fenetre.Bottom - Diameter){ // Vérification des collisions verticales avec les limites
                Velocity = new Size(Velocity.Width, -Velocity.Height)}; // Inversion de la vitesse verticale en cas de collision
        }

        public void Draw(Graphics g) // Méthode publique pour dessiner la balle  qui
                                    //prend un paramètre de type Graphics nommé g, qui 
                                   //représente la surface de dessin.Cette methode je vais 
                                  //pas mentir moi meme j'ai pas tout compris
        {
            using (SolidBrush brush = new SolidBrush(Color)) // Création d'un pinceau solide avec la couleur de la balle
            {
                g.FillEllipse(brush, new Rectangle(Position, new Size(Diameter, Diameter))); // Dessin d'une ellipse remplie représentant la balle
                                                                                            //brush: Utilise le pinceau solide créé précédemment pour remplir l'ellipse.
                                                                                           //new Rectangle(Position, new Size(Diameter, Diameter)): Crée un nouvel objet
                                                                                          // Rectangle avec la position de la balle (Position) et une taille carrée 
                                                                                         //définie par le diamètre de la balle (Diameter).
            }
        }
        public bool IsCollidingWith(Ball other) // Méthode publique pour vérifier la collision avec une autre balle verifie
                                               //si il a collision entre les balles et prend en parametre une autre ball
                                              //parmis celle present dans le cadre mais ca c'est lors de l'appel de la methode qu'on 
                                             //va le precisé ici on se contante juste d'un parametre de type ball.Aussi la methode retourne 
                                            //un donne de type boolean en gros True ou False selon si il y a collision ou pas
                                           //La méthode `IsCollidingWith` vérifie si deux balles se chevauchent en calculant la distance
                                          // entre leurs centres et en comparant cette distance au diamètre de la balle actuelle. Si la 
                                         //distance est inférieure au diamètre, cela signifie que les balles se chevauchent et la méthode 
                                        //retourne `true`. Sinon, elle retourne `false`.  
        {
            int dx = this.Position.X - other.Position.X; // Calcul de la différence en X entre les deux balles(la difference des coordone X vu que nos ball on des attribut de type point pour represneté la position de la ball a l'instant T)
            int dy = this.Position.Y - other.Position.Y; // Calcul de la différence en Y entre les deux balles(pareille pour les Y)
            int distance = (int)Math.Sqrt(dx * dx + dy * dy); // Calcul de la distance entre les deux balles
            return distance < this.Diameter; // Retourne vrai si la distance est inférieure au diamètre de la balle
        }

        public void Bounce() // Méthode publique pour faire rebondir la balle .cette methode sera donc appelle quand les 
                            //vont se cogner ou quand elle vont se cogner au bord
        {
            this.Velocity = new Size(-this.Velocity.Width, -this.Velocity.Height); // Inversion de la vitesse de la balle.En en cas de colission il renvoie la balle dans la direction 
                                                                                  //inverse en inversant sa vitesse et comme tout ceux qui on fait un pct a l'ecole vous s'avez que la 
                                                                                 //la vitesse c'est un histoire de vecteur donc ca a un sens <- ->
        }
    }
}
// Bon voila pour la class ball et les methode qu'elle implement j'ai fait l'effor d'etre le plus detaille possible 
//