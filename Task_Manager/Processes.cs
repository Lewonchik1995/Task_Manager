using System.Diagnostics;

namespace Task_Manager
{
    static class Processes
    {
        public static Process[] processesList = Array.Empty<Process>();
        private static void GetRunningProcesses()
        {
            processesList = Process.GetProcesses();
            processesList = processesList.OrderBy(p => p.ProcessName).ToArray();
            Console.SetCursorPosition(0, Cursor.mainPosition);
            Cursor.position = Cursor.mainPosition;
            for (int i = 0; i < processesList.Length; i++)
            {
                Console.WriteLine($"  {processesList[i].ProcessName}");
                Console.SetCursorPosition(60, i + Cursor.mainPosition);
                Console.WriteLine(Math.Round(Convert.ToDouble((processesList[i].PagedMemorySize64 / 8) / 1024.0 / 1024.0), 2) + " МБ");
                Console.SetCursorPosition(74, i + Cursor.mainPosition);
                Console.WriteLine(Math.Round(Convert.ToDouble((processesList[i].WorkingSet64 / 8) / 1024.0 / 1024.0), 2) + " МБ");
                Console.SetCursorPosition(90, i + Cursor.mainPosition);
                Console.WriteLine(processesList[i].BasePriority);
                Console.SetCursorPosition(97, i + Cursor.mainPosition);
                Console.WriteLine("|");
            }
        }
        private static void GetProcessInfo()
        {
            Console.SetCursorPosition(45, Cursor.mainPosition);
            Console.WriteLine(Math.Round(Convert.ToDouble((processesList[Cursor.selPosition].WorkingSet64 / 8) / 1024.0 / 1024.0), 2) + " МБ");
            Console.SetCursorPosition(45, Cursor.mainPosition + 1);
            Console.WriteLine(processesList[Cursor.selPosition].BasePriority);
            Console.SetCursorPosition(45, Cursor.mainPosition + 2);
            try
            {
                Console.WriteLine(processesList[Cursor.selPosition].PriorityClass);
            }
            catch
            {
                Console.WriteLine("Нет доступа");
            }
            Console.SetCursorPosition(45, Cursor.mainPosition + 3);
            try
            {
                Console.WriteLine(processesList[Cursor.selPosition].UserProcessorTime);
            }
            catch
            {
                Console.WriteLine("Нет доступа");
            }

            Console.SetCursorPosition(45, Cursor.mainPosition + 4);
            try
            {
                Console.WriteLine(processesList[Cursor.selPosition].TotalProcessorTime);
            }
            catch
            {
                Console.WriteLine("Нет доступа");
            }
            Console.SetCursorPosition(45, Cursor.mainPosition + 5);
            Console.WriteLine(Math.Round(Convert.ToDouble((processesList[Cursor.selPosition].PagedMemorySize64 / 8) / 1024.0 / 1024.0), 2) + " МБ");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            bool isResponding = processesList[Cursor.selPosition].Responding;
            if (isResponding)
            {
                Console.WriteLine("  Статус = Запущен");
            }
            else
            {
                Console.WriteLine("  Статус = Заморожен");
            }
        }
        public static void KillProcess()
        {
            try
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.Id == Cursor.processID)
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("==================================================================================================\n" +
                                  "||                                       Диспетчер задач                                        ||\n" +
                                  "==================================================================================================");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Вы будете перенаправлены на список процессов");
                Thread.Sleep(2000);
                VievProcesses();
            }
            finally
            {
                VievProcesses();
            }
        }
        public static void KillSameProcess()
        {
            try
            {
                foreach (Process process in Process.GetProcessesByName(Cursor.processName))
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("==================================================================================================\n" +
                                  "||                                       Диспетчер задач                                        ||\n" +
                                  "==================================================================================================");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Вы будете перенаправлены на список процессов");
                Thread.Sleep(2000);
                VievProcesses();
            }
            finally 
            {
                VievProcesses();
            }
        }
        public static void VievProcesses()
        {
            Console.Clear();
            Console.WriteLine("==================================================================================================\n" +
                              "||                                       Диспетчер задач                                        ||\n" +
                              "==================================================================================================");
            Console.SetCursorPosition(25, Cursor.headerPosition);
            Console.WriteLine("Процесс");
            Console.SetCursorPosition(57, Cursor.headerPosition);
            Console.WriteLine("Опер. память");
            Console.SetCursorPosition(72, Cursor.headerPosition);
            Console.WriteLine("Физ. память");
            Console.SetCursorPosition(87, Cursor.headerPosition);
            Console.WriteLine("Приоритет");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            GetRunningProcesses();
            Cursor.CursorMenu();
        }
        public static void VievProcessInfo()
        {
            Console.Clear();
            Console.WriteLine("==================================================================================================\n" +
                              "||                                       Диспетчер задач                                        ||\n" +
                              "==================================================================================================");
            Console.WriteLine($"System.Diagnostics.Process ({processesList[Cursor.selPosition].ProcessName})");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("  Использование диска:");
            Console.WriteLine("  Приоритет:");
            Console.WriteLine("  Класс приоритета:");
            Console.WriteLine("  Время использования процесса:");
            Console.WriteLine("  Все время использования:");
            Console.WriteLine("  Использование оперативной памяти:");
            Console.WriteLine("  ------------------------------------------------------------------------------------------------");
            GetProcessInfo();
            Console.WriteLine("  ------------------------------------------------------------------------------------------------");
            Console.WriteLine($"  {Ops.D} - Завершить процесс");
            Console.WriteLine($"  {Ops.Delete} - Завершить все процессы с этим именем");
        }
    }
}
