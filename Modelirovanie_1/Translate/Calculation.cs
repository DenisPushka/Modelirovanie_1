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
        
        // Булевая переменная, говорящая, что операция была произведена
        private bool _doOperation;
        // Булевая переменная, говорящая, что элемент был добавлен где-то в середине стека (главное, не в конце)
        private bool _addToStack;
        
        public Calculation(string postfixStr, Dictionary<string, char> dictionaryForFunction,
            Dictionary<char, string> dictionaryNumber,MainForm mainForm)
        {
            _postfixStr = postfixStr;
            _dictionaryForFunction = dictionaryForFunction;
            _dictionaryNumber = dictionaryNumber;
            _mainForm = mainForm;
        }
        
        public async Task<double> Start()
        {
            while (_index < _postfixStr.Length)
            {
                if (char.IsUpper(_postfixStr[_index]))
                {
                    // Добавление числа в стек
                    Add(double.Parse(_dictionaryNumber[_postfixStr[_index]]), false);
                }
                else
                {
                    _addToStack = false;
                    // получение текущей операции
                    var operation = _postfixStr[_index].ToString();
                    
                    // поиск функции
                    foreach (var c in _dictionaryForFunction
                                 .Where(c => c.Value == _postfixStr[_index]))
                    {
                        operation = c.Key;
                    }
                    
                    // Нахождение последней переменной
                    var p2 = _stack[StackIndex];
                    double result;
                    
                    // Если вычисление функции, то берем только одно число 
                    if (_dictionaryForFunction.ContainsKey(operation) && operation != "^")
                    {
                        // Вычисление значения функции
                        result = Operators[operation](p2, 0);
                    }
                    else
                    {
                        StackIndex--;
                        // Нахождение следующего числа для вычислений 
                        var p1 = _stack[StackIndex];

                        // Проверка функции возведения в степень на разумное число
                        if (operation == "^" && p2 >= 30)
                            p2 = 29;
                        else if (operation == "^" && p2 <= -30) 
                            p2 = -29;
                        // Вычисление значения функции
                        result = Operators[operation](p1, p2);
                    }
                    // Добавление результата вычисления в стек, на место числа, ближнего к дну стеку
                    Add(result, true);
                    // Булевая переменная, говорящая, что операция была произведена
                    _doOperation = true;
                }

                // Увеличение индекса, отвечающего за обращение к постфиксной строке
                _index++;
                // Отображение арифметического стека
                _mainForm.ShowStackArithmetic(_stack, StackIndex);
                // Задержка
                await Task.Delay(1500);
            }

            _mainForm.Result.Text = "Результат вычисления: " + _stack[StackIndex];
            return _stack[StackIndex];
        }

        /// <summary>
        /// Метод, добавляющий элемент в стек
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="calculation">Переменная, говорящая, вычесленно было число или пришло с постфиксной строки</param>
        private void Add(double number, bool calculation)
        {
            // Если индекс находится в пределах границ стека (то есть не в начале и не в конце) или значение было вычеслено,
            // то выполняются след действия
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