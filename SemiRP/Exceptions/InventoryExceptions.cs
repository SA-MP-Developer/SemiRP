using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Exceptions
{
    public class InventoryExceptions : Exception
    {
        public InventoryExceptions()
        {

        }
        public InventoryExceptions(string message) : base(message)
        {

        }
        public InventoryExceptions(string message, Exception inner) : base(message, inner)
        {

        }
    }

    public class InventoryAddingExceptions : Exception
    {
        public InventoryAddingExceptions()
        {

        }
        public InventoryAddingExceptions(string message) : base(message)
        {

        }
        public InventoryAddingExceptions(string message, Exception inner) : base(message, inner)
        {

        }
    }

    public class InventoryRemovingExceptions : Exception
    {
        public InventoryRemovingExceptions()
        {

        }
        public InventoryRemovingExceptions(string message) : base(message)
        {

        }
        public InventoryRemovingExceptions(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
