using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            //При загрузке формы Text Box очищается
            textBox1.Clear();

        }

        public void SaveText()
        {
            string curDateLong = DateTime.Now.ToShortDateString();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "No name";
            sfd.Filter = "Text file|*.txt|Add file|*.*";

            //Сохраняется и по завершению работы Text Box очищается
            if (sfd.ShowDialog() == DialogResult.OK)
            {
               
               File.WriteAllText(sfd.FileName,"Text " + textBox1.Text + " | Date read: "+ curDateLong, Encoding.Unicode);
               textBox1.Clear();
            }
        }

        public void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Проверка на наличие символов в textBox
            if (textBox1.TextLength!=0)
            {
                //Если символы есть возникает сообщение и две кнопки "Yes" и "No"
                DialogResult rezult = MessageBox.Show("The program contains text. Save?","",MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                //Если "Yes"
                if (rezult == DialogResult.Yes)
                {
                    //Вызывается метод SaveText
                    SaveText();
                }
                if (rezult == DialogResult.No)
                {
                    //Text Box очищается 
                    textBox1.Clear();
                }
            }
        }

        public void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //Создание фильтра для открываемого файла
            ofd.Filter = "Text file|*.txt|Add file|*.*";
            //Диалог открытия файла
            ofd.ShowDialog();
            if (ofd.FileName == String.Empty)
                return;
            //Чтение файла и кодировка
            try
            {
                var Reader = new System.IO.StreamReader(ofd.FileName,
                    Encoding.Unicode);
                textBox1.Text = Reader.ReadToEnd();
            }
            //Обработка исключения на отсутствие файла
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message + "\not file", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            //Обработка исключения ошибки
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }


        }

        public void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveText();
        }

        public void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText.CopyTo(0, copy, textBox1.TextLength, 0);
        }

        public void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string past = copy.ToString();
            textBox1.Paste(past);
        }

        char[] copy = new char[500];

        public void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //обработка закрытия формы
            if (textBox1.Modified == false)
                return;
            //Закрыть или нет если были изменения
            var MBox = MessageBox.Show("Text modify. " + "Save modification?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (MBox == DialogResult.No)
                return;
            if (MBox == DialogResult.Cancel)

                if (MBox == DialogResult.Yes)
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        SaveText();
                        return;
                    }

                }
            this.Close();
        }


    }
}
