using DapperWith4DatabaseCommunication.Dtos;
using System.Text;

namespace DapperWith4DatabaseCommunication.Utils
{
    public static class ValidationMessages
    {

        public static StringBuilder AddEmployee(EmployeeDto empdto)
        {
            //don't use multiple string  concadination purpose below process.
            //if you use a normal string for concadination,each and every string adding/manuplulation,it will create new object.
            //So realtime ,if any String Adding Requirments are there use(stringBuilder concept).
            //Dont use like below process
            string a = "hello";//it will create object
            string b = "hai";//it will create object
            string c = "hyderabad";//it will create object
            string result = a + b + c;//it will create object (Here total 4 objects created)

            //======================Use this string builder process for string concadination============
            //use stringbuilder concept beacuse it reduces the no.of object creation.
            //if you use a stringbuilder,each and every string adding/manuplulation,it will  not create a new object.
            //StringBuilder Having Append() method ,it will add the new string to existing string.
            // Initialize a StringBuilder to store validation messages
            StringBuilder validationMessages = new StringBuilder();//here it will create only one object

            // Validate input fields
            //if the empname is empty or whitespace we are showing validation messages
            if (string.IsNullOrWhiteSpace(empdto.empname) || string.IsNullOrEmpty(empdto.empname))
            {
                validationMessages.AppendLine("EmployeeName is required,please enter correct EmployeeName.");//in that object we are appending the data
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(empdto.empsalary)) || string.IsNullOrEmpty(Convert.ToString(empdto.empsalary)))
            {
                validationMessages.AppendLine("EmpSalary is required,please enter correct EmpSalary.");//in that object we are appending the data
            }
            return validationMessages;
        }
        public static StringBuilder UserSignIn(LoginDTO loginDTOObj)
        {
            StringBuilder validationMessages = new StringBuilder();//here it will create only one object

            // Validate input fields
            //if the empname is empty or whitespace we are showing validation messages
            if (string.IsNullOrWhiteSpace(loginDTOObj.UserName) || string.IsNullOrEmpty(loginDTOObj.UserName))
            {
                validationMessages.AppendLine("UserName is required,please enter correct username.");//in that object we are appending the data
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(loginDTOObj.Password)) || string.IsNullOrEmpty(Convert.ToString(loginDTOObj.Password)))
            {
                validationMessages.AppendLine("Password is required,please enter correct password.");//in that object we are appending the data
            }
            return validationMessages;
        }
    }
}
/*
 * 1.what is validation,how do you implemnet?
 * A)Validation means verifying the input fileds,mandatory or not.
 * =>if the field is mandatory we need check the value  of the filed.
 * if the filed does not contain any value then we need to raise a validation messages for that one.
 * =>here some fileds are optional,those fileds you can ignore to apply the validations.
 * =>whatever the fileds are mandatory you can write validation for those fileds,if not mandatory,you can ignore those fileds for validation.
 */



