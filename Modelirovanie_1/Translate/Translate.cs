﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie_1.Translate
{
    public class Translate
    {
        public readonly Dictionary<string, char> DictionaryForFunction;
        private const int MilliSecond = 3000;
        private string _workString;
        private readonly MainForm _mainForm;
        private int _stackIndex;
        private int _index;
        private readonly List<char> _stack = new List<char>();
        private readonly StringBuilder _resultString = new StringBuilder();

        // Режим работы
        public bool Mode { get; set; }

        public Translate(MainForm mainForm)
        {
            _mainForm = mainForm;
            DictionaryForFunction = new Dictionary<string, char>
            {
                { "sin", 'а' },
                { "cos", 'б' },
                { "arcsin", 'в' },
                { "arccos", 'г' },
                { "^", 'д' }
            };
        }

        // Перевод из цифр и формул в буквы
        private string TranslateInputStrToWork(string workString)
        {
            // var ch = 'A';
            var index = 0;
            var result = new StringBuilder();

            while (index != workString.Length)
            {
                // if (char.IsDigit(workString[index]))
                // {
                //     var buffer = new StringBuilder().Append(workString[index]);
                //     while (++index != workString.Length && char.IsDigit(workString[index]))
                //         buffer.Append(workString[index]);
                //     _dictionaryForNumber.Add(ch, buffer.ToString());
                //     result.Append(ch);
                //     ch++;
                // }
                // else 
                if (char.IsLower(workString[index]) || workString[index] == '^')
                {
                    var c = ' ';
                    switch (workString[index])
                    {
                        case 's':
                            c = DictionaryForFunction[workString.Substring(index, 3)];
                            break;
                        case 'c':
                            c = DictionaryForFunction[workString.Substring(index, 3)];
                            break;
                        // arc sin && arc cos
                        case 'a':
                            c = DictionaryForFunction[workString.Substring(index, 6)];
                            index += 3;
                            break;
                        case '^':
                            c = DictionaryForFunction[workString.Substring(index, 1)];
                            index -= 2;
                            break;
                    }

                    index += 3;
                    result.Append(c);
                }
                else
                {
                    result.Append(workString[index]);
                    index++;
                }
            }

            return result.ToString();
        }

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

        // Перевод из инфиксной формы в постфиксную
        public void Step()
        {
            var operation = _arrayBytes[GetRow(), GetColumn()];

            if ( Operation(operation) && _index < _workString.Length)
                _index++;
            
            _mainForm.ShowPostfix(_resultString.ToString());
            _mainForm.ShowChangeInputStr(_index, _workString);
            _mainForm.ShowStack(_stack, _stackIndex);
        }

        public async Task<string> TranslateToPostfix(string str)
        {
            _workString = TranslateInputStrToWork(str);
            if (Mode)
            {
                Step();
                return await Task.FromResult(_resultString.ToString());
            }
            
            while (_index < _workString.Length || _stackIndex != -1)
            {
                Step();
               //  await Task.Delay(MilliSecond);
            }
            
            return await Task.FromResult(_resultString.ToString());
        }

        private readonly Dictionary<char, int> _dictionaryColumn = new Dictionary<char, int>
        {
            { '+', 1 }, { '-', 2 }, { '*', 3 }, { '/', 4 },
            { '^', 5 }, { '(', 6 }, { ')', 7 }
        };

        private int GetColumn()
        {
            // Случий, для работы со стеком, после прохождения входной строки
            if (_workString.Length == _index)
                return 0;
            
            // для операции из словаря операций
            if (_dictionaryColumn.ContainsKey(_workString[_index]))
                return _dictionaryColumn[_workString[_index]];

            return char.IsUpper(_workString[_index])
                ? 9 // для числа
                : 8; // для функции
        }

        private int GetRow()
        {
            // Выполняется при пустом стеке
            if (_stackIndex == -1 || _stack.Count == 0)
                return 0;
            // Если в стеке по месту указанию будет оператор, то вернуть цифровое значение словаря по данному ключу
            // Иначе вернуть восьмой столбец
            return _dictionaryColumn.ContainsKey(_stack[_stackIndex]) ? _dictionaryColumn[_stack[_stackIndex]] : 7;
        }

        // Поле для редактирования элемента (показывает, был ли считан в вывод элемент из стека)
        private bool _read;
        private bool Operation(int operation)
        {
            switch (operation)
            {
                // Добавление символа в стек
                case 1:
                    // Проверка на выход за границы стека
                    if (_stackIndex == -1) _stackIndex = 0;
                    
                    // Проверка на изменение элемента внутри стека (перезапись)
                    if (_stackIndex + 1 < _stack.Count || _read)
                    {
                        // Если элемент не был считан, то увеличить указатель
                        if (!_read)
                            _stackIndex++;
                        _stack[_stackIndex] = _workString[_index];
                    }
                    else
                    {
                        _stack.Add(_workString[_index]);
                        _stackIndex = _stack.Count - 1;
                    }
                    
                    _read = false;
                    break;

                // Извлекаем символ из стека и отправляем его в выходную строку
                case 2:
                    if (_stackIndex == -1) _stackIndex = 0;
                    _resultString.Append(_stack[_stackIndex]);
                    _stackIndex--;
                    _read = true;
                    return false;

                // Удалить ")"
                case 3:
                    _stackIndex--;
                    _read = false;
                    break;

                case 4:
                    var z = 0;
                    break;
                case 5:
                    var a = 0;
                    break;

                // Пересылаем символ из входной строки в выходную
                case 6:
                    _resultString.Append(_workString[_index]);
                    break;
            }

            return true;
        }
    }
}