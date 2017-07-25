//**********************************************************************************************
//*** Johan Rombouts - 25/7/2017 - Convert Finance File ADXALG.csv to AdvanTex-format op.txt ***
//**********************************************************************************************

using System;
using System.Text;
using System.IO;

namespace ParseTxtFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string cSourceFile;
            string cTargetFile;
            cSourceFile = "U:\\sarchiv1\\SDBNL1\\NL_TWS\\Transfer\\FiBu\\OPStamm\\ADXALG.csv";
            cTargetFile = "U:\\sarchiv1\\SDBNL1\\NL_TWS\\Transfer\\FiBu\\OPStamm\\op.txt";
  
            System.IO.StreamWriter file = new System.IO.StreamWriter(cTargetFile);
            using ( StreamReader sr = new StreamReader(cSourceFile) )
            {   
                string cRecordInformation;
                string cRecTyp;               //  8 characters
                string cDebitor;              // 12 characters
                string cBelegNr;              // 10 characters
                string cBelegDatum;           //  8 characters
                string cValutaDatum;          //  8 characters
                string cRechnungsbetrag;      // 13 characters
                string cRechnungsbetragOffen; // 13 characters
                string cMahnStufe;            //  3 characters
                string cMahnSperre;           //  1 characters
                string cConvertedRecord;
                string cSep;
                string cRecordSeparator;
                string cNewLines;
                string cBlanks;
                int nNumberOfRecords;
                
                cSep = ";";
                cRecordSeparator = "\r\n";
                cConvertedRecord = "";
                nNumberOfRecords = 0;
                cNewLines = "";
                cBlanks = "                    "; //20 spaces
                                
                while ((cRecordInformation = sr.ReadLine()) != null)
                {
                    if (nNumberOfRecords > 0)
                    {
                        cConvertedRecord = "";
                        cRecTyp = "OP"+cBlanks.Substring(1,6);
                        cDebitor = cRecordInformation.Substring(1, 9).Replace(" ", "") + cBlanks.Substring( 1, 12 - cRecordInformation.Substring(1, 9).Replace(" ", "").Length );
                        cBelegNr = cRecordInformation.Substring(12, 7).Replace(" ","") + cBlanks.Substring(1, 10 - cRecordInformation.Substring(12, 7).Replace(" ", "").Length );
                        cBelegDatum = "20" + cRecordInformation.Substring(27, 2) + cRecordInformation.Substring(24, 2) + cRecordInformation.Substring(21, 2);
                        cValutaDatum = cBelegDatum;

                        cRechnungsbetrag      = cBlanks.Substring(1, 13 - (((cRecordInformation.Substring(43, 17)).Replace(",", "")).Replace(" ", "")).Replace(".","").Length) + 
                                                (((cRecordInformation.Substring(43, 17)).Replace(",","")).Replace(" ","")).Replace(".","");
                        cRechnungsbetragOffen = cBlanks.Substring(1, 13 - (((cRecordInformation.Substring(64, 16)).Replace(",", "")).Replace(" ", "")).Replace(".","").Length) + 
                                                (((cRecordInformation.Substring(64, 16)).Replace(",","")).Replace(" ","")).Replace(".","");

                        cMahnStufe  = cBlanks.Substring(1,2)+"0";
                        cMahnSperre = "N";
                        cConvertedRecord = cRecTyp + cSep + cDebitor + cSep + cBelegNr + cSep + cBelegDatum + cSep + cValutaDatum + cSep + cRechnungsbetrag + cSep + cRechnungsbetragOffen + 
                                           cSep + cMahnStufe + cSep + cMahnSperre;

                        cNewLines = cNewLines + cConvertedRecord + cRecordSeparator;
                    }
                    nNumberOfRecords++;
                    Console.WriteLine(cConvertedRecord);
                }
                file.WriteLine(cNewLines);
                file.Close();

            }
        }
    }
}
