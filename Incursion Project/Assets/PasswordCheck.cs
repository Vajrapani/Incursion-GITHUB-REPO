using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PasswordCheck
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

namespace IntegerTypeCheck
{
    class program
    {
        static void Main(string[] args)
        { 
            System.Console.WriteLine(checkPassword ("!aB08555555", 10));
            System.Console.ReadLine(); 
        }

        static bool checkPassword(string input, int minimum) 
        {
            bool hasNum = false;
            bool hasCap = false;
            bool hasLow = false;
            bool hasSpec = false;
            char currentCharacter;

            if (!(input.Length >= minimum))
            {
                return false;
            }

            for (int i = 0; i < input.Length; i++)
            {
                currentCharacter = input[i];
                if (char.IsDigit(currentCharacter))
                {
                    hasNum = true;
                }
                else if (char.IsUpper(currentCharacter))
                {
                    hasCap = true;
                }
                else if (char.IsLower(currentCharacter))
                {
                    hasLow = true;
                }
                else if (!char.IsLetterOrDigit(currentCharacter))
                {
                    hasSpec = true;
                }  
                if (hasNum && hasCap && hasLow && hasSpec)
                {
                    return true;
                }
            }
            return false; 


        } 
    }



}