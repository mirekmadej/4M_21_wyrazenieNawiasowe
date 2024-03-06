namespace _4M_21_wyrazenieNawiasowe
{
    public partial class MainPage : ContentPage
    {
        private static Stack<char> stos = new Stack<char>();

        public MainPage()
        {
            InitializeComponent();
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
    }

}
