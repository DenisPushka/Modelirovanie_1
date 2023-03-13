using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelirovanie_1.Translate
{
    public class Calculation
    {
        private readonly string _postfixStr;
        private int _index;
        private readonly List<double> _stack = new List<double>();
        private readonly Dictionary<string, char> _dictionaryForFunction;
        private readonly Dictionary<char, string> _dictionaryNumber;
        private readonly MainForm _mainForm;
        private int StackIndex { get; set; }

        public Calculation(string postfixStr, Dictionary<string, char> dictionaryForFunction,
            Dictionary<char, string> dictionaryNumber,MainForm mainForm)
        {
            _postfixStr = postfixStr;
            _dictionaryForFunction = dictionaryForFunction;
            _dictionaryNumber = dictionaryNumber;
            _mainForm = mainForm;
        }

        // Определим функции для доступных операторов
        private static Dictionary<string, Func<double, double, double>> Operators { get; } =
            new Dictionary<string, Func<double, double, double>>
            {
                { "+", (a, b) => a + b },
                { "-", (a, b) => a - b },
                { "/", (a, b) => a / b },
                { "*", (a, b) => a * b },
                { "sin", (a, b) => Math.Sin(a) },
                { "cos", (a, b) => Math.Cos(a) },
                { "arcsin", (a, b) => Math.Asin(a) },
                { "arccos", (a, b) => Math.Acos(a) },
                { "^", Math.Pow },
            };

        private bool _doOperation;
        private bool _addToStack;

        public async Task<double> Start()
        {
            while (_index < _postfixStr.Length)
            {
                if (char.IsUpper(_postfixStr[_index]))
                {
                    Add(double.Parse(_dictionaryNumber[_postfixStr[_index]]), false);
                }
                else
                {
                    _addToStack = false;
                    var operation = _postfixStr[_index].ToString();
                    foreach (var c in _dictionaryForFunction
                                 .Where(c => c.Value == _postfixStr[_index]))
                    {
                        operation = c.Key;
                    }

                    var p2 = _stack[StackIndex];
                    double result;
                    if (_dictionaryForFunction.ContainsKey(operation) && operation != "^")
                    {
                        result = Operators[operation](p2, 0);
                    }
                    else
                    {
                        StackIndex--;
                        var p1 = _stack[StackIndex];
                        result = Operators[operation](p1, p2);
                    }

                    Add(result, true);
                    _doOperation = true;
                }

                _index++;
                _mainForm.ShowStackArithmetic(_stack, StackIndex);
                await Task.Delay(1500);
            }

            return _stack[StackIndex];
        }

        private void Add(double number, bool calculation)
        {
            if (StackIndex + 1 < _stack.Count || StackIndex == -1 || calculation)
            {
                if (StackIndex == -1 || (_doOperation && !calculation) || _addToStack)
                {
                    StackIndex++;
                    _addToStack = true;
                    _doOperation = false;
                }

                _stack[StackIndex] = number;
            }
            else
            {
                _addToStack = false;
                _stack.Add(number);
                StackIndex = _stack.Count - 1;
            }
        }
    }
}