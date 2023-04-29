using System.Net;

namespace BizimBook
{
    public partial class Form1 : Form
    {

        List<Kitap> books = new List<Kitap>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            WebClient webKitap = new WebClient();

            string gelenWebKitap = webKitap.DownloadString("https://www.goodreads.com/list/show/18834.BBC_Top_200_Books");


            int BaslangicIndex = gelenWebKitap.IndexOf("tableList js-dataTooltip");

            int SonIndex = gelenWebKitap.IndexOf("</table>");

            string AralikIndex = gelenWebKitap.Substring(BaslangicIndex, SonIndex - BaslangicIndex);

            List<string> list = new List<string>();

            //bool kontrol = AralikIndex.Contains("<tr");
            //while (kontrol)
            //{
            //    int StarIndex = AralikIndex.IndexOf("<tr itemscope=\"\" itemtype=\"http://schema.org/Book\">");
            //    int EndIndex = AralikIndex.IndexOf("5 of 5 stars</a></div>");

            //    string BookListele = AralikIndex.Substring(StarIndex + 15, EndIndex - StarIndex - 75);
            //    AralikIndex = BookListele;

            //    list.Add(BookListele);
            //}

            //int count = 0;
            //AralikIndex.Count("<tr")


            while (AralikIndex.Contains("<tr"))
            {
                int StarIndex = AralikIndex.IndexOf("<tr itemscope itemtype=\"http://schema.org/Book\">");
                int EndIndex = AralikIndex.IndexOf("</tr>");

                string BookListele = AralikIndex.Substring(StarIndex, EndIndex - StarIndex);

                list.Add(BookListele);

                AralikIndex = AralikIndex.Substring(EndIndex + 6);
            }

            foreach (string dongu in list)
            {
                Kitap kitap = new Kitap();

                int KitapIsmiIlkIndex = dongu.IndexOf("<a title");

                int KitapIsmýIkinciIndex = dongu.IndexOf("href");

                kitap.Name = dongu.Substring(KitapIsmiIlkIndex + 9 , KitapIsmýIkinciIndex - KitapIsmiIlkIndex - 9);

                books.Add(kitap);
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = books;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}