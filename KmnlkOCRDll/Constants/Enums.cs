using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkOCRDll.Constants
{
    public static class Enums
    {

        public enum Enum_Convert_Result
        {
            SUCCESS,
            NOT_SUCCESS,
            ERROR_PARAMETERS
        }

        public enum Enum_Barcode_Type
        {
            CODE_QR,
            CODE_128
        }
    }
}
