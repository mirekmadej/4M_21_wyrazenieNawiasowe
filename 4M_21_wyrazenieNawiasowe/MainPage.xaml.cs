using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;
namespace _4M_21_wyrazenieNawiasowe
{
    public partial class MainPage : ContentPage
    {
        private static Stack<char> stos = new Stack<char>();

        public MainPage()
        {
            string connStr = "server=localhost;user=root;database=pojazd;port=3306;password=";
            MySqlConnection con = new MySqlConnection(connStr);
            string q = "SELECT marka FROM pojazd";
            con.Open();
            MySqlCommand com = new MySqlCommand(q, con);
            MySqlDataReader rdr = com.ExecuteReader();
            var marki = new List<string>();
            while (rdr.Read())
            {
                marki.Add(rdr[0].ToString());
            }
            rdr.Close();
            con.Close();

            InitializeComponent();

            var ht = new HashSet<string>();
            foreach (var s in marki)
                ht.Add(s);
            lblLiczbaMarek.Text = $"Liczba unikalnych marek {ht.Count}";
            var slownik = new Dictionary<string, int>();
            foreach(var s in marki)
            {
                if (!slownik.ContainsKey(s))
                    slownik.Add(s, 1);
                else
                    slownik[s]++;
            }
            string m = "";
            foreach (var s in slownik)
            {
                m += s.Key + " " + s.Value + "\n";
            }

            lblMarki.Text = m;
        }

        private void onCompleted(object sender, EventArgs e)
        {
            stos.Clear();
            string s = entWyrazenie.Text;
            foreach (char c in s)
            {
                if(c == '(') stos.Push(c);
                else
                {
                    if(stos.Count == 0)
                    {
                        lblStatus.Text = "Status wyrażenia: niepoprawne";
                        return;
                    }
                    else
                        stos.Pop();
                }
            }
            if (stos.Count == 0)
                lblStatus.Text = "Status wyrażenia: poprawne";
            else
                lblStatus.Text = "Status wyrażenia: niepoprawne";
        }

        private void onCompletedKierunki(object sender, EventArgs e)
        {
            string s = entKierunki.Text.ToUpper();
            var ht = new HashSet<string>();
            int x=0, y=0;
            foreach (char c in s)
            {
                switch (c)
                {
                    case 'N': y++; break;
                    case 'S': y--; break;
                    case 'E': x++; break;
                    case 'W': x--; break;                    
                }
                ht.Add(x.ToString()+"*"+y.ToString());
            }
            lblKierunki.Text = $"Liczba unikalnych pól: {ht.Count}";

        }




    }

}
