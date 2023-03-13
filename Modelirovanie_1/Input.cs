using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Modelirovanie_1;

namespace Modelirovanie
{
    public partial class Input : Form
    {
        public readonly List<char> InputStr;
        private int _index;
        private readonly MainForm _mainForm;

        public Input(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            textBox_input.Focus();
            InputStr = new List<char>();
            _index = 0;
        }

        private new void Show()
        {
            var result = new StringBuilder();
            foreach (var s in InputStr)
                result.Append(s);
            textBox_input.Text = result.ToString();
            textBox_input.SelectionStart = ++_index;
            textBox_input.Focus();
        }
        
        private void Input_Load(object sender, EventArgs e)
        {
            AddButton(groupBox1);
            AddButton(groupBox2);
        }
        
        private void AddButton(Control groupBox)
        {
            foreach (var item in groupBox.Controls)
            {
                if (item is Button button)
                {
                    button.Click += CommonBtn_Click;
                }
            }
        }

        private void CommonBtn_Click(object sender, EventArgs e)
        {
            var msg = ((Button)sender).Text;
            if (msg.Length > 1)
            {
                foreach (var c in msg) 
                    InputStr.Insert(_index++, c);
                InputStr.Insert(_index++, '(');
                InputStr.Insert(_index, ')');
                _index--;
            }
            else
                InputStr.Insert(_index, msg[0]);

            Show();

        }

         private void button_ok_Click(object sender, EventArgs e)
        {
            if (CheckOperation() || CheckBranch())
                return;

            _mainForm.ShowInputData(this);
            Close();
        }

        private void button_close_Click(object sender, EventArgs e) => Close();

        #region Move

        private void button_move_left_Click(object sender, EventArgs e)
        {
            _index = _index > 0 ? _index : ++_index;
            textBox_input.SelectionStart = --_index;
            textBox_input.Focus();
        }

        private void button_move_right_Click(object sender, EventArgs e)
        {
            _index = _index <= InputStr.Count - 1 ? _index : --_index;
            textBox_input.SelectionStart = ++_index;
            textBox_input.Focus();
        }

        #endregion

        private void button_clear_Click(object sender, EventArgs e)
        {
            _index = 0;
            InputStr.Clear();
            var result = new StringBuilder();
            foreach (var s in InputStr)
                result.Append(s);
            textBox_input.Text = result.ToString();
            textBox_input.SelectionStart = _index + 1;
            textBox_input.Focus();
        }

        private bool CheckOperation()
        {
            var arrayOperations = new[] {'+', '-', '*', '/', '^'};
            for (var index = 0; index < InputStr.Count - 1; index++)
            {
                var c = InputStr[index];
                var c2 = InputStr[index + 1];
                if (arrayOperations.Contains(c) && arrayOperations.Contains(c2))
                {
                    MessageBox.Show(@"Операторы:" + c + c2 + @" идут подряд!", @"Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }

                if (char.IsUpper(c) && char.IsUpper(c2))
                {
                    MessageBox.Show(@"Переменные: " + c +" "+ c2 + @" идут подряд!", @"Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }

            if (arrayOperations.Contains(InputStr[InputStr.Count - 1]))
            {
                MessageBox.Show(@"Оператор:" + InputStr[InputStr.Count - 1] + @" идет последний!", @"Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            return false;
        }

        private bool CheckBranch()
        {
            for (var index = 0; index < InputStr.Count - 1; index++)
            {
                var c = InputStr[index];
                var c2 = InputStr[index + 1];
                if (c == '(' && c2 == ')')
                {
                    MessageBox.Show(@"В скобках ничего не указано!", @"Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }

                if (char.IsUpper(c) && c2 == '(')
                {
                    MessageBox.Show(@"Между переменной " + c + @" и скобкой отсутствует знак!", @"Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
                
                if (c == ')' && char.IsUpper(c2))
                {
                    MessageBox.Show(@"Между cкобкой ) и переменной " + c2 + @" отсутствует знак!", @"Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }

            return false;
        }
    }
}