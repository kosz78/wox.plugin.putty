using System;
using System.Collections.Generic;
using System.Text;

namespace PowerToys.Plugin.Putty
{
    public static class Program
    {
      public static void Main() {
        new PuttySessionService().GetAll();
      }
    }
}
