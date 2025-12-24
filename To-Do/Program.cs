using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace To_Do;

// TODO: ---------------------------------------------------------------------------
// TODO: CHANGE THE "PATH" to the "tasks.txt" FROM THE PROJECT PATH IN YOUR COMPUTER
// TODO: ---------------------------------------------------------------------------

class Program
{
    public static void Main(string[] args)
    {
        Menu menu = new Menu();
        menu.ShowMenu();
        int answer = Convert.ToInt32(Console.ReadLine());
        int whileCount = 0;

        while (answer != 4)
        {
            switch (answer)
            {
                case 1:
                {
                    Task task = new Task();
                    task.AddTask();
                    break;
                }
                case 2:
                {
                    Task vtask = new Task();
                    vtask.ViewTasks();
                    break;
                }
                case 3:
                {
                    Task dtask = new Task();
                    dtask.DeleteTask();
                    break;
                }
                case 4:
                {
                    Console.WriteLine("Exiting...");
                    break;
                }    
            }

            whileCount++;

            if (answer != 4 && whileCount % 2 == 0)
            {
                Console.WriteLine("Do you want to return to the main menu? (y/n): ");
                string selection = Console.ReadLine().ToLower();

                if (selection == "y")
                {
                    menu.ShowMenu();
                    answer = Convert.ToInt32(Console.ReadLine());
                }
            }
        }
    }
}

public class Menu
{
    public void ShowMenu()
    {
        Console.WriteLine("================================");
        Console.WriteLine(" Welcome to the To-Do List");
        Console.WriteLine("================================");
        Console.WriteLine("1. Add Task");
        Console.WriteLine("2. View Tasks");
        Console.WriteLine("3. Delete Task");
        Console.WriteLine("4. Exit");
        Console.Write("Select an option: ");
    }
}

public class Task
{
    //* Finished
    public void AddTask()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Enter the task you want to add: ");
        Console.WriteLine("----------------------------------------");

        string path = "PATH";

        using (StreamWriter sw = File.AppendText(path))
        {
            string savedTask = Console.ReadLine();
            sw.WriteLine(savedTask);
            Console.WriteLine("The task " + savedTask + " has been added to the task list.");
        }
    }

    //* Finished
    public void ViewTasks()
    {
        Console.WriteLine("----------------");
        Console.WriteLine("Current tasks:");
        Console.WriteLine("----------------");
        Console.WriteLine(" ");

        string[] lines = File.ReadAllLines("PATH");

        for (int i = 0; i < lines.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(lines[i]))
            {
                Console.WriteLine($"{i + 1}. {lines[i]}");
            }
        }
    }

    //* Finished
    public void DeleteTask()
    {
        Console.WriteLine("Tasks available to delete:");
        string[] lines = File.ReadAllLines("PATH");

        for (int i = 0; i < lines.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(lines[i]))
            {
                Console.WriteLine($"{i + 1}. {lines[i]}");
            }
        }

        List<string> filteredTasks = new List<string>();
        foreach (string task in lines)
        {
            if (!string.IsNullOrWhiteSpace(task))
            {
                filteredTasks.Add(task);
            }
        }

        Console.WriteLine("Enter the number of the task you want to delete: ");
        int deleteTask = Convert.ToInt32(Console.ReadLine());

        int index = deleteTask - 1;
        filteredTasks.RemoveAt(index);

        string path = "PATH";
        File.WriteAllLines(path, filteredTasks);

        Console.WriteLine("Task deleted successfully.");
    }
}

// TODO: Use try & catch to handle errors