using System.Diagnostics;

//1. Using Aggregate function to find largest string, sum of numbers and concatenating strings
AggregateFunctionSamples();
void AggregateFunctionSamples()
{

    //Find lomngest fruit name and convert to uppercase
    string[] fruits = { "apple", "banana", "mango", "orange", "Jackfruit" };

    var maxLengthf = fruits.Aggregate("fruit", (current, next) => current.Length > next.Length ? current:next, u => u.ToUpper());
    Console.WriteLine(maxLengthf);

    //Sum of numbers using Aggregate function
    List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

    var sumOfNumbers = numbers.Aggregate((current, next) => current + next);
    Console.WriteLine(sumOfNumbers);

    //Cancatenate string using Aggregate function
    string[] names = { "Nithin", "Nithia", "Newman" };
    var concatenatedNames = names.Aggregate((current,next) =>  current.ToString()  + next.ToString() );
    Console.WriteLine(concatenatedNames);



}

//2. Find First and Second Largest number in an array
int[] numbers = { 23, 24, 45, 12, 67, 8, 90, 95, 95 };
var (l,s)=(FindFirst_Second_LargestNumber(numbers));

Console.WriteLine($"Largest Number: {l}, Second Largest Number: {s}");

(int,int) FindFirst_Second_LargestNumber(int[] numbers)
{
    int largestNumber = 0;
    int secondLargestNumber = 0;

    foreach (int num in numbers)
    {
        if (num > largestNumber)
        {
            secondLargestNumber = largestNumber;
            largestNumber = num;
        }
        else if (secondLargestNumber > num && num != largestNumber)
        {
            secondLargestNumber = num;
        }
    }
    return (largestNumber, secondLargestNumber);
}

