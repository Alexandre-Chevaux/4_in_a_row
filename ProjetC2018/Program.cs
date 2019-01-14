using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
namespace ProjetInformatique_Bitsch_Chevaux
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Programme de projet d'inforamtique de Xavier Bitsch et Alexandre Chevaux
           
            
                JMCTS j1 = new JMCTS(100, 100, 100);
                JMCTS j2 = new JMCTS(10, 10, 100);

                PositionP4 posall = new PositionP4(true, 6, 7);
                Partie P4 = new Partie(j1, j2, posall);
                P4.Commencer(false);
                
                j1.NouvellePartie();
                j2.NouvellePartie();
                
            
            
           
                
                JMCTSp j1p = new JMCTSp(100, 100, 100, 4);
                JMCTSp j2p = new JMCTSp(10, 10, 100, 4);
                PositionP4 puissanceJMCTSp = new PositionP4(true, 6, 7);
                Partie P4JMCTSp = new Partie(j1p, j2p, puissanceJMCTSp);

                P4JMCTSp.Commencer(false);
               
                j1p.NouvellePartie(); j2p.NouvellePartie();
            
           

            
                JMCTSP j1P = new JMCTSP(100, 100, 100, 4);
                JMCTSP j2P = new JMCTSP(10, 10, 100, 4);
                PositionP4 puissanceJMCTSP = new PositionP4(true, 6, 7);
                Partie P4JMCTSP = new Partie(j1P, j2P, puissanceJMCTSP);

                P4JMCTSP.Commencer(false);

                
               
               
            
            

            
            /*Code pour tester plusieurs valeurs de a*/

            /*
            int cpt = 0;
            int cptnul = 0;
            string[,] matricedeRes = new string[10,10];
            int gg = 0;
            string somme = "";
            int m = 0;
            int l = 0;
            int cptj2 = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int k = 6; k < 15; k++)
            {
                
                for (int j = 1; j < 51; j=j+5)
                {
                    
                    for (int i = 0; i < 20; i++)
                    {
                        JMCTSP j1 = new JMCTSP(k, k, 100,4);
                        JMCTSP j2 = new JMCTSP(j, j,100,4);
                        PositionP4 posall = new PositionP4(true,6,7);
                        Partie P4 = new Partie(j1, j2, posall);
                        
                        P4.Commencer(false);
                        j1.NouvellePartie();j2.NouvellePartie();
                       if (P4.r == Resultat.j0gagne) cpt++;
                        if (P4.r == Resultat.partieNulle) cptnul++;
                        if (P4.r == Resultat.j1gagne) cptj2++;
                    }
                    somme = cpt.ToString() + " " + cptnul.ToString()+" "+cptj2.ToString();
                    matricedeRes[m, l] = somme;
                    Console.WriteLine(k + " " + j + " " + matricedeRes[m, l]);
                    cpt = 0;
                    cptnul = 0;
                    cptj2 = 0;
                    l++;
               }
                l = 0;
                m++;
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            for (int k = 0; k <= 9; k++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    Console.Write(k + "/" + j + " =" + matricedeRes[k,j] + " ");
                }
                Console.WriteLine("");
            }
            
            */

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }

        }
    }

    public enum Resultat { j1gagne, j0gagne, partieNulle, indetermine }

    public abstract class Position
    {
        public bool j1aletrait;
        public Position(bool j1aletrait) { this.j1aletrait = j1aletrait; }
        public Resultat Eval { get; protected set; }
        public int NbCoups { get; protected set; }
        public abstract void EffectuerCoup(int i);
        public abstract Position Clone();
        public abstract void Affiche();
        public abstract override bool Equals(Object a);
    }

    public abstract class Joueur
    {
        public abstract int Jouer(Position p);
        public virtual void NouvellePartie() { }
    }



    public class Partie
    {
        Position pCourante;
        Joueur j1, j0;
        public Resultat r;

        public Partie(Joueur j1, Joueur j0, Position pInitiale)
        {
            this.j1 = j1;
            this.j0 = j0;
            pCourante = pInitiale.Clone();
        }

        public void NouveauMatch(Position pInitiale)
        {
            pCourante = pInitiale.Clone();
        }

        public void Commencer(bool affichage)
        {
            do
            {
                if (affichage) pCourante.Affiche();
                if (pCourante.j1aletrait)
                {
                    //Console.WriteLine("Tour de j1");
                    pCourante.EffectuerCoup(j1.Jouer(pCourante));
                }
                else
                {
                    //Console.WriteLine("Tour de j2");
                    pCourante.EffectuerCoup(j0.Jouer(pCourante));
                }
            } while (pCourante.NbCoups > 0);
            r = pCourante.Eval;
            if (affichage)
            {
                pCourante.Affiche();
                switch (r)
                {
                    case Resultat.j1gagne: Console.WriteLine("j1 {0} a gagné.", j1); break;
                    case Resultat.j0gagne: Console.WriteLine("j2 {0} a gagné.", j0); break;
                    case Resultat.partieNulle: Console.WriteLine("Partie nulle."); break;
                }
            }
        }
    }



    public class PositionP4 : Position
    {
        public int lignes;
        public int colonnes;
        public int[,] mat;

        public override bool Equals(Object obj)
        {

            PositionP4 o = (PositionP4)obj;
            if (this.lignes != o.lignes || this.colonnes != o.colonnes)
            {
                return false;
            }
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < lignes; j++)
                {
                    if (mat[i, j] != o.mat[i, j]) { return false; }
                }
            }
            return true;
        }



        public PositionP4(bool j1aletrait, int l, int c) : base(j1aletrait)
        {
            lignes = l;
            colonnes = c;
            mat = new int[lignes, colonnes];
            Eval = Resultat.indetermine;
            NbCoups = colonnes;
        }

        int verifier(int[,] m)
        {
            for (int i = 0; i < lignes - 3; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    if (m[i, j] == m[i + 1, j] && m[i, j] == m[i + 2, j] && m[i, j] == m[i + 3, j] && m[i, j] > 0) return m[i, j];
                }
            }

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes - 3; j++)
                {
                    if (m[i, j] == m[i, j + 1] && m[i, j] == m[i, j + 2] && m[i, j] == m[i, j + 3] && m[i, j] != 0)
                        return m[i, j];
                }
            }

            for (int i = 0; i < lignes - 3; i++)
            {
                for (int j = 0; j < colonnes - 3; j++)
                {
                    if (m[i, j] == m[i + 1, j + 1] && m[i, j] == m[i + 2, j + 2] && m[i, j] == m[i + 3, j + 3] && m[i, j] != 0)
                        return m[i, j];
                }
            }

            for (int i = lignes - 1; i > 2; i--)
            {
                for (int j = 0; j < colonnes - 3; j++)
                {
                    if (m[i, j] == m[i - 1, j + 1] && m[i, j] == m[i - 2, j + 2] && m[i, j] == m[i - 3, j + 3] && m[i, j] != 0)
                        return m[i, j];
                }
            }

            return 0;
        }
        public override void EffectuerCoup(int i)
        {
            int k = 0;
            int h = 0;
            while (h != i + 1)
            {
                if (mat[lignes - 1, k] != 0)
                {
                    k++;
                }
                else
                {
                    k++;
                    h++;
                }
            }
            k--;
            int l = 0;
            while (mat[l, k] != 0) { l++; }
            if (j1aletrait)
            {
                mat[l, k] = 1;
            }
            else
            {
                mat[l, k] = 2;
            }

            int f = verifier(mat);

            if (f == 1)
                Eval = Resultat.j1gagne;

            if (f == 2)
                Eval = Resultat.j0gagne;
            if (f == 0)
            {
                Eval = Resultat.partieNulle;
                for (int j = 0; j < colonnes; j++)
                {
                    if (mat[lignes - 1, j] == 0)
                    {
                        Eval = Resultat.indetermine;
                        break;
                    }
                }
            }
            if (mat[lignes - 1, k] != 0) { NbCoups--; }
            if (Eval != Resultat.indetermine)
            {
                NbCoups = 0;
            }
            j1aletrait = !j1aletrait;
        }

        public override Position Clone()
        {
            PositionP4 a = new PositionP4(j1aletrait, lignes, colonnes);
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    int qsd = mat[i, j];
                    a.mat[i, j] = qsd;
                }
            }
            a.NbCoups = this.NbCoups;
            a.Eval = this.Eval;
            return a;
        }

        public override void Affiche()
        {
            for (int i = lignes - 1; i >= 0; i--)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    Console.Write(mat[i, j] + " ");
                }
                Console.Write("\n");
            }
        }
    }

    public class JoueurHumainP4 : Joueur
    {
        public JoueurHumainP4() { }
        public override int Jouer(Position P)
        {
            Console.WriteLine("Vous pouvez jouer dans {0} colonnes, laquelle?", P.NbCoups);
            int m = int.Parse(Console.ReadLine());
            return m - 1;
        }
    }
    public class NoeudP
    {
        static Random gen = new Random();

        public Position p;
        public NoeudP pere;
        public NoeudP[] fils;
        public int[] cross;
        public int[] win;
        public int indiceMeilleurFils;

        public NoeudP(NoeudP pere, Position p, int N)
        {
            this.pere = pere;
            this.p = p;
            fils = new NoeudP[this.p.NbCoups];
            cross = new int[N];
            win = new int[N];
        }

        public void CalculMeilleurFils(Func<int, int, float> phi)
        {
            float s;
            float sM = 0;
            if (p.j1aletrait)
            {
                for (int i = 0; i < fils.Length; i++)
                {
                    if (fils[i] == null)
                    {
                        s = phi(0, 0);

                    }
                    else
                    {
                        int w = 0;
                        int c = 0;
                        for (int j = 0; j < fils[i].win.Length; j++)
                        {
                            w += fils[i].win[j];
                            c += fils[i].cross[j];
                        }

                        s = phi(w, c);

                    }

                    if (s > sM) { sM = s; indiceMeilleurFils = i; }

                }
            }
            else
            {
                for (int i = 0; i < fils.Length; i++)
                {
                    if (fils[i] == null) { s = phi(0, 0); }
                    else
                    {
                        int w = 0;
                        int c = 0;
                        for (int j = 0; j < fils[i].win.Length; j++)
                        {
                            w += fils[i].win[j];
                            c += fils[i].cross[j];
                        }

                        s = phi(c - w, c);
                    }
                    if (s > sM) { sM = s; indiceMeilleurFils = i; }
                }
            }
        }


        public NoeudP MeilleurFils()
        {
            if (fils[indiceMeilleurFils] != null)
            {
                return this.fils[indiceMeilleurFils];
            }
            Position q = p.Clone();
            q.EffectuerCoup(indiceMeilleurFils);
            fils[indiceMeilleurFils] = new NoeudP(this, q, cross.Length);
            
            return this.fils[indiceMeilleurFils];
        }

        /*public override string ToString()
        {
            string s = "";
            s = s + "indice MF = " + indiceMeilleurFils;
            s += String.Format(" note= {0}\n", fils[indiceMeilleurFils] == null ? "?" : ((1F * fils[indiceMeilleurFils].win) / fils[indiceMeilleurFils].cross).ToString());
            int sc = 0;
            for (int k = 0; k < fils.Length; k++)
            {
                if (fils[k] != null)
                {
                    sc += fils[k].cross;
                    s += (fils[k].win + "/" + fils[k].cross + " ");
                }
                else s += (0 + "/" + 0 + " ");
            }
            s += "\n nbC=" + (sc / 2);
            return s;
        }*/

    }
    public class JMCTSP : Joueur
    {
        public static Random gen = new Random();
        static Stopwatch sw = new Stopwatch();
        private object verrou = new Object();
        int NbrThreads;
        float a, b;
        int temps;
        NoeudP racine;
        NoeudP[] memoire;
        public override void NouvellePartie()
        {
            memoire = null;
        }
        public JMCTSP(float a, float b, int temps,int N)
        {
            this.a = 2 * a;
            this.b = 2 * b;
            this.temps = temps;
            this.NbrThreads = N;
        }

        public override string ToString()
        {
            return string.Format("JMCTS[{0} - {1} - temps={2}]", a / 2, b / 2, temps);
        }

        int JeuHasard(Position p)
        {
            Position q = p.Clone();
            int re = 1;
            while (q.NbCoups > 0)
            {
                q.EffectuerCoup(gen.Next(0, q.NbCoups));
            }
            if (q.Eval == Resultat.j1gagne) { re = 2; }
            if (q.Eval == Resultat.j0gagne) { re = 0; }
            return re;
        }

        public int Thread(Func<int, int, float> phi, int iff)
        {
            lock (verrou)
            {
                int iter = 0;
                while (sw.ElapsedMilliseconds < temps)
                {
                    NoeudP no = racine;
                    int SommeDesCross = 0;

                    do // Sélection
                    {
                        no.CalculMeilleurFils(phi);

                        no = no.MeilleurFils();

                        SommeDesCross = 0;
                        for (int j = 0; j < no.cross.Length; j++)
                        {
                            SommeDesCross = SommeDesCross + no.cross[j];
                        }
                    } while (SommeDesCross > 0 && no.fils.Length > 0);


                    int re = JeuHasard(no.p); // Simulation

                    while (no != null) // Rétropropagation
                    {
                        no.cross[iff] += 2;
                        no.win[iff] += re;
                        no = no.pere;
                    }
                    iter++;
                }
                return iter;
            }
        }
        public override int Jouer(Position p)
        {
           
            Func<int, int, float> phi = (W, C) => (a + W) / (b + C);
            racine = null;
            int j = 0;
            while (racine == null)
            {
                if (memoire == null) { racine = new NoeudP(null, p, NbrThreads); break; }
                if (memoire[j] == null)
                {
                 
                    racine = new NoeudP(null, p, NbrThreads); break;
                }

                if (p.Equals((object)memoire[j].p))
                {
                    racine = memoire[j];
                }
                j++;
            }
            int iter = j;
            sw.Restart();

            Task<int>[] tblthread = new Task<int>[NbrThreads];
            for (int k = 0; k < NbrThreads; k++)
            {
                int indiceThread = k;
                tblthread[indiceThread] = Task.Run(() => Thread(phi, indiceThread));
                iter = tblthread[indiceThread].Result + iter;

            }
            Task.WaitAll(tblthread);
            racine.CalculMeilleurFils(phi);
            Console.WriteLine("{0} itérations", iter);
            //Console.WriteLine(racine.fils);
            memoire = racine.fils[racine.indiceMeilleurFils].fils;
            return racine.indiceMeilleurFils;


        }
    }

    public class Noeud
    {
        static Random gen = new Random();

        public Position p;
        public Noeud pere;
        public Noeud[] fils;
        public int cross, win;
        public int indiceMeilleurFils;

        public Noeud(Noeud pere, Position p)
        {
            this.pere = pere;
            this.p = p;
            fils = new Noeud[this.p.NbCoups];
        }

        public void CalculMeilleurFils(Func<int, int, float> phi)
        {
            float s;
            float sM = 0;
            if (p.j1aletrait)
            {
                for (int i = 0; i < fils.Length; i++)
                {
                    if (fils[i] == null)
                    {
                        s = phi(0, 0);

                    }
                    else
                    {
                        s = phi(fils[i].win, fils[i].cross);

                    }

                    if (s > sM) { sM = s; indiceMeilleurFils = i; }

                }
            }
            else
            {
                for (int i = 0; i < fils.Length; i++)
                {
                    if (fils[i] == null) { s = phi(0, 0); }
                    else { s = phi(fils[i].cross - fils[i].win, fils[i].cross); }
                    if (s > sM) { sM = s; indiceMeilleurFils = i; }
                }
            }
        }


        public Noeud MeilleurFils()
        {
            if (fils[indiceMeilleurFils] != null)
            {
                return fils[indiceMeilleurFils];
            }
            Position q = p.Clone();
            q.EffectuerCoup(indiceMeilleurFils);
            fils[indiceMeilleurFils] = new Noeud(this, q);
            return fils[indiceMeilleurFils];
        }

        public override string ToString()
        {
            string s = "";
            s = s + "indice MF = " + indiceMeilleurFils;
            s += String.Format(" note= {0}\n", fils[indiceMeilleurFils] == null ? "?" : ((1F * fils[indiceMeilleurFils].win) / fils[indiceMeilleurFils].cross).ToString());
            int sc = 0;
            for (int k = 0; k < fils.Length; k++)
            {
                if (fils[k] != null)
                {
                    sc += fils[k].cross;
                    s += (fils[k].win + "/" + fils[k].cross + " ");
                }
                else s += (0 + "/" + 0 + " ");
            }
            s += "\n nbC=" + (sc / 2);
            return s;
        }
        
    }
    public class JMCTSp: Joueur
    {
        public static Random gen = new Random();
        static Stopwatch sw = new Stopwatch();
        private Object verrou = new Object();
        float a, b;
        int Nbrthreads;
        int temps;
        Noeud racine;
        Noeud[] memoire;
        public override void NouvellePartie()
        {
            memoire = null;
        }
        
        public JMCTSp(float a,float b,int temps, int N)
        {
            this.Nbrthreads = N;
            this.a = a;
            this.b = b;
            this.temps = temps;
        }
        public override int Jouer(Position p)
        {
            
            Func<int, int, float> phi = (W, C) => (a + W) / (b + C);
            
            racine = null;
            int j = 0;
            
            while (racine == null)
            {
                if (memoire == null) { racine = new Noeud(null, p); break; }
                if (memoire[j] == null)
                {
                    
                    racine = new Noeud(null, p); break;
                }
               

                if (p.Equals((object)memoire[j].p))
                {
                    racine = memoire[j];
                }
               
                j++;
            }
           
            int iter = j;
            sw.Restart();
            
            while (sw.ElapsedMilliseconds < temps)
            {
                Noeud no = racine;
                
                do // Sélection
                {
                    no.CalculMeilleurFils(phi);
                    no = no.MeilleurFils();
                 
                } while (no.cross > 0 && no.fils.Length > 0);


                Task<int>[] tblthread = new Task<int>[Nbrthreads];
                int re =0;
                for (int i = 0; i < Nbrthreads; i++)
                {
                tblthread[i] = Task.Run(() => JeuHasard(no.p));
                   
                
                }
                Task.WaitAll(tblthread);
                for(int jj=0;jj<Nbrthreads;jj++)
                {
                    re = re + tblthread[jj].Result;
                }
                
                // Simulation

                while (no != null) // Rétropropagation
                {
                    no.cross += 2*Nbrthreads;
                    no.win += re;
                    no = no.pere;
                 
                }
              
                iter++;
            }
            racine.CalculMeilleurFils(phi);
            racine.MeilleurFils().p.Affiche();
            Console.WriteLine("{0} itérations", iter);
            Console.WriteLine(racine);
            memoire = racine.fils[racine.indiceMeilleurFils].fils;
            return racine.indiceMeilleurFils;


        }
        public override string ToString()
        {
            return string.Format("JMCTS[{0} - {1} - temps={2}]", a / 2, b / 2, temps);
        }
        int JeuHasard(Position p)
        {
            Position q = p.Clone();
            int re = 1;
            while (q.NbCoups > 0)
            {
                q.EffectuerCoup(gen.Next(0, q.NbCoups));
            }
            if (q.Eval == Resultat.j1gagne) { re = 2; }
            if (q.Eval == Resultat.j0gagne) { re = 0; }
            return re;
        }
    }
    public class JMCTS : Joueur
    {
        public static Random gen = new Random();
        static Stopwatch sw = new Stopwatch();

        float a, b;
        int temps;
        Noeud racine;
        Noeud[] memoire;
        public override void NouvellePartie()
        {
            memoire = null;
        }
        public JMCTS(float a, float b, int temps)
        {
            this.a = 2 * a;
            this.b = 2 * b;
            this.temps = temps;
        }

        public override string ToString()
        {
            return string.Format("JMCTS[{0} - {1} - temps={2}]", a / 2, b / 2, temps);
        }

        int JeuHasard(Position p)
        {
            Position q = p.Clone();
            int re = 1;
            while (q.NbCoups > 0)
            {
                q.EffectuerCoup(gen.Next(0, q.NbCoups));
            }
            if (q.Eval == Resultat.j1gagne) { re = 2; }
            if (q.Eval == Resultat.j0gagne) { re = 0; }
            return re;
        }


        public override int Jouer(Position p)
        {
           
            Func<int, int, float> phi = (W, C) => (a + W) / (b + C);
         
            racine = null;
            int j = 0;
         
            while (racine == null)
            {
                if (memoire == null) { racine = new Noeud(null, p); break; }
                if (memoire[j] == null)
                {
                    
                    racine = new Noeud(null, p); break;
                }
               

                if (p.Equals((object) memoire[j].p))
                {
                    racine = memoire[j];
                }
                
                j++;
            }
           
            int iter = j;
            sw.Restart();
            while (sw.ElapsedMilliseconds < temps)
            {
                Noeud no = racine;
             
                do // Sélection
                {
                    no.CalculMeilleurFils(phi);
                    no = no.MeilleurFils();
                  
                } while (no.cross > 0 && no.fils.Length > 0);


                int re = JeuHasard(no.p); // Simulation

                while (no != null) // Rétropropagation
                {
                    no.cross += 2;
                    no.win += re;
                    no = no.pere;
                    
                }
           
                iter++;
            }
            racine.CalculMeilleurFils(phi);
            racine.MeilleurFils().p.Affiche();
            Console.WriteLine("{0} itérations", iter);
            Console.WriteLine(racine);
            memoire = racine.fils[racine.indiceMeilleurFils].fils;
            return racine.indiceMeilleurFils;


        }
    }
}

