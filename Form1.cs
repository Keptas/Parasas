using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
namespace parasas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ASCIIEncoding ByteConverter = new ASCIIEncoding();
        RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
       
        public static byte[] HashAndSignBytes(byte[] DataToSign, RSAParameters Key)
        {
            try
            {     
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.ImportParameters(Key);
                return RSAalg.SignData(DataToSign, SHA256.Create());
            }
            catch (CryptographicException e)
            {
                MessageBox.Show(e.Message);

                return null;
            }
        }
        public static bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData, RSAParameters Key)
        {
            try
            {           
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.ImportParameters(Key);
                return RSAalg.VerifyData(DataToVerify, SHA256.Create(), SignedData);
            }
            catch (CryptographicException e)
            {
                

                return false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            string dataString = textBox1.Text;
            RSAParameters Key = RSAalg.ExportParameters(true);
            byte[] originalData = ByteConverter.GetBytes(dataString);

            richTextBox1.Text = String.Join(" ", HashAndSignBytes(originalData, Key));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = richTextBox1.Text;
        }
        //
        private void button3_Click(object sender, EventArgs e)
        {
            RSAParameters Key = RSAalg.ExportParameters(true);
            byte[] originalData = ByteConverter.GetBytes(textBox1.Text);
            byte[] bytes = ByteConverter.GetBytes(richTextBox2.Text);
            if (VerifySignedHash( originalData , bytes, Key))
            {
                MessageBox.Show("Patvirtinta");
            }
            else
            {
                MessageBox.Show("Nepatvirtinta");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string dataString = textBox1.Text;
            RSAParameters Key = RSAalg.ExportParameters(true);
            byte[] originalData = ByteConverter.GetBytes(dataString);
            byte[] str = HashAndSignBytes(originalData, Key);
            if (VerifySignedHash(originalData, str, Key))
            {
                MessageBox.Show("Patvirtinta");
            }
            else
            {
                MessageBox.Show("Nepatvirtinta");
            }
        }
    }
}
