using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Prog_Graph_BDD_A2024
{
    internal class Statistiques
    {
        int idStatistiques;
        int nbrTotalParticipants;
        string participantPlusActif;
        double moyenneNoteApp;
        int idSeance;

        public Statistiques(int idStatistiques, int nbrTotalParticipants, string participantPlusActif, double moyenneNoteApp, int idSeance)
        {
            this.idStatistiques = idStatistiques;
            this.nbrTotalParticipants = nbrTotalParticipants;
            this.participantPlusActif = participantPlusActif;
            this.moyenneNoteApp = moyenneNoteApp;
            this.idSeance = idSeance;
        }

        public int IdStatistique {  
            get { return idStatistiques; } 
            set { idStatistiques = value; } 
        }

        public int NbrTotalPartipants { 
            get { return nbrTotalParticipants; } 
            set { nbrTotalParticipants = value; } 
        }

        public string ParticipantPlusActif { 
            get { return participantPlusActif; } 
            set { participantPlusActif = value; } 
        }
        
        public double MoyenneNoteApp { 
            get { return moyenneNoteApp; } 
            set { moyenneNoteApp = value; } 
        }

        public int IdSeance { 
            get { return idSeance; } 
            set { idSeance = value; } 
        }  
    }
}
