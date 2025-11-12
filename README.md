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
