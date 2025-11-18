# .NETCore 10 and C# 14

**Find .Net SDK versions and .Net Runtime version**
- dotnet --list-sdks
- dotnet --list-runtimes
- dotnet --info

All future versions of .NET 10 runtime are compatible with its major version. example, if a project target net10.0, then we can upgrade the .NET runtime to future version like 10.0.1, 10.0.2 etc.

All future versions of the .NET SDK maintain the ability to build project that target previous versions of the runtime. For example, if a project targets net10.0 and you initially build it using .NET SDK 10.0.100 then you can upgrade the .NET SDK to future versions like 10.0.101 etc.

So we can safely remove all previous version of .NET SDK

**C# Compiler (Roslyn) ----> IL ------- CLR + JIT ---> CPU Instruction** 


Each operating system has its own CLR that understands the IL code and compile it to that operating system understandable code.


**.NET 10 File based app** :
We can create single file based c# code and execute in independantly as we do in Python

<p>dotnot run hello.cs</p>

For now we can have only one C# file in a file based app. Multiple file will be supported in .NET11.

We can add Nuget Package / Project referance to the single file as below:

<pre>
#:package Humanizer@2.14.1
#:project ../MyClassLib/MyClassLib.csproj
  
using Humanizer;
  Console.WriteLine(TimeSpan.FromDays(1).Humanize());
</pre>

Later we can convert file-based app to a project-based app
<pre>
  dotnet project convert app.cs
</pre>

Publish file-based app
<pre>
  dotnet publish app.cs
</pre>

By default it is published as a native-compiled AOT app, we can doisable that by setting a propery at the top of the file
<pre>
  #:property PublishAot false
</pre>


<p>
<b>Using digit separator: </b>
We can use underscore anywhere in the numbers to separate the numbers, Example:
1_000_0000 or Indian format 10_00_000.
Not only in whole number we can use it in decimal, hexadecimal or binary numbers.
</p>


## Pattern Matching

After C# 7, We can use pattern matching in IF statement or Switch statement .

Ex.
<pre>
	//If Sample
	object 0="3";
	int j = 4;
if (o is int i)
{
	Console.WriteLine($"{i} X {j} = {i * j}");
}
else
{
	Console.WriteLine("o is not an int so it cannot multiply");
}

//Case sample
class Animal;
class Cat : Animal
class Spider : Animal

var animals = new Animal[]
{
	new Cat {};
	new Cat {};
	new Spider{}
}

foreach (Animal animal in animals)
{
	string message;
	switch (animal)
	{
		case Cat fourlegs when fourleg.Legs ==4:
			message = $"The cat named {fourlegs.Name} has four legs";
			break;
		case Cat wildcat when wildcat.Isdomestic == false;
			message ="";
			break;
		case Spider spider when spider.IsVenomous==true
			message="";
			break;
		case default:
			break;
		case null:
			break
	}
}
</pre>

In C#8 and later, we can Switch expression that further simplify the case statement.
Ex.
<pre>
	message = animal switch
	{
		Cat fourlegs when fourlegs.Legs == 4 =>  $"The cat named {fourlegs.Name} has four legs";
		Cat wildCat when wildCat.IsDomestic == false => "";
	}
</pre>


## Try .. Catch With filter
After C#7 and later, we can use filters in catch blcok
ex.
<pre>
	string amount = ReanLine()!;    // ! is null forgiving operator
	if( string IsNullOrEmpty(amount)) return;
try
{
	decimal amountValue = decimal.Parse(amount);
	WriteLine($"Amount formatted as currency: {amountValue:C}");
}
catch (FormatException) when (amount.Contains('$'))
{
	WriteLine("Amount cannot use the dollar sign");
}
catch (FormatException)
{
	WriteLine("Amount must only contain digits");
}
</pre>


### field Keyword
field in side auto property has been introduced in C#14.
Ex.
Previous Syntax:
<pre>
	private string name;
	Public string Name
	{
		get {return name;}
		set {name = value}
	}
</pre> 
Now we can use field:
<pre>
	public string Name
	{
		get;
		set {field = value;}
	}
</pre>
If we have variable name field in our code to avoid keyword use error we can use this.field or @field
<pre>
	private string field;
	public string Name
	{
		get;
		set {@field = value;}  //This will use local variable
	}
</pre>

In C#14 'field' keyword to access the compiler generated property directly within its accessor.

### Lazy Initialization
If we have a property Relative in a class, If we call this property before assigning the value it will throw exception or default value. In C#14 we can use **null-coalescing** assignment.
<img width="426" height="101" alt="image" src="https://github.com/user-attachments/assets/17412772-2377-4696-a4c1-dd3dbdd10c13" />


Here, If the call to Relative before initiating the value, it will return empty array. In the second example, If we call Name before assign value it will call the **DefaultName** function.


## Extension Block

<img width="1269" height="433" alt="image" src="https://github.com/user-attachments/assets/adc42863-014e-4380-a46b-bd0821ec5d55" />
<img width="1249" height="828" alt="image" src="https://github.com/user-attachments/assets/bfa370e1-6dfb-44c6-8d22-b82caf3c9eaf" />
<img width="1263" height="395" alt="image" src="https://github.com/user-attachments/assets/5b0bd9fc-204d-41d9-a4ac-2f29ff53b8fb" />



Extension block has been introduced in C#14, here we can use **extension** as a wrapper, inside it we can have multiple extension methods, also we can create extension property.

