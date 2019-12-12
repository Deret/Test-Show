using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Caesar_sCode
{
    public partial class Caesar : Form
    {
        private static string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static char[] newAlpha = new char[36];
        public Caesar()
        {
            InitializeComponent();
        }
        public static string encrypt(string Message)
        {
            string res = "";
            foreach (char ch in Message)
            {
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (ch == alpha[i])
                    {
                        res += newAlpha[i];
                        break;
                    }
                }
            }
            return res;
        }
 
        public static string decrypt(string Message)
        {
            string res = "";
            foreach (char ch in Message)
            {
                for (int i = 0; i < newAlpha.Length; i++)
                {
                    if (ch == newAlpha[i])
                    {
                        res += alpha[i];
                        break;
                    }
                }
            }
            return res;
        }
 
        public static void createNewAlpha(string keyWord, int key) // создаёт новый алфавит с помощью ключа
        {
            newAlpha = new char[36];
            bool findSame = false;
         
            int beg = 0, current = key;
            // добавить ключевое слово в новый алфавит
            for (int i = key; i < keyWord.Length + key; i++)
            {
                for (int j = key; j < keyWord.Length + key; j++)
                {
                    if (keyWord[i - key] == newAlpha[j])
                    {
                        findSame = true;
                        break;
                    }
                }
                if (!findSame)// если повторений нету, то буква добавляется в новый алфавит
                {
                    newAlpha[current] = keyWord[i - key];
                    current++;
                }
                findSame = false;
            }
 
            // добавить буквы после ключевого слова
            for (int i = 0; i < alpha.Length; i++)
            {
                for (int j = 0; j < newAlpha.Length; j++)
                {
                    if (alpha[i] == newAlpha[j])
                    {
                        findSame = true;
                        break;
                    }
                }
                if (!findSame)
                {
                    newAlpha[current] = alpha[i];
                    current++;
                }
                findSame = false;
                if (current == newAlpha.Length)
                {
                    beg = i;
                    break;
                }
            }
 
            // добавить буквы после ключевого слова
            current = 0;
            for (int i = beg; i < alpha.Length; i++)
            {
                for (int j = 0; j < newAlpha.Length; j++)
                {
                    if (alpha[i] == newAlpha[j])
                    {
                        findSame = true;
                        break;
                    }
                }
                if (!findSame)
                {
                    newAlpha[current] = alpha[i];
                    current++;
                }
                findSame = false;
            }
        }
 
        public static string getNewAlpha()
        {
            string strNewAlpha = new string(newAlpha);
            return strNewAlpha;
        }
        string open = "",close = "";
        private void button1_Click(object sender, EventArgs e)
        {
            
            richTextBox2.Clear();
            string keyWord = textBox1.Text;
            int key = int.Parse(textBox2.Text);
            
           
            Caesar.createNewAlpha(keyWord, key);  
            open = "";
            close = "";
            close = Caesar.encrypt(richTextBox1.Text);
            richTextBox2.AppendText("Зашифрованное: " + close + "\n");
            richTextBox2.AppendText("");
            try
            {
                richTextBox2.AppendText("1. " + close.GroupBy(c => c).OrderByDescending(g => g.Count()).ElementAt(0).Key.ToString() + " " + close.GroupBy(c => c).OrderByDescending(g => g.Count()).ElementAt(0).Count().ToString() + "\n");
                richTextBox2.AppendText("2. " + close.GroupBy(c => c).OrderByDescending(g => g.Count()).ElementAt(1).Key.ToString() + " " + close.GroupBy(c => c).OrderByDescending(g => g.Count()).ElementAt(1).Count().ToString() + "\n");
                richTextBox2.AppendText("3. " + close.GroupBy(c => c).OrderByDescending(g => g.Count()).ElementAt(2).Key.ToString() + " " + close.GroupBy(c => c).OrderByDescending(g => g.Count()).ElementAt(2).Count().ToString() + "\n");
            }
            catch
            {
                MessageBox.Show("ENTER AT LEAST 3 SYMBOLS");
            }
           
            File.WriteAllText("message.txt", close);
            textBox3.Clear();
            for (int i = 0; i < newAlpha.Length; i++)
            {
                textBox3.Text +=newAlpha[i];
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            string message = "";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                message = File.ReadAllText(openFileDialog1.FileName);
            }
            richTextBox2.AppendText(message);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string close = File.ReadAllText(openFileDialog1.FileName);
                close = Caesar.decrypt(close);
                richTextBox2.AppendText("Рассшифрованное: " + close + "\n");
            }
        }
    }
}
