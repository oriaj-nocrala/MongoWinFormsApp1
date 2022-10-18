using MongoDB.Bson;
using MongoDB.Driver;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<Persona>listape = new List<Persona>();
        static MongoClient client = new MongoClient("mongodb://localhost:27017");
        static IMongoDatabase database = client.GetDatabase("basedatos_csharp");
        IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("personas");
        public Form1()
        {
            InitializeComponent();
            listBox1.DataSource = ObtenerPersonas();
        }
        private void AñadirPersona(Persona p)
        {
            var documento = new BsonDocument {
                {"nombre", p.Nombre},
                {"esbakan", p.EsBakan }
            };
            collection.InsertOneAsync(documento).Wait();
            listBox1.DataSource = ObtenerPersonas();
        }
        private void EliminarPersona(Persona p)
        {
            listBox1.Items.Remove(p.Nombre);
            listape.Remove(p);
        }

        List<BsonDocument> ObtenerPersonas()
        {
            return collection.Find(new BsonDocument()).ToList();
        }

        class Persona
        {
            public string Nombre { get; set; } = string.Empty;
            public Boolean EsBakan { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona();
            persona.Nombre = textBox1.Text;
            persona.EsBakan = radioButton1.Checked? true:false;
            AñadirPersona(persona);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            // Tengo que buscar cómo hacer para que al hacer doble click
            // Pueda tener el nombre de la persona, para mostrarlo en un
            // alert.
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Delete) EliminarPersona(listape[listBox1.SelectedIndex]);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guardarListaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}