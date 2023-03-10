using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Modelirovanie;

namespace Modelirovanie_1
{
    public partial class MainForm : Form
    {
        private string _inputStr;
        private const string Str = "$+-*/^()FP";
        private readonly Label _indicator;

        private readonly byte[,] _arrayBytes =
        {
            { 4, 1, 1, 1, 1, 1, 1, 5, 1, 6 },
            { 2, 2, 2, 1, 1, 1, 1, 2, 1, 6 },
            { 2, 2, 2, 1, 1, 1, 1, 2, 1, 6 },
            { 2, 2, 2, 2, 2, 1, 1, 2, 1, 6 },
            { 2, 2, 2, 2, 2, 1, 1, 2, 1, 6 },
            { 2, 2, 2, 2, 2, 2, 1, 2, 1, 6 },
            { 5, 1, 1, 1, 1, 1, 1, 3, 1, 6 },
            { 2, 2, 2, 2, 2, 2, 1, 2, 5, 6 }
        };

        private readonly Translate.Translate _translate;

        public MainForm()
        {
            InitializeComponent();
            ShowTable();
            _indicator = new Label();
            _translate = new Translate.Translate(this);
            _indicator.Text = "←";
            _indicator.Size = new Size(250, 230);
            _indicator.Location = new Point(165, 650);
            _indicator.Font = new Font("Microsoft Sans Serif", 20);
            Controls.Add(_indicator);
        }

        private void ShowTable()
        {
            const int size = 35;
            const int width = 400;
            const int height = 250;
            var s = 0;
            for (var x = 0; x < 10; x++)
            {
                if (x == 0)
                    for (var index = 0; index < Str.Length; index++)
                    {
                        var l = new Label();
                        l.Text = Str[index].ToString();
                        l.BorderStyle = BorderStyle.Fixed3D;
                        l.Location = new Point(index * size + width, height - size);
                        l.Size = new Size(size, size);
                        l.TextAlign = ContentAlignment.MiddleCenter;
                        l.Font = new Font("Microsoft Sans Serif", 18);
                        Controls.Add(l);
                    }

                for (var y = 0; y < 8; y++)
                {
                    if (y == 0 && x != 9)
                        if (Str[x] == ')')
                            s = -size;
                        else
                        {
                            var label = new Label();
                            label.Text = Str[x].ToString();
                            label.Location = new Point(width - size, x * size + height + s);
                            label.Size = new Size(size, size);
                            label.BorderStyle = BorderStyle.Fixed3D;
                            label.TextAlign = ContentAlignment.MiddleCenter;
                            label.Font = new Font("Microsoft Sans Serif", 18);
                            Controls.Add(label);
                        }

                    var l = new Label();
                    l.Text = _arrayBytes[y, x].ToString();
                    l.Location = new Point(x * size + width, y * size + height);
                    l.Size = new Size(size, size);
                    l.BorderStyle = BorderStyle.FixedSingle;
                    l.TextAlign = ContentAlignment.MiddleCenter;
                    l.Font = new Font("Microsoft Sans Serif", 18);
                    Controls.Add(l);
                }
            }
        }

        private void label_input_main(object sender, EventArgs e)
        {
            var input = new Input(this);
            input.Show();
        }

        public void ShowInputData(Input input)
        {
            var buffer = new StringBuilder();
            foreach (var t in input.InputStr)
                buffer.Append(t);

            _inputStr = buffer.ToString();
            var result = new StringBuilder();
            foreach (var c in _inputStr)
                result.Append(c);
            label_input.Text = result.ToString();
        }

        private void button_to_input(object sender, EventArgs e) => label_input_main(sender, e);

        public void ShowStack(List<char> stack, int index)
        {
            if (stack.Count == 0) return;
            label_stack.Text = "";
            label_stack.Font = new Font("Microsoft Sans Serif", 20);

            for (var i = stack.Count - 1; i >= 0; i--)
            {
                // index++;
                var c = stack[i];
                label_stack.Text += c + "\n";
            }

            if (index == -1) index = 0;
            _indicator.Location = new Point(165, 620 - index * 40);
        }

        public void ShowPostfix(string str)
        {
            label_postfix_symbol.Text = str;
        }

        public void ShowChangeInputStr(int index, string workStr)
        {
            label_input_change.Text = "";
            for (var i = index; i < workStr.Length; i++)
            {
                if (_translate.DictionaryForFunction.ContainsValue(workStr[i]))
                {
                    foreach (var c in _translate
                                 .DictionaryForFunction
                                 .Where(c => c.Value == workStr[i]))
                        label_input_change.Text += c.Key;
                }
                else
                    label_input_change.Text += workStr[i];
            }
        }

        private async void button_Start(object sender, EventArgs e)
        {
            await _translate.TranslateToPostfix(_inputStr);
        }

        private void radioButton_Auto(object sender, EventArgs e) => _translate.Mode = false;

        private void radioButton_Step(object sender, EventArgs e) => _translate.Mode = true;

        private async void button_Tact(object sender, EventArgs e) => await _translate.TranslateToPostfix(_inputStr);
    }
}