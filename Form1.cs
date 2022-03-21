using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DES_algoritmas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }



        private static string Encrypt(string message, string password)
        {
            //Encode message and password
            byte[] messagebytes = ASCIIEncoding.ASCII.GetBytes(message);
            byte[] passwordbytes = ASCIIEncoding.ASCII.GetBytes(password);

            //Set encryption setting
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            ICryptoTransform transfrom = provider.CreateEncryptor(passwordbytes, passwordbytes);
            CryptoStreamMode mode = CryptoStreamMode.Write;

            //Set up streams and encrypt

            MemoryStream newstream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(newstream, transfrom, mode);
            cryptoStream.Write(messagebytes, 0, messagebytes.Length);
            cryptoStream.FlushFinalBlock();

            //Read the encrypted message from the memory stream
            byte[] encryptedMessageBytes = new byte[newstream.Length];
            newstream.Position = 0;
            newstream.Read(encryptedMessageBytes, 0, encryptedMessageBytes.Length);
            

            //Encode the encrypted message as base64 string

            string encryptedMessage = Convert.ToBase64String(encryptedMessageBytes);

            return encryptedMessage;


        }

        private static string Decrypt(string encryptedMessage, string password)
        {
            //Convert encrypted message and pasword to bytes
            byte[] encryptedMessageBytes = Convert.FromBase64String(encryptedMessage);
            byte[] passwordBytes = ASCIIEncoding.ASCII.GetBytes(password);

            //Set encryption settings
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            ICryptoTransform transform = provider.CreateDecryptor(passwordBytes, passwordBytes);
            CryptoStreamMode mode = CryptoStreamMode.Write;

            //Set up streams and decrpyt
            MemoryStream newStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(newStream, transform, mode);
            cryptoStream.Write(encryptedMessageBytes, 0, encryptedMessageBytes.Length);
            cryptoStream.FlushFinalBlock();

            //Read the decrypted message from memory stream
            byte[] decryptedMessageBytes = new byte[newStream.Length];
            newStream.Position = 0;
            newStream.Read(decryptedMessageBytes, 0, decryptedMessageBytes.Length);

            //Encode decrypted message data to base64 string

            string message = ASCIIEncoding.ASCII.GetString(decryptedMessageBytes);

            return message;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = textBox1.Text;
            string password = textBox2.Text;

            string message2 = Encrypt(message, password);
           textBox3.Text = message2;
            textBox4.Text = Decrypt(message2, password);

            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string message = textBox1.Text;
            string password = textBox2.Text;

            string message2 = Decrypt(message, password);
            textBox3.Text = message2;
            textBox4.Text = Encrypt(message2, password);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;

                using (StreamWriter writer = new StreamWriter(sFileName))
                {
                    writer.WriteLine(textBox3.Text);
                    writer.WriteLine(textBox2.Text);

                    MessageBox.Show("Užšifruotas tekstas išsaugotas (su raktu)");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;

                using (StreamReader writer = new StreamReader(sFileName))
                {
                    textBox1.Text = writer.ReadLine();
                    textBox2.Text = writer.ReadLine();

                    textBox3.Clear();
                    textBox4.Clear();

                    MessageBox.Show("Užšifruotas tekstas sukaitytas");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
}