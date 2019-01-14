public class JMCTSP : Joueur
{
    public static Random gen = new Random();
    static Stopwatch sw = new Stopwatch();
    int NbrThreads;
    float a, b;
    int temps;
    NoeudP racine;
    NoeudP[] memoire;
    public override void NouvellePartie()
    {
        memoire = null;
    }
    public JMCTSP(float a, float b, int temps, int N)
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
        int iter = 0;
        while (sw.ElapsedMilliseconds < temps)
        {
            NoeudP no = this.racine;
            //Console.WriteLine(no);
            int Sommenocross = 0;

            do // Sélection
            {
                no.CalculMeilleurFils(phi);
                no = no.MeilleurFils();
                //Sommenocross = 0;

                //for (int j = 0; j < no.cross.Length;j++)
                //{
                //  Sommenocross =Sommenocross+ no.cross[j];
                //}
                //Console.WriteLine("bbbbb");
            } while (no.cross[iff] > 0 && no.fils.Length > 0);


            int re = JeuHasard(no.p); // Simulation

            while (no != null) // Rétropropagation
            {
                no.cross[iff] += 2;
                no.win[iff] += re;
                no = no.pere;
                //Console.WriteLine(no);
            }
            //Console.WriteLine("aaaaa");
            iter++;
        }
        return iter;
    }
    public override int Jouer(Position p)
    {

        Func<int, int, float> phi = (W, C) => (a + W) / (b + C);
        //Console.WriteLine("C'est p au début");
        //p.Affiche();
        racine = null;
        int j = 0;
        //Console.WriteLine(memoire);
        while (racine == null)
        {
            if (memoire == null) { racine = new NoeudP(null, p, NbrThreads); break; }
            if (memoire[j] == null)
            {
                Console.WriteLine("Rentré");
                racine = new NoeudP(null, p, NbrThreads); break;
            }
            //memoire[j].p.Affiche();

            if (p.Equals((object)memoire[j].p))
            {
                racine = memoire[j];
            }
            //Console.WriteLine(j);
            j++;
        }
        //racine.p.Affiche();
        //Console.WriteLine(racine);
        int iter = j;
        sw.Restart();
        Task<int>[] tblthread = new Task<int>[NbrThreads];
        int cassecouille = 0;
        for (int k = 0; k < NbrThreads; k++)
        {
            cassecouille = k;
            tblthread[cassecouille] = Task.Run(() => Thread(phi, cassecouille));


        }
        Task.WaitAll(tblthread);
        racine.CalculMeilleurFils(phi);
        //racine.MeilleurFils().p.Affiche();
        //Console.WriteLine("{0} itérations", iter);
        //Console.WriteLine(racine);
        memoire = racine.fils[racine.indiceMeilleurFils].fils;
        return racine.indiceMeilleurFils;


    }
}