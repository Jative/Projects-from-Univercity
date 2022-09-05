namespace Lab8_14
{
    class Tree
    {
        private string value;
        private int count;
        private Tree left;
        private Tree right;

        public void Insert(string value)
        {
            if (this.value == null)
                this.value = value;
            else
            {
                if (int.Parse(value) < int.Parse(this.value))
                {
                    if (left == null)
                        this.left = new Tree();
                    left.Insert(value);
                }
                else if (int.Parse(value) > int.Parse(this.value))
                {
                    if (right == null)
                        this.right = new Tree();
                    right.Insert(value);
                }
                else
                    throw new Exception("Узел уже существует");
            }

            this.count = Recount(this);
        }

        public Tree Search(string value)
        {
            if (this.value == value)
                return this;
            else if (this.value.CompareTo(value) == 1)
            {
                if (left != null)
                    return this.left.Search(value);
                else
                    throw new Exception("Искомого узла в дереве нет");
            }
            else
            {
                if (right != null)
                    return this.right.Search(value);
                else
                    throw new Exception("Искомого узла в дереве нет");
            }
        }

        public string Display(Tree t)
        {
            if (this == t && this.value == null)
            {
                return "В дереве нет элементов";
            }
            else if (this == null)
            {
                return "";
            }
            string result = "";
            if (t.left != null)
                result += Display(t.left);

            result += t.value + " ";

            if (t.right != null)
                result += Display(t.right);

            return result;
        }

        private int Recount(Tree t)
        {
            int count = 0;

            if (t.left != null)
                count += Recount(t.left);

            count++;

            if (t.right != null)
                count += Recount(t.right);

            return count;
        }

        public void Remove(string value)
        {
            Tree t = Search(value);
            string[] str1 = Display(t).TrimEnd().Split(' ');
            string[] str2 = new string[str1.Length - 1];

            int i = 0;
            foreach (string s in str1)
            {
                if (s != value)
                    str2[i++] = s;
            }

            t.Clear();
            foreach (string s in str2)
                t.Insert(s);

            this.count = Recount(this);
        }

        public void SetValue(string oldValue, string newValue)
        {
            Remove(oldValue);
            try
            {
                Insert(newValue);
                this.count = Recount(this);
            } catch { Insert(oldValue); }
        }

        public void Clear()
        {
            this.value = null;
            this.left = null;
            this.right = null;
        }

        public string Draw(Tree t)
        {
            if (this == t && this.value == null)
            {
                return "В дереве нет элементов";
            }
            else if (this == null)
            {
                return "";
            }

            string result = t.value;
            if (t.left != null)
            {
                result += "\n" + Draw(t.left);

                string[] lines = result.Split('\n');
                result = "";

                for (int i = 1; i < lines.Length; i++)
                {
                    lines[i] += new string(' ', t.value.Length);
                }

                lines[0] = new string(' ', lines[1].Length - t.value.Length) + t.value;

                foreach (string line in lines)
                {
                    result += line + "\n";
                }
            }

            if (t.right != null)
            {
                string[] leftLines = { t.value };
                if (result.Contains("\n"))
                {
                    leftLines = result.Split('\n').Skip(1).ToArray();
                }

                string[] rightLines = { t.right.Draw(t.right) };
                if (rightLines[0].Contains("\n"))
                {
                    rightLines = rightLines[0].Split('\n');
                }
                int leftLength = result.Split('\n')[0].Length;
                int rightLength = rightLines[rightLines.Length - 1].Length;
                int maxLines = leftLines.Length > rightLines.Length ? leftLines.Length : rightLines.Length;

                string[] lines = new string[maxLines];
                for (int i = 0; i < maxLines; i++)
                {
                    bool leftAdded = false, rightAdded = false;
                    string line = "";
                    try
                    {
                        line = leftLines[i];
                        leftAdded = true;
                    } catch { line = new string(' ', leftLength); }

                    try
                    {
                        line += rightLines[i] + new string(' ', rightLength - rightLines[i].Length);
                    }
                    catch { line += new string(' ', rightLength); }

                    lines[i] = line;
                }
                result = result.Split('\n')[0] + new string(' ', rightLength);
                for (int i = 0; i < maxLines; i++)
                {
                    result += "\n" + lines[i];
                }
            }

            return result;
        }
    }
}