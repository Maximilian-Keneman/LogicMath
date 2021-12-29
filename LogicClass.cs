using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicMath
{
    public enum Operation
    {
        Variable,
        /// <summary>
        /// AND
        /// </summary>
        Conjunction,
        /// <summary>
        /// OR
        /// </summary>
        Disjunction,
        /// <summary>
        /// Следствие
        /// </summary>
        Implication,
        /// <summary>
        /// XOR
        /// </summary>
        ExclusiveDisjunction,
        /// <summary>
        /// Эквивалентность
        /// </summary>
        Equivalence
    }
    public abstract class LogicAbstract
    {
        protected static string Letters { get; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        protected static string[] Signs { get; } = { null, "&", "v", "->", "(+)", "<->" };

        protected bool TruthTableIsFill { get; set; }
        protected bool NormalFormsIsFill { get; set; }
        public abstract void FillProperties();

        public char[] Variables { get; protected set; }
        public TruthTable TruthTable { get; protected set; }
        public string PDNF { get; protected set; }
        public string PCNF { get; protected set; }
        public string DNF { get; protected set; }
        public string CNF { get; protected set; }
        protected static LogicOperation FuseOperations(Operation operation, params LogicOperation[] operations)
        {
            LogicOperation Oper = null;
            int length = operations.Length;
            if (length == 1)
                Oper = operations[0];
            else if (length > 1)
            {
                Oper = new LogicOperation(operations[0], operations[1], operation, false);
                for (int i = 2; i < length; i++)
                    Oper = new LogicOperation(Oper.Clone() as LogicOperation, operations[i], operation, false);
            }
            return Oper;
        }
        protected string GetPDNF()
        {
            List<LogicOperation> operations = new List<LogicOperation>();
            if (!TruthTableIsFill)
                throw new Exception("Не вызван метод FillProperties().");
            for (int i = 0; i < TruthTable.GetRowCount(); i++)
            {
                if (TruthTable.OutputValues[i])
                {
                    LogicVariable[] letters = new LogicVariable[Variables.Length];
                    for (int j = 0; j < Variables.Length; j++)
                        letters[j] = new LogicVariable(Variables[j], !TruthTable.InputValues[i, j]);
                    if (letters.Length == 0)
                        throw new Exception("Нет переменных");
                    else
                        operations.Add(FuseOperations(Operation.Conjunction, letters));
                }
            }
            if (operations.Count == 0)
                return "0";
            else
                return FuseOperations(Operation.Disjunction, operations.ToArray()).ToString();
        }
        protected string GetDNF()
        {
            List<List<string>> elements = PDNF.Split('v').Select(e =>
            {
                e = e.RemoveChars('&', '(', ')');
                List<string> els = new List<string>();
                string s = "";
                for (int i = 0; i < e.Length; i++)
                {
                    s += e[i];
                    if (e[i] != '!')
                    {
                        els.Add(s);
                        s = "";
                    }
                }
                return els;
            }).ToList();
            bool CompareLetters(List<string> elements0, List<string> elements1)
            {
                var _0 = elements0.Select(e => e.Length == 1 ? e : e.Substring(1));
                var _1 = elements1.Select(e => e.Length == 1 ? e : e.Substring(1));
                var compare = _0.Where(e => !_1.Contains(e));
                return compare.Count() == 0;
            }
            bool IsContinue = true;
            while (IsContinue)
            {
                IsContinue = false;
                for (int i = 0; i < elements.Count - 1; i++)
                {
                    for (int j = i + 1; j < elements.Count; j++)
                    {
                        if (elements[i].Count == elements[j].Count && CompareLetters(elements[i], elements[j]))
                        {
                            for (int k = 0; k < elements[i].Count; k++)
                            {
                                if (elements[i][k].Contains("!") && !elements[j][k].Contains("!") ||
                                    !elements[i][k].Contains("!") && elements[j][k].Contains("!"))
                                {
                                    if (elements[i].Count == 1)
                                    {
                                        elements[i].RemoveAt(k);
                                        elements.RemoveAt(j);
                                        j--;
                                        IsContinue = true;
                                    }
                                    else
                                    {
                                        int c = elements[i].Count - 1;
                                        for (int y = 0; y < elements[i].Count; y++)
                                            if (y != k && elements[i][y].Contains("!") == elements[j][y].Contains("!"))
                                                c--;
                                        if (c == 0)
                                        {
                                            elements[i].RemoveAt(k);
                                            elements.RemoveAt(j);
                                            j--;
                                            IsContinue = true;
                                        }
                                    }
                                    if (i == j)
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            if (elements.Count == 1 && elements[0].Count == 0)
                return "1";
            else
                return string.Join("v", elements.Select(e => string.Join("&", e)));
        }
        protected string GetPCNF()
        {
            List<LogicOperation> operations = new List<LogicOperation>();
            if (!TruthTableIsFill)
                throw new Exception("Не вызван метод FillProperties().");
            for (int i = 0; i < TruthTable.GetRowCount(); i++)
            {
                if (!TruthTable.OutputValues[i])
                {
                    LogicVariable[] letters = new LogicVariable[Variables.Length];
                    for (int j = 0; j < Variables.Length; j++)
                        letters[j] = new LogicVariable(Variables[j], TruthTable.InputValues[i, j]);
                    if (letters.Length == 0)
                        throw new Exception("Нет переменных");
                    else
                        operations.Add(FuseOperations(Operation.Disjunction, letters));
                }
            }
            if (operations.Count == 0)
                return "1";
            else
                return FuseOperations(Operation.Conjunction, operations.ToArray()).ToString();
        }
        protected string GetCNF()
        {
            List<List<string>> elements = PCNF.Split('&').Select(e =>
            {
                e = e.RemoveChars('v', '(', ')');
                List<string> els = new List<string>();
                string s = "";
                for (int i = 0; i < e.Length; i++)
                {
                    s += e[i];
                    if (e[i] != '!')
                    {
                        els.Add(s);
                        s = "";
                    }
                }
                return els;
            }).ToList();
            bool CompareLetters(List<string> elements0, List<string> elements1)
            {
                var _0 = elements0.Select(e => e.Length == 1 ? e : e.Substring(1));
                var _1 = elements1.Select(e => e.Length == 1 ? e : e.Substring(1));
                var compare = _0.Where(e => !_1.Contains(e));
                return compare.Count() == 0;
            }
            bool IsContinue = true;
            while (IsContinue)
            {
                IsContinue = false;
                for (int i = 0; i < elements.Count - 1; i++)
                {
                    for (int j = i + 1; j < elements.Count; j++)
                    {
                        if (elements[i].Count == elements[j].Count && CompareLetters(elements[i], elements[j]))
                        {
                            for (int k = 0; k < elements[i].Count; k++)
                            {
                                if (elements[i][k].Contains("!") && !elements[j][k].Contains("!") ||
                                    !elements[i][k].Contains("!") && elements[j][k].Contains("!"))
                                {
                                    if (elements[i].Count == 1)
                                    {
                                        elements[i].RemoveAt(k);
                                        elements.RemoveAt(j);
                                        j--;
                                        IsContinue = true;
                                    }
                                    else
                                    {
                                        int c = elements[i].Count - 1;
                                        for (int y = 0; y < elements[i].Count; y++)
                                            if (y != k && elements[i][y].Contains("!") == elements[j][y].Contains("!"))
                                                c--;
                                        if (c == 0)
                                        {
                                            elements[i].RemoveAt(k);
                                            elements.RemoveAt(j);
                                            j--;
                                            IsContinue = true;
                                        }
                                    }
                                }
                                if (i == j)
                                    break;
                            }
                        }
                    }
                }
            }
            if (elements.Count == 1)
                if (elements[0].Count == 0)
                    return "0";
                else
                    return string.Join("v", elements[0]);
            else
                return '(' + string.Join(")&(", elements.Select(e => string.Join("v", e))) + ')';
        }
    }

    public class LogicVariable : LogicOperation
    {
        public char Variable { get; }

        public LogicVariable(char variable, bool NOT) : base(null, null, Operation.Variable, NOT)
            => Variable = variable;

        public override string ToString() => (IsNegative ? "!" : "") + Variable;
    }
    public class LogicOperation : LogicAbstract, ICloneable
    {
        private LogicOperation leftOperand;
        private LogicOperation rightOperand;
        public Operation Operation { get; }
        public bool IsNegative { get; }

        public LogicOperation(LogicOperation left, LogicOperation right, Operation operation, bool NOT)
        {
            leftOperand = right;
            rightOperand = left;
            Operation = operation;
            IsNegative = NOT;
            if (operation == Operation.Variable && !(this is LogicVariable))
                throw new ArgumentException("", nameof(operation));
        }
        public LogicOperation(in int countOfOperations, in int countOfVariables, int[] operations, bool HasNegative, int Seed)
        {
            Random r = new Random(Seed);
            int LeftCountOfOperations = r.Next() % countOfOperations;
            int RightCountOfOperations = countOfOperations - LeftCountOfOperations - 1;
            leftOperand = LeftCountOfOperations == 0
                ? new LogicVariable(Letters[r.Next() % countOfVariables], HasNegative && r.Next() % 3 == 1)
                : new LogicOperation(in LeftCountOfOperations, in countOfVariables, operations, HasNegative, r.Next());
            rightOperand = RightCountOfOperations == 0
                ? new LogicVariable(Letters[r.Next() % countOfVariables], HasNegative && r.Next() % 3 == 1)
                : new LogicOperation(in RightCountOfOperations, in countOfVariables, operations, HasNegative, r.Next());
            int cashe = 0;
            do
            {
                int rOperation = r.Next() % 6;
                if (rOperation != 0 && operations.Contains(rOperation))
                {
                    Operation = (Enum.GetValues(typeof(Operation)) as Operation[])[rOperation];
                    break;
                }
                cashe++;
            }
            while (cashe < 10000);
            if (cashe >= 10000)
                throw new Exception("Удача не на вашей стороне");
            IsNegative = HasNegative && r.Next() % 3 == 1;
        }
        public LogicOperation(string example)
        {
            if (example[0] == '(' && example.Last() == ')')
            {
                int n = 0;
                bool b = true;
                for (int i = 1; i < example.Length - 1; i++)
                {
                    if (example[i] == '(')
                        n++;
                    else if (example[i] == ')')
                        n--;
                    if (n < 0)
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                    example = example.Substring(1, example.Length - 2);
            }

            string Oper = null;
            int IndOper = 0;
            int CountOper = 0;
            int DifferenceInPriority(string operation1, string operation2)
            {
                if (!Signs.Contains(operation1) || !Signs.Contains(operation2))
                    throw new Exception();
                var L = Signs.ToList();
                int i1 = L.IndexOf(operation1),
                    i2 = L.IndexOf(operation2);
                if (i1 < i2)
                    return 1;
                else
                    return 2;
            }
            void CheckOperation(int index, int lenght)
            {
                string operation = example.Substring(index, lenght);
                if (Oper == null)
                {
                    Oper = operation;
                    IndOper = index + lenght - 1;
                    CountOper = lenght;
                }
                else
                {
                    int n = CheckBracketsItoJ(example, index, IndOper);
                    if (n < 0 || (n == 0 && DifferenceInPriority(Oper, operation) == 1))
                    {
                        Oper = operation;
                        IndOper = index + lenght - 1;
                        CountOper = lenght;
                    }
                }
            }
            for (int i = 0; i < example.Length; i++)
            {
                switch (example[i])
                {
                    case '&':
                    case 'v':
                        CheckOperation(i, 1);
                        break;
                    case '-':
                        if (example[i - 1] != '<')
                            CheckOperation(i, 2);
                        break;
                    case '(':
                        if (example[i + 1] == '+')
                            CheckOperation(i, 3);
                        break;
                    case '<':
                        CheckOperation(i, 3);
                        break;
                }
            }

            Operation = (Enum.GetValues(typeof(Operation)) as Operation[])[Signs.ToList().IndexOf(Oper)];

            string left = example.Substring(0, IndOper + 1 - CountOper);
            string right = example.Substring(IndOper + 1);
            IsNegative = false;
            if (CheckBracketsItoJ(left, -1, left.Length) > 0 && CheckBracketsItoJ(right, -1, right.Length) < 0)
            {
                int c = 1;
                right = right.Remove(right.Length - c);
                if (left[0] == '!')
                {
                    c = 2;
                    IsNegative = true;
                }
                left = left.Substring(c);
            }

            if (left.Length == 2)
                leftOperand = new LogicVariable(left[1], true);
            else if (left.Length == 1)
                leftOperand = new LogicVariable(left[0], false);
            else
                leftOperand = new LogicOperation(left);

            if (right.Length == 2)
                rightOperand = new LogicVariable(right[1], true);
            else if (right.Length == 1)
                rightOperand = new LogicVariable(right[0], false);
            else
                rightOperand = new LogicOperation(right);
        }

        private bool VariablesIsFill { get; set; } = false;
        private char[] GetVariables()
        {
            List<char> vars = new List<char>();
            if (leftOperand.Operation == Operation.Variable)
                vars.Add((leftOperand as LogicVariable).Variable);
            else
                vars = vars.Union(leftOperand.GetVariables()).ToList();
            if (rightOperand.Operation == Operation.Variable)
                vars.Add((rightOperand as LogicVariable).Variable);
            else
                vars = vars.Union(rightOperand.GetVariables()).ToList();
            return vars.Distinct().ToArray();
        }
        public override void FillProperties()
        {
            Variables = GetVariables();
            VariablesIsFill = true;
            TruthTable = new TruthTable(this);
            TruthTableIsFill = true;
            PDNF = GetPDNF();
            PCNF = GetPCNF();
            DNF = GetDNF();
            CNF = GetCNF();
            NormalFormsIsFill = true;
        }

        public bool Solution(bool[] input, out bool[] SolutionPath)
        {
            if (!VariablesIsFill)
                throw new Exception("Не вызван метод FillProperties().");
            SolutionPath = new bool[0];
            return SelfSolution(Variables, input, ref SolutionPath);
        }
        public bool Solution(bool left, bool right)
        {
            bool s = Operation switch
            {
                Operation.Conjunction => left && right,
                Operation.Disjunction => left || right,
                Operation.Implication => !left || right,
                Operation.ExclusiveDisjunction => left != right,
                Operation.Equivalence => left == right,
                Operation.Variable => left,
            };
            return IsNegative ? !s : s;
        }
        private bool SelfSolution(char[] vars, bool[] input, ref bool[] SolutionPath)
        {
            bool l = leftOperand.Operation == Operation.Variable ? leftOperand.Solution(input[vars.ToList().IndexOf((leftOperand as LogicVariable).Variable)], false)
                                                                 : leftOperand.SelfSolution(vars, input, ref SolutionPath);
            bool r = rightOperand.Operation == Operation.Variable ? rightOperand.Solution(input[vars.ToList().IndexOf((rightOperand as LogicVariable).Variable)], false)
                                                                  : rightOperand.SelfSolution(vars, input, ref SolutionPath);
            bool s = Solution(l, r);
            SolutionPath = SolutionPath.Append(s).ToArray();
            return s;
        }
        
        public static string Correct(string example, List<string>[] AlternativeSigns)
        {
            #region Variable in Brackets
            Regex reg = new Regex(@"\((!?[A-Z])\)");
            reg.Replace(example, m => m.Groups[0].Value);
            #endregion
            #region Double Brackets
            for (int i = 0; i < example.Length - 1; i++)
            {
                if (example.Substring(i, 2) == "((" || (i != example.Length - 2 && example.Substring(i, 3) == "(!("))
                {
                    for (int j = i + 2; j < example.Length - 1; j++)
                    {
                        if (example.Substring(j, 2) == "))")
                        {
                            int n = 0, c = 0;
                            for (int k = i + 2; k < j; k++)
                            {
                                if (example[k] == '(')
                                    n++;
                                if (example[k] == ')' && n > 0)
                                {
                                    n--;
                                    continue;
                                }
                                if (example[k] == ')')
                                    c++;
                            }
                            if (n == 0 && c == 0)
                            {
                                example = example.Remove(i, 1);
                                example = example.Remove(j, 1);
                                i--;
                            }
                        }
                    }
                }
            }
            #endregion
            #region All Brackets
            if (example[0] == '(' && example.Last() == ')')
            {
                int n = 0;
                bool b = true;
                for (int i = 1; i < example.Length - 1; i++)
                {
                    if (example[i] == '(')
                        n++;
                    else if (example[i] == ')')
                        n--;
                    if (n < 0)
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                    example = example.Substring(1, example.Length - 2);
            }
            #endregion
            #region Standart Sings
            for (int i = 0; i < AlternativeSigns.Length; i++)
            {

            }
            #endregion
            return example;
        }
        public static bool CheckBrackets(string example, out string message)
        {
            int N = CheckBracketsItoJ(example, -1, example.Length);
            if (N > 0)
                message = "Есть не закрытые скобки";
            else if (N < 0)
                message = "Есть лишние закрывающие скобки";
            else
                message = "Good";
            return N == 0;
        }
        private static int CheckBracketsItoJ(string s, int i, int j)
        {
            if (i == j)
                return 0;
            if (i > j)
            {
                i += j;
                j = i - j;
                i -= j;
            }
            int c = 0;
            for (int k = i + 1; k < j; k++)
                if (s[k] == '(')
                {
                    if (s.Substring(k, 3) == "(+)")
                        k += 2;
                    else
                        c++;
                }
                else if (s[k] == ')')
                    c--;
            return c;
        }

        public override string ToString() =>
            (IsNegative ? "!(" : "") +
            ((Operation < leftOperand.Operation && !leftOperand.IsNegative) ? "(" + leftOperand.ToString() + ")"
                                                                            : leftOperand.ToString()) +
            Signs[(int)Operation] +
            (((Operation < rightOperand.Operation
            || Operation == Operation.Implication && Operation == rightOperand.Operation)
                                                && !rightOperand.IsNegative) ? "(" + rightOperand.ToString() + ")"
                                                                               : rightOperand.ToString()) +
            (IsNegative ? ")" : "");
        public string[] ToListLong()
        {
            List<string> list = new List<string>();
            if (!(leftOperand is LogicVariable))
                list.AddRange(leftOperand.ToListLong());
            if (!(rightOperand is LogicVariable))
                list.AddRange(rightOperand.ToListLong());
            string leftS = leftOperand.ToString(),
                   rightS = rightOperand.ToString();
            string s =
            (IsNegative ? "!(" : "") +
            ((Operation < leftOperand.Operation && !leftOperand.IsNegative) ? "(" + leftS + ")" : leftS) +
            Signs[(int)Operation] +
            (((Operation < rightOperand.Operation
            || Operation == Operation.Implication && Operation == rightOperand.Operation)
                                                && !rightOperand.IsNegative) ? "(" + rightS + ")" : rightS) +
            (IsNegative ? ")" : "");
            list.Add(s);
            return list.ToArray();
        }
        public string[] ToListShort(in int AllNumber, ref int needNumber, out int Number)
        {
            List<string> list = new List<string>();
            int leftNumber = 0, rightNumber = 0;
            if (!(leftOperand is LogicVariable))
                list.AddRange(leftOperand.ToListShort(in AllNumber, ref needNumber, out leftNumber));
            if (!(rightOperand is LogicVariable))
                list.AddRange(rightOperand.ToListShort(in AllNumber, ref needNumber, out rightNumber));
            Number = AllNumber - needNumber + 1;
            needNumber--;
            string leftS = leftNumber == 0 ? (leftOperand as LogicVariable).ToString() : leftNumber.ToString(),
                   rightS = rightNumber == 0 ? (rightOperand as LogicVariable).ToString() : rightNumber.ToString();
            string s = Number + ": " +
            (IsNegative ? "!(" : "") +
            leftS + Signs[(int)Operation] + rightS +
            (IsNegative ? ")" : "");
            list.Add(s);
            return list.ToArray();
        }
        public object Clone() => new LogicOperation(leftOperand, rightOperand, Operation, IsNegative);
    }

    public class TruthTable
    {
        public char[] Variables { get; }
        public bool[,] InputValues { get; }
        public string[] OperationsLong { get; }
        public string[] OperationsShort { get; }
        public bool[,] OperationsSolutions { get; }
        public bool[] OutputValues { get; }

        public bool HasOperationsSolutions { get; }

        public int GetRowCount() => InputValues.GetLength(0);
        public int GetColumnCount(bool WithSolutions) =>
            Variables.Length + (WithSolutions ? OperationsShort.Length : 1);

        public TruthTable(LogicOperation example)
        {
            Variables = example.Variables;
            int vars = Variables.Length;
            int count = (int)Math.Pow(2, vars);
            InputValues = new bool[count, vars];
            OutputValues = new bool[count];
            OperationsLong = example.ToListLong();
            int allNumber = OperationsLong.Length;
            int needNumber = allNumber;
            OperationsShort = example.ToListShort(in allNumber, ref needNumber, out int number);
            if (number != allNumber)
                throw new Exception();
            OperationsSolutions = new bool[count, allNumber];
            for (int i = 0; i < count; i++)
            {
                bool[] input = Convert.ToInt32(Convert.ToString(i, 2)).ToString("D" + vars)
                               .Select(n => int.Parse(n.ToString()) != 0).ToArray();
                for (int j = 0; j < vars; j++)
                    InputValues[i, j] = input[j];
                OutputValues[i] = example.Solution(input, out bool[] solutionPath);
                for (int j = 0; j < allNumber; j++)
                    OperationsSolutions[i, j] = solutionPath[j];
            }
            HasOperationsSolutions = true;
        }
        public TruthTable(LogicVector example)
        {
            Variables = example.Variables;
            OutputValues = example.Vector;
            int vars = Variables.Length;
            int count = OutputValues.Length;
            InputValues = new bool[count, vars];
            for (int i = 0; i < count; i++)
            {
                bool[] input = Convert.ToInt32(Convert.ToString(i, 2)).ToString("D" + vars)
                               .Select(n => int.Parse(n.ToString()) != 0).ToArray();
                for (int j = 0; j < vars; j++)
                    InputValues[i, j] = input[j];
            }
            HasOperationsSolutions = false;
        }

        public override string ToString()
        {
            string s = string.Join(" ", Variables) + '\n';
            for (int i = 0; i < GetRowCount(); i++)
            {
                for (int j = 0; j < Variables.Length; j++)
                    s += InputValues[i, j].ToBinary() + " ";
                s += OutputValues[i].ToBinary() + '\n';
            }
            return s;
        }
    }

    public class LogicVector : LogicAbstract
    {
        public bool[] Vector { get; }

        public LogicVector(in int CountOfVariables)
        {
            Random rnd = new Random();
            Variables = Letters.Substring(0, CountOfVariables).ToCharArray();
            int count = (int)Math.Pow(2, CountOfVariables);
            Vector = new bool[count];
            for (int i = 0; i < count; i++)
                Vector[i] = rnd.Next(3) != 0;
        }
        public LogicVector(string example)
        {
            double log = Math.Log(example.Length, 2);
            if (log % 1 == 0 && example.Length <= Math.Pow(2, 26))
            {
                int count = (int)log;
                Vector = example.Select(i => i != '0').ToArray();
                Variables = Letters.Substring(0, count).ToCharArray();
            }
            else
                throw new ArgumentException();
        }

        public override void FillProperties()
        {
            TruthTable = new TruthTable(this);
            TruthTableIsFill = true;
            PDNF = GetPDNF();
            PCNF = GetPCNF();
            DNF = GetDNF();
            CNF = GetCNF();
            NormalFormsIsFill = true;
        }

        public override string ToString() => string.Join("", Vector.Select(i => i.ToBinary()));
    }

    public static class Expansion
    {
        public static string RemoveChars(this string s, params char[] cs)
        {
            for (int i = 0; i < cs.Length; i++)
                s = s.Replace(cs[i].ToString(), "");
            return s;
        }
        public static string ToBinary(this bool b) => b ? "1" : "0";
        public static int NextUnique(this Random rnd) =>
            Math.Abs(rnd.Next() * DateTime.UtcNow.Millisecond * Environment.TickCount);
    }
}