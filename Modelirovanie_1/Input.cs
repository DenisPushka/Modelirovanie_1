using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Modelirovanie_1
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

        #region Symbol

        private void number_1_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '1');
            Show();
        }

        private void number_2_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '2');
            Show();
        }

        private void number_3_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '3');
            Show();
        }

        private void number_4_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '4');
            Show();
        }

        private void number_5_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '5');
            Show();
        }

        private void number_6_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '6');
            Show();
        }

        private void number_7_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '7');
            Show();
        }

        private void number_8_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '8');
            Show();
        }

        private void number_9_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '9');
            Show();
        }

        private void number_0_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '0');
            Show();
        }

        private void number_left_bracket_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '(');
            Show();
        }

        private void number_right_bracket_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, ')');
            Show();
        }

        #endregion

        #region Operation

        private void number_plus_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '+');
            Show();
        }

        private void number_minus_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '-');
            Show();
        }

        private void number_multiplication_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '*');
            Show();
        }

        private void number_division_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '/');
            Show();
        }

        #endregion

        #region Function

        private void number_sin_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index++, 's');
            InputStr.Insert(_index++, 'i');
            InputStr.Insert(_index++, 'n');
            InputStr.Insert(_index++, '(');
            InputStr.Insert(_index, ')');
            _index--;
            Show();
        }

        private void number_cos_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index++, 'c');
            InputStr.Insert(_index++, 'o');
            InputStr.Insert(_index++, 's');
            InputStr.Insert(_index++, '(');
            InputStr.Insert(_index, ')');
            _index--;
            Show();
        }

        private void number_arc_sin_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index++, 'a');
            InputStr.Insert(_index++, 'r');
            InputStr.Insert(_index++, 'c');
            InputStr.Insert(_index++, 's');
            InputStr.Insert(_index++, 'i');
            InputStr.Insert(_index++, 'n');
            InputStr.Insert(_index++, '(');
            InputStr.Insert(_index, ')');
            _index--;
            Show();
        }

        private void number_arc_cos_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index++, 'a');
            InputStr.Insert(_index++, 'r');
            InputStr.Insert(_index++, 'c');
            InputStr.Insert(_index++, 'c');
            InputStr.Insert(_index++, 'o');
            InputStr.Insert(_index++, 's');
            InputStr.Insert(_index++, '(');
            InputStr.Insert(_index, ')');
            _index--;
            Show();
        }

        private void number_degree_Click(object sender, EventArgs e)
        {
            InputStr.Insert(_index, '^');
            Show();
        }

        #endregion

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
                        MessageBoxButtons.OK);
                    return true;
                }
            }

            if (arrayOperations.Contains(InputStr[InputStr.Count - 1]))
            {
                MessageBox.Show(@"Оператор:" + InputStr[InputStr.Count - 1] + @" идет последний!", @"Ошибка!",
                    MessageBoxButtons.OK);
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
                        MessageBoxButtons.OK);
                    return true;
                }

                if (char.IsDigit(c) && c2 == '(')
                {
                    MessageBox.Show(@"Между переменной " + c + @" и скобкой отсутствует знак!", @"Ошибка!",
                        MessageBoxButtons.OK);
                    return true;
                }
            }

            return false;
        }
    }
}