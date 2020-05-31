using System;

namespace PersonalIdentificationNumberValidator
{
    class Program
    {
        //I use the bulagarian wikipedia as a reference
        static void Main(string[] args)
        {
            while (true)
            {
                //Askling the user to type in the validation number
                Console.WriteLine("Type in the number you want to validate: ");

                //Save the number as a string without whitespaces on the sides
                string numberToValidate = Console.ReadLine().Trim();

                //See if users number is a 10-digit integer 
                if (numberToValidate.Length != 10)
                {
                    Console.WriteLine("The number is can't be an bulgarian personal identification number, as it has no 10 digits.");
                    continue;
                }

                //Make sure the users number is an integer
                if (!long.TryParse(numberToValidate, out long i))
                {
                    Console.WriteLine("The number you enter must be an integer!");
                    continue;
                }

                //Final Check
                CheckIfNumberCanBePersonalIdentificationNumber(numberToValidate);
            }
        }

        private static void CheckIfNumberCanBePersonalIdentificationNumber(string number)
        {
            //split the 10-digit number into a stringarray
            char[] numbers = number.ToCharArray();

            //declare and save the birthday-information
            int birthYear = int.Parse(numbers[0].ToString() + numbers[1].ToString()),
                birthMonth = int.Parse(numbers[2].ToString() + numbers[3].ToString()),
                birthDay = int.Parse(numbers[4].ToString() + numbers[5].ToString());

            //Check if birthday can exist
            bool dateDateExist = CheckIfBirthdayCanExist(birthYear, birthMonth, birthDay);

            if (dateDateExist)
            {
                //As the sixth, seventh and eighth numbers are basically randoms, we only need to check, if the tenth (controlnumber) is made correctly
                bool controlNumberIsRight = CheckTheControlNumber(numbers);
                if (controlNumberIsRight)
                {
                    Console.WriteLine("Your number can be a personal identification number!");
                }
                else
                {
                    Console.WriteLine("Your number can't be a personal identification number. The controlnumber (tenth digit) is wrong!");
                }
            }
            else
            {
                Console.WriteLine("Your number can't be a personal identification number. The birthdate (first six digits) cannot exist");
            }
        }

        private static bool CheckTheControlNumber(char[] numbers)
        {
            int[] number = new int[numbers.Length];

            //Turn the numbers into int-variables
            for (int i = 0; i < numbers.Length; i++)
            {
                number[i] = int.Parse(numbers[i].ToString());
            }

            //Create sum of the numbers multiplicated by their weight (depends on position, not on number!)
            double sum = 0;
            double position;
            double weight;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                position = i + 1;
                weight = Math.Pow(2, position) % 11;
                sum += number[i] * weight;
            }

            //Check, if tenth number is correct
            double tenthNumberChecker = sum % 11;
            if (tenthNumberChecker < 10)
            {
                if (number[9] == tenthNumberChecker)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                if (number[9] == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static bool CheckIfBirthdayCanExist(int birthYear, int birthMonth, int birthDay)
        {
            string dateOfBirth = $"{birthDay}.{birthMonth}.{birthYear}";
            if (DateTime.TryParse(dateOfBirth, out DateTime result) == false)
            {
                //Check if the person is born in the 19th century
                dateOfBirth = $"{birthDay}.{birthMonth - 20}.{birthYear}";
                if (DateTime.TryParse(dateOfBirth, out result) == false)
                {
                    //Check if the person is born in the 21th century
                    dateOfBirth = $"{birthDay}.{birthMonth - 40}.{birthYear}";
                    if (DateTime.TryParse(dateOfBirth, out result) == false)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

    }
}
