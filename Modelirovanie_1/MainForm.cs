﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modelirovanie_1
{
    public partial class MainForm : Form
    {
        private string _inputStr;
        private readonly Dictionary<string, char> _dictionaryForFunction;
        private const string Str = "$+-*/^()FP";
        private const int MilliSecond = 1000;
        private Label _indicator;

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

        private bool _mode;

        public MainForm()
        {
            InitializeComponent();
            _dictionaryForFunction = new Dictionary<string, char>
            {
                { "sin", 'а' },
                { "cos", 'б' },
                { "arcsin", 'в' },
                { "arccos", 'г' },
                { "^", 'д' }
            };
            ShowTable();
            _indicator = new Label();
            _indicator.Text = "←";
            _indicator.Size = new Size(250, 230);
            _indicator.Location = new Point(165, 640);
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

        private readonly Stack<char> _liveStack = new Stack<char>();
        private readonly Queue<char> _liveQueue = new Queue<char>();
        private int _liveIndex;
        private string _workString;
        private bool _end;
        private bool _isFirst = true;
        private readonly List<bool> _isLife = new List<bool>();

        // Перевод из инфиксной формы в постфиксную
        public async Task<string> TranslateToPostfix(string input)
        {
            Stack<char> stack;
            Queue<char> queue;
            List<bool> isLife;
            int index;

            if (_mode)
            {
                stack = _liveStack;
                queue = _liveQueue;
                index = _liveIndex;
                isLife = _isLife;

                if (_isFirst)
                {
                    _workString = Translate(input);
                    _isFirst = false;
                }
            }
            else
            {
                stack = new Stack<char>();
                queue = new Queue<char>();
                isLife = new List<bool>();
                index = 0;
                _workString = Translate(input);
            }

            while (_workString.Length != index)
            {
                var element = _workString[index];

                switch (element)
                {
                    case '(':
                        stack.Push(element);
                        _stackShow.Push(element);
                        isLife.Add(true);
                        break;
                    case ')':
                        Pop(stack, queue, element, isLife);

                        if (index + 1 < _workString.Length && _workString[index + 1] == 'д')
                        {
                            stack.Push(_workString[++index]);
                            _stackShow.Push(_workString[index]);
                            isLife.Add(true);
                            _liveIndex++;
                        }

                        break;
                }

                // Для цифр
                if (char.IsUpper(element))
                    queue.Enqueue(element);
                else
                    switch (element)
                    {
                        case '/':
                        case '*':
                        case 'а':
                        case 'б':
                        case 'в':
                        case 'г':
                        case 'д':
                        {
                            if (stack.Count != 0 && stack.Peek() != 'д' &&
                                (stack.Peek() == '*' || stack.Peek() == '/' ||
                                 _dictionaryForFunction.ContainsValue(stack.Peek())))
                                Pop(stack, queue, element, isLife);
                            stack.Push(element);
                            isLife.Add(true);
                            _stackShow.Push(element);
                            break;
                        }
                        case '+':
                        case '-':
                        {
                            if (stack.Count == 0 || stack.Peek() == '(')
                            {
                                stack.Push(element);
                                _stackShow.Push(element);
                                isLife.Add(true);
                            }
                            else if (stack.Peek() == '/' || stack.Peek() == '*' ||
                                     _dictionaryForFunction.ContainsValue(stack.Peek()))
                            {
                                Pop(stack, queue, element, isLife);
                                stack.Push(element);
                                _stackShow.Push(element);
                                isLife.Add(true);
                            }
                            else
                            {
                                queue.Enqueue(stack.Pop());
                                isLife[isLife.Count - 1] = false;
                                stack.Push(element);
                                _stackShow.Push(element);
                                isLife.Add(true);
                            }

                            break;
                        }
                    }

                ShowStack(stack, isLife);
                ShowChangeInputStr(index, _workString);
                ShowQueue(queue);
                if (_mode)
                {
                    _liveIndex++;
                    break;
                }

                index++;
                await Task.Delay(MilliSecond);
            }

            if (_workString.Length <= index)
            {
                ShowStack(stack, isLife);
                ShowQueue(queue);
                await Task.Delay(MilliSecond);
                if (stack.Count != 0)
                    for (var i = 0; stack.Count != 0; i++)
                    {
                        if (stack.Peek() != '(')
                            queue.Enqueue(stack.Pop());
                        else
                            stack.Pop();
                        isLife[isLife.Count - 1 - i] = false;
                        ShowStack(stack, isLife);
                        ShowQueue(queue);
                        await Task.Delay(MilliSecond);
                        if (_mode)
                            break;
                    }
                else
                    _end = true;
            }

            // Выход в буквах
            var result = ShowQueue(queue);

            if (_workString.Length <= index && _end)
            {
                _liveIndex = 0;
                _liveQueue.Clear();
                _liveStack.Clear();
                _isLife.Clear();
                _end = false;
                label_stack.Text = "";
            }

            return result;
        }

        private void Pop(Stack<char> stack, Queue<char> queue, char element, List<bool> isLife)
        {
            for (var i = 0; i < stack.Count; i++)
            {
                if (stack.Peek() == '(')
                {
                    if (element == ')' && (element != '-' || element != '+'))
                    {
                        stack.Pop();
                        isLife[isLife.Count - 1 - i] = false;
                    }

                    return;
                }

                if (element != ')' && (stack.Peek() == '+' || stack.Peek() == '-' ||
                                       (_dictionaryForFunction.ContainsValue(element) &&
                                        (stack.Peek() == '*' || stack.Peek() == '/'))))
                    return;

                queue.Enqueue(stack.Pop());
                isLife[isLife.Count - 1 - i] = false;
            }
        }

        private readonly Stack<char> _stackShow = new Stack<char>();

        private void ShowStack(Stack<char> stack, IReadOnlyList<bool> isLife)
        {
            if (stack.Count == 0) return;
            label_stack.Text = "";
            var index = 0;
            var branch = false;

            foreach (var c in _stackShow)
            {
                if (c == '(')
                {
                    if (c == stack.Peek() && !branch)
                    {
                        label_stack.Text += '(' + "\n";
                        branch = true;
                        index = _stackShow.Count == 1 ? 0 : index; 
                        _indicator.Location = new Point(165, 670 - (_stackShow.Count - index + 1) * 30);
                    }
                    else
                        label_stack.Text += '(' + "\n";
                }
                else if (stack.Count != 0 && c == stack.Peek() && isLife[isLife.Count - 1 - index])
                {
                    label_stack.Text += c + "\n";
                    index = _stackShow.Count == 1 ? 0 : index;
                    _indicator.Location = new Point(165, 670 - (_stackShow.Count - index + 1) * 30);
                }
                else
                    label_stack.Text += c + "\n";

                label_stack.Font = new Font("Microsoft Sans Serif", 20);

                index++;
            }
        }

        private string ShowQueue(Queue<char> queue)
        {
            label_postfix_symbol.Text = "";
            var result = new StringBuilder();
            foreach (var c in queue)
                result.Append(c);
            label_postfix_symbol.Text = result.ToString();
            return result.ToString();
        }

        private void ShowChangeInputStr(int index, string workStr)
        {
            label_input_change.Text = "";
            for (var i = index + 1; i < workStr.Length; i++)
                if (_dictionaryForFunction.ContainsValue(workStr[i]))
                {
                    foreach (var c in _dictionaryForFunction.Where(c => c.Value == _workString[i]))
                        label_input_change.Text += c.Key;
                }
                else
                    label_input_change.Text += workStr[i];
        }

        private string Translate(string workString)
        {
            var result = new StringBuilder();
            var index = 0;

            while (index < workString.Length)
            {
                if (char.IsLower(workString[index]) || workString[index] == '^')
                {
                    var c = ' ';
                    switch (workString[index])
                    {
                        case 's':
                            c = _dictionaryForFunction[workString.Substring(index, 3)];
                            break;
                        case 'c':
                            c = _dictionaryForFunction[workString.Substring(index, 3)];
                            break;
                        // arc sin && arc cos
                        case 'a':
                            c = _dictionaryForFunction[workString.Substring(index, 6)];
                            index += 3;
                            break;
                        case '^':
                            c = _dictionaryForFunction[workString.Substring(index, 1)];
                            index -= 2;
                            break;
                    }

                    index += 2;
                    result.Append(c);
                }
                else
                    result.Append(workString[index]);

                index++;
            }

            return result.ToString();
        }

        private async void button_Start(object sender, EventArgs e)
        {
            _stackShow.Clear();
            await TranslateToPostfix(_inputStr);
        }

        private void radioButton_Auto(object sender, EventArgs e) => _mode = false;

        private void radioButton_Step(object sender, EventArgs e) => _mode = true;

        private async void button_Tact(object sender, EventArgs e) => await TranslateToPostfix(_inputStr);
    }
}