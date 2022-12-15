namespace Task_Manager
{
    class Cursor
    {
        public static int headerPosition = 3;
        public static int mainPosition = headerPosition + 2;
        public static int position = mainPosition;
        public static int selPosition;
        private static ConsoleKeyInfo key;
        private static int op;
        public static int processID;
        public static string? processName;
        public static int CursorMenu()
        {
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, mainPosition);
            Console.WriteLine("->");
            while (true)
            {
                key = Console.ReadKey();
                int op = Actions();
                if (op == (int)Ops.DownArrow)
                {
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("  ");
                    position = Fix(position + 1);
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("->");
                }
                else if (op == (int)Ops.UpArrow)
                {
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("  ");
                    position = Fix(position - 1);
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("->");
                }
                else if (op == (int)Ops.Enter)
                {
                    selPosition = position - mainPosition;
                    processID = Processes.processesList[selPosition].Id;
                    processName = Processes.processesList[selPosition].ProcessName;
                    Processes.VievProcessInfo();
                }
                else if (op == (int)Ops.Backspace)
                {
                    Processes.VievProcesses();
                }
                else if (op == (int)Ops.D)
                {
                    Processes.KillProcess();
                }
                else if (op == (int)Ops.Delete)
                {
                    Processes.KillSameProcess();
                }
            }
        }
        public static int Actions()
        {
            while (true)
            {
                if (key.Key == ConsoleKey.DownArrow)
                {
                    op = (int)Ops.DownArrow;
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    op = (int)Ops.UpArrow;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    op = (int)Ops.Enter;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    op = (int)Ops.Backspace;
                }
                else if (key.Key == ConsoleKey.D)
                {
                    op = (int)Ops.D;
                }
                else if (key.Key == ConsoleKey.Delete)
                {
                    op = (int)Ops.Delete;
                }
                return op;
            }
        }
        private static int Fix(int position)
        {
            int maxPosition = Processes.processesList.Length + mainPosition - 1;
            if (position < mainPosition)
            {
                position = maxPosition;
            }
            else if (position > maxPosition)
            {
                Console.SetCursorPosition(0, 0);
                position = mainPosition;
            }
            return position;
        }
    }
}
