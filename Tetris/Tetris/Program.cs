using System;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            var form = new MenuForm();
            Application.Run(form);
            //TODO: Заняться рефакторингом.
            //TODO: Создать собственный класс для вектора.
            //TODO: Доработать дизайн, добавить меню.
        }
    }
}
