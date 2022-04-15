using MileageManagerFoems;
using System;

namespace MileageManagerForms
{
    public static class CodeFile
    {
        public static void CodeFileCall()
        {
            try
            {
                ManagerClass st = new ManagerClass();
                st.ProcessData();
            }
            catch (Exception ex)
            {
                string crap = ex.ToString();
            }
        }
    }
}