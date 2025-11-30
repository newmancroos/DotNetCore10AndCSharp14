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

### Object Oriented Concepts

* **Abstraction** - Hiding Complex implementation and showing the essential features
* **Encapsulation**   - Bundling data(attributes) and methods that operate on that data into a single unit and controlling access to that data (Private, Public, protected, internal
* **Inheritance**  - Using base class functionality inside child class
* **Polymorphism** - Allow derived class to changing/modifying the implantation of a method of a class
* **Composition** - What an object made of. ex. A Car is composed of different parts such as Wheel, Seats ...
* **Aggregation** - What can be combined with the object. ex. Person object not related to Car object but we can combine Person as a driver of a car object
  
## Access Modifiers

* **public:** Access is not restricted. Members declared as public can be accessed from any code, anywhere.
* **private:** Access is limited to the containing type. Members declared as private can only be accessed from within the class or struct in which they are defined.
* **protected:** Access is limited to the containing class or types derived from the containing class. This means protected members are accessible within the same class and in any class that inherits from it. 
* **internal:** Access is limited to the current assembly. Members declared as internal can be accessed by any code within the same assembly (e.g., a single project or DLL), but not from outside that assembly.
* **protected internal:** Access is limited to the current assembly OR types derived from the containing class (even if those derived types are in a different assembly). This combines the accessibility of protected and internal.
* **private protected:** Access is limited to the containing class OR types derived from the containing class within the current assembly. This is a more restrictive combination than protected internal. 
* **file:** This modifier is used for file-scoped types and is only available in C# 11 and later. It makes the declared type visible only within the current source file. File-scoped types are typically used for source generators.
  
### Passing  variable number of params to a function

<pre>
	public void ParamsParameters(string text, params int[] numbers)
	{
		foreach(int a in numbers)
		{
			Console.WriteLine(a);
		}
	}
	
	ParamsParameters("text", 1,2,3,4,5,6);
</pre>


### Immutable property

Immutable property just like read-only field that can me set during initialization. For immutable property we use **init**   keywork
ex:
<pre>
public class ImmutablePerson
{
	public string? FirstName { get; init; }
	public string? LastName { get; init; }
}

ImmutablePerson jeff = new()
{
	FirstName = "Jeff",
	LastName = "Winger"
};
jeff.FirstName = "Updated"
</pre>

here, everything fine but the last line, we can't assign value to Immutable property except during initialization.


We can use it in **Record** too,

ex:
<pre>
public record ImmutablePerson
{
	public string? FirstName { get; init; }
	public string? LastName { get; init; }
}

ImmutablePerson jeff = new()
{
	FirstName = "Jeff",
	LastName = "Winger"
};
</pre>
Record type can be declare in one line like below
<pre>
	public  record RecordTypePerson(string? FirstName, string? LastName);
	var person = new RecordTypePerson("Newman", "Croos");  
	Console.WriteLine(person.FirstName);    
</pre>


### Delegate and Delegate Handler

**Delegate :**   It is a function pointer, we can use it to point a method and use it as alias and call the function in the alias name

**Delegate Handler:**  IS use to raise event 

**Exmaple:**

<pre>
//Out side the class declaration
//----------------------------------------------------------------------------------------
//Delegate Declaration
delegate int DelegateWithMatchingSignature(string s);
//Declare EventHandler
public delegate void EventHandler(object? sender, EventArgs e);
//-----------------------------------------------------------------------------------------

var person = new Person();
//Delegate Mapping
DelegateWithMatchingSignature methodCall = new(person.MethodIWantToCall);
//Call method using Delegate
Console.WriteLine(methodCall("Newman Croos"));

person.Name = "Harry";  

//Map EventHandler in Person Class
person.Shout = Harry_Shout;
person.Poke();
person.Poke();
person.Poke();
static void Harry_Shout(object? sender, EventArgs e)
{
	    if (sender is null) return;
	    if (sender is not Person p) return;
	    Console.WriteLine($"{p.Name} is this angery : {p.AngerLevel}");
}

Console.ReadLine();

public class Person
{
    public string Name { get; set; }
    public EventHandler? Shout;

    public int AngerLevel;
    public int MethodIWantToCall(string input)
    {
        return input.Length;
    }

    public void Poke()
    {
        AngerLevel++;
        if (AngerLevel < 3) return;

        if (Shout is not null)
        {
            Shout(this, EventArgs.Empty);
        }
    }
}
</pre>



### Multicast Delegates

When we declare Delegate method inside a class we use
<pre>
		public EventHandler? Shout
</pre>

Here we can only point a single method, but when we use **event**, we can convert it to multicast delegate and then point multiple mthods as call back.
<pre>
		public event EventHandler? Shout
</pre>

**Complete Sample:**

<pre>
var person = new Person();

//Delegate Mapping
DelegateWithMatchingSignature methodCall = new(person.MethodIWantToCall);

//Call method using Delegate
Console.WriteLine(methodCall("Newman Croos"));

person.Name = "Harry";

//Map EventHandler in Person Class
//person.Shout = Harry_Shout;   // Regular Event Handler 

person.Shout += Harry_Shout;   // Calling multicast delegate Event
person.Shout += Harry_Shout1;

person.Poke();
person.Poke();
person.Poke();
person.Poke();
static void Harry_Shout(object? sender, EventArgs e)
{

    if (sender is null) return;

    if (sender is not Person p) return;

    Console.WriteLine($"{p.Name} is this angery : {p.AngerLevel}");
}

static void Harry_Shout1(object? sender, EventArgs e)
{

    if (sender is null) return;

    if (sender is not Person p) return;

    Console.WriteLine($"{p.Name} is this angery : {p.AngerLevel}");
}


Console.ReadLine();

//Delegate Declaration
delegate int DelegateWithMatchingSignature(string s);

//Declare EventHandler
public delegate void EventHandler(object? sender, EventArgs e);



public class Person
{
    public string Name { get; set; }
    //public EventHandler? Shout;

    //Changing it to Multicast delegate
    public event EventHandler Shout;

    public int AngerLevel;
    public int MethodIWantToCall(string input)
    {
        return input.Length;
    }

    public void Poke()
    {
        AngerLevel++;
        if (AngerLevel < 3) return;

        if (Shout is not null)
        {
            Shout(this, EventArgs.Empty);
        }
    }
}
</pre>


### IEnumerable Interface
IEnumberable interface enables Enumerable natuer for the class member
Example:
<img width="684" height="832" alt="image" src="https://github.com/user-attachments/assets/9c6df8b5-ca14-480d-8486-214b64a8ac0d" />

### IComparable Interface
Adds Sorting capability to a instances of the class

Example:
<img width="997" height="468" alt="image" src="https://github.com/user-attachments/assets/30f49eb1-e229-4e26-b2d3-956051a15d18" />
<img width="999" height="765" alt="image" src="https://github.com/user-attachments/assets/10ba0e33-e7f6-4e1b-8b6c-84a6efb3e312" />



### Hiding a method from Base class

If base class has a method but I want to hid it and force to use the same method name in the drived class, We should use **new** keyword.

<pre>
	public class Person
	{
		public string Name{get;set;}
		public void WriteToConsole()
		{
			Console.WriteLine($"Person Name is {Name}");
		}
	}

	public class Employee : Person
	{
			public string Department {get;set;}
			public new void WriteToConsole()
			{
				console.WriteLine($"Person Name is {Name} and department is {Department}")
			}
	}
</pre>

### Note:

**Now Interface has default implementation methods that can be override in the derived class, that means, We don't need abstract class in future?** No
* All the methods of a Interface are public (or at least match the interface access level, which could be internal, If it should only be used in the class library that it's defined in).  BUT abstract class has more flexible in member access modifiers.
* Serialization does not work on interfaces
So we still need abstract class.





## Casting Objects

**Implicit Casting (Upcasting)**

Implicit casting, also known as upcasting, occurs when you assign an instance of a derived class to a variable of its base class type. This conversion is automatic and safe because a derived class always contains all the members of its base class. No data loss is possible during upcasting.

<pre>
	class BaseClass { }
	class DerivedClass : BaseClass { }

	// Implicit casting: DerivedClass instance assigned to BaseClass variable
	DerivedClass derived = new DerivedClass();
	BaseClass baseObj = derived; 
</pre>

**Explicit Casting (Downcasting)**

**Explicit casting, or down casting**, occurs when you attempt to assign an instance of a base class to a variable of its derived class type. This conversion is not automatic and requires a cast operator  `()`  because it's not guaranteed that the base class instance is actually an instance of the derived class. Down casting can lead to a runtime  `InvalidCastException`  if the object being cast is not truly an instance of the target derived type.

<pre>
	class BaseClass { }
	class DerivedClass : BaseClass { }

	// Explicit casting: BaseClass instance cast to DerivedClass
	BaseClass baseObj = new DerivedClass(); // Or new BaseClass()
	DerivedClass derived = (DerivedClass)baseObj; // Requires explicit cast
</pre>


Better we can check the object type before we casting it.

ex:
<pre>
	if (aliceInPerson is BaseClass)
	{
	WriteLine($"{nameof(aliceInPerson)} is an Employee.");
	DervivedClass explicitAlice = (DervivedClass)aliceInPerson;
	// Safely do something with explicitAlice.
	}
</pre>


## Custom Exception

This could be the basic format for a custom exceptions
<pre>
	public class PersonException : Exception
	{
	public PersonException() : base() { }
	public PersonException(string message) : base(message) { }
	public PersonException(string message, Exception innerException)
	: base(message, innerException) { }
	}
</pre>
